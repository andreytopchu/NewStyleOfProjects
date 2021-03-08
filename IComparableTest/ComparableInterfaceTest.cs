using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ObjectsLib;

namespace Tests
{
    [TestClass]
    public class ComparableInterfaceTest
    {
        private Figure[] _figuresArray = new Figure[100];

        private enum ParametersOfSort { Area, Perimetr }

        private void GenerateFigure()
        {
            Random rnd = new Random();

            for (int i = 0; i < _figuresArray.Length; i++)
            {
                switch (rnd.Next(1, 4))
                {
                    case 1:
                        _figuresArray[i] = new Circle(rnd.Next(1, 15));
                        break;
                    case 2:
                        _figuresArray[i] = new Square(rnd.Next(1, 15));
                        break;
                    case 3:
                        while (_figuresArray[i] == null)
                        {
                            try
                            {
                                int a = rnd.Next(3, 9);
                                int b = rnd.Next(3, 9);
                                int c = (int)(a + b) / 2;
                                _figuresArray[i] = new Triangle(a, b, c);
                            }
                            catch (Exception)
                            {
                                continue;
                            }
                        }
                        break;
                }
            }
        }

        private void SortByArea()
        {
            Array.Sort(_figuresArray, new AreaFigureComparer());
        }

        private void SortByPerimetr()
        {
            Array.Sort(_figuresArray, new PerimetrFigureComparer());
        }

        private bool IsSorted(Figure[] array, ParametersOfSort parameter)
        {
            if (array != null)
            {
                for (int i = 1; i < array.Length; i++)
                {
                    if (parameter == ParametersOfSort.Area)
                    {
                        if (array[i - 1].CalcArea() > array[i].CalcArea())
                        {
                            return false;
                        }
                    }
                    else
                    {
                        if (array[i - 1].CalcPerimetr() > array[i].CalcPerimetr())
                        {
                            return false;
                        }
                    }
                }
                return true; 
            }
            else throw new ArgumentException("Передаваемый масси объектов не должен быть null.");
        }

        private void PrintArray(ParametersOfSort parametr)
        {
            if (parametr==null)
            {
                throw new ArgumentException("Вывод без параметра недоступен!");
            }
            else
            {
                foreach (var figure in _figuresArray)
                {
                    if (parametr == ParametersOfSort.Area)
                    {
		                Console.WriteLine(figure + " имеет площадь: "+figure.CalcArea());
                    }
                    else
                    {
                        Console.WriteLine(figure + " имеет периметр: "+figure.CalcPerimetr());
                    }
                }
                Console.WriteLine();
            }
        }

        [TestInitialize]
        public void Initialization()
        {
            GenerateFigure();
        }

        [TestMethod]
        public void SortByAreaTest()
        {
            SortByArea();
            PrintArray(ParametersOfSort.Area);
            Assert.IsTrue(IsSorted(_figuresArray, ParametersOfSort.Area));
        }

        [TestMethod]
        public void SortByPerimetrTest()
        {
            SortByPerimetr();
            PrintArray(ParametersOfSort.Perimetr);
            Assert.IsTrue(IsSorted(_figuresArray, ParametersOfSort.Perimetr));
        }
    }

}
