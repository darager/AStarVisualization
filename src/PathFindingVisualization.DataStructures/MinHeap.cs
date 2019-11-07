using System;

namespace PathFindingVisualization.DataStructures
{
    public class MinHeap<T> where T : IComparable
    {
        public int Capacity => _Capacity;
        public int Count => _Count;

        private int _Capacity;
        private int _Count = 0;
        private T[] _Items;

        public MinHeap(int Capacity)
        {
            if (Capacity <= 0)
                throw new Exception("The Capacity can not be 0 or smaller than 0!");

            _Capacity = Capacity;
            _Items = new T[_Capacity];
        }

        public T Peek()
        {
            if (_Count == 0)
                throw new Exception("The Heap does not contain any values");

            return _Items[0];
        }
        public T Pop()
        {
            if (_Count == 0)
                throw new Exception("The Heap does not contain any values");

            T item = _Items[0];
            _Items[0] = _Items[_Count - 1];
            _Count--;
            HeapifyDown();

            return item;
        }
        public void Add(T item)
        {
            if (_Count == _Capacity)
                IncreaseCapacity();

            _Items[_Count] = item;
            _Count++;
            HeapifyUp();
        }

        private int GetRightChildIndex(int index) => (index * 2 + 2);
        private int GetLeftChildIndex(int index) => (index * 2 + 1);
        private int GetParentIndex(int index) => (index - 1) / 2;
        private bool HasLeftChild(int index)
        {
            return GetLeftChildIndex(index) < _Count;
        }
        private bool HasRightChild(int index)
        {
            return GetRightChildIndex(index) < _Count;
        }
        private bool HasParent(int index)
        {
            return GetParentIndex(index) >= 0;
        }
        private T GetLeftChild(int index)
        {
            return _Items[GetLeftChildIndex(index)];
        }
        private T GetRightChild(int index)
        {
            return _Items[GetRightChildIndex(index)];
        }
        private T GetParent(int index)
        {
            return _Items[GetParentIndex(index)];
        }

        private void Swap(int indexOne, int indexTwo)
        {
            T temp = _Items[indexOne];
            _Items[indexOne] = _Items[indexTwo];
            _Items[indexTwo] = temp;

        }
        private void HeapifyUp()
        {
            int index = _Count - 1;

            while (HasParent(index) && _Items[index].CompareTo(GetParent(index)) == -1)
            {
                Swap(GetParentIndex(index), index);
                index = GetParentIndex(index);
            }
        }
        private void HeapifyDown()
        {
            int index = 0;

            while (HasLeftChild(index))
            {
                int LeftChildIndex = GetLeftChildIndex(index);

                int SmallerChildIndex = LeftChildIndex;

                if (HasRightChild(index))
                {
                    int RightChildIndex = GetRightChildIndex(index);

                    if (_Items[RightChildIndex].CompareTo(_Items[LeftChildIndex]) == -1)
                        SmallerChildIndex = RightChildIndex;
                }

                if (_Items[index].CompareTo(_Items[SmallerChildIndex]) == -1)
                    break;

                Swap(index, SmallerChildIndex);

                index = SmallerChildIndex;
            }
        }

        private void IncreaseCapacity()
        {
            _Capacity = _Capacity * 2;
            Array.Resize(ref _Items, _Capacity);
        }
    }
}
