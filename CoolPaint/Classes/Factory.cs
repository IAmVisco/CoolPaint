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
using System.Reflection;

namespace CoolPaint
{
    abstract class Factory
    {
        public abstract Shape FactoryMethod(Color color, Point v1, Point v2);
    }

    class RectangleFactory : Factory
    {
        public override Shape FactoryMethod(Color color, Point v1, Point v2)
        {
            return new Rectangle(color, v1, v2);
        }
    }

    class SquareFactory : Factory
    {
        public override Shape FactoryMethod(Color color, Point v1, Point v2)
        {
            return new Square(color, v1, v2);
        }
    }

    class EllipseFactory : Factory
    {
        public override Shape FactoryMethod(Color color, Point v1, Point v2)
        {
            return new Ellipse(color, v1, v2);
        }
    }

    class CircleFactory : Factory
    {
        public override Shape FactoryMethod(Color color, Point v1, Point v2)
        {
            return new Circle(color, v1, v2);
        }
    }

    class IsoscelesTriangleFactory : Factory
    {
        public override Shape FactoryMethod(Color color, Point v1, Point v2)
        {
            return new Triangle(color, v1, v2);
        }
    }
}
