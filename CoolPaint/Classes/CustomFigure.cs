using System;
using System.Windows;
using System.Windows.Media;
using System.Runtime.Serialization;
using System.Collections.Generic;
using System.Windows.Controls;

namespace CoolPaint
{
    public class CustomFigure
    {
        List<Shape> list = new List<Shape>();

        public CustomFigure(List<Shape> list)
        {
            this.list = list;
            ResetPosition();
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
        }

        public void Draw(Canvas cnv)
        {
            foreach (var shape in list)
                cnv.Children.Add(shape.dBase);
        }
    }
}
