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
            foreach (var propInfo in propertyInfos)
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
            const string className = "ObjectsLib.Book, ObjectsLib";
            Type type = Type.GetType(className, true, true);
            Type[] types = {typeof(string), typeof(string), typeof(int)};
            ConstructorInfo bookConstructor = type.GetConstructor(types);
            if ((bookConstructor is null)) throw new ArgumentNullException(nameof(bookConstructor));

            object book = bookConstructor.Invoke(new object[]
                {"Колобок", "Рассказ о приключениях комочка теста по лесу.", 120});

            if (book == null) throw new NullReferenceException("book == null");

            MethodInfo getReadingProgressMethod = type.GetMethod("GetReadingProgress");
            if (!(getReadingProgressMethod is null))
            {
                object result = getReadingProgressMethod.Invoke(book, new object[] {60});

                if (result != null)
                {
                    Assert.AreEqual(50.0, (double) result);
                }
                else throw new NullReferenceException("result == null");
            }
        }

        [TestMethod]
        public void CreateClassTriangleTest()
        {
            const string className = "ObjectsLib.Triangle, ObjectsLib";
            Type type = Type.GetType(className, true, true);
            Type[] types = { typeof(double), typeof(double), typeof(double) };

            ConstructorInfo triangleConstructorInfo = type.GetConstructor(types);
            if ((triangleConstructorInfo is null)) throw new ArgumentNullException(nameof(triangleConstructorInfo));

            object triangleClass = triangleConstructorInfo.Invoke(new object[]
                {8.0, 10.0, 12.0});

            Triangle triangle = new Triangle(10.0, 8.0, 12.0);

            Assert.AreEqual(triangle, triangleClass);
        }
    }
}
