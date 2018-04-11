using System;
using System.Windows;
using System.Windows.Media;
using System.Runtime.Serialization;

namespace CoolPaint
{
    [Serializable]
    public class Circle : Ellipse
    {
        public Circle(Color color, Point p1, Point p2) : base(color, p1, p2)
        {

        }

        protected Circle(SerializationInfo info, StreamingContext context) : base(info, context)
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
