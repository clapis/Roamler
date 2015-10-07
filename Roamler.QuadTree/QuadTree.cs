using System.Collections.Generic;
using System.Linq;

namespace Roamler.QuadTree
{
    /// <summary>
    /// Very (very) simple implementation of the QuadTree data structure.
    /// Plenty of room for optimizations here.
    /// </summary>
    public class QuadTree<T> where T : IPoint
    {
        private const int MaxSize = 4;

        private readonly Boundary _bounds;
        private readonly List<T> _points;

        private QuadTree<T> _northWest, _northEast, _southWest, _southEast;

        public bool HasChildren { get { return _northWest != null; } }

        public QuadTree(Boundary bounds)
        {
            _bounds = bounds;
            _points = new List<T>(MaxSize);
        }

        public bool Insert(T point)
        {
            if (!_bounds.Contains(point)) return false;

            if (_points.Count < MaxSize)
            {
                _points.Add(point);
                return true;
            }
            
            if (!this.HasChildren) Split();

            if (_northWest.Insert(point)) return true;
            if (_northEast.Insert(point)) return true;
            if (_southWest.Insert(point)) return true;
            if (_southEast.Insert(point)) return true;

            // shouldn't happen
            return false;
        }

        public List<T> Search(Boundary searchArea)
        {
            List<T> points = new List<T>();

            if (!_bounds.Intersects(searchArea)) return points;

            var pointsWithinSearchArea = _points.Where(p => searchArea.Contains(p));
            points.AddRange(pointsWithinSearchArea);

            if (this.HasChildren)
            {
                points.AddRange(_northWest.Search(searchArea));
                points.AddRange(_northEast.Search(searchArea));
                points.AddRange(_southWest.Search(searchArea));
                points.AddRange(_southEast.Search(searchArea));
            }

            return points;
        }

        private void Split()
        {
            var center = _bounds.Center;
            var halfWidth = _bounds.HalfWidth / 2;
            var halfHeight = _bounds.HalfHeight / 2;

            var northWestCenter = new Point(center.X - halfWidth, center.Y + halfHeight);
            var northEastCenter = new Point(center.X + halfWidth, center.Y + halfHeight);
            var southWestCenter = new Point(center.X - halfWidth, center.Y - halfHeight);
            var southEastCenter = new Point(center.X + halfWidth, center.Y - halfHeight);

            _northWest = new QuadTree<T>(new Boundary(northWestCenter, halfWidth, halfHeight));
            _northEast = new QuadTree<T>(new Boundary(northEastCenter, halfWidth, halfHeight));
            _southWest = new QuadTree<T>(new Boundary(southWestCenter, halfWidth, halfHeight));
            _southEast = new QuadTree<T>(new Boundary(southEastCenter, halfWidth, halfHeight));
        }


    }
}
