using System;

namespace ObjectsLib
{
    [Serializable]
    public class PersonAlreadyExistsException:Exception
    {
        public Person Person { get;}

        public PersonAlreadyExistsException()
        { }

        public PersonAlreadyExistsException(string message):base (message)
        { }

        public PersonAlreadyExistsException(string message, Exception inner) : base(message, inner)
        { }

        public PersonAlreadyExistsException(string message, Person person) : base(message) => Person = person;


    }
}
