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
    /// Interaction logic for ShapePropertyControl.xaml
    /// </summary>
    public partial class ShapePropertyControl : UserControl
    {
        public Shape shape;

        public ShapePropertyControl(Shape shape)
        {
            InitializeComponent();
            this.shape = shape;

            Name.Text = shape.ToString().Substring(shape.ToString().LastIndexOf('.') + 1);
            Height.Text = shape.Height.ToString();
            Width.Text = shape.Width.ToString();
        }
    }
}
