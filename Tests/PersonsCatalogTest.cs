using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using ObjectsLib;
using System;
using System.Runtime.Serialization;

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
            baseOfPersons.Load();
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

            baseOfPersons.SaveToBinary("peoples.dat");
            BaseDumper baseDumper = new BaseDumper();
            baseDumper.LoadFromBinary("peoples.dat");
            Assert.AreEqual(25, baseDumper.PersonsCatalog.Count());
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

            baseOfPersons.SaveToXml("peoples.xml");
            BaseDumper baseDumper = new BaseDumper();
            baseDumper.LoadFromXml("peoples.xml");
            Assert.AreEqual(25, baseDumper.PersonsCatalog.Count());
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

            baseOfPersons.SaveToJson("peoples.dat");
            BaseDumper baseDumper = new BaseDumper();
            baseDumper.LoadFromJson("peoples.dat");
            Assert.AreEqual(25, baseDumper.PersonsCatalog.Count());
        }

        [ExpectedException(typeof(JsonReaderException))]
        [TestMethod]
        public void TrySerializedBinaryAndDeserializedPersonCatalogToJsonTest()
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

            baseOfPersons.SaveToBinary("peoples.dat");
            BaseDumper baseDumper = new BaseDumper();
            baseDumper.LoadFromJson("peoples.dat");
            Assert.AreEqual(null, baseDumper.PersonsCatalog);
        }

        [ExpectedException(typeof(SerializationException))]
        [TestMethod]
        public void TrySerializedJsonAndDeserializedPersonCatalogToBinaryTest()
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

            baseOfPersons.SaveToJson("peoples.dat");
            BaseDumper baseDumper = new BaseDumper();
            baseDumper.LoadFromBinary("peoples.dat");
            Assert.AreEqual(null, baseDumper.PersonsCatalog);
        }

        [ExpectedException(typeof(InvalidOperationException))]
        [TestMethod]
        public void TrySerializedBinaryAndDeserializedPersonCatalogToXmlTest()
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

            baseOfPersons.SaveToBinary("peoples.dat");
            BaseDumper baseDumper = new BaseDumper();
            baseDumper.LoadFromXml("peoples.dat");
        }
    }
}
