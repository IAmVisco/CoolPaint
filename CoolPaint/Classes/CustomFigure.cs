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
        List<Shape> list = new List<Shape>();
        public Point p1;
        public Point p2;
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

            for (int i = 0; i < list.Count; i++)
            {
                list[i].P1 = new Point(list[i].P1.X - diffX, list[i].P1.Y - diffY);
            }
            p1 = new Point(0, 0);

            list.Sort((s1, s2) => s2.P2.X.CompareTo(s1.P2.X));
            p2 = new Point(list[0].P2.X, 0);

            list.Sort((s1, s2) => s2.P2.Y.CompareTo(s1.P2.Y));
            p2.Y = list[0].P2.Y;

            //MessageBox.Show(p1.ToString() + " " + p2.ToString());
        }

        public void Draw(Canvas cnv)
        {
            foreach (var shape in list)
                cnv.Children.Add(shape.dBase);
        }

        public void Move(Point delta)
        {
            p1 = new Point(p1.X + delta.X, p1.Y + delta.Y);
            p2 = new Point(p1.X + Width, p1.Y + Height);
        }

        //private void StartPosSet()
        //{
        //    Canvas.SetLeft(dBase, p1.X);
        //    Canvas.SetTop(dBase, p1.Y);
        //}
    }
}
