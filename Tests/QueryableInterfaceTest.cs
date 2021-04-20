using Microsoft.VisualStudio.TestTools.UnitTesting;
using ObjectsLib;
using System;
using System.Linq;

namespace Tests
{
    [TestClass]
    public class QueryableInterfaceTest
    {
        private readonly string[] _actions = new string[]
        {
            " Написать тест ",
            " Сделать уборку ", 
            " Составить сочинение ", 
            " Полить цветы ", 
            " Убрать рабочее место ",
            " Сдать проект ",
            " Подмести ",
            " Приготовить ужин ",
            " Купить подарок ",
            " Кино с девушкой ",
            " Важное собеседование "
        };

        private static Random _rand = new Random();
        private Note[] _notes = new Note[100];

        private void GenerateNotes()
        {
            for (int i = 0; i < 100; i++)
            {
                _notes[i] = new Note(GenerateAction(), i + 1, GenerateDate(), GenerateBool());
            }
        }

        private string GenerateAction()
        {
            return _actions[_rand.Next(0, 11)];
        }

        private DateTime GenerateDate()
        {
            DateTime start = new DateTime(2021, 2, 1);
            int range = (DateTime.Today - start).Days;
            return start.AddDays(_rand.Next(range));
        }

        private bool GenerateBool()
        {
            return _rand.Next(0, 2) > 0;
        }

        [TestInitialize]
        public void Initialization()
        {
            GenerateNotes();
        }

        [TestMethod]
        public void SearchByDateTest()
        {
            DateTime randomDate = GenerateDate();

            var selectedNotes = _notes.Where(note => note.Date == randomDate);

            Console.WriteLine("Все заметки за {0}:", randomDate.ToShortDateString());
            foreach (var note in selectedNotes)
            {
                Console.WriteLine(note.ToString());
            }
        }

        [TestMethod]
        public void FindFirstTenNotesInAlphabeticalOrderTest()
        {
            var selectedNotes2 = _notes.Where(note => note.SerialNumber <= 10).OrderBy(note => note.Text);

            Console.WriteLine("\n\nПервые 10 заметок в алфавитном порядке: ");
            foreach (var note in selectedNotes2)
            {
                Console.WriteLine(note.ToString());
            }
        }

        [TestMethod]
        public void SumIsCompletedTest()
        {
            var countIsCompleted = _notes.Sum(note => Convert.ToInt16(note.IsCompleted));
            Console.WriteLine("\n\nКоличество выполненных заметок: {0}", countIsCompleted);
            var expected = _notes.Where(note => note.IsCompleted).Count();
            Assert.AreEqual(expected, countIsCompleted);
        }

        [TestMethod]
        public void OldestAndNewestNoteTest()
        {
            var minDate = _notes.Min(note => note.Date);
            var maxDate = _notes.Max(note => note.Date);

            var oldestNotes = _notes.Where(note => note.Date == minDate);
            var newestNotes = _notes.Where(note => note.Date == maxDate);

            Console.WriteLine("Самые старые записи: ");
            foreach (var oldestNote in oldestNotes)
            {
                Console.WriteLine(oldestNote);
            }

            Console.WriteLine("\nСамые новые записи: ");
            foreach (var newestNote in newestNotes)
            {
                Console.WriteLine(newestNote);
            }
        }

        [TestMethod]
        public void SortByDateTest()
        {
            var noteGroups = _notes.OrderBy(note => note.Date).GroupBy(note => note.Date);

            Console.WriteLine("\nОтсортировано по дате:");
            foreach (IGrouping<DateTime, Note> note in noteGroups)
            {
                Console.WriteLine(note.Key.ToShortDateString());
                foreach (var n in note)
                    Console.WriteLine(n.ToString());
                Console.WriteLine();
            }
        }

        [TestMethod]
        public void AllAndAnyTest()
        {
            bool result1 = _notes.All(note => note.Date.Month == 2);
            bool result2 = _notes.Any(note => note.Date.Month == 3);
            if (result1)
            {
                Console.WriteLine("Все записи сделаны в феврале.");
            }
            else
            {
                Console.WriteLine("Не все записи сделаны в феврале.");
                if (result2)
                {
                    Console.WriteLine("Некоторые записи сделаны в марте.");
                }
            }
        }
    }
}
