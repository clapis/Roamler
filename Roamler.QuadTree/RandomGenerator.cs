using System;
using System.Collections.Generic;

namespace Roamler.QuadTree
{
    public class RandomGenerator
    {
        private readonly Random _random = new Random();

        public List<Point> GeneratePointsWithin(Boundary boundary, int quantity)
        {
            var points = new List<Point>(quantity);

            for (var i = 0; i < quantity; i++)
                points.Add(GeneratePointWithin(boundary));

            return points;
        }
        
        public Point GeneratePointWithin(Boundary boundary)
        {
            var x = boundary.Center.X + (2 * (_random.NextDouble() - 0.5) * boundary.HalfWidth);
            var y = boundary.Center.Y + (2 * (_random.NextDouble() - 0.5) * boundary.HalfHeight);

            return new Point(x, y);
        }

        public List<Boundary> GenerateIntersectingBoundaries(Boundary boundary, int quantity)
        {
            var boundaries = new List<Boundary>();

            for (var i = 0; i < quantity; i++)
                boundaries.Add(GenerateIntersectingBoundary(boundary));

            return boundaries;
        } 

        public Boundary GenerateIntersectingBoundary(Boundary boundary)
        {
            var center = GeneratePointWithin(boundary);
            var halfWidth = boundary.HalfWidth * 0.05 * (_random.NextDouble() + 0.0001);
            var halfHeight = boundary.HalfWidth * 0.05 * (_random.NextDouble() + 0.0001);

            return new Boundary(center, halfWidth, halfHeight);
        }

    }
}
