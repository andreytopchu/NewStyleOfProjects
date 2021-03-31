using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjectsLib
{
    public class PersonsCatalog
    {

        Dictionary<int, Person> _personsDictionary;

        public int Count()
        {
            return _personsDictionary.Count;
        }

        public PersonsCatalog()
        {
            _personsDictionary = new Dictionary<int, Person>();

        }

        private bool IsContainsInDictionary(Person person)
        {
            if (_personsDictionary.ContainsKey(person.GetHashCode()))
                return true;
            else return false;
        }

        public void AddPerson(Person person)
        {
            if (!IsContainsInDictionary(person))
            {
                _personsDictionary.Add(person.GetHashCode(), person);
            }
            else
                throw new InvalidOperationException("Данный человек уже содержится в каталоге. Невозможно выполнить добавление.");
        }

        public void DeletePerson(Person person)
        {
            if (person == null)
                throw new ArgumentNullException("Невозможно произвести удаление. Человек не может быть null.");
            if (_personsDictionary.ContainsKey(person.GetHashCode()))
            {
                _personsDictionary.Remove(person.GetHashCode());

            }
            else
                throw new InvalidOperationException("Данного человека нет в каталоге. Невозможно выполнить удаление.");
        }

        public Person GetPerson(int key)
        {
            if (_personsDictionary.ContainsKey(key)) return _personsDictionary[key];
            else throw new ArgumentException("В справочнике нет человека с таким ключом.");
        }

        public void Save()
        {
            var path = Directory.GetCurrentDirectory();
            using (var file = new StreamWriter(path + @"/PersonsCatalog.txt"))
            {
                foreach (var person in _personsDictionary)
                    file.WriteLine("{0} {1}", person.Key, person.Value.ToString());
            }
            Console.WriteLine("Каталог сохранен в файл.");
        }

        public void Load(string path)
        {
            using (var sr = new StreamReader(path))
            {
                var newDirectory = new Dictionary<int, Person>();
                try
                {
                    string line = sr.ReadLine();
                    while (line != null)
                    {
                        var tuple = ParseLine(line);
                        newDirectory.Add(tuple.Item1, tuple.Item2);
                        line = sr.ReadLine();
                    }
                    _personsDictionary = newDirectory;
                }
                catch (Exception e)
                {
                    Console.WriteLine("Ошибка при чтении файла. " + e);
                }
            }

            Tuple<int, Person> ParseLine(string line)
            {
                var words = line.Split(' ');
                var date = words[10].Split('.');

                var person = new Person(
                    words[3],
                    words[5],
                    words[7],
                    new DateTime(Int32.Parse(date[1]),Int16.Parse(date[2]),Int16.Parse(date[3])),
                    words[13],
                    words[16]
                    );
                return new Tuple<int, Person>(Int32.Parse(words[0]), person);
            }
        }
    }
}
