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
        MainWindow mainWin = new MainWindow();

        public ShapesWindow()
        {
            InitializeComponent();
        }

        private void ReClrBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void DelBtn_Click(object sender, RoutedEventArgs e)
        {
            //int z = shapesBox.SelectedIndex;
            //Object obj = shapesBox.SelectedItem;
        }
    }
}
