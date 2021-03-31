using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjectsLib
{
    public interface IFigureDoublerTheSize<out T>
    {
         Square DoublerTheSize();
    }
}
