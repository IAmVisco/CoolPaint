using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CoolPaint
{
    abstract class Shape
    {
        public System.Windows.Shapes.Shape dBase;
        public Color Color
        {
            get
            {
                return color;
            }
            set
            {
                color = value;
                FillFig();
            }
        }
        private Color color;
        public Point P1
        {
            get
            {
                return p1;
            }
            set
            {
                p1 = value;
                SetSides();
            }
        }
        protected Point p1;
        public Point P2
        {
            get
            {
                return p2;
            }
            set
            {
                p2 = value;
                SetSides();
            }
        }
        protected Point p2;
        public virtual double Height
        {
            get
            {
                return dBase.Height;
            }
            set
            {
                dBase.Height = value;
            }
        }
        public virtual double Width
        {
            get
            {
                return dBase.Width;
            }
            set
            {
                dBase.Width = value;
            }
        }

        protected abstract System.Windows.Shapes.Shape GenerateDrawBase();

        public Shape(Color color, Point p1, Point p2)
        {
            this.color = color;
            this.p1 = p1;
            this.p2 = p2;

            dBase = GenerateDrawBase();
            FillFig();
            SetSides();
        }

        public void Draw(Canvas cnv)
        {
            Canvas.SetLeft(dBase, p1.X);
            Canvas.SetTop(dBase, p1.Y);
            cnv.Children.Add(dBase);
        }

        private void FillFig()
        {
            dBase.Fill = new SolidColorBrush(color);
        }

        protected abstract void SetSides();
    }
}
