using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ObjectsLib;

namespace Tests
{
    [TestClass]
    public class DictionaryTest
    {
        private static Random _rand = new Random();

        private readonly string[] _placeOfWork = new string[]
        {
            " Зеленый рынок ",
            " Супермаркет Шериф ",
            " ККК Тирасполь ",
            " Завод Молдавизолит ",
            " Дом культуры ",
            " Школа ",
            " Индивидуальный предприниматель ",
            " Завод Квинт ",
            " Продуктовый магазин ",
            " Строительная компания ",
        };

        private readonly string[] _names = new string[]
        {
            " Андрей ",
            " Владимир ",
            " Анатолий ",
            " Георгий ",
            " Валентин ",
            " Валерий ",
            " Александр ",
            " Аркадий ",
            " Дмитрий ",
            " Кирилл "
        };

        private readonly string[] _secondNames = new string[]
        {
            " Иванов ",
            " Петров ",
            " Давыдов ",
            " Сидоров ",
            " Беляков ",
            " Шумило ",
            " Симоненко ",
            " Шубин ",
            " Кузнецов ",
            " Игнатов  ",
        };

        private readonly string[] _patronymics = new string[]
        {
            " Владимирович ",
            " Петрович ",
            " Алексеевич ",
            " Леонидович ",
            " Эдуардович ",
            " Васильевич ",
            " Сергеевич ",
            " Андреевич ",
            " Владимирович ",
            " Викторович  ",
        };

        private readonly string[] _placesOfBirth = new string[]
        {
            " Тирасполь ",
            " Бендеры ",
            " Слободзея ",
            " Суклея ",
            " Григориополь ",
            " Дубоссары ",
            " Каменка ",
            " Рыбница ",
            " Красное ",
            " Копанка  ",
        };

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
                    Person person = GeneratePerson();
                    string plaseOfWork = GeneratePlaceOfWork();

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
                    PersonWithHashCodeConst person = GeneratePersonWithHashCodeConst();
                    string plaseOfWork = GeneratePlaceOfWork();

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

        private string GeneratePlaceOfWork()
        {
            return _placeOfWork[_rand.Next(0, _placeOfWork.Length)];
        }

        private Person GeneratePerson()
        {
            return new Person(GenerateSecondName(), GenerateFirstName(), GeneratePatronymic(), GenerateDate(), GeneratePlaceOfBirth(), GenerateNumberOfPassport());
        }

        private PersonWithHashCodeConst GeneratePersonWithHashCodeConst()
        {
            return new PersonWithHashCodeConst(GenerateSecondName(), GenerateFirstName(), GeneratePatronymic(), GenerateDate(), GeneratePlaceOfBirth(), GenerateNumberOfPassport());
        }

        private string GenerateFirstName()
        {
            return _names[_rand.Next(0, _names.Length)];
        }

        private string GenerateSecondName()
        {
            return _secondNames[_rand.Next(0, _secondNames.Length)];
        }

        private string GeneratePatronymic()
        {
            return _patronymics[_rand.Next(0, _patronymics.Length)];
        }

        private DateTime GenerateDate()
        {
            DateTime start = new DateTime(1950, 1, 1);
            int range = ((TimeSpan)(new DateTime(2005, 1, 1) - start)).Days;
            return start.AddDays(_rand.Next(range));
        }

        private string GeneratePlaceOfBirth()
        {
            return _placesOfBirth[_rand.Next(0, _placesOfBirth.Length)];
        }

        HashSet<int> hs = new HashSet<int>();

