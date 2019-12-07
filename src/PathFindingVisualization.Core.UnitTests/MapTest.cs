using System;
using System.Collections;
using NUnit.Framework;
using PathFindingVisualization.Core.Node;

namespace PathFindingVisualization.Core.UnitTests
{
    [TestFixture]
    public class MapTest
    {
        [Test]
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(10)]
        [TestCase(23)]
        [TestCase(1000)]
        public void GetLength_GetsFirstDimension_ReturnsRightLength(int length)
        {
            var map = new Map.Map(length, 1);
            int mapLength = map.GetLength(0);

            Assert.AreEqual(mapLength, length);
        }

        [Test]
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(10)]
        [TestCase(23)]
        [TestCase(1000)]
        public void GetLength_GetsSecondDimension_ReturnsRightLength(int length)
        {
            var map = new Map.Map(1, length);
            int mapLength = map.GetLength(1);

            Assert.AreEqual(mapLength, length);
        }

        [Test]
        [TestCase(2)]
        [TestCase(-1)]
        [TestCase(10)]
        [TestCase(-92)]
        public void GetLength_DimensionDoesNotExist_ThrowsError(int dimension)
        {
            var map = new Map.Map(10, 10);

            Assert.That(() => map.GetLength(dimension),
                    Throws.Exception
                    .TypeOf<ArgumentOutOfRangeException>());
        }

        [Test]
        public void GetEnumerator_MapIsPopulated_ReturnsEnumerator()
        {
            var map = new Map.Map(10, 10);
            Node.Node[] expectedRow = map.Data[0];

            IEnumerator enumerator = map.GetEnumerator();
            enumerator.MoveNext();
            var firstRow = (Node.Node[])enumerator.Current;

            Assert.AreEqual(firstRow, expectedRow);
        }
    }
}
