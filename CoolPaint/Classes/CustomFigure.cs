using System;
using System.Windows;
using System.Windows.Media;
using System.Runtime.Serialization;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Linq;

namespace CoolPaint
{
    public class CustomFigure
    {
        public List<Shape> list = new List<Shape>();
        public Point P1
        {
            get
            {
                return p1;
            }
            set
            {
                p1 = value;
            }
        }
        public virtual Point P2
        {
            get
            {
                return p2;
            }
            set
            {
               p2 = value;
            }
        }
        protected Point p1;
        protected Point p2;
        public double Height;
        public double Width;

        public CustomFigure(List<Shape> list)
        {
            if (list.Count > 0)
            {
                this.list = list;
                list.Sort((s1, s2) => s1.P1.X.CompareTo(s2.P1.X));
                double diffX = list[0].P1.X;

                list.Sort((s1, s2) => s1.P1.Y.CompareTo(s2.P1.Y));
                double diffY = list[0].P1.Y;

                p1 = new Point(diffX, diffY);

                list.Sort((s1, s2) => s2.P2.X.CompareTo(s1.P2.X));

                p2 = new Point(list[0].P2.X, 0);

                list.Sort((s1, s2) => s2.P2.Y.CompareTo(s1.P2.Y));
                p2.Y = list[0].P2.Y;

                Height = p2.Y - p1.Y;
                Width = p2.X - p1.X;
            }
        }

        public void Draw(Canvas cnv)
        {
            foreach (var shape in list)
                cnv.Children.Add(shape.dBase);
        }

        public void Move(Point delta)
        {
            for (int i = 0; i < list.Count; i++)
            {
                list[i].P1 = new Point(list[i].P1.X + delta.X, list[i].P1.Y + delta.Y);
                list[i].P2 = new Point(list[i].P2.X + delta.X, list[i].P2.Y + delta.Y);
            }
        }
    }
}
