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
    public abstract class Shape
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
                StartPosSet();
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
                SetP2X(value);
                SetP2Y(value);

                StartPosSet();
                SideSet();
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

        public bool revX, revY;

        protected abstract System.Windows.Shapes.Shape GenerateDrawBase();

        public Shape(Color color, Point p1, Point p2)
        {
            
            this.color = color;
            this.p1 = p1;

            SetP2X(p2);
            SetP2Y(p2);

            dBase = GenerateDrawBase();
            StartPosSet();

            FillFig();
            SideSet();
        }

        protected void SetP2X(Point p3)
        {
            double p1x = p1.X;
            if (revX)
            {
                p1x += Width;
            }

            if (p1x > p3.X)
            {
                p1.X = p3.X;
                p2.X = p1x;
                revX = true;
            }
            else
            {
                p2.X = p3.X;
                revX = false;
            }
        }

        protected void SetP2Y(Point p3)
        {
            double p1y = p1.Y;
            if (revY)
            {
                p1y += Height;
            }

            if (p1y > p3.Y)
            {
                p1.Y = p3.Y;
                p2.Y = p1y;
                revY = true;
            }
            else
            {
                p2.Y = p3.Y;
                revY = false;
            }
        }

        public void Draw(Canvas cnv)
        {

            cnv.Children.Add(dBase);
        }

        private void FillFig()
        {
            dBase.Fill = new SolidColorBrush(color);
        }

        private void StartPosSet()
        {
            Canvas.SetLeft(dBase, p1.X);
            Canvas.SetTop(dBase, p1.Y);
        }

        protected abstract void SideSet();
    }
}
