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
        Point p1;
        Random r = new Random();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            rect = new Rectangle(RNGColor(), new Point(100, 100), new Point(200, 250));
            ell = new Ellipse(RNGColor(), new Point(200, 200), new Point(400, 344));
            sqr = new Square(RNGColor(), new Point(0, 0), new Point(100, 100));
            crc = new Circle(RNGColor(), new Point(400, 0), new Point(600, 200));
            crc.Draw(cnv);
            sqr.Draw(cnv);
            rect.Draw(cnv);
            ell.Draw(cnv);
            sqr = null;
            rect = null;
            ell = null;
        }

        private void cnv_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            p1 = e.GetPosition(cnv);
            rect = new Rectangle(RNGColor(), p1, p1);
            rect.Draw(cnv);
            rect.Draw(cnv);
        }

        private void cnv_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            rect = null;
        }

        private void cnv_MouseMove(object sender, MouseEventArgs e)
        {
            if (rect != null && e.LeftButton == MouseButtonState.Pressed)
            {
                Point p2 = e.GetPosition(cnv);
                rect.P2 = p2;
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
    }     
}
