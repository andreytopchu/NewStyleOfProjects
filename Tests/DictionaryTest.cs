using Microsoft.VisualStudio.TestTools.UnitTesting;
using ObjectsLib;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Tests
{
    [TestClass]
    public class DictionaryTest
    {
        private static Random _rand = new Random();
        
        private static Stopwatch stopwatch = new Stopwatch();

        private Dictionary<Person, string> GeneratePersonsDictionary(int count)
        {
            if (count <= 0) throw new ArgumentException("Недопустимое значение для количества элементов в справочнике");

            stopwatch.Reset();

            Dictionary<Person, string> dictionary = new Dictionary<Person, string>();

            while(count>0)
            {
                try
                {
                    Person person = GeneratorOfPersons.GeneratePerson();
                    string plaseOfWork = GeneratorOfPersons.GeneratePlaceOfWork();

                    stopwatch.Start();
                    dictionary.Add(person, plaseOfWork);
                    stopwatch.Stop();

                    count--;
                }
                catch(ArgumentException)
                {
                    Console.WriteLine("Ошибка про добавлении элемента в справочник.");
                }
            }

            return dictionary;
        }

        private Dictionary<PersonWithHashCodeConst, string> GeneratePersonsWithHashCodeConstDictionary(int count)
        {
            if (count <= 0) throw new ArgumentException("Недопустимое значение для количества элементов в справочнике");

            stopwatch.Reset();

            Dictionary<PersonWithHashCodeConst, string> dictionary = new Dictionary<PersonWithHashCodeConst, string>();

            while (count > 0)
            {
                try
                {
                    PersonWithHashCodeConst person = GeneratorOfPersons.GeneratePersonWithHashCodeConst();
                    string plaseOfWork = GeneratorOfPersons.GeneratePlaceOfWork();

                    stopwatch.Start();
                    dictionary.Add(person, plaseOfWork);
                    stopwatch.Stop();

                    count--;
                }
                catch (ArgumentException)
                {
                    Console.WriteLine("Ошибка про добавлении элемента в справочник.");
                }
            }

            return dictionary;
        }

        private bool IsContainsInDictionary(Dictionary<Person,string> dictionary, Person person)
        {
            stopwatch.Restart();
            bool result = dictionary.ContainsKey(person);
            stopwatch.Stop();
            return result;
        }

        private bool IsContainsInBadDictionary(Dictionary<PersonWithHashCodeConst, string> dictionary, PersonWithHashCodeConst person)
        {
            stopwatch.Restart();
            bool result = dictionary.ContainsKey(person);
            stopwatch.Stop();
            return result;
        }

        private void FixedTimeOfAddAndContainsToDictionary(int countOfElements)
        {
            Dictionary<Person, string> referenceBookOfPersons = GeneratePersonsDictionary(countOfElements);
            Console.WriteLine("Время, затраченное на добавление " + countOfElements + " элементов c генерированным хешем: " + stopwatch.ElapsedMilliseconds + " миллисекунд");
            Console.WriteLine();

            Dictionary<PersonWithHashCodeConst, string> referenceBookOfPersonsWithHashCodeConst = GeneratePersonsWithHashCodeConstDictionary(countOfElements);
            Console.WriteLine("Время, затраченное на добавление " + countOfElements + " элементов с хешем-const: " + stopwatch.ElapsedMilliseconds + " миллисекунд");

            Dictionary<Person, string>.KeyCollection keys = referenceBookOfPersons.Keys;
            Person[] keysMass = new Person[keys.Count];
            keys.CopyTo(keysMass, 0);
            int randomIndex1 = _rand.Next(0, keys.Count);

            Person person1 = keysMass[randomIndex1];
            IsContainsInDictionary(referenceBookOfPersons, person1);
            Console.WriteLine("Время, затраченное на поиск элемента в словаре с " + countOfElements + " элементов с генерированным хешем: " + stopwatch.ElapsedMilliseconds + " миллисекунд");

            Dictionary<PersonWithHashCodeConst, string>.KeyCollection keysOfBadCollection = referenceBookOfPersonsWithHashCodeConst.Keys;
            PersonWithHashCodeConst[] keysOfBadCollectionMass = new PersonWithHashCodeConst[keysOfBadCollection.Count];
            keysOfBadCollection.CopyTo(keysOfBadCollectionMass, 0);
            int randomIndex2 = _rand.Next(0, keysOfBadCollection.Count);

            PersonWithHashCodeConst person2 = keysOfBadCollectionMass[randomIndex2];
            IsContainsInBadDictionary(referenceBookOfPersonsWithHashCodeConst, person2);
            Console.WriteLine("Время, затраченное на поиск элемента в словаре с " + countOfElements + " элементов с хешем-const: " + stopwatch.ElapsedMilliseconds + " миллисекунд");
        }

        [TestMethod]
        public void FindPersonNotInDictionaryTest()
        {
            int countOfElements = 10000;

            Dictionary<Person, string> referenceBook = GeneratePersonsDictionary(countOfElements);

            Person person = GeneratorOfPersons.GeneratePerson();

            bool actual = IsContainsInDictionary(referenceBook, person);
            bool expected = false;

            Assert.AreEqual(expected, actual, "Искомого человека нет в справочнике.");
        }

        [TestMethod]
        public void FindPersonIsInDictionaryTest()
        {
            int countOfElements = 10000;

            Dictionary<Person, string> referenceBook = GeneratePersonsDictionary(countOfElements);

            Dictionary<Person, string>.KeyCollection keys = referenceBook.Keys;
            Person[] keysMass = new Person[keys.Count];
            keys.CopyTo(keysMass, 0);
            int randomIndex = _rand.Next(0, keys.Count);

            Person person = keysMass[randomIndex];
            bool actual = IsContainsInDictionary(referenceBook, person);

            bool expected = true;

            Assert.AreEqual(expected, actual, "Искомый человек есть в справочнике.");
        }

        [TestMethod]
        public void FixedTimeOn100ElementsOfAddAndContainsToDictionaryTest()
        {
            int countOfElements = 100;

            FixedTimeOfAddAndContainsToDictionary(countOfElements); 
        }

        [TestMethod]
        public void FixedTimeOn1000ElementsOfAddAndContainsToDictionaryTest()
        {
            int countOfElements = 1000;

            FixedTimeOfAddAndContainsToDictionary(countOfElements);
        }

        [TestMethod]
        public void FixedTimeOn10000ElementsOfAddAndContainsToDictionaryTest()
        {
            int countOfElements = 10000;

            FixedTimeOfAddAndContainsToDictionary(countOfElements);
        }
    }
}
