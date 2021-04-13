using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ObjectsLib;

namespace Tests
{
    [TestClass]
    public class IntExtentionTest
    {
        [TestMethod]
        public void IntToTimeSpanMillisecondsTest()
        {
            int number = 2500000;
            Console.WriteLine(number.Milliseconds().ToString());
            Assert.AreEqual(new TimeSpan(0, 0, 0, 0, number), number.Milliseconds());
        }
        [TestMethod]
        public void IntToTimeSpanSecondsTest()
        {
            int number = 3700;
            Console.WriteLine(number.Seconds().ToString());
            Assert.AreEqual(new TimeSpan(0, 0, 0, number), number.Seconds());
        }
        [TestMethod]
        public void IntToTimeSpanMinutesTest()
        {
            int number = 3725;
            Console.WriteLine(number.Minutes().ToString());
            Assert.AreEqual(new TimeSpan(0, 0, number, 0), number.Minutes());
        }
        [TestMethod]
        public void IntToTimeSpanHoursTest()
        {
            int number = 50;
            Console.WriteLine(number.Hours().ToString());
            Assert.AreEqual(new TimeSpan(0, number, 0 ,0), number.Hours());
        }
        [TestMethod]
        public void IntToTimeSpanDaysTest()
        {
            int number = 50;
            Console.WriteLine(number.Days().ToString());
            Assert.AreEqual(new TimeSpan(number, 0, 0, 0), number.Days());
        }
    }
}
