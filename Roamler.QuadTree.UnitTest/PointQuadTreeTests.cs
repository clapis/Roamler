using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Roamler.QuadTree.UnitTest
{
    [TestClass]
    public class PointQuadTreeTests
    {

        [TestMethod]
        public void Construct_New_HasNoChildren()
        {
            var center = new Point(0, 0);
            var boundary = new Boundary(center, 1, 1);

            var quad = new QuadTree<Point>(boundary);

            Assert.IsNotNull(quad);
            Assert.IsFalse(quad.HasChildren);
        }

        [TestMethod]
        public void Insert_OutOfBoundary_ReturnsFalse()
        {
            var center = new Point(0, 0);
            var boundary = new Boundary(center, 1, 1);
            var quad = new QuadTree<Point>(boundary);

            Assert.IsFalse(quad.Insert(new Point(2, 0)));
            Assert.IsFalse(quad.Insert(new Point(0, 2)));
        }

        [TestMethod]
        public void Insert_OnBoundary_ReturnsTrue()
        {
            var center = new Point(0, 0);
            var boundary = new Boundary(center, 1, 1);
            var quad = new QuadTree<Point>(boundary);

            Assert.IsTrue(quad.Insert(new Point(1, 0)));
            Assert.IsTrue(quad.Insert(new Point(0, 1)));
            Assert.IsTrue(quad.Insert(new Point(-1, 1)));
        }

        [TestMethod]
        public void Insert_WithinBoundary_ReturnsTrue()
        {
            var center = new Point(0, 0);
            var boundary = new Boundary(center, 1, 1);
            var quad = new QuadTree<Point>(boundary);

            Assert.IsTrue(quad.Insert(new Point(0, 0)));
            Assert.IsTrue(quad.Insert(new Point(-0.5, 0.5)));
        }

        [TestMethod]
        public void Search_OutOfBoundary_ReturnsEmtpyCollection()
        {
            var center = new Point(0, 0);
            var boundary = new Boundary(center, 1, 1);
            var quad = new QuadTree<Point>(boundary);

            var searchArea = new Boundary(new Point(3,3), 0.1, 0.1);
            var results = quad.Search(searchArea);

            Assert.IsNotNull(results);
            Assert.IsTrue(results.Count == 0);
        }

        [TestMethod]
        public void Search_PartialBoundary_ReturnResults()
        {
            var center = new Point(0, 0);
            var boundary = new Boundary(center, 1, 1);
            var quad = new QuadTree<Point>(boundary);
            
            var point = new Point(0.75, 0.75);
            var searchArea = new Boundary(new Point(1, 1), 1, 1);

            Assert.IsTrue(boundary.Contains(point));
            Assert.IsTrue(searchArea.Contains(point));

            quad.Insert(point);
            
            var results = quad.Search(searchArea);
            
            Assert.IsTrue(results.Count == 1);
            Assert.IsTrue(results.Contains(point));
        }

        [TestMethod]
        public void Search_AreaWithOnePoint_ReturnsOnePoint()
        {
            var center = new Point(0, 0);
            var boundary = new Boundary(center, 1, 1);
            var quad = new QuadTree<Point>(boundary);

            // insert one point in the second quadrant
            var point = new Point(-0.75, +0.75);
            quad.Insert(point);

            // then insert one point in each remaining quadrant
            var points = new List<Point>
            {
                new Point(+0.75, +0.75), // first quadrant
                new Point(-0.75, -0.75), // third quadrant
                new Point(+0.75, -0.75) // fourth quadrant
            };

            points.ForEach(p => quad.Insert(p));

            // search second quadrant
            var searchArea = new Boundary(new Point(-0.5, 0.5), 0.5, 0.5);

            var results = quad.Search(searchArea);

            Assert.IsTrue(results.Count == 1);
            Assert.IsTrue(results.Contains(point));
        }


        [TestMethod]
        public void Search_Area_ReturnsAreaPoints()
        {
            var center = new Point(0, 0);
            var boundary = new Boundary(center, 1, 1);
            var quad = new QuadTree<Point>(boundary);

            var firstQuadrant = new Boundary(new Point(+0.5, +0.5), 0.5, 0.5);
            var secondQuadrant = new Boundary(new Point(-0.5, +0.5), 0.5, 0.5);
            var thirdQuadrant = new Boundary(new Point(-0.5, -0.5), 0.5, 0.5);
            var fourthQuadrant = new Boundary(new Point(+0.5, -0.5), 0.5, 0.5);

            var pointsPerQuadrant = 10;
            var generator = new RandomGenerator();

            // insert points in the third quadrant
            var pointsInThirdQuadrant = generator.GeneratePointsWithin(thirdQuadrant, pointsPerQuadrant);
            pointsInThirdQuadrant.ForEach(p => quad.Insert(p));

            // insert points in other quadrants
            var pointsInOtherQuadrants = new List<Point>();
            pointsInOtherQuadrants.AddRange(generator.GeneratePointsWithin(firstQuadrant, pointsPerQuadrant));
            pointsInOtherQuadrants.AddRange(generator.GeneratePointsWithin(secondQuadrant, pointsPerQuadrant));
            pointsInOtherQuadrants.AddRange(generator.GeneratePointsWithin(fourthQuadrant, pointsPerQuadrant));
            pointsInOtherQuadrants.ForEach(p => quad.Insert(p));

            // search third quadrant
            var results = quad.Search(thirdQuadrant);

            // assert all result points are in third quadrant
            Assert.IsTrue(results.Count == pointsPerQuadrant);
            results.ForEach(p => Assert.IsTrue(thirdQuadrant.Contains(p)));
        }

        [TestMethod]
        public void Search_PointAtBoundaryCenter_IsReturnedOnlyOnce()
        {
            var center = new Point(0, 0);
            var boundary = new Boundary(center, 1, 1);
            var quad = new QuadTree<Point>(boundary);

            // insert a point in each  quadrant
            var points = new List<Point>
            {
                new Point(+0.75, +0.75),
                new Point(-0.75, +0.75),
                new Point(-0.75, -0.75),
                new Point(+0.75, -0.75)
            };

            points.ForEach(p => quad.Insert(p));

            // insert an additional point at the center
            var point = new Point(0, 0);
            quad.Insert(point);


            // search center
            var searchArea = new Boundary(center, 0.5, 0.5);
            var results = quad.Search(searchArea);

            Assert.IsTrue(results.Count == 1);
            Assert.IsTrue(results.Contains(point));
        }        

    }
}
