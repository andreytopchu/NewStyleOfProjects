using System;
using System.Collections.Generic;

namespace ObjectsLib
{
    public static class GeneratorOfPersons
    {
        private static Random _rand = new Random();

        private static readonly string[] PlaceOfWork = new string[]
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

        private static readonly string[] Names = new string[]
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

        private static readonly string[] SecondNames = new string[]
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

        private static readonly string[] Patronymics = new string[]
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

        private static readonly string[] PlacesOfBirth = new string[]
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

        public static string GeneratePlaceOfWork()
        {
            return PlaceOfWork[_rand.Next(0, PlaceOfWork.Length)];
        }

        public static string GenerateFirstName()
        {
            return Names[_rand.Next(0, Names.Length)];
        }

        public static string GenerateSecondName()
        {
            return SecondNames[_rand.Next(0, SecondNames.Length)];
        }

        public static string GeneratePatronymic()
        {
            return Patronymics[_rand.Next(0, Patronymics.Length)];
        }

        public static DateTime GenerateDate()
        {
            DateTime start = new DateTime(1950, 1, 1);
            int range = (new DateTime(2005, 1, 1) - start).Days;
            return start.AddDays(_rand.Next(range));
        }

        public static string GeneratePlaceOfBirth()
        {
            return PlacesOfBirth[_rand.Next(0, PlacesOfBirth.Length)];
        }

        static HashSet<int> hs = new HashSet<int>();

        public static string GenerateNumberOfPassport()
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

        public static Person GeneratePerson()
        {
            return new Person(GenerateSecondName(), GenerateFirstName(), GeneratePatronymic(), GenerateDate(), GeneratePlaceOfBirth(), GenerateNumberOfPassport());
        }

        public static PersonWithHashCodeConst GeneratePersonWithHashCodeConst()
        {
            return new PersonWithHashCodeConst(GenerateSecondName(), GenerateFirstName(), GeneratePatronymic(), GenerateDate(), GeneratePlaceOfBirth(), GenerateNumberOfPassport());
        }
    }
}
