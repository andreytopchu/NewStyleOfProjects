using System;
using System.Collections.Generic;

namespace ObjectsLib
{
    public class PersonsCatalog
    {

        public HashSet<Person> PersonsHashSet;

        public int Count()
        {
            return PersonsHashSet.Count;
        }

        public PersonsCatalog()
        {
            PersonsHashSet = new HashSet<Person>();
        }

        private bool IsContainsInCatalog(Person person)
        {
            if (PersonsHashSet.Contains(person))
                return true;
            else return false;
        }

        public void AddPerson(Person person)
        {
            if (!IsContainsInCatalog(person))
            {
                PersonsHashSet.Add(person);
            }
            else 
                throw new PersonAlreadyExistsException("Данный человек уже содержится в каталоге. Невозможно выполнить добавление.",person);
        }

        public void DeletePerson(Person person)
        {
            if (person == null)
                throw new ArgumentNullException(nameof(person));
            if (IsContainsInCatalog(person))
            {
                PersonsHashSet.Remove(person);
            }
            else
                throw new PersonDoesNotExistException("Данного человека нет в каталоге. Невозможно выполнить удаление.", person);
        }


    }
}
