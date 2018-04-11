using System.Windows;
using System.Windows.Media;

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
