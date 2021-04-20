using System;

namespace ObjectsLib
{
    public class PrintFigureAreaToConsole<T>: IPrintFigureArea<T> where T: Figure
    {
        public delegate void Notifier(string message);
        public event Notifier Notify;

        public void PrintFigureAreaToConsoleWithCreareEvent(T figure)
        {
            if (figure == null)
            {
                throw new ArgumentNullException(nameof(figure));
            }

            if (Notify == null)
                Notify += PrintMessage;

            Notify?.Invoke("Площадь фигуры: " + figure.CalcArea());
        }
        //метод raise
        private void PrintMessage(string message)
        {
            Console.WriteLine(message);
        }
    }
}
