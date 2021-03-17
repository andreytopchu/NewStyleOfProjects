using System;

namespace ObjectsLib
{
    public class Circle : Figure
    {
        public int Radius { get; private set; }
        public double Area { get; private set; }

        public Circle(int radius)
        {
            if (radius>0)
            {
                Radius = radius;
                Area = CalcArea();
            }
            else
                throw new ArgumentException("Ошибка! Неверно введены параметры для создания фигуры.");
        }

        public override double CalcArea()
        {
            return Math.PI * Math.Pow(this.Radius, 2);
        }

        public override double CalcPerimetr()
        {
            return 2 * Math.PI * this.Radius;
        }

        public override string ToString()
        {
            return "Круг с радиусом: " + this.Radius;
        }
    }
}
