using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Roamler.QuadTree.UnitTest
{
    [TestClass]
    public class BoundaryTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Constructor_ZeroDimension_Throws()
        {
            var center = new Point(1, 1);
            var boundary = new Boundary(center, 0, 0);

            Assert.Fail("Boundary with dimension zero should throw");
        }

        [TestMethod]
        public void Contains_PointOutOfBoundary_ReturnsFalse()
        {
            var center = new Point(1,1);
            var boundary = new Boundary(center, 2, 2);

            var pointOutOfBounds = new Point(-2, -4);
            var result = boundary.Contains(pointOutOfBounds);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Contains_PointWithinBoundary_ReturnsTrue()
        {
            var center = new Point(-1, 1);
            var boundary = new Boundary(center, 1, 1);

            var pointWithinBounds = new Point(-0.3, 0.7);
            var result = boundary.Contains(pointWithinBounds);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Contains_PointOnBoundary_ReturnsTrue()
        {
            var center = new Point(-1, -1);
            var boundary = new Boundary(center, 1, 1);

            var pointOnBoundary = new Point(0, 0);
            var result = boundary.Contains(pointOnBoundary);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Intersects_AreaOutOfBoundary_ReturnsFalse()
        {
            var center = new Point(-1, -1);
            var boundary = new Boundary(center, 1, 1);

            var areaOutOfBoundary = new Boundary(new Point(1, 1), 0.5, 0.5);
            var result = boundary.Intersects(areaOutOfBoundary);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Intersects_AreaWithPartialIntersection_ReturnsTrue()
        {
            var center = new Point(0, 0);
            var boundary = new Boundary(center, 0.5, 0.5);

            var areaWithPartialIntersection = new Boundary(new Point(1, 0), 1, 1);
            var result = boundary.Intersects(areaWithPartialIntersection);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Intersects_AreaWithBorderIntersection_ReturnsTrue()
        {
            var center = new Point(0, 0);
            var boundary = new Boundary(center, 0.5, 0.5);

            var areaWithBorderIntersection = new Boundary(new Point(1, 0), 0.5, 0.5);
            var result = boundary.Intersects(areaWithBorderIntersection);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Intersects_AreaWithinBoundary_ReturnsTrue()
        {
            var center = new Point(-3, -3);
            var boundary = new Boundary(center, 2, 2);

            var areaWithinBoundary = new Boundary(new Point(-3, -3), 1, 1);
            var result = boundary.Intersects(areaWithinBoundary);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Intersects_AreaContainingBoundary_ReturnsTrue()
        {
            var center = new Point(-3, -3);
            var boundary = new Boundary(center, 1, 1);

            var areaContainingBoundary = new Boundary(new Point(-3, -3), 5, 5);
            var result = boundary.Intersects(areaContainingBoundary);

            Assert.IsTrue(result);

        }


    }
}
