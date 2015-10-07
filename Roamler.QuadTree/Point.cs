namespace Roamler.QuadTree
{
    public class Point : IPoint
    {
        private readonly double _x, _y;

        public Point(double x, double y)
        {
            _x = x;
            _y = y;
        }

        public double X { get { return _x; } }
        public double Y { get { return _y; } }

        public override string ToString()
        {
            return string.Format("[{0:N4},{1:N4}]", _x, _y);
        }
    }

}
