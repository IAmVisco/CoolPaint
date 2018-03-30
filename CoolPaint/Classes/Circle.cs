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
    public class Circle : Ellipse
    {
        public Circle(Color color, Point p1, Point p2) : base(color, p1, p2)
        {
        }

        protected override void SideSet()
        {
            if (p2.X - p1.X >= 0)
                Width = p2.X - p1.X;
            Height = Width;
            ((System.Windows.Shapes.Rectangle)dBase).RadiusX = Width / 2;
            ((System.Windows.Shapes.Rectangle)dBase).RadiusY = Height / 2;
        }
    }
}
