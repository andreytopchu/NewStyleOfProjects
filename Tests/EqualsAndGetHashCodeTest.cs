using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ObjectsLib;

namespace Tests
{
    [TestClass]
    public class EqualsAndGetHashCodeTest
    {
        private readonly Person[] _persons = new Person[]
            {
                new Person("Васильев", "Александр", "Владимирович", new DateTime(1986,2,12), "Фрунзовка", "I-ПР №213468"),
                new Person("Петров", "Георгий", "Олегович", new DateTime(1996, 04,01 ), "Григориополь", "I-ПР №215668"),
                new Person("Иванов", "Андриан", "Дмитриевич", new DateTime(1989, 02, 01), "Днестровск", "I-ПР №215768"),
                new Person("Воснецов", "Валентин", "Валерьевич", new DateTime(1982, 05, 21), "Луганск", "I-ПР №213968"),
                new Person("Поплавская", "Николетта", "Юрьевна", new DateTime(1976, 03, 13), "Калуга", "I-ПР №213938"),
            };

        [TestMethod]
        public void FindsTwoIdenticalElementsWithOperatorEquallyTest()
        {
            Person person = new Person("Воснецов", "Валентин", "Валерьевич", new DateTime(1982, 05, 21), "Луганск", "I-ПР №213968");

            bool actualEqually = false;

            int i;
            for (i = 0; i < _persons.Length; i++)
            {
                if ( person == _persons[i] )
                {
                    actualEqually = true;
                    break;
                }
            }

            bool expectated = false;

            if (person.FirstName == _persons[i].FirstName && person.SecondName == _persons[i].SecondName && person.Patronymic == _persons[i].Patronymic
                && person.DateOfBirth == _persons[i].DateOfBirth && person.PlaceOfBirth == _persons[i].PlaceOfBirth && person.NumberOfPassport == _persons[i].NumberOfPassport)
            {
                expectated = true;
            }

            Assert.AreEqual(expectated, actualEqually, "Совпадение с помощью оператора == найденно успешно! Данный нам человек идентичен с человеком из базы.");
            Console.WriteLine("Информация о данном нам человеке:");
            person.PrintInfo();
            Console.WriteLine("\nИнформация из базы:");
            _persons[i].PrintInfo();
        }

        [TestMethod]
        public void TwoElementsIsEqualTest()
        {
            Person person = new Person("Воснецов", "Валентин", "Валерьевич", new DateTime(1982, 05, 21), "Луганск", "I-ПР №213968");

            bool actualEquals = false;
            int i = 3;

            if (person.Equals(_persons[i]))
            {
                actualEquals = true;
            }

            bool expectated = false;

            if (person.FirstName == _persons[i].FirstName && person.SecondName == _persons[i].SecondName && person.Patronymic == _persons[i].Patronymic
                && person.DateOfBirth == _persons[i].DateOfBirth && person.PlaceOfBirth == _persons[i].PlaceOfBirth && person.NumberOfPassport == _persons[i].NumberOfPassport)
            {
                expectated = true;
            }

            Assert.AreEqual(expectated, actualEquals, "Совпадение проверено успешно! Данный нам человек идентичен с человеком из базы.");
            Console.WriteLine("Информация о данном нам человеке:");
            person.PrintInfo();
            Console.WriteLine("\nИнформация из базы:");
            _persons[i].PrintInfo();
        }

        [TestMethod]
        public void FindsTwoElementsWithIdenticalHashCodeTest()
        {
            Person person = new Person("Петров", "Георгий", "Олегович", new DateTime(1996, 04, 01), "Григориополь", "I-ПР №215668");

            int actual = int.MaxValue;

            for (int i = 0; i < _persons.Length; i++)
            {
                if (person.GetHashCode() == _persons[i].GetHashCode())
                {
                    actual = _persons[i].GetHashCode();
                    break;
                }
            }

            int expected = person.GetHashCode();

            Assert.AreEqual(expected, actual,"Искомый человек найден в массиве по Hash коду. Hash искомого = {0}, hash человека из базы: {1}",expected, actual);
        }
    }
}
