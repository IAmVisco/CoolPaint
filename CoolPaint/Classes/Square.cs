using System;
using System.Windows;
using System.Windows.Media;

namespace CoolPaint
{
    [Serializable]
    public class Square : Rectangle
    {
        public Square(Color color, Point p1, Point p2) : base(color, p1, p2)
        {
        }

        protected override void SideSet()
        {
            if (p2.X - p1.X >= 0)
                Width = p2.X - p1.X;
            Height = Width;
        }
    }
}