        private string GenerateNumberOfPassport()
        {
            bool flag = false;
            int serialNumber = -1;

            while (!flag)
            {
                serialNumber = _rand.Next(100000, 1000000);
                if (hs.Add(serialNumber)) flag = true;
            }
           
            return "I-ПР №" + serialNumber;
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

        private int _countOfElements = 100000;
        [TestMethod]
        public void FindPersonNotInDictionaryTest()
        {
            Dictionary<Person, string> referenceBook = GeneratePersonsDictionary(_countOfElements);

            Person person = GeneratePerson();

            bool actual = IsContainsInDictionary(referenceBook, person);
            bool expected = false;

            Assert.AreEqual(expected, actual, "Искомого человека нет в справочнике.");
        }

        [TestMethod]
        public void FindPersonIsInDictionaryTest()
        {
            Dictionary<Person, string> referenceBook = GeneratePersonsDictionary(_countOfElements);

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
            _countOfElements = 100;
            Dictionary<Person, string> referenceBookOfPersons = GeneratePersonsDictionary(_countOfElements);
            Console.WriteLine("Время, затраченное на добавление "+_countOfElements+" элементов c генерированным хешем: "+stopwatch.ElapsedTicks+" тактов");
            Console.WriteLine();

            Dictionary<PersonWithHashCodeConst, string> referenceBookOfPersonsWithHashCodeConst = GeneratePersonsWithHashCodeConstDictionary(_countOfElements);
            Console.WriteLine("Время, затраченное на добавление " + _countOfElements + " элементов с хешем-const: " + stopwatch.ElapsedTicks + " тактов");

            Dictionary<Person, string>.KeyCollection keys = referenceBookOfPersons.Keys;
            Person[] keysMass = new Person[keys.Count];
            keys.CopyTo(keysMass, 0);
            int randomIndex1 = _rand.Next(0, keys.Count);

            Person person1 = keysMass[randomIndex1];
            IsContainsInDictionary(referenceBookOfPersons, person1);
            Console.WriteLine("Время, затраченное на поиск элемента в словаре с " + _countOfElements + " элементов с генерированным хешем: " + stopwatch.ElapsedTicks + " тактов");

            Dictionary<PersonWithHashCodeConst, string>.KeyCollection keysOfBadCollection = referenceBookOfPersonsWithHashCodeConst.Keys;
            PersonWithHashCodeConst[] keysOfBadCollectionMass = new PersonWithHashCodeConst[keysOfBadCollection.Count];
            keysOfBadCollection.CopyTo(keysOfBadCollectionMass, 0);
            int randomIndex2 = _rand.Next(0, keysOfBadCollection.Count);

            PersonWithHashCodeConst person2 = keysOfBadCollectionMass[randomIndex2];
            IsContainsInBadDictionary(referenceBookOfPersonsWithHashCodeConst, person2);
            Console.WriteLine("Время, затраченное на поиск элемента в словаре с " + _countOfElements + " элементов с хешем-const: " + stopwatch.ElapsedTicks + " тактов");
        }

        [TestMethod]
        public void FixedTimeOn1000ElementsOfAddAndContainsToDictionaryTest()
        {
            _countOfElements = 1000;
            Dictionary<Person, string> referenceBookOfPersons = GeneratePersonsDictionary(_countOfElements);
            Console.WriteLine("Время, затраченное на добавление " + _countOfElements + " элементов c генерированным хешем: " + stopwatch.ElapsedTicks + " тактов");
            Console.WriteLine();

            Dictionary<PersonWithHashCodeConst, string> referenceBookOfPersonsWithHashCodeConst = GeneratePersonsWithHashCodeConstDictionary(_countOfElements);
            Console.WriteLine("Время, затраченное на добавление " + _countOfElements + " элементов с хешем-const: " + stopwatch.ElapsedTicks + " тактов");

            Dictionary<Person, string>.KeyCollection keys = referenceBookOfPersons.Keys;
            Person[] keysMass = new Person[keys.Count];
            keys.CopyTo(keysMass, 0);
            int randomIndex1 = _rand.Next(0, keys.Count);

            Person person1 = keysMass[randomIndex1];
            IsContainsInDictionary(referenceBookOfPersons, person1);
            Console.WriteLine("Время, затраченное на поиск элемента в словаре с " + _countOfElements + " элементов с генерированным хешем: " + stopwatch.ElapsedTicks + " тактов");

            Dictionary<PersonWithHashCodeConst, string>.KeyCollection keysOfBadCollection = referenceBookOfPersonsWithHashCodeConst.Keys;
            PersonWithHashCodeConst[] keysOfBadCollectionMass = new PersonWithHashCodeConst[keysOfBadCollection.Count];
            keysOfBadCollection.CopyTo(keysOfBadCollectionMass, 0);
            int randomIndex2 = _rand.Next(0, keysOfBadCollection.Count);

            PersonWithHashCodeConst person2 = keysOfBadCollectionMass[randomIndex2];
            IsContainsInBadDictionary(referenceBookOfPersonsWithHashCodeConst, person2);
            Console.WriteLine("Время, затраченное на поиск элемента в словаре с " + _countOfElements + " элементов с хешем-const: " + stopwatch.ElapsedTicks + " тактов");
        }

        [TestMethod]
        public void FixedTimeOn10000ElementsOfAddAndContainsToDictionaryTest()
        {
            _countOfElements = 10000;
            Dictionary<Person, string> referenceBookOfPersons = GeneratePersonsDictionary(_countOfElements);
            Console.WriteLine("Время, затраченное на добавление " + _countOfElements + " элементов c генерированным хешем: " + stopwatch.ElapsedTicks + " тактов");
            Console.WriteLine();

            Dictionary<PersonWithHashCodeConst, string> referenceBookOfPersonsWithHashCodeConst = GeneratePersonsWithHashCodeConstDictionary(_countOfElements);
            Console.WriteLine("Время, затраченное на добавление " + _countOfElements + " элементов с хешем-const: " + stopwatch.ElapsedTicks + " тактов");

            Dictionary<Person, string>.KeyCollection keys = referenceBookOfPersons.Keys;
            Person[] keysMass = new Person[keys.Count];
            keys.CopyTo(keysMass, 0);
            int randomIndex1 = _rand.Next(0, keys.Count);

            Person person1 = keysMass[randomIndex1];
            IsContainsInDictionary(referenceBookOfPersons, person1);
            Console.WriteLine("Время, затраченное на поиск элемента в словаре с " + _countOfElements + " элементов с генерированным хешем: " + stopwatch.ElapsedTicks + " тактов");

            Dictionary<PersonWithHashCodeConst, string>.KeyCollection keysOfBadCollection = referenceBookOfPersonsWithHashCodeConst.Keys;
            PersonWithHashCodeConst[] keysOfBadCollectionMass = new PersonWithHashCodeConst[keysOfBadCollection.Count];
            keysOfBadCollection.CopyTo(keysOfBadCollectionMass, 0);
            int randomIndex2 = _rand.Next(0, keysOfBadCollection.Count);

            PersonWithHashCodeConst person2 = keysOfBadCollectionMass[randomIndex2];
            IsContainsInBadDictionary(referenceBookOfPersonsWithHashCodeConst, person2);
            Console.WriteLine("Время, затраченное на поиск элемента в словаре с " + _countOfElements + " элементов с хешем-const: " + stopwatch.ElapsedTicks + " тактов");
        }
    }
}
