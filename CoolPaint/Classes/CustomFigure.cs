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
                StartPosSet();
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
                StartPosSet();
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
                ResetPosition();
                Height = p2.Y;
                Width = p2.X;
            }
        }

        private void ResetPosition()
        {
            list.Sort((s1, s2) => s1.P1.X.CompareTo(s2.P1.X));
            double diffX = list[0].P1.X;

            list.Sort((s1, s2) => s1.P1.Y.CompareTo(s2.P1.Y));
            double diffY = list[0].P1.Y;

            //for (int i = 0; i < list.Count; i++)
            //{
            //    list[i].P1 = new Point(list[i].P1.X - diffX, list[i].P1.Y - diffY);
            //}
            p1 = new Point(0, 0);
            //p1 = new Point(diffX, diffY);
            list.Sort((s1, s2) => s2.P2.X.CompareTo(s1.P2.X));
            p2 = new Point(list[0].P2.X, 0);

            list.Sort((s1, s2) => s2.P2.Y.CompareTo(s1.P2.Y));
            p2.Y = list[0].P2.Y;
        }

        public void Draw(Canvas cnv)
        {
            foreach (var shape in list)
                cnv.Children.Add(shape.dBase);
        }

        public void Move(Point delta)
        {
            P1 = new Point(p1.X + delta.X, p1.Y + delta.Y);
            P2 = new Point(p1.X + Width, p1.Y + Height);
        }

        private void StartPosSet()
        {
            for (int i = 0; i < list.Count; i++)
            {
                Canvas.SetLeft(list[i].dBase, p1.X + list[i].P1.X);
                Canvas.SetTop(list[i].dBase, p1.Y + list[i].P1.Y);
            }

        }
    }
}
