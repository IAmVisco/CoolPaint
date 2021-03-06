﻿using System.Windows;
using System.Windows.Media;
using System.Runtime.Serialization;

namespace CoolPaint
{
    public abstract class Polygon : Shape
    {
        public override double Width
        {
            get
            {
                return width;
            }
            set
            {
                width = value;
            }
        }
        private double width;

        public override double Height
        {
            get
            {
                return height;
            }
            set
            {
                height = value;
            }
        }
        private double height;

        public Polygon(Color color, Point p1, Point p2) : base(color, p1, p2)
        {

        }

        protected Polygon(SerializationInfo info, StreamingContext context) : base(info, context)
        {

        }

        protected override System.Windows.Shapes.Shape GenerateDrawBase()
        {
            return new System.Windows.Shapes.Polygon();
        }

        protected override void SideSet()
        {
            Width = p2.X - p1.X;
            Height = p2.Y - p1.Y;

            ((System.Windows.Shapes.Polygon)dBase).Points = new PointCollection(GeneratePolygon());
        }

        protected abstract Point[] GeneratePolygon();
    }
}
