using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Collections.ObjectModel;

namespace ObjectsLib
{
    public class MyQueue<T> where T: new()
    {
        private Collection<T> _elements;
        private int _front = 0;
        private int count = 0;
        private readonly int _maxLenght;

        public delegate void QueueExceptionNotifier(string toNotify);

        public event QueueExceptionNotifier QueueOverflow;
        public event QueueExceptionNotifier QueueUnderflow;

        public MyQueue(int maxLenght)
        {
            if (maxLenght <= 0) throw new ArgumentException("Недопустимый размер очереди.");

            _maxLenght = maxLenght;

            _elements = new Collection<T>();
        }

        public void Enqueue(T item)
        {
            if (count >= _maxLenght)
            {
                QueueOverflow.Invoke("Очередь переполнена");
                throw new InvalidOperationException("Невозможно выполнить операцию, т.к. очередь пуста.");
            }
                
            else
            {
                _elements.Add(item);
                count++;
            }
        }

        public T Dequeue()
        {
            if (count<=0)
            {
                QueueUnderflow.Invoke("Очередь пуста");
                throw new InvalidOperationException("Невозможно выполнить операцию, т.к. очередь пуста.");
            }
            else
            {
                T returnedElement = _elements[_front];
                _front++;
                count--;
                return returnedElement;
            }
                    
        }
    }
}
