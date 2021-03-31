using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjectsLib
{
    public class PrintFigureAreaToConsole<T>: IPrintFigureArea<T> where T: Figure
    {
        public delegate void Notifier(string message);
        public event Notifier Notify;

        public void PrintFigureAreaToConsoleWithCreareEvent(T figure)
        {
            if (Notify == null)
                Notify += PrintMessage;

            Notify?.Invoke("Площадь фигуры: " + figure.CalcArea());
        }

        private void PrintMessage(string message)
        {
            Console.WriteLine(message);
        }
    }
}
