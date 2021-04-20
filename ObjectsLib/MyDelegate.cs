using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace ObjectsLib
{
    public class MyDelegate
    {
        private List<MethodInfo> _listOfMethods;

        private ParametersAndType _signatureOfFunction;
        public List<Exception> Exceptions { get; }

        public MyDelegate(MethodInfo method)
        {
            if (method == null) throw new ArgumentNullException(nameof(method));
            _listOfMethods = new List<MethodInfo> { method };
            _signatureOfFunction = new ParametersAndType(method.GetParameters(), method.ReturnType);

            Exceptions = new List<Exception>();
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
            if (_listOfMethods.Count == 0)
            {
                _listOfMethods = new List<MethodInfo>();
                _signatureOfFunction = new ParametersAndType(method.GetParameters(), method.ReturnType);
            }
            if (method.ReturnType != _signatureOfFunction.ReturnType || 
                !ParametersInfoEquality(method.GetParameters(), _signatureOfFunction.Parameters))
            {
                throw new InvalidOperationException("Сигнатуры функций не совпадают");
            }

            _listOfMethods.Add(method);
        }

        public static MyDelegate operator +(MyDelegate first, MyDelegate second)
        {
            if (first == null || second == null) throw new ArgumentNullException();

            if (first == second)
            {
                var newList = new List<MethodInfo>();
                newList = newList.Concat(first._listOfMethods).ToList();
                first._listOfMethods = newList.Concat(first._listOfMethods).ToList();
                return first;
            }

            foreach (var methodInfo in second._listOfMethods)
            {
                first.AddMethod(methodInfo);
            }

            return first;
        }

        public static MyDelegate operator -(MyDelegate first, MyDelegate second)
        {

            if (first == second)
            {
                first._listOfMethods.Clear();
                first._signatureOfFunction.Clear();
            }

            if (first._listOfMethods.Count == 0) return first;

            foreach (var methodInfoInSecond in second._listOfMethods)
            {
                first._listOfMethods.Remove(methodInfoInSecond);
            }
            return first;
        }

        public object Invoke(object classInstance, object[] parameters)
        {
            if (_listOfMethods.Count == 0) return null;

            Exceptions.Clear();
            object result = null;

            foreach (var methodInfo in _listOfMethods)
            {
                try
                {
                   result = methodInfo.Invoke(classInstance, parameters);
                }
                catch (Exception exception)
                {
                    if (exception.InnerException != null) 
                        Exceptions.Add(exception.InnerException);
                }
            }
            Console.WriteLine("Все методы делегата выполнены. Вызвано " + Exceptions.Count + " исключений.");
            return result;
        }
    }
}
