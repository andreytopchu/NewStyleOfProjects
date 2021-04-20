using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;
using ObjectsLib;

namespace Tests
{
    [TestClass]
    public class MyDelegateTest
    {
        public double Divide(double a, double b)
        {
            return a / b;
        }

        public double DivideByZero(double a, double b)
        {
            throw new DivideByZeroException("Делить на ноль нельзя.");
        }

        [TestMethod]
        public void WithoutExceptionTest()
        {
            var type = Type.GetType(this.ToString());
            Debug.Assert(type != null, nameof(type) + " != null");

            var devideMethodInfo = type.GetMethod(nameof(Divide));

            var myDelegate = new MyDelegate(devideMethodInfo);

            double expected = 32;
            Assert.AreEqual(expected, myDelegate.Invoke(this, new object[] { 64, 2 }));
        }

        [TestMethod]
        public void WithIgnoreExceptionTest()
        {
            var type = Type.GetType(this.ToString());
            Debug.Assert(type != null, nameof(type) + " != null");

            var devideByZeroMethodInfo = type.GetMethod(nameof(this.DivideByZero));
            var devideMethodInfo = type.GetMethod(nameof(Divide));

            var myDelegate = new MyDelegate(devideMethodInfo);
            myDelegate += new MyDelegate(devideByZeroMethodInfo);
            myDelegate += new MyDelegate(devideByZeroMethodInfo);
            myDelegate += myDelegate;
            myDelegate += new MyDelegate(devideMethodInfo);

            var result = myDelegate.Invoke(this, new object[] { 3200, 10 });
            Assert.AreEqual(320, (double)result);
            Console.WriteLine("Результат после исключений: " + result);
        }
    }
}
