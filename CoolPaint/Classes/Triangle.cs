using System;
using System.Windows;
using System.Windows.Media;


namespace CoolPaint
{
    [Serializable]
    public class Triangle : Polygon
    {
        public Triangle(Color color, Point p1, Point p2) : base(color, p1, p2)
        {
        }

        protected override Point[] GeneratePolygon()
        {
            Point[] triangle = new Point[3]
            {
                new Point((p2.X - p1.X) / 2, 0),
                new Point(p2.X - p1.X, p2.Y - p1.Y),
                new Point(0, p2.Y - p1.Y)
            };
            return triangle;
        }
    }
}
