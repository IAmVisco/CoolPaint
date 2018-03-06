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
        
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            rect = new Rectangle(Colors.Black, new Point(100, 100), new Point(200, 250));
            ell = new Ellipse(Colors.Black, new Point(200, 200), new Point(400, 344));
            rect.Draw(cnv);
            ell.Draw(cnv);
            rect = null;
            ell = null;
        }

        private void cnv_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Point p1 = e.GetPosition(cnv);
            rect = new Rectangle(Colors.Black, p1, p1);
            rect.Draw(cnv);
        }

        private void cnv_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            rect = null;
        }

        private void cnv_MouseMove(object sender, MouseEventArgs e)
        {
            if (rect != null)
            {
                Point p2 = e.GetPosition(cnv);
                rect.P2 = p2;
            }
        }
    }     
}
