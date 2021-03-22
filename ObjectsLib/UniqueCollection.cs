using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace ObjectsLib
{
    public class UniqueCollection<T>
    {
        private Collection<T> _collection;
        
        public UniqueCollection()
        {
            _collection = new Collection<T>();
        }

        public void Add(T data)
        {
            if (data == null) throw new ArgumentNullException("Ошибка! Невозможно добавить в коллекцию элемент равный null.");

            if (!_collection.Contains(data))
                _collection.Add(data);
            else throw new ArgumentException("Невозможно добавить элемент! Данный элемент уже существует в коллекции.");
        }
    }
}
