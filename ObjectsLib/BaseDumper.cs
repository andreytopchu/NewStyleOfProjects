using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;
using Newtonsoft.Json;

namespace ObjectsLib
{
    public class BaseDumper
    {
        public PersonsCatalog PersonsCatalog;
        private string _path = " D:/АНДРЕИНО/универ/4 семестр/Практика C#/Projects/TwoLayersSolution/Tests";
        private BinaryFormatter _binaryFormatter = new BinaryFormatter();
        private XmlSerializer _xmlSerializer = new XmlSerializer(typeof(Dictionary<Person,Person>));

        public BaseDumper()
        {
            PersonsCatalog = new PersonsCatalog();
        }
        public void Save()
        {
            Path.Combine();
            using (var file = new StreamWriter(Path.Combine(_path, "PersonsSerialized", "PersonsCatalog.txt")))
            {
                foreach (var person in PersonsCatalog.PersonsDictionary)
                    file.WriteLine("{0} {1}", person.Key, person.Value);
                file.Flush();
            }
            Console.WriteLine("Каталог сохранен в файл.");
        }

        public void SaveToBinary()
        {
            using (FileStream fs = new FileStream(Path.Combine(_path, "PersonsSerialized", "peoples.dat"),FileMode.OpenOrCreate))
                {
                    _binaryFormatter.Serialize(fs, PersonsCatalog.PersonsDictionary);
                    Console.WriteLine("Объект сериализован");
                }
        }

        public void SaveToXml()
        {
            using (FileStream fs = new FileStream(Path.Combine(_path, "PersonsSerialized", "peoples.dat"), FileMode.OpenOrCreate))
            {
               _xmlSerializer.Serialize(fs, PersonsCatalog.PersonsDictionary);
                Console.WriteLine("Объект сериализован");
            }
        }

        public void SaveToJson()
        {
            using (FileStream fs = new FileStream(Path.Combine(_path, "PersonsSerialized", "peoples.dat"), FileMode.OpenOrCreate))
            {
                string json = JsonConvert.SerializeObject(PersonsCatalog.PersonsDictionary);
                byte[] array = System.Text.Encoding.Default.GetBytes(json);

                fs.Write(array, 0, array.Length);
                Console.WriteLine("Объект сериализован");
            }
        }

        public void LoadFromBinary()
        {
            using (FileStream fs = new FileStream(Path.Combine(_path, "PersonsSerialized","peoples.xml"), FileMode.OpenOrCreate))
            {
                Dictionary<Person, Person> deserializesPeople =
                    (Dictionary<Person, Person>) _binaryFormatter.Deserialize(fs);
                PersonsCatalog.PersonsDictionary = deserializesPeople;

                Console.WriteLine("Объект десериализован");

                foreach (var person in PersonsCatalog.PersonsDictionary)
                {
                    person.Value.PrintInfo();
                }
            }
        }

        public void LoadFromXml()
        {
            using (FileStream fs = new FileStream(Path.Combine(_path, "PersonsSerialized", "peoples.xml"), FileMode.OpenOrCreate))
            {
                Dictionary<Person, Person> deserializesPeople =
                    (Dictionary<Person, Person>)_xmlSerializer.Deserialize(fs);
                PersonsCatalog.PersonsDictionary = deserializesPeople;

                Console.WriteLine("Объект десериализован");

                foreach (var person in PersonsCatalog.PersonsDictionary)
                {
                    person.Value.PrintInfo();
                }
            }
        }

        public void LoadFromJson()
        {
            using (FileStream fs = new FileStream(Path.Combine(_path, "PersonsSerialized", "peoples.xml"), FileMode.OpenOrCreate))
            {
                byte[] array = new byte[fs.Length];
                fs.Read(array, 0, array.Length);
                string jsonText = System.Text.Encoding.Default.GetString(array);

                Dictionary<Person, Person> deserializesPeople =
                    JsonConvert.DeserializeObject<Dictionary<Person, Person>>(jsonText);
                PersonsCatalog.PersonsDictionary = deserializesPeople;

                Console.WriteLine("Объект десериализован");

                foreach (var person in PersonsCatalog.PersonsDictionary)
                {
                    person.Value.PrintInfo();
                }
            }
        }

        public void Load(string path)
        {
            using (var sr = new StreamReader(path))
            {
                var newDirectory = new Dictionary<Person, Person>();
                try
                {
                    string line = sr.ReadLine();
                    while (line != null)
                    {
                        var person = ParseLine(line);
                        newDirectory.Add(person, person);
                        line = sr.ReadLine();
                    }
                    PersonsCatalog.PersonsDictionary = newDirectory;
                }
                catch (Exception e)
                {
                    Console.WriteLine("Ошибка при чтении файла. " + e);
                }
            }
        }

        private static Person ParseLine(string line)
        {
            var words = line.Split(' ');
            var date = words[10].Split('.');

            var person = new Person(
                words[3],
                words[5],
                words[7],
                new DateTime(Int32.Parse(date[1]), Int16.Parse(date[2]), Int16.Parse(date[3])),
                words[13],
                words[16]
            );
            return person;
        }
    }
}
