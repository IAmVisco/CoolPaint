using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace CoolPaint
{
    class Triangle : Polygon
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
