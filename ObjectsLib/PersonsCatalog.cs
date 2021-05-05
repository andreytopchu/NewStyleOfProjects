using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace ObjectsLib
{
    public class PersonsCatalog
    {

        public Dictionary<Person, Person> PersonsDictionary;

        public int Count()
        {
            return PersonsDictionary.Count;
        }

        public PersonsCatalog()
        {
            PersonsDictionary = new Dictionary<Person, Person>();
        }

        private bool IsContainsInDictionary(Person person)
        {
            if (PersonsDictionary.ContainsKey(person))
                return true;
            else return false;
        }

        public void AddPerson(Person person)
        {
            if (!IsContainsInDictionary(person))
            {
                PersonsDictionary.Add(person, person);
            }
            else 
                throw new PersonAlreadyExistsException("Данный человек уже содержится в каталоге. Невозможно выполнить добавление.",person);
        }

        public void DeletePerson(Person person)
        {
            if (person == null)
                throw new ArgumentNullException(nameof(person));
            if (IsContainsInDictionary(person))
            {
                PersonsDictionary.Remove(person);
            }
            else
                throw new PersonDoesNotExistException("Данного человека нет в каталоге. Невозможно выполнить удаление.", person);
        }


    }
}
