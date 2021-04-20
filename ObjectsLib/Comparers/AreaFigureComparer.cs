using System;
using System.Collections.Generic;

namespace ObjectsLib.Comparers
{
   public class AreaFigureComparer: IComparer<Figure>
    {
       public int Compare(Figure x, Figure y)
       {
           const double Precision = 0.00001;

           if (x != null && y != null)
           {
               double xArea = x.CalcArea();
               double yArea = y.CalcArea();

               if (xArea < yArea) 
                   return -1;
               else 
                   if (Math.Abs(xArea - yArea) < Precision) 
                       return 0;
                   else 
                       return 1;
           }
           else
           {
               throw new ArgumentNullException("Ошибка сравнения фигур! Фигура не может быть null");
           }
       }
    }
}
