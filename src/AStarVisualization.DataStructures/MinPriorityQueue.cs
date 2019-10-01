using System;
using System.Collections.Generic;
using System.Linq;

namespace AStarVisualization.DataStructures
{
    public class MinPriorityQueue<T, K> where T : IComparable
    {
        public int Capacity => _minHeap.Capacity;
        public int Count => _Count;

        private int _Count;
        private MinHeap<T> _minHeap;
        private Dictionary<T, List<K>> _dictionary;

        public MinPriorityQueue(int Capacity)
        {
            _minHeap = new MinHeap<T>(Capacity);
            _dictionary = new Dictionary<T, List<K>>();
        }

        public bool Contains(T key)
        {
            return _dictionary.ContainsKey(key);
        }
        public void Add(T key, K value)
        {
            _minHeap.Add(key);

            if (_dictionary.ContainsKey(key))
                _dictionary[key].Add(value);
            else
                _dictionary.Add(key, new List<K> { value });

            _Count++;
        }
        public KeyValuePair<T, K> Peek()
        {
            if (_Count == 0)
                throw new Exception("The Priority Queue is Empty");

            T key = _minHeap.Peek();
            List<K> list = _dictionary[key];

            return new KeyValuePair<T, K>(key, list.First());
        }
        public KeyValuePair<T, K> Pop()
        {
            if (_Count == 0)
                throw new Exception("The Priority Queue is empty");

            T key = _minHeap.Pop();
            List<K> list = _dictionary[key];

            K result = list.First();
            list.Remove(result);

            if (list.Count == 0)
                _dictionary.Remove(key);

            _Count--;

            return new KeyValuePair<T, K>(key, result);
        }
    }
}
