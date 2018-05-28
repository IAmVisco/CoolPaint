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
                Height = p2.Y;
                Width = p2.X;
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
            }
        }
    }
}
