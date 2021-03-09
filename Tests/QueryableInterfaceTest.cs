using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ObjectsLib;

namespace Tests
{
    [TestClass]
    public class QueryableInterfaceTest
    {
        private string[] _actions = new string[]
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
                _notes[i] = new Note(GenerateAction(), i + 1, GenerateDate());
            }
        }

        private string GenerateAction()
        {
            return _actions[_rand.Next(0, 11)];
        }

        private DateTime GenerateDate()
        {
            DateTime start = new DateTime(2021, 2, 1);
            int range = ((TimeSpan)(DateTime.Today - start)).Days;
            return start.AddDays(_rand.Next(range));
        }

        [TestInitialize]
        public void Initialization()
        {
            GenerateNotes();
        }

        [TestMethod]
        public void DateSearch()
        {
            DateTime dateFilter = GenerateDate();

            var selectedNotes = from note in _notes
                                where note.Date.ToShortDateString() == dateFilter.ToShortDateString()
                                select note;

            Console.WriteLine("Все заметки за {0}:", dateFilter.ToShortDateString());
            foreach (var note in selectedNotes)
            {
                Console.WriteLine(note.ToString());
            }
        }

        [TestMethod]
        public void FindFirstTenNotesInAlphabeticalOrderTest()
        {
            var selectedNotes2 = from note in _notes
                                 where note.SerialNumber <= 10
                                 orderby note.Text
                                 select note;

            Console.WriteLine("\n\nПервые 10 заметок в алфавитном порядке: ");
            foreach (var note in selectedNotes2)
            {
                Console.WriteLine(note.ToString());
            }
        }

        [TestMethod]
        public void SumSerialNumbersTest()
        {
            var sumSeriesNumbers = _notes.Sum(note => note.SerialNumber);
            Console.WriteLine("\n\nСумма порядковых номеров всех ста заметок: {0}",sumSeriesNumbers);
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
            
            var noteGroups = from note in _notes
                             orderby note.Date
                              group note by note.Date;

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
