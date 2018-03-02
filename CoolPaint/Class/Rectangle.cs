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
    class Rectangle : Shape
    {
        double width, height;

        public Rectangle(Color color, Point topLeft, Point botRight):base(color, topLeft, botRight)
        {
            width = botRight.X - topLeft.X;
            height = botRight.Y - topLeft.Y;
        }
    }
}
