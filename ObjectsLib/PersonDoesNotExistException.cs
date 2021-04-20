using System;

namespace ObjectsLib
{
    [Serializable]
    class PersonDoesNotExistException:Exception
    {
        public Person Person { get; }

        public PersonDoesNotExistException()
        { }

        public PersonDoesNotExistException(string message):base(message)
        { }

        public PersonDoesNotExistException(string message, Exception inner):base (message, inner)
        { }

        public PersonDoesNotExistException(string message, Person person):base(message)
        {
            Person = person;
        }
    }
}
