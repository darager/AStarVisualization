using System;

namespace AStarVisualization.DataStructures
{
    public class MinHeap<T> where T : IComparable
    {
        private int _Capacity;
        private int _Count = 0;

        public int Capacity { get { return _Capacity; } }
        public int Count { get { return _Count; } }

        private T[] Items;

        public MinHeap(int Capacity)
        {
            if (Capacity == 0)
                throw new Exception("The Capacity can not be 0");


            this._Capacity = Capacity;

            Items = new T[_Capacity];
        }

        public T Peek()
        {
            if (_Count == 0)
                throw new Exception("The Heap does not contain any values");

            return Items[0];
        }
        public T GetMinimumElement()
        {
            if (_Count == 0)
                throw new Exception("The Heap does not contain any values");

            T item = Items[0];
            Items[0] = Items[_Count - 1];
            _Count--;
            HeapifyDown();

            return item;
        }
        public void Add(T item)
        {
            if (_Count == _Capacity)
                throw new Exception("The Capacity of the Heap is not large enough");

            Items[_Count] = item;
            _Count++;
            HeapifyUp();
        }

        private int GetRightChildIndex(int index)
        {
            return index * 2 + 2;
        }
        private int GetLeftChildIndex(int index)
        {
            return index * 2 + 1;
        }
        private int GetParentIndex(int index)
        {
            return (index - 1) / 2;

        }

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
            return Items[GetLeftChildIndex(index)];
        }
        private T GetRightChild(int index)
        {
            return Items[GetRightChildIndex(index)];
        }
        private T GetParent(int index)
        {
            return Items[GetParentIndex(index)];
        }

        private void Swap(int indexOne, int indexTwo)
        {
            T temp = Items[indexOne];
            Items[indexOne] = Items[indexTwo];
            Items[indexTwo] = temp;

        }
        private void HeapifyUp()
        {
            int index = _Count - 1;

            while (HasParent(index) && Items[index].CompareTo(GetParent(index)) == -1)
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

                    if (Items[RightChildIndex].CompareTo(Items[LeftChildIndex]) == -1)
                        SmallerChildIndex = RightChildIndex;
                }

                if (Items[index].CompareTo(Items[SmallerChildIndex]) == -1)
                    break;
                else
                    Swap(index, SmallerChildIndex);

                index = SmallerChildIndex;
            }
        }
    }
}
