using System;
using System.Collections.Generic;
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

        private Dictionary<Person, string> GenerateDictionary(int count)
        {
            if (count <= 0) throw new ArgumentException("Недопустимое значение для количества элементов в справочнике");

            Dictionary<Person, string> dictionary = new Dictionary<Person, string>();

            while(count>0)
            {
                try
                {
                    dictionary.Add(GeneratePerson(), GeneratePlaceOfWork());
                }
                catch(ArgumentException)
                {
                    Console.WriteLine("Ошибка про добавлении элемента в справочник!");
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
            return new Person(GenerateSecondName(), GenerateFirstName(), GeneratePatronymic(), GenerateDate(), GeneratePlaceOfBirth());
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

        private string SearchPersonInDictionaryByKey(Dictionary<Person,string> dictionary, Person person)
        {
            try
            {
                return dictionary[person];
            }
            catch (KeyNotFoundException)
            {
                return "Данного человека нет в справочнике.";
            }
        }

        [ExpectedException (typeof(KeyNotFoundException),"Исключение не сработало!")]
        [TestMethod]
        public void FindPersonNotInDictionaryTest()
        {
            Dictionary<Person, string> referenceBook = new Dictionary<Person, string>();

            referenceBook = GenerateDictionary(100);

            Person person = GeneratePerson();

            SearchPersonInDictionaryByKey(referenceBook, person);
        }

        public void FindPersonIsInDictionaryTest()
        {
            Dictionary<Person, string> referenceBook = new Dictionary<Person, string>();

            referenceBook = GenerateDictionary(100);
        }
    }
}
