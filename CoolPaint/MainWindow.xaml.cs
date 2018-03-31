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
        private Point p1;
        private Random r = new Random();
        private Factory factory;
        private Shape shape;
        private ShapesWindow shapesWindow;
        ShapePropertyControl spc;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            cnv.Children.Clear();
            shapesWindow.shapesBox.Items.Clear();
        }

        private void cnv_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Cursor = Cursors.Pen;
            p1 = e.GetPosition(cnv);

            shape = factory.FactoryMethod(RNGColor(), p1, p1);
            shape.Draw(cnv);
            shapesWindow.shapesBox.Items.Add(new ShapePropertyControl(shape));
            shapesWindow.shapesBox.SelectedIndex = shapesWindow.shapesBox.Items.Count - 1;
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

                spc = shapesWindow.shapesBox.Items[shapesWindow.shapesBox.Items.Count - 1] as ShapePropertyControl;
                spc.Height.Text = Math.Round(shape.Height, 1).ToString();
                spc.Width.Text = Math.Round(shape.Width, 1).ToString();
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
        #region Factories
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
        #endregion
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            void reposWindow(Window w)
            {
                w.Left = this.Left + this.Width;
                w.Top = this.Top;
            }

            void changeHeight(Window w)
            {
                w.Height = this.Height;
                reposWindow(w);
            }

            shapesWindow = new ShapesWindow
            {
                Width = 300,
                Owner = this,
            };
            reposWindow(shapesWindow);
            shapesWindow.Show();
            this.LocationChanged += (s, _) => reposWindow(shapesWindow);
            this.SizeChanged += (s, _) => changeHeight(shapesWindow);
        }
    }     
}
