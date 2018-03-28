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
    class ShapesList
    {
        public static List<Shape> list = new List<Shape>()
        {
            new Rectangle(new Color(), new Point(), new Point()),
            new Square(new Color(), new Point(), new Point()),
            new Circle(new Color(), new Point(), new Point()),
            new Ellipse(new Color(), new Point(), new Point()),
            new Triangle(new Color(), new Point(), new Point()),
            new Hexagon(new Color(), new Point(), new Point())
        };
    }
}
