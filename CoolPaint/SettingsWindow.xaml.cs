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
    /// Interaction logic for SettingsWindow.xaml
    /// </summary>
    public partial class SettingsWindow : Window
    {
        List<Uri> themes = new List<Uri>()
        {
            new Uri("pack://application:,,,/MaterialDesignColors;component/Themes/Recommended/Primary/MaterialDesignColor.Teal.xaml"),
            new Uri("pack://application:,,,/MaterialDesignColors;component/Themes/Recommended/Primary/MaterialDesignColor.DeepPurple.xaml"),
            new Uri("pack://application:,,,/MaterialDesignColors;component/Themes/Recommended/Primary/MaterialDesignColor.Indigo.xaml"),
            new Uri("pack://application:,,,/MaterialDesignColors;component/Themes/Recommended/Primary/MaterialDesignColor.Cyan.xaml"),
            new Uri("pack://application:,,,/MaterialDesignColors;component/Themes/Recommended/Primary/MaterialDesignColor.Pink.xaml"),
        };

        public bool isLight = false;
        public string accentColor = "";

        public SettingsWindow(MainWindow win)
        {
            SetTheme();
            this.Owner = win;       
            InitializeComponent();
        }

        private void SetTheme()
        {
            ResourceDictionary rd = new ResourceDictionary()
            {
                Source = new Uri("pack://application:,,,/MaterialDesignThemes.Wpf;component/Themes/MaterialDesignTheme.Dark.xaml")
            };
            Resources.MergedDictionaries.Add(rd);
            rd = new ResourceDictionary()
            {
                Source = new Uri("pack://application:,,,/MaterialDesignThemes.Wpf;component/Themes/MaterialDesignTheme.Defaults.xaml")
            };
            Resources.MergedDictionaries.Add(rd);
            rd = new ResourceDictionary()
            {
                Source = new Uri("pack://application:,,,/MaterialDesignColors;component/Themes/Recommended/Primary/MaterialDesignColor.Teal.xaml")
            };
            Resources.MergedDictionaries.Add(rd);
            rd = new ResourceDictionary()
            {
                Source = new Uri("pack://application:,,,/MaterialDesignColors;component/Themes/Recommended/Accent/MaterialDesignColor.Lime.xaml")
            };
            Resources.MergedDictionaries.Add(rd);
        }

        private void ToggleButton_Click(object sender, RoutedEventArgs e)
        {
            if ((bool)themeToggle.IsChecked)
            {
                (Owner as MainWindow).grid.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#EEEEEE"));
                (Owner as MainWindow).Resources.MergedDictionaries[0].Source = new Uri("pack://application:,,,/MaterialDesignThemes.Wpf;component/Themes/MaterialDesignTheme.Light.xaml");
                settingsGrid.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#EEEEEE"));
                Resources.MergedDictionaries[0].Source = new Uri("pack://application:,,,/MaterialDesignThemes.Wpf;component/Themes/MaterialDesignTheme.Light.xaml");
                isLight = true;
            }
            else
            {
                (Owner as MainWindow).grid.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#333333"));
                (Owner as MainWindow).Resources.MergedDictionaries[0].Source = new Uri("pack://application:,,,/MaterialDesignThemes.Wpf;component/Themes/MaterialDesignTheme.Dark.xaml");
                settingsGrid.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#333333"));
                Resources.MergedDictionaries[0].Source = new Uri("pack://application:,,,/MaterialDesignThemes.Wpf;component/Themes/MaterialDesignTheme.Dark.xaml");
                isLight = false;
            }
        }

        private void themeChanged(object sender, SelectionChangedEventArgs e)
        {
            (Owner as MainWindow).Resources.MergedDictionaries[2].Source = themes[themeBox.SelectedIndex];
            accentColor = (themes[themeBox.SelectedIndex]).ToString();
        }
    }
}
