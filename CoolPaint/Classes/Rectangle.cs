using System;
using System.Windows;
using System.Windows.Media;
using System.Runtime.Serialization;

namespace CoolPaint
{
    [Serializable]
    public class Rectangle : Shape
    {
        public Rectangle(Color color, Point p1, Point p2) : base(color, p1, p2)
        {

        }

        protected Rectangle(SerializationInfo info, StreamingContext context) : base(info, context)
        {

        }

        protected override System.Windows.Shapes.Shape GenerateDrawBase()
        {
            return new System.Windows.Shapes.Rectangle();
        }

        protected override void SideSet()
        {
            if (p2.X - p1.X >= 0)
                Width = p2.X - p1.X;
            if (p2.Y - p1.Y >= 0)
                Height = p2.Y - p1.Y;
        }
    }
}
