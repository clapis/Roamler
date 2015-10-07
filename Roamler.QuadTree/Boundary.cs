using System;

namespace Roamler.QuadTree
{
    public class Boundary
    {
        private readonly IPoint _center;
        private readonly double _halfWidth;
        private readonly double _halfHeight;

        public Boundary(IPoint center, double halfWidth, double halfHeight)
        {
            if (halfWidth <= 0 || halfHeight <= 0) 
                throw new ArgumentException("Dimension must be a non-zero positive number");

            _center = center;
            _halfWidth = halfWidth;
            _halfHeight = halfHeight;
        }

        public IPoint Center { get { return _center; } }
        public double HalfWidth { get { return _halfWidth; } }
        public double HalfHeight { get {  return _halfHeight; } }
        public double Top { get { return _center.Y + _halfHeight; } }
        public double Bottom { get { return _center.Y - _halfHeight; } }
        public double Left { get { return _center.X - _halfWidth; } }
        public double Right { get { return _center.X + _halfWidth; } }

        public bool Contains(IPoint location)
        {
            return location.X >= this.Left && location.X <= this.Right &&
                   location.Y >= this.Bottom && location.Y <= this.Top;
        }

        public bool Intersects(Boundary other)
        {
            return this.Left <= other.Right && this.Right >= other.Left &&
                   this.Bottom <= other.Top && this.Top >= other.Bottom;
        }
    }
}
