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
            try
            {
                for (int i = 0; i < 25; i++)
                {
                    personsCatalog.AddPerson(new Person());
                }

            }
            catch (PersonAlreadyExistsException ex)
            {
                Console.WriteLine(ex.Message + "\nОшибка при попытке повторного добавления человека: " + ex.Person);
                throw;
            }

            personsCatalog.Save();
            personsCatalog.Load(Directory.GetCurrentDirectory() + "/PersonsCatalog.txt");
            Assert.AreEqual(25, personsCatalog.Count());
        }

        [ExpectedException(typeof(PersonAlreadyExistsException))]
        [TestMethod]
        public void TryAddingNotUniquePersonTest()
        {
            PersonsCatalog personsCatalog = new PersonsCatalog();

            Person person = new Person();
            try
            {
                personsCatalog.AddPerson(person);
                personsCatalog.AddPerson(person);
            }
            catch (PersonAlreadyExistsException ex)
            {
                Console.WriteLine(ex.Message+"\nОшибка при попытке повторного добавления человека: "+ex.Person);
                throw;
            }
        }

        [TestMethod]
        public void SavePersonToHtmlFilesTest()
        {
            Person person = new Person();
            person.PrintPersonToHtml();
        }
    }
}
