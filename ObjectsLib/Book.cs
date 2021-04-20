using System;

namespace ObjectsLib
{
    public class Book
    {
        public string Name { get; private set; }
        private string Description { get; set; }
        private int CountOfPage { get; set; }

        public Book(string name, string description, int countOfPage)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Description = description ?? throw new ArgumentNullException(nameof(description));
            if (countOfPage <= 0) throw new ArgumentOutOfRangeException(nameof(countOfPage));
            CountOfPage = countOfPage;
        }

        public double GetReadingProgress(int page)
        {
            return (double)page / CountOfPage * 100;
        }
    }
}
