using System;
using System.Windows;

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
            //ResourceDictionary rd = new ResourceDictionary()
            //{
            //    Source = new Uri("pack://application:,,,/MaterialDesignThemes.Wpf;component/Themes/MaterialDesignTheme.Dark.xaml")
            //};
            //Resources.MergedDictionaries.Add(rd);

            //rd = new ResourceDictionary()
            //{
            //    Source = new Uri("pack://application:,,,/MaterialDesignThemes.Wpf;component/Themes/MaterialDesignTheme.Defaults.xaml")
            //};
            //Resources.MergedDictionaries.Add(rd);
            //rd = new ResourceDictionary()
            //{
            //    Source = new Uri("pack://application:,,,/MaterialDesignColors;component/Themes/Recommended/Primary/MaterialDesignColor.Teal.xaml")
            //};
            //Resources.MergedDictionaries.Add(rd);
            //rd = new ResourceDictionary()
            //{
            //    Source = new Uri("pack://application:,,,/MaterialDesignColors;component/Themes/Recommended/Accent/MaterialDesignColor.Lime.xaml")
            //};
            //Resources.MergedDictionaries.Add(rd);
        }

        private void ReClrBtn_Click(object sender, RoutedEventArgs e)
        {
            if (shapesBox.SelectedItem != null)
                (shapesBox.SelectedItem as ShapePropertyControl).shape.Color = (Owner as MainWindow).RNGColor();
        }

        private void DelBtn_Click(object sender, RoutedEventArgs e)
        {
            if (shapesBox.SelectedItem != null)
            {
                (Owner as MainWindow).cnv.Children.Remove((shapesBox.SelectedItem as ShapePropertyControl).shape.dBase);
                shapesBox.Items.Remove(shapesBox.SelectedItem);
                shapesBox.SelectedIndex = shapesBox.Items.Count - 1;
            }
        }
    }
}
