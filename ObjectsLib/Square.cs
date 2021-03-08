using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ObjectsLib
{
   public class Square:Figure
    {
        public double A { get; private set; }

        public Square(double a)
        {
            if (a > 0)
            {
                this.A = a;
            }
            else
                throw new ArgumentException("Ошибка! Неверно введены параметры для создания фигуры.");
        }

        public override double CalcArea()
        {
            return this.A * this.A;
        }

        public override double CalcPerimetr()
        {
            return this.A * 4;
        }

        public override string ToString()
        {
            return "Квадрат со стороной: " + this.A ;
        }
    }
}
