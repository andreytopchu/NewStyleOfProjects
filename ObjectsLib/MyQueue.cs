using System;
using System.Collections.ObjectModel;

namespace ObjectsLib
{
    public class MyQueue<T> where T: new()
    {
        private Collection<T> _elements;
        private int _front;
        private int _count;
        private readonly int _maxLenght;

        public delegate void QueueExceptionNotifier(string toNotify);

        public event QueueExceptionNotifier QueueOverflow;
        public event QueueExceptionNotifier QueueUnderflow;

        public MyQueue(int maxLenght)
        {
            if (maxLenght <= 0) throw new ArgumentException("Недопустимый размер очереди.");

            _maxLenght = maxLenght;

            _elements = new Collection<T>();

            _front = 0;
            _count = 0;
        }

        public void Enqueue(T item)
        {
            if (_count >= _maxLenght)
            {
                if (QueueOverflow != null) QueueOverflow.Invoke("Очередь переполнена");
                throw new InvalidOperationException("Невозможно выполнить операцию, т.к. очередь пуста.");
            }
                
            else
            {
                _elements.Add(item);
                _count++;
            }
        }

        public T Dequeue()
        {
            if (_count<=0)
            {
                if (QueueUnderflow != null) QueueUnderflow.Invoke("Очередь пуста");
                throw new InvalidOperationException("Невозможно выполнить операцию, т.к. очередь пуста.");
            }
            else
            {
                T returnedElement = _elements[_front];
                _front++;
                _count--;
                return returnedElement;
            }
                    
        }
    }
}
