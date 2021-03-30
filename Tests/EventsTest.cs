using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ObjectsLib;
using System.ComponentModel;
using System.Collections.ObjectModel;

namespace Tests
{

    [TestClass]
    public class EventsTest
    {

        [TestMethod]
        public void ChangePropertyTest()
        {
            Person testingPerson = new Person();
            string propertyName = null;
            testingPerson.PropertyChanged += delegate (object sender, PropertyChangedEventArgs e)
            {
                propertyName = e.PropertyName;
            };
            testingPerson.FirstName = "Алексей";
            Assert.IsNotNull(propertyName);
            Assert.AreEqual("FirstName", propertyName);
        }

        [TestMethod]
        public void MyQuequeUnderflowTest()
        {
            MyQueue<Person> myQueue = new MyQueue<Person>(5);
            
            string underflowMessage = null;

            myQueue.QueueUnderflow += delegate (string notify)
            {
                underflowMessage = notify;
            };

            string overflowMessage = null;

            myQueue.QueueOverflow += delegate (string notify)
            {
                overflowMessage = notify;
            };

            for (int i = 0; i < 5; i++)
            {
                try
                {
                    myQueue.Enqueue(new Person());
                }
                catch (InvalidOperationException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                catch (Exception ex)
                {

                    Console.WriteLine(ex.Message);
                }
            }
            for (int i = 0; i < 6; i++)
            {
                try
                {
                    myQueue.Dequeue();
                }
                catch (InvalidOperationException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            Assert.IsNotNull(underflowMessage);
            Assert.IsNull(overflowMessage);
            Assert.AreEqual("Очередь пуста", underflowMessage);
        }

        [TestMethod]
        public void MyQuequeOverflowTest()
        {
            MyQueue<Person> myQueue = new MyQueue<Person>(5);

            string overflowMessage = null;

            myQueue.QueueOverflow += delegate (string notify)
            {
                overflowMessage = notify;
            };

            for (int i = 0; i < 6; i++)
            {
                try
                {
                    myQueue.Enqueue(new Person());
                }
                catch (InvalidOperationException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                catch (Exception ex)
                {

                    Console.WriteLine(ex.Message);
                }
            }
            Assert.IsNotNull(overflowMessage);
            Assert.AreEqual("Очередь переполнена", overflowMessage);
        }

        [TestMethod]
        public void AnaliseStreamOfNumbersWithBigDifferenceInPercentageTest()
        {
            Collection<double> streamOfNumbers = new Collection<double>() { 1.34, 5.876, 2.7, 3, 12.12736 };
            NumberAnalyzer numberAnalyzer = new NumberAnalyzer(streamOfNumbers);

            string analizeNotify = null;

            numberAnalyzer.BigDifferenceEvent += delegate (string notify)
            {
                analizeNotify = notify;
            };

            numberAnalyzer.AnalyzeStreamOfNumbersWithPercentage(50);

            Assert.IsNotNull(analizeNotify);
            Console.WriteLine(analizeNotify);
        }
    }
}
