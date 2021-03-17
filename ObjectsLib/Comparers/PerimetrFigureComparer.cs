using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ObjectsLib.Comparers
{
   public class PerimetrFigureComparer: IComparer<Figure>
    {
        public int Compare(Figure x, Figure y)
        {
            const double Precision = 0.00001;

            if (x != null && y != null)
            {
                double xPerimetr = x.CalcPerimetr();
                double yPerimetr = y.CalcPerimetr();

                if (xPerimetr < yPerimetr)
                    return -1;
                else
                    if (Math.Abs(xPerimetr - yPerimetr) < Precision)
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
