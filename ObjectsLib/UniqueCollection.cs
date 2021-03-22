using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace ObjectsLib
{
    public class UniqueCollection<T>:ICollection<T>
    {
        private Collection<T> _collection;
        
        public UniqueCollection()
        {
            _collection = new Collection<T>();
        }

        public int Count => ((ICollection<T>)_collection).Count;

        public bool IsReadOnly => ((ICollection<T>)_collection).IsReadOnly;

        public void Add(T data)
        {
            if (data == null) throw new ArgumentNullException("Ошибка! Невозможно добавить в коллекцию элемент равный null.");

            if (!_collection.Contains(data))
                _collection.Add(data);
            else throw new ArgumentException("Невозможно добавить элемент! Данный элемент уже существует в коллекции.");
        }

        public void Clear()
        {
            ((ICollection<T>)_collection).Clear();
        }

        public bool Contains(T item)
        {
            return ((ICollection<T>)_collection).Contains(item);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            ((ICollection<T>)_collection).CopyTo(array, arrayIndex);
        }

        public IEnumerator<T> GetEnumerator()
        {
            return ((ICollection<T>)_collection).GetEnumerator();
        }

        public bool Remove(T item)
        {
            return ((ICollection<T>)_collection).Remove(item);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((ICollection<T>)_collection).GetEnumerator();
        }
    }
}
