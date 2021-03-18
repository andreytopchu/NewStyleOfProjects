using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ObjectsLib
{
    public class Person
    {
        public string FirstName { get; private set; }
        public string SecondName { get; private set; }
        public string Patronymic { get; private set; }
        public DateTime DateOfBirth { get; private set; }
        public string PlaceOfBirth { get; private set; }
        public string NumberOfPassport { get; private set; }

        public Person(string secondName, string firstName, string patronymic, DateTime dateOfBirth, string placeOfBirth, string numberOfPassport) 
        {
            if (firstName == "" || firstName == null)
                throw new ArgumentException("Имя не может быть пустой строкой или null");

            if (secondName == "" || secondName == null)
                throw new ArgumentException("Фамилия не может быть пустой строкой или null");

            if (patronymic == "" || patronymic == null)
                throw new ArgumentException("Отчество не может быть пустой строкой или null");

            if (placeOfBirth == "" || placeOfBirth == null)
                throw new ArgumentException("Место рождения не может быть пустой строкой или null");

            if (numberOfPassport == "" || numberOfPassport == null)
                throw new ArgumentException("Серия паспорта не может быть пустой строкой или null");

            this.FirstName = firstName;
            this.SecondName = secondName;
            this.Patronymic = patronymic;
            this.DateOfBirth = dateOfBirth;
            this.PlaceOfBirth = placeOfBirth;
            this.NumberOfPassport = numberOfPassport;
        }

        public Person(string secondName, string firstName, string patronymic, DateTime dateOfBirth, string placeOfBirth)
        {
            if (firstName == "" || firstName == null)
                throw new ArgumentException("Имя не может быть пустой строкой или null");

            if (secondName == "" || secondName == null)
                throw new ArgumentException("Фамилия не может быть пустой строкой или null");

            if (patronymic == "" || patronymic == null)
                throw new ArgumentException("Отчество не может быть пустой строкой или null");

            if (placeOfBirth == "" || placeOfBirth == null)
                throw new ArgumentException("Место рождения не может быть пустой строкой или null");

            this.FirstName = firstName;
            this.SecondName = secondName;
            this.Patronymic = patronymic;
            this.DateOfBirth = dateOfBirth;
            this.PlaceOfBirth = placeOfBirth;
        }

        public void PrintInfo()
        {
            Console.WriteLine("Данные о человеке:");

            Console.Write("Имя: " + FirstName);
            Console.Write("\nФамилия: " + SecondName);
            Console.Write("\nОтчество: " + Patronymic);
            Console.Write("\nДата рождения: " + DateOfBirth.ToShortDateString());
            Console.Write("\nМесто рождения: " + PlaceOfBirth);

            if (this.NumberOfPassport!=null)
            {
                Console.Write("\nНомер паспорта: " + NumberOfPassport);
            }
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            if (!(obj is Person))
                return false;

            var person = (Person)obj;
            return person.FirstName == FirstName && person.SecondName == SecondName && person.Patronymic == Patronymic && person.DateOfBirth == DateOfBirth &&
                person.PlaceOfBirth == PlaceOfBirth && person.NumberOfPassport == NumberOfPassport;
        }

        public override int GetHashCode()
        {
            return (SecondName + FirstName + Patronymic + DateOfBirth.ToShortDateString() + PlaceOfBirth + NumberOfPassport).GetHashCode();
        }

        public static bool operator == (Person p1, Person p2) => p1.Equals(p2);

        public static bool operator !=(Person p1, Person p2) => !p1.Equals(p2);
    }
}
