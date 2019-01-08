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
			test.AddParameter(1);
			test.AddParameter(2);
			test.AddMethod
			(
				new Func<List<object>, object>
				(
					(List<object> p) => 
					{
						return (int)p[0] + (int)p[1];
					}
				)
			);
			Console.WriteLine((int)test.Invoke());

			Console.ReadKey();
		}
	}

	class CustomizedMethodWrapper2
	{
		private List<object> paramsList = new List<object>();
		private List<Func<List<object>, object>> methods = new List<Func<List<object>, object>>();

		public void AddParameter(object parameter)
		{
			paramsList.Add(parameter);
		}

		public void AddMethod(Func<List<object>, object> method)
		{
			methods.Add(method);
		}

		public object Invoke()
		{
			object tempResult;

			tempResult = methods[0].Invoke(paramsList);

			for (int i = 1; i < methods.Count; i++)
			{
				tempResult = methods[i].Invoke(new List<object>() {tempResult});
			}

			return tempResult;
		}
	}

	class CustomizedMethodWrapper
	{
		List<TypedObject> ParamsList = new List<TypedObject>();
		TypedObject returnValue;
		List<Func<List<TypedObject>>> functionList = new List<Func<List<TypedObject>>>();
		List<Action<List<TypedObject>>> subProcedureList = new List<Action<List<TypedObject>>>();

		public CustomizedMethodWrapper(Type returnType)
		{
			if (returnType.GetType() == typeof(void))
			{
				object a = new object();
				Convert.ChangeType(a, returnType);
			}

			TypedObject returnValue = new TypedObject(returnType, null);
		}

		public void Invoke()
		{

		}

		public void AddParameter(Type objectType, object objectData)
		{
			ParamsList.Add(new TypedObject(objectType, objectData));
		}

		public void AddFunction()
		{

		}

		public void AddSubProcedure()
		{

		}

		private class TypedObject
		{
			public Type ObjectType { get; private set; }
			public object ObjectData { get; private set; }

			public TypedObject(Type objectType, object objectData)
			{
				ObjectType = objectType;
				ObjectData = objectData;
			}
		}
	}
}
