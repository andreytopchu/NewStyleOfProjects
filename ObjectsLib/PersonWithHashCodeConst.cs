using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ObjectsLib
{
    public class PersonWithHashCodeConst:Person
    {
       public PersonWithHashCodeConst(string secondName, string firstName, string patronymic, 
           DateTime dateOfBirth, string placeOfBirth, string numberOfPassport)
            :base(secondName, firstName, patronymic, dateOfBirth, placeOfBirth, numberOfPassport)
        {

        }

        public override int GetHashCode()
        {
            return 245;
        }
    }
}
