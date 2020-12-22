using System;
using System.Collections.Generic;

namespace AdventOfCode2020.Day22
{
    public class LifoStack<T> : IEnumerable<T>
    {
        private T[] _array;
        
        public LifoStack (IEnumerable<T> collection)
        {
            _array = new T[0];
            foreach (var item in collection)
            {
                Push(item);
            }
        }

        public T Pop()
        {
            if (_array.Length == 0)
            {
                return default;
            }
            
            
            var valToReturn = _array[0];
            for (var i = 1; i < _array.Length; i++)
            {
                _array[i - 1] = _array[i];
            }
            Array.Resize(ref _array, _array.Length - 1);
            return valToReturn;
        }

        public void Push(T value)
        {
            Array.Resize(ref _array, _array.Length + 1);
            _array[^1] = value;
        }

        public IEnumerator<T> GetEnumerator() => ((IEnumerable<T>) _array).GetEnumerator();

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => GetEnumerator();
    }
}