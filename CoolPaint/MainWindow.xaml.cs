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
        private Rectangle rect;
        private Ellipse ell;
        private Square sqr;
        private Circle crc;
        private Triangle tri;
        private Point p1;
        private Hexagon hex;

        private Random r = new Random();
        private Factory factory;
        private Shape shape;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            cnv.Children.Clear();

            sqr = new Square(RNGColor(), new Point(50, 50), new Point(100, 100));
            sqr.Draw(cnv);
            sqr = null;
            rect = new Rectangle(RNGColor(), new Point(120, 50), new Point(300, 180));
            rect.Draw(cnv);
            rect = null;
            ell = new Ellipse(RNGColor(), new Point(100, 200), new Point(300, 340));
            ell.Draw(cnv);
            ell = null;
            crc = new Circle(RNGColor(), new Point(400, 50), new Point(600, 250));
            crc.Draw(cnv);
            crc = null;
            tri = new Triangle(RNGColor(), new Point(650, 100), new Point(850, 250));
            tri.Draw(cnv);
            tri = null;
            hex = new Hexagon(RNGColor(), new Point(400, 270), new Point(600, 500));
            hex.Draw(cnv);
            hex = null;
        }

        private void cnv_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Cursor = Cursors.Pen;
            p1 = e.GetPosition(cnv);

            shape = factory.FactoryMethod(RNGColor(), p1, p1);
            shape.Draw(cnv);
        }

        private void cnv_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Cursor = Cursors.Arrow;
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

        private void rectRadio_Checked(object sender, RoutedEventArgs e)
        {
            factory = new RectangleFactory();
        }

        private void crcRadio_Checked(object sender, RoutedEventArgs e)
        {
            factory = new CircleFactory();
        }

        private void ellRadio_Checked(object sender, RoutedEventArgs e)
        {
            factory = new EllipseFactory();
        }

        private void triRadio_Checked(object sender, RoutedEventArgs e)
        {
            factory = new TriangleFactory();
        }

        private void hexRadio_Checked(object sender, RoutedEventArgs e)
        {
            factory = new HexagonFactory();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Action<Window> reposWindow = w =>
            {
                w.Left = this.Left + this.Width;
                w.Top = this.Top;
            };

            Action<Window> changeHeight = w =>
            {
                w.Height = this.Height;
            };

            ShapesWindow list = new ShapesWindow();
            list.Width = 300;
            reposWindow(list);
            list.Owner = this;
            list.Title = "Shape List";
            list.Show();
            this.LocationChanged += (s, _) => reposWindow(list);
            this.SizeChanged += (s, _) => changeHeight(list);
        }
    }     
}
