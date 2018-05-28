using System;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Input;

namespace CoolPaint
{
    /// <summary>
    /// Interaction logic for ShapePropertyControl.xaml
    /// </summary>
    public partial class ShapePropertyControl : UserControl
    {
        public Shape shape;
        public CustomFigure custom;
        List<Shape> list = new List<Shape>();

        public ShapePropertyControl(Shape shape)
        {
            InitializeComponent();
            this.shape = shape;

            Name.Text = shape.ToString().Substring(shape.ToString().LastIndexOf('.') + 1);
            Height.Text = shape.Height.ToString();
            Width.Text = shape.Width.ToString();
        }

        public ShapePropertyControl(CustomFigure shape)
        {
            InitializeComponent();
            custom = shape;

            Name.Text = "Custom";
            Height.Text = shape.Height.ToString();
            Width.Text = shape.Width.ToString();
        }

        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
        }

        private void Width_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                //if (!(Mouse.LeftButton == MouseButtonState.Pressed))
                //    shape.Width = Convert.ToDouble(Width.Text);
            }
            catch { }
        }

        private void Height_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                //if (!(Mouse.LeftButton == MouseButtonState.Pressed))
                //    shape.Height = Convert.ToDouble(Height.Text);
            }
            catch { }
        }
    }
}
