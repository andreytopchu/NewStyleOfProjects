using System;

namespace ObjectsLib
{
    public class Triangle: Figure
    {
        public double A { get; }
        public double B { get; }
        public double C { get; }


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

        public override bool Equals(object obj)
        {
            if (!(obj is Triangle tr)) return false;

            return Math.Abs(this.CalcArea() - tr.CalcArea()) == 0 && Math.Abs(this.CalcPerimetr() - tr.CalcPerimetr()) == 0;
        }

        public override int GetHashCode()
        {
            return (this.CalcArea() + this.CalcPerimetr()).GetHashCode();
        }
    }
}
