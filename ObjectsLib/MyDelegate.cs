using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace ObjectsLib
{
    public class MyDelegate
    {
        public List<MethodInfo> ListOfMethods { get; private set; }

        public ParametersAndType SignatureOfFunction;

        public MyDelegate(MethodInfo method)
        {
            if (method == null) throw new ArgumentNullException(nameof(method));

            ListOfMethods = new List<MethodInfo> { method };
            SignatureOfFunction = new ParametersAndType(method.GetParameters(), method.ReturnType);
        }

        private bool ParametersInfoEquality(ParameterInfo[] first, ParameterInfo[] second)
        {
            if (first.Length != second.Length) return false;
            for (int i = 0; i < first.Length; i++)
            {
                if (first[i].ParameterType != second[i].ParameterType) return false;
            }
            return true;
        }

        public void AddMethod(MethodInfo method)
        {
            if (method == null) throw new ArgumentNullException(nameof(method));
            if (ListOfMethods.Count == 0)
            {
                ListOfMethods = new List<MethodInfo>();
                SignatureOfFunction = new ParametersAndType(method.GetParameters(), method.ReturnType);
            }
            if (method.ReturnType != SignatureOfFunction.ReturnType || 
                !ParametersInfoEquality(method.GetParameters(), SignatureOfFunction.Parameters))
            {
                throw new InvalidOperationException("Сигнатуры функций не совпадают");
            }

            ListOfMethods.Add(method);
        }

        public static MyDelegate operator +(MyDelegate first, MyDelegate second)
        {
            if (first == null || second == null) throw new ArgumentNullException();

            if (first == second)
            {
                var newList = new List<MethodInfo>();
                newList = newList.Concat(first.ListOfMethods).ToList();
                first.ListOfMethods = newList.Concat(first.ListOfMethods).ToList();
                return first;
            }

            foreach (var methodInfo in second.ListOfMethods)
            {
                first.AddMethod(methodInfo);
            }

            return first;
        }

        public static MyDelegate operator -(MyDelegate first, MyDelegate second)
        {

            if (first == second)
            {
                first.ListOfMethods.Clear();
                first.SignatureOfFunction.Clear();
            }

            if (first.ListOfMethods.Count == 0) return first;

            foreach (var methodInfoInSecond in second.ListOfMethods)
            {
                first.ListOfMethods.Remove(methodInfoInSecond);
            }
            return first;
        }

        public object Invoke(object classInstance, object[] parameters)
        {
            if (ListOfMethods.Count == 0) return null;
            else
            {
                foreach (var methodInfo in ListOfMethods)
                {
                    try
                    {
                        return methodInfo.Invoke(classInstance, parameters);
                    }
                    catch (Exception exception)
                    {
                        if (exception.InnerException != null) Console.WriteLine(exception.InnerException.Message);
                    }
                }
            }
            return null;
        }
    }
}
