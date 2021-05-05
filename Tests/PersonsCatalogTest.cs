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
            BaseDumper baseOfPersons = new BaseDumper();
            try
            {
                for (int i = 0; i < 25; i++)
                {
                    baseOfPersons.PersonsCatalog.AddPerson(new Person());
                }

            }
            catch (PersonAlreadyExistsException ex)
            {
                Console.WriteLine(ex.Message + "\nОшибка при попытке повторного добавления человека: " + ex.Person);
                throw;
            }

            baseOfPersons.Save();
            baseOfPersons.Load(Directory.GetCurrentDirectory() + "/PersonsCatalog.txt");
            Assert.AreEqual(25, baseOfPersons.PersonsCatalog.Count());
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

        [TestMethod]
        public void SerializedAndDeserializedPersonCatalogToBinaryTest()
        {
            BaseDumper baseOfPersons = new BaseDumper();
            try
            {
                for (int i = 0; i < 25; i++)
                {
                    baseOfPersons.PersonsCatalog.AddPerson(new Person());
                }
            }
            catch (PersonAlreadyExistsException ex)
            {
                Console.WriteLine(ex.Message + "\nОшибка при попытке повторного добавления человека: " + ex.Person);
                throw;
            }

            baseOfPersons.SaveToBinary();
            baseOfPersons.LoadFromBinary();
            Assert.AreEqual(25, baseOfPersons.PersonsCatalog.Count());
        }


        [TestMethod]
        public void SerializedAndDeserializedPersonCatalogToXmlTest()
        {
            BaseDumper baseOfPersons = new BaseDumper();
            try
            {
                for (int i = 0; i < 25; i++)
                {
                    baseOfPersons.PersonsCatalog.AddPerson(new Person());
                }
            }
            catch (PersonAlreadyExistsException ex)
            {
                Console.WriteLine(ex.Message + "\nОшибка при попытке повторного добавления человека: " + ex.Person);
                throw;
            }

            baseOfPersons.SaveToXml();
            baseOfPersons.LoadFromXml();
            Assert.AreEqual(25, baseOfPersons.PersonsCatalog.Count());
        }
        [TestMethod]
        public void SerializedAndDeserializedPersonCatalogToJsonTest()
        {
            BaseDumper baseOfPersons = new BaseDumper();
            try
            {
                for (int i = 0; i < 25; i++)
                {
                    baseOfPersons.PersonsCatalog.AddPerson(new Person());
                }
            }
            catch (PersonAlreadyExistsException ex)
            {
                Console.WriteLine(ex.Message + "\nОшибка при попытке повторного добавления человека: " + ex.Person);
                throw;
            }

            baseOfPersons.SaveToJson();
            baseOfPersons.LoadFromJson();
            Assert.AreEqual(25, baseOfPersons.PersonsCatalog.Count());
        }
    }
}
