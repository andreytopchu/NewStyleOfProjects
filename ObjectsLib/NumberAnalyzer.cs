using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjectsLib
{
    public class NumberAnalyzer
    {
        private readonly Collection<double> _numbers;

        public delegate void IsNumberDifferenceNotifier(string notify);
        public event IsNumberDifferenceNotifier BigDifferenceEvent;

        private readonly double _averageValue;

        public NumberAnalyzer(Collection<double> numbersStream)
        {
            if (numbersStream == null)
                throw new ArgumentNullException("Невозможно принять в анализатор несуществующую коллекцию.");
            else
            {
                _numbers = numbersStream;
                _averageValue = CalculateAverageValue();
            }   
        }

        private double CalculateAverageValue()
        {
            double sum = 0;
            foreach (var number in _numbers)
            {
                sum += number;
            }
            return sum / _numbers.Count;
        }

        public void AnalyzeStreamOfNumbersWithPercentage(int percentage)
        {
            if (percentage < 0) throw new ArgumentException("Проценты не могут иметь значение меньше нуля.");

            foreach (var number in _numbers)
            {
                if (100 * number / _averageValue > percentage)
                {
                    BigDifferenceEvent?.Invoke("Элемент " + number + " слишком отличается от среднего значения " + _averageValue);              
                }
            }
        }
    }
}
