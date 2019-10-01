using NUnit.Framework;
using System.Linq;
using System;

namespace AStarVisualization.DataStructures.UnitTests
{
    [TestFixture]
    public class MinHeapTest
    {
        [Test]
        [TestCase(100)]
        [TestCase(25)]
        [TestCase(25000)]
        [TestCase(4508)]
        public void Constructor_SetsCapacity_SetsRightCapacity(int capacity)
        {
            var minHeap = new MinHeap<int>(capacity);
            int actualCapacity = minHeap.Capacity;

            Assert.AreEqual(actualCapacity, capacity);
        }

        [Test, TestCaseSource()]
        public void Pop_GetsPushedMultipleValues_ReturnsSmallestValue(object[] values)
        {
            int expectedSmallestValue = (int)values[0];
            int[] numbers = (int[])values.Skip(1).Cast<int>();

            var minHeap = new MinHeap<int>(values.Length);
            foreach (int num in numbers)
                minHeap.Add(num);
            int actualSmallestValue = minHeap.GetMinimumElement();

            Assert.AreEqual(actualSmallestValue, expectedSmallestValue);
        }
        private static object[] hedMultipleValues_ReturnsSmallestValue_Cases =
        {
            new object[] { 1, 10, 38, 1, 2183, 8372, 293 },
        };
    }
}
