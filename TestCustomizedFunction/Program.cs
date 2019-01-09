using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace TestCustomizedFunction
{
	class Program
	{
		static void Main(string[] args)
		{
			var test = new CustomizedMethodWrapper2();
			test.AddParameter("num1",1);
			test.AddParameter("num2",2);
			test.AddFunction
			(
				"add", 
				new Func<Dictionary<string, object>, object>
				(
					(Dictionary<string, object> p) =>
					{
						return (int)test["num1"] + (int)test["num2"];
					}
				)
			);
			Console.WriteLine((int)test.Invoke());

			Console.ReadKey();
		}
	}

	class CustomizedMethodWrapper2
	{
		private Dictionary<string, object> tempVariables = new Dictionary<string, object>();
		private Dictionary<string, Func<Dictionary<string, object>, object>> functions =
			new Dictionary<string, Func<Dictionary<string, object>, object>>();

		public void AddParameter(string name, object data)
		{
			tempVariables.Add(name, data);
		}

		public void AddFunction(string name, Func<Dictionary<string, object>, object> method)
		{
			functions.Add(name, method);
		}

		public object Invoke()
		{
			KeyValuePair<string, object> tempResult = new KeyValuePair<string, object>();

			foreach (var item in functions)
			{
				tempResult = new KeyValuePair<string, object>(item.Key, item.Value.Invoke(tempVariables));

				if (tempResult.Value != null)
				{
					tempVariables.Add(tempResult.Key, tempResult.Value);
				}
			}
			return tempResult.Value;
		}

		public T GetTempVariable<T>(string name) => (T)tempVariables[name];
		public object GetTempVariable(string name) => tempVariables[name];
		public object this[string name] => GetTempVariable(name);

		public IEnumerable<string> ShowMethodsList()
		{
			foreach (KeyValuePair<string, Func<Dictionary<string, object>, object>> function in functions)
			{
				yield return function.Key;
			}
		}
	}
}