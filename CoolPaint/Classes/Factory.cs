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
        public abstract Shape FactoryMethod(Color color, Point p1, Point p2);
    }

    class RectangleFactory : Factory
    {
        public override Shape FactoryMethod(Color color, Point p1, Point p2)
        {
            return new Rectangle(color, p1, p2);
        }
    }

    class SquareFactory : Factory
    {
        public override Shape FactoryMethod(Color color, Point p1, Point p2)
        {
            return new Square(color, p1, p2);
        }
    }

    class EllipseFactory : Factory
    {
        public override Shape FactoryMethod(Color color, Point p1, Point p2)
        {
            return new Ellipse(color, p1, p2);
        }
    }

    class CircleFactory : Factory
    {
        public override Shape FactoryMethod(Color color, Point p1, Point p2)
        {
            return new Circle(color, p1, p2);
        }
    }

    class TriangleFactory : Factory
    {
        public override Shape FactoryMethod(Color color, Point p1, Point p2)
        {
            return new Triangle(color, p1, p2);
        }
    }

    class HexagonFactory : Factory
    {
        public override Shape FactoryMethod(Color color, Point p1, Point p2)
        {
            return new Hexagon(color, p1, p2);
        }
    }
}
