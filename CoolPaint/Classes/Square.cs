﻿using System;
using System.Windows;
using System.Windows.Media;
using System.Runtime.Serialization;

namespace CoolPaint
{
    [Serializable]
    public class Square : Rectangle
    {
        public Square(Color color, Point p1, Point p2) : base(color, p1, p2)
        {

        }

        protected Square(SerializationInfo info, StreamingContext context) : base(info, context)
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
