using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;
using Newtonsoft.Json;

namespace ObjectsLib
{
    public class BaseDumper
    {
        public PersonsCatalog PersonsCatalog;
        private string _path = " D:/АНДРЕИНО/универ/4 семестр/Практика C#/Projects/TwoLayersSolution/Tests/PersonsSerialized";
        private BinaryFormatter _binaryFormatter = new BinaryFormatter();
        private XmlSerializer _xmlSerializer = new XmlSerializer(typeof(HashSet<Person>));

        public BaseDumper()
        {
            PersonsCatalog = new PersonsCatalog();
        }
        public void Save()
        {
            Path.Combine();
            using (var file = new StreamWriter(Path.Combine(_path, "PersonsCatalog.txt")))
            {
                foreach (var person in PersonsCatalog.PersonsHashSet)
                    file.WriteLine(person);
                file.Flush();
            }
            Console.WriteLine("Каталог сохранен в файл.");
        }

        public void SaveToBinary(string nameOfFile)
        {
            using (FileStream fs = new FileStream(Path.Combine(_path, nameOfFile),FileMode.Create))
                {
                    _binaryFormatter.Serialize(fs, PersonsCatalog.PersonsHashSet);
                    Console.WriteLine("Объект сериализован");
                }
        }

        public void SaveToXml(string nameOfFile)
        {
            using (FileStream fs = new FileStream(Path.Combine(_path, nameOfFile),
                FileMode.Create))
            {
               _xmlSerializer.Serialize(fs, PersonsCatalog.PersonsHashSet);
                Console.WriteLine("Объект сериализован");
            }
        }

        public void SaveToJson(string nameOfFile)
        {
            using (FileStream fs = new FileStream(Path.Combine(_path, nameOfFile), FileMode.Create))
            {
                string json = JsonConvert.SerializeObject(PersonsCatalog.PersonsHashSet);
                byte[] array = System.Text.Encoding.Default.GetBytes(json);

                fs.Write(array, 0, array.Length);
                Console.WriteLine("Объект сериализован");
            }
        }

        public void LoadFromBinary(string nameOfFile)
        {
            using (FileStream fs = new FileStream(Path.Combine(_path, nameOfFile),
                FileMode.Open))
            {
                try
                {
                    HashSet<Person> deserializesPeople =
                        (HashSet<Person>) _binaryFormatter.Deserialize(fs);
                    PersonsCatalog.PersonsHashSet = deserializesPeople;

                    Console.WriteLine("Объект десериализован");

                    foreach (var person in PersonsCatalog.PersonsHashSet)
                    {
                        person.PrintInfo();
                    }
                }
                catch (SerializationException)
                {
                    Console.WriteLine("Ошибка при чтении файла. Возможно объект сериализован в другом формате.");
                    throw;
                }
            }
        }

        public void LoadFromXml(string nameOfFile)
        {
            using (FileStream fs = new FileStream(Path.Combine(_path, nameOfFile),
                FileMode.Open))
            {
                try
                {
                    HashSet<Person> deserializesPeople =
                        (HashSet<Person>) _xmlSerializer.Deserialize(fs);
                    PersonsCatalog.PersonsHashSet = deserializesPeople;

                    Console.WriteLine("Объект десериализован");

                    foreach (var person in PersonsCatalog.PersonsHashSet)
                    {
                        person.PrintInfo();
                    }
                }
                catch (InvalidOperationException)
                {
                    Console.WriteLine("Ошибка при чтении файла. Возможно объект сериализован в другом формате.");
                    throw;
                }
            }
        }

        public void LoadFromJson(string nameOfFile)
        {
            using (FileStream fs = new FileStream(Path.Combine(_path, nameOfFile),
                FileMode.Open))
            {
                try
                {
                    byte[] array = new byte[fs.Length];
                    fs.Read(array, 0, array.Length);
                    string jsonText = System.Text.Encoding.Default.GetString(array);

                    HashSet<Person> deserializesPeople =
                        JsonConvert.DeserializeObject<HashSet<Person>>(jsonText);
                    PersonsCatalog.PersonsHashSet = deserializesPeople;

                    Console.WriteLine("Объект десериализован");

                    if (PersonsCatalog.PersonsHashSet != null)
                        foreach (var person in PersonsCatalog.PersonsHashSet)
                        {
                            person.PrintInfo();
                        }
                }
                catch (JsonReaderException)
                {
                    Console.WriteLine("Ошибка при чтении файла. Возможно объект сериализован в другом формате.");
                    throw;
                }
            }
        }

        public void Load()
        {
            using (var sr = new StreamReader(Path.Combine(_path, "PersonsCatalog.txt")))
            {
                var newHashSet = new HashSet<Person>();
                try
                {
                    string line = sr.ReadLine();
                    while (line != null)
                    {
                        var person = ParseLine(line);
                        newHashSet.Add(person);
                        line = sr.ReadLine();
                    }

                    PersonsCatalog.PersonsHashSet = newHashSet;
                }
                catch (Exception e)
                {
                    Console.WriteLine("Ошибка при чтении файла. " + e);
                    throw;
                }
            }
        }

        private static Person ParseLine(string line)
        {
            var words = line.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            var date = words[8].Split('.');

            var person = new Person(
                words[1],
                words[3],
                words[5],
                new DateTime(Int32.Parse(date[2]), Int16.Parse(date[1]), Int16.Parse(date[0])),
                words[11],

                words[14]+" "+words[15]
            );
            return person;
        }
    }
}
