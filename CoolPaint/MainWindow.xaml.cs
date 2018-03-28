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
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Rectangle rect;
        Ellipse ell;
        Square sqr;
        Circle crc;
        Triangle tri;
        Point p1;
        Random r = new Random();
        Factory factory;
        Shape shape;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            cnv.Children.Clear();
            sqr = new Square(RNGColor(), new Point(50, 50), new Point(100, 100));
            rect = new Rectangle(RNGColor(), new Point(120, 50), new Point(300, 180));
            ell = new Ellipse(RNGColor(), new Point(100, 200), new Point(300, 340));
            crc = new Circle(RNGColor(), new Point(400, 50), new Point(600, 250));
            tri = new Triangle(RNGColor(), new Point(650, 100), new Point(750, 150));
            tri.Draw(cnv);
            crc.Draw(cnv);
            sqr.Draw(cnv);
            rect.Draw(cnv);
            ell.Draw(cnv);
            tri = null;
            crc = null;
            sqr = null;
            rect = null;
            ell = null;
        }

        private void cnv_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Cursor = Cursors.Pen;
            p1 = e.GetPosition(cnv);
            shape = factory.FactoryMethod(RNGColor(), p1, p1);
            //rect = new Rectangle(RNGColor(), p1, p1);
            shape.Draw(cnv);
        }

        private void cnv_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Cursor = Cursors.Arrow;
            //rect = null;
        }

        private void cnv_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                Point p2 = e.GetPosition(cnv);
                shape.P2 = p2;
            }
        }

        public Color RNGColor()
        {
            Color color = new Color
            {
                R = (byte)r.Next(256),
                G = (byte)r.Next(256),
                B = (byte)r.Next(256),
                A = (byte)(255 - r.Next(100))
            };
            
            return color;
        }

        private void sqrRadio_Checked(object sender, RoutedEventArgs e)
        {
            factory = new SquareFactory();
        }
    }     
}
