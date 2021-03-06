using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;


namespace ObjectsLib
{
    [Serializable]
    public class Person: INotifyPropertyChanged
    {
        public string _firstName;
        public string _secondName;
        public string _patronymic;
        public DateTime _dateOfBirth;
        public string _placeOfBirth;
        public string _numberOfPassport;

        public string FirstName
        {
            get { return _firstName; }
            set
            {
                if (value != _firstName)
                {
                    if (string.IsNullOrEmpty(value))
                        throw new ArgumentException("Имя не может быть пустой строкой или null");
                    _firstName = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public string SecondName
        {
            get { return _secondName; }
            set
            {
                if (value != _secondName)
                {
                    if (string.IsNullOrEmpty(value))
                        throw new ArgumentException("Фамилия не может быть пустой строкой или null");
                    _secondName = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public string Patronymic
        {
            get { return _patronymic; }
            set
            {
                if (value != _patronymic)
                {
                    if (string.IsNullOrEmpty(value))
                        throw new ArgumentException("Отчество не может быть пустой строкой или null");
                    _patronymic = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public DateTime DateOfBirth
        {
            get { return _dateOfBirth; }
            set
            {
                if (value != _dateOfBirth)
                {
                    _dateOfBirth = value;
                    NotifyPropertyChanged();
                }
            }
        }


        public string PlaceOfBirth
        {
            get { return _placeOfBirth; }
            set
            {
                if (value != _placeOfBirth)
                {
                    if (string.IsNullOrEmpty(value))
                        throw new ArgumentException("Место рождения не может быть пустой строкой или null");
                    _placeOfBirth = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public string NumberOfPassport
        {
            get { return _numberOfPassport; }
            set
            {
                if (value != _numberOfPassport)
                {
                    if (string.IsNullOrEmpty(value))
                        throw new ArgumentException("Серия паспорта не может быть пустой строкой или null");
                    _numberOfPassport = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public Person(string secondName, string firstName, string patronymic, DateTime dateOfBirth, string placeOfBirth, string numberOfPassport) 
        {
            FirstName = firstName;
            SecondName = secondName;
            Patronymic = patronymic;
            DateOfBirth = dateOfBirth;
            PlaceOfBirth = placeOfBirth;
            NumberOfPassport = numberOfPassport;
        }


        public Person(string secondName, string firstName, string patronymic, DateTime dateOfBirth, string placeOfBirth)
        {

            this.FirstName = firstName;
            this.SecondName = secondName;
            this.Patronymic = patronymic;
            this.DateOfBirth = dateOfBirth;
            this.PlaceOfBirth = placeOfBirth;
        }

        public Person()
        {
            _firstName = GeneratorOfPersons.GenerateFirstName();
            _secondName = GeneratorOfPersons.GenerateSecondName();
            _patronymic = GeneratorOfPersons.GeneratePatronymic();
            _dateOfBirth = GeneratorOfPersons.GenerateDate();
            _placeOfBirth = GeneratorOfPersons.GeneratePlaceOfBirth();
            _numberOfPassport = GeneratorOfPersons.GenerateNumberOfPassport();
        }


        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void PrintInfo()
        {
            Console.WriteLine("\nДанные о человеке:");

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

        public override string ToString()
        {
            return " Фамилия: " + SecondName + " Имя: " + FirstName + " Отчество: " 
                + Patronymic + " Дата рождения: " + DateOfBirth.ToShortDateString() + 
                " Место рождения: " + PlaceOfBirth + " Номер паспорта: " + NumberOfPassport + " ";
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

        public static bool operator == (Person p1, Person p2) => !(p1 is null) && p1.Equals(p2);

        public static bool operator !=(Person p1, Person p2) => !(p1 is null) && !p1.Equals(p2);
    }
}
