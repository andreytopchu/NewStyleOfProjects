using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ObjectsLib
{
    public class Triangle: Figure
    {
        public double A { get; private set; }
        public double B { get; private set; }
        public double C { get; private set; }


        public Triangle(double a, double b, double c)
        {
            if (a >= 0 && b >= 0 && c >= 0)
            {
                if (a + b > c && a + c > b && b + c > a)
                {
                    this.A = a;
                    this.B = b;
                    this.C = c;
                }
                else
                    throw new ArgumentException("Ошибка! Неверно введены параметры для создания фигуры.");
            }
            else
                throw new ArgumentException("Стороны не могут быть отрицательными или равны нулю!");
            
        }

        public override double CalcArea()
        {
            double hp = HalfPerimeter();
            return Math.Sqrt(hp * (hp - this.A) * (hp - this.B) * (hp - this.C));
        }

        public override double CalcPerimetr()
        {
            return this.A + this.B + this.C;
        }

        public override string ToString()
        {
            return "Треугольник со сторонами: " + this.A+", "+this.B+" и "+this.C;
        }

        public double HalfPerimeter()
        {
            return CalcPerimetr() / 2;
        }
    }
}
