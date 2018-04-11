using System;
using System.Windows;
using System.Windows.Media;

namespace CoolPaint
{
    [Serializable]
    public class Hexagon : Polygon
    {
        public Hexagon(Color color, Point p1, Point p2) : base(color, p1, p2)
        {
        }

        protected override Point[] GeneratePolygon()
        {
            Point[] hexagon = new Point[]
            {
                new Point(0.5 * Width, 0),
                new Point(Width, 0.25 * Height),
                new Point(Width, 0.75 * Height),
                new Point(0.5 * Width, Height),
                new Point(0, 0.75 * Height),
                new Point(0, 0.25 * Height),
            };
            return hexagon;
        }
    }
}
