using System;
using System.Linq;
using System.Collections.Generic;

namespace AStarDataStructures
{
    public class MinPriorityQueue<T, K> where T : IComparable
    {
        MinHeap<T> minHeap;
        Dictionary<T, List<K>> dictionary;

        private int _Capacity;
        private int _Count;

        public int Capacity { get { return _Capacity; } }
        public int Count { get { return _Count; } }

        public MinPriorityQueue(int Capacity)
        {
            _Capacity = Capacity;

            minHeap = new MinHeap<T>(Capacity);
            dictionary = new Dictionary<T, List<K>>();
        }

        public bool Contains(T key)
        {
            return dictionary.ContainsKey(key);
        }
        public void Add(T key, K value)
        {
            if (_Count == Capacity)
                throw new Exception("The Priority Queue is full");

            minHeap.Add(key);

            if (dictionary.ContainsKey(key))
                dictionary[key].Add(value);
            else
                dictionary.Add(key, new List<K> { value });

            _Count++;
        }
        public KeyValuePair<T, K> Peek()
        {
            if (_Count == 0)
                throw new Exception("The Priority Queue is Empty");

            T key = minHeap.Peek();
            List<K> list = dictionary[key];

            return new KeyValuePair<T, K>(key, list.First());
        }
        public KeyValuePair<T, K> GetMinPriorityPair()
        {
            if (_Count == 0)
                throw new Exception("The Priority Queue is empty");

            T key = minHeap.GetMinimumElement();
            List<K> list = dictionary[key];

            K result = list.First();
            list.Remove(result);

            if(list.Count == 0)
                dictionary.Remove(key);

            _Count--;

            return new KeyValuePair<T, K>(key, result);
        }
    }
}
