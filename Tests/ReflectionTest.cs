using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ObjectsLib;
using System.Reflection;

namespace Tests
{
    [TestClass]
    public class ReflectionTest
    {
        public void DisplayPropertiInfo(PropertyInfo[] propertyInfos, object obj)
        {
            foreach(var propInfo in propertyInfos)
            {
                Console.WriteLine($"Имя свойства: {propInfo.Name}");
                Console.WriteLine($"Тип свойства: {propInfo.PropertyType}");
                if (propInfo.CanRead)
                {
                    MethodInfo getAccessor = propInfo.GetMethod;
                    Console.WriteLine($"Тип доступа:    {GetVisibility(getAccessor)}");
                    Console.WriteLine($"Значение свойства: {propInfo.GetValue(obj)}");
                }
                Console.WriteLine();
            }
        }
        public static String GetVisibility(MethodInfo accessor)
        {
            if (accessor.IsPublic)
                return "Public";
            else if (accessor.IsPrivate)
                return "Private";
            else if (accessor.IsFamily)
                return "Protected";
            else if (accessor.IsAssembly)
                return "Internal/Friend";
            else
                return "Protected Internal/Friend";
        }


        [TestMethod]
        public void ToPrintPrivatePropertiesTest()
        {
            Book book = new Book("Антошка", "Про рыжего мальчика", 120);
            Type type = typeof(Book);
            PropertyInfo[] privatePropertyInfos = type.GetProperties(BindingFlags.NonPublic | BindingFlags.Instance);
            DisplayPropertiInfo(privatePropertyInfos, book);
        }

        [TestMethod]
        public void ToCallMetodWithParametersTest()
        {
            string className = "TwoLayersSolution.ObjectsLib.Book";
            Type type = Type.GetType(className);
            Type[] types = { typeof(string), typeof(string), typeof(int) };
            if (type != null)
            {
                ConstructorInfo bookConstructor = type.GetConstructor(types);
                if (!(bookConstructor is null))
                {
                    object book = bookConstructor.Invoke(new object[]
                        {"Колобок", "Рассказ о приключениях комочка теста по лесу.", 120});

                    if (book != null)
                    {
                        MethodInfo getReadingProgressMethod = type.GetMethod("GetReadingProgress");
                        if (!(getReadingProgressMethod is null))
                        {
                            object result = getReadingProgressMethod.Invoke(book, new object[] { 60 });
                    
                            if(result != null)
                            {
                                Assert.AreEqual(50.0, (double)result);
                            }
                            else throw new NullReferenceException("result == null");
                        }
                    }
                    else throw new NullReferenceException("book == null");
                }
            }
            else
                throw new NullReferenceException("type == null");

        }
    }
}
