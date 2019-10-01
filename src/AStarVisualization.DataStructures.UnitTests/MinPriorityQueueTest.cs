using NUnit.Framework;
using System.Linq;

namespace AStarVisualization.DataStructures.UnitTests // TODO: implement the tests for this class
{
    [TestFixture]
    public class MinPriorityQueueTest
    {
        [Test]
        [TestCase(100)]
        [TestCase(25)]
        [TestCase(25000)]
        [TestCase(4508)]
        public void Constructor_SetsCapacity_SetsRightCapacity(int capacity)
        {
            var minPriorityQueue = new MinPriorityQueue<int, int>(capacity);
            int actualCapacity = minPriorityQueue.Capacity;

            Assert.AreEqual(actualCapacity, capacity);
        }

        private static object[] Pop_GetsPushedMultipleValues_ReturnsSmallestValue_Cases =
        {
            new object[] { 1, 10, 38, 1, 2183, 8372, 293 },
            new object[] { 112, 0131, 8392, 290, 128, 112, 238 },
            new object[] { 1023, 10381234, 823984137, 1992, 1902, 1023, 2839 },
        };
        [Test, TestCaseSource("Pop_GetsPushedMultipleValues_ReturnsSmallestValue_Cases")]
        public void Pop_GetsPushedMultipleValues_ReturnsSmallestValue(object[] values)
        {
            int smallestKey = (int)values[0];
            int[] numbers = values.Skip(1).Cast<int>().ToArray<int>();
            var minPriorityQueue = new MinPriorityQueue<int, string>(10);

            foreach (int num in numbers)
                minPriorityQueue.Add(num, "test");
            var lowestPriorityPair = minPriorityQueue.Pop();

            Assert.AreEqual(lowestPriorityPair.Key, smallestKey);
        }
        [Test, TestCaseSource("Pop_GetsPushedMultipleValues_ReturnsSmallestValue_Cases")]
        public void Peek_GetsPushedMultipleValues_ReturnsSmallestValue(object[] values)
        {
            int smallestKey = (int)values[0];
            int[] numbers = values.Skip(1).Cast<int>().ToArray<int>();
            var minPriorityQueue = new MinPriorityQueue<int, string>(10);

            foreach (int num in numbers)
                minPriorityQueue.Add(num, "test");
            var lowestPriorityPair = minPriorityQueue.Peek();

            Assert.AreEqual(lowestPriorityPair.Key, smallestKey);
        }
        [Test]
        public void Add_AddsElementToHeap_CountIncreases()
        {
            int[] keys = { 10, 123, 352, 1024 };
            var minPriorityQueue = new MinPriorityQueue<int, string>(10);

            foreach (int key in keys)
                minPriorityQueue.Add(key, "test");
            int previousCount = minPriorityQueue.Count;
            minPriorityQueue.Add(10323, "test");
            int actualCount = minPriorityQueue.Count;

            Assert.That(previousCount < actualCount);
        }
        [Test]
        public void Pop_RemovesElement_DecreasesCount()
        {
            int[] keys = { 10, 123, 352, 1024 };
            var minPriorityQueue = new MinPriorityQueue<int, string>(10);

            foreach (int key in keys)
                minPriorityQueue.Add(key, "test");
            int previousCount = minPriorityQueue.Count;
            var _ = minPriorityQueue.Pop();
            int actualCount = minPriorityQueue.Count;

            Assert.That(previousCount > actualCount);
        }
        [Test]
        public void Add_ExceedsCapacity_IncreasesCapacity()
        {
            int[] keys = { 10, 1234, 583, 28, 28 };
            var minPriorityQueue = new MinPriorityQueue<int, string>(5);

            foreach (var key in keys)
                minPriorityQueue.Add(key, "test");
            int previousCapacity = minPriorityQueue.Capacity;
            minPriorityQueue.Add(103, "test");
            int actualCapacity = minPriorityQueue.Capacity;

            Assert.That(actualCapacity == previousCapacity * 2);
        }
    }
}
