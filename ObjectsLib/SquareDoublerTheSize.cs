using System;

namespace ObjectsLib
{
    public class SquareDoublerTheSize<T> : IFigureDoublerTheSize<T> where T: Figure
    {
        public delegate void Notifier(string message);
        public event Notifier Notify;

        private T _square;

        public SquareDoublerTheSize(T figure)
        {
            _square = figure ?? throw new ArgumentNullException(nameof(figure));
        }

        public Square DoublerTheSize()
        {
            double sideA = _square.CalcPerimetr() / 4;
            Notify?.Invoke("Квадрат был увеличен вдвое.");
            return new Square(sideA * 2);
        }
    }
}
