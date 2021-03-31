using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ObjectsLib;

namespace Tests
{
    [TestClass]
    public class CovariantsAndContrvariansTest
    {
        [TestMethod]
        public void DoublingTheSizeOfTheSquareConvarianceTest()
        {
            SquareDoublerTheSize<Square> square = new SquareDoublerTheSize<Square>(new Square(3));

            string message = null;
            square.Notify += delegate (string notify)
              {
                  message = notify;
              };

            IFigureDoublerTheSize<Figure> squareDoublerTheSize = square;
            squareDoublerTheSize.DoublerTheSize();
            Assert.IsNotNull(message);
            Assert.AreEqual("Квадрат был увеличен вдвое.", message);
        }

        [TestMethod]
        public void PrintFigureAreaContrVarianceTest()
        {
            
            IPrintFigureArea<Circle> circle = new PrintFigureAreaToConsole<Figure>();
            circle.PrintFigureAreaToConsoleWithCreareEvent(new Circle(5));

        }
    }
}
