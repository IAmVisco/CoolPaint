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
using System.Windows.Shapes;

namespace CoolPaint
{
    /// <summary>
    /// Interaction logic for ShapesWindow.xaml
    /// </summary>
    public partial class ShapesWindow : Window
    {
        public ShapesWindow()
        {
            InitializeComponent();
        }

        private void ReClrBtn_Click(object sender, RoutedEventArgs e)
        {
            (shapesBox.SelectedItem as ShapePropertyControl).shape.Color = (Owner as MainWindow).RNGColor();
        }

        private void DelBtn_Click(object sender, RoutedEventArgs e)
        {
            (Owner as MainWindow).cnv.Children.Remove((shapesBox.SelectedItem as ShapePropertyControl).shape.dBase);
            shapesBox.Items.Remove(shapesBox.SelectedItem);

        }
    }
}
