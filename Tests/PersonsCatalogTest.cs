using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ObjectsLib;

namespace Tests
{
    [TestClass]
    public class PersonsCatalogTest
    {
        [TestMethod]
        public void SaveAndLoadTest()
        {
            PersonsCatalog personsCatalog = new PersonsCatalog();

            for (int i = 0; i < 25; i++)
            {
                personsCatalog.AddPerson(new Person());
            }

            personsCatalog.Save();
            personsCatalog.Load(Directory.GetCurrentDirectory() + "/PersonsCatalog.txt");
            Assert.AreEqual(25, personsCatalog.Count());
        }

        [ExpectedException(typeof(InvalidOperationException))]
        [TestMethod]
        public void TryAddingNotUniquePersonTest()
        {
            PersonsCatalog personsCatalog = new PersonsCatalog();

            Person person = new Person();

            personsCatalog.AddPerson(person);
            personsCatalog.AddPerson(person);
        }
    }
}
