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
			test.AddVariable("num1",1);
			test.AddVariable("num2",2);
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
			test.AddVariable("num3", 3);
			test.AddFunction
			(
				"multiply",
				(Dictionary<string, object> p) =>
				{
					return (int)test["add"] * (int)test["num3"];
				}
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

		public void AddVariable(string name, object data)
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

			foreach (var function in functions)
			{
				tempResult = new KeyValuePair<string, object>(function.Key, function.Value.Invoke(tempVariables));

				if (tempResult.Value != null)
				{
					tempVariables.Add(tempResult.Key, tempResult.Value);
				}
			}
			return tempResult.Value;
		}

		public T GetTempVariable<T>(string name) => (T)tempVariables[name];
		public object this[string name] => tempVariables[name];

		public IEnumerable<string> ShowMethodsList()
		{
			foreach (KeyValuePair<string, Func<Dictionary<string, object>, object>> function in functions)
			{
				yield return function.Key;
			}
		}
	}
}