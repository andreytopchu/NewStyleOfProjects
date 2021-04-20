using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ObjectsLib;
using System.Collections;
using System.Collections.Generic;

namespace Tests
{
    [TestClass]
    public class UniqueCollectionTest
    {
        public bool CheckForNotRepetInTheCollection<T>(UniqueCollection<T> uniqueCollection)
        {
            HashSet<T> hs = new HashSet<T>();
            foreach (var item in uniqueCollection)
            {
                try
                {
                    hs.Add(item);
                }
                 catch(ArgumentException)
                {
                    Console.WriteLine("Коллекция не уникальна. В ней найдены два одинаковых элемента.");
                    return false;
                }
            }
            return true;
        }

        [TestMethod]
        public void GenerationUniqueCollectionWith10000UniquePersonsTest()
        {
            int countOfElements = 10000;
            UniqueCollection<Person> uniqueCollection = new UniqueCollection<Person>();

            for (int i = 0; i < countOfElements; i++)
            {
                uniqueCollection.Add(GeneratorOfPersons.GeneratePerson());
            }

            int actual = uniqueCollection.Count;
            int expected = countOfElements;
            Assert.AreEqual(expected, actual);

            Assert.IsTrue(CheckForNotRepetInTheCollection(uniqueCollection));
        }

        [ExpectedException(typeof(ArgumentException))]
        [TestMethod]
        public void GenerationUniqueCollectionWith100NotUniquePersonsTest()
        {
            int countOfElements = 100;
            UniqueCollection<Person> uniqueCollection = new UniqueCollection<Person>();

            for (int i = 0; i < countOfElements; i++)
            {
                uniqueCollection.Add(GeneratorOfPersons.GeneratePerson());
            }

            Person[] uniqueCollectionItems = new Person[uniqueCollection.Count];
            uniqueCollection.CopyTo(uniqueCollectionItems, 0);
            uniqueCollection.Add(uniqueCollectionItems[countOfElements / 2]);

            int actual = uniqueCollection.Count;
            int expected = countOfElements;
            Assert.AreEqual(expected, actual);

            CollectionAssert.AllItemsAreUnique((ICollection) uniqueCollection);
        }

        [ExpectedException(typeof(ArgumentException))]
        [TestMethod]
        public void GenerateUniqueCollectionWithNotUniqueDoubleItemsTest()
        {
            UniqueCollection<double> uniqueCollection = new UniqueCollection<double>
            {
                12.12,
                5.5,
                6.3,
                2.2,
                5.5
            };

            List<double> list = new List<double>() { 12.12, 5.5, 6.3, 2.2 };

            int actual = uniqueCollection.Count;
            int expected = list.Count;
            Assert.AreEqual(expected, actual);

            CollectionAssert.AreEqual((ICollection)uniqueCollection, list);
            CollectionAssert.AllItemsAreUnique((ICollection)uniqueCollection);
        }


    }
}
