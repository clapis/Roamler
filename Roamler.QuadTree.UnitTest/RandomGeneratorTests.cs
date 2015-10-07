using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Roamler.QuadTree.UnitTest
{
    [TestClass]
    public class RandomGeneratorTests
    {

        [TestMethod]
        public void GeneratePointsWithin_Quantity_ReturnsCorrectQuantity()
        {
            var center = new Point(0, 0);
            var boundary = new Boundary(center, 1, 1);
            var generator = new RandomGenerator();

            var quantity = 100;

            var results = generator.GeneratePointsWithin(boundary, quantity);

            Assert.IsNotNull(results);
            Assert.IsTrue(results.Count == quantity);
        }

        [TestMethod]
        public void GeneratePointsWithin_Boundary_BoundaryContainsAllPoints()
        {
            var center = new Point(0, 0);
            var boundary = new Boundary(center, 1, 1);
            var generator = new RandomGenerator();

            var results = generator.GeneratePointsWithin(boundary, 1000);

            results.ForEach(p => Assert.IsTrue(boundary.Contains(p)));
        }

        [TestMethod]
        public void GenerateIntersectingBoundary_Boundary_Intersects()
        {
            var center = new Point(0, 0);
            var boundary = new Boundary(center, 10, 10);
            var generator = new RandomGenerator();

            var result = generator.GenerateIntersectingBoundary(boundary);

            Assert.IsNotNull(result);
            Assert.IsTrue(boundary.Intersects(result));
        }

        [TestMethod]
        public void GenerateIntersectingBoundaries_Boundaries_Intersects()
        {
            var center = new Point(0, 0);
            var boundary = new Boundary(center, 10, 10);
            var generator = new RandomGenerator();

            var result = generator.GenerateIntersectingBoundaries(boundary, 100);

            Assert.IsNotNull(result);
            Assert.IsTrue(result.Count == 100);
            
            result.ForEach(b => Assert.IsTrue(boundary.Intersects(b)));
        }
    }
}
