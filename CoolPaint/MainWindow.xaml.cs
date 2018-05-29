using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using Microsoft.Win32;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using System.Windows.Controls;
using System.Xml;
using System.Text;

namespace CoolPaint
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Point p1;
        private Point RightButtonDownPos;
        private Random r = new Random();
        private Factory factory;
        private Shape shape;
        private readonly string pluginPath = System.IO.Path.Combine(Directory.GetCurrentDirectory(), "Plugins");

        private ShapesWindow shapesWindow;
        private SettingsWindow settingsWindow;
        ShapePropertyControl spc;

        List<Type> typeList = new List<Type>()
        {
            typeof(Rectangle),
            typeof(Square),
            typeof(Circle),
            typeof(Ellipse),
            typeof(Triangle),
            typeof(Hexagon),
            typeof(Color),
            typeof(Point)
        };
        List<Factory> factoryList = new List<Factory>
        {
            new RectangleFactory(),
            new SquareFactory(),
            new EllipseFactory(),
            new CircleFactory(),
            new TriangleFactory(),
            new HexagonFactory()
        };
        List<CustomFigure> customFigList = new List<CustomFigure>();

        public MainWindow()
        {
            InitializeComponent();
            GetPlugins();
            GetPluginFactories();

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
            xy.Content = String.Format("X: {0} Y: {1}", e.GetPosition(cnv).X, e.GetPosition(cnv).Y);
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                Point p2 = e.GetPosition(cnv);
                shape.P2 = p2;

                spc = shapesWindow.shapesBox.Items[shapesWindow.shapesBox.Items.Count - 1] as ShapePropertyControl;
                spc.Height.Text = shape.Height.ToString();
                spc.Width.Text = shape.Width.ToString();
            }
            else if (e.RightButton == MouseButtonState.Pressed && shapesWindow.shapesBox.SelectedItem != null)
            {
                Point delta = new Point(e.GetPosition(cnv).X - RightButtonDownPos.X, e.GetPosition(cnv).Y - RightButtonDownPos.Y);
                if ((shapesWindow.shapesBox.SelectedItem as ShapePropertyControl).shape != null)
                {
                    (shapesWindow.shapesBox.SelectedItem as ShapePropertyControl).shape.Move(delta);
                    spc = shapesWindow.shapesBox.SelectedItem as ShapePropertyControl;
                    spc.Height.Text = shape.Height.ToString();
                    spc.Width.Text = shape.Width.ToString();
                }
                else
                {
                    (shapesWindow.shapesBox.SelectedItem as ShapePropertyControl).custom.Move(delta);
                }


                RightButtonDownPos = e.GetPosition(cnv);
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

            ResourceDictionary rd = new ResourceDictionary()
            {
                Source = new Uri("pack://application:,,,/MaterialDesignThemes.Wpf;component/Themes/MaterialDesignTheme.Light.xaml")
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
            settingsWindow = new SettingsWindow(this);
            XmlDocument doc = new XmlDocument();
            try
            {
                doc.Load("config.xml");
                XmlElement element = doc.DocumentElement;
                XmlNode node = element["isLightTheme"];
                Resources.MergedDictionaries[0] = new ResourceDictionary();
                if (Convert.ToBoolean(node.InnerText))
                {
                    settingsWindow.themeToggle.IsChecked = true;
                }
                else
                {
                    settingsWindow.themeToggle.IsChecked = false;
                    shapeComboBox.Foreground = new SolidColorBrush(Colors.White);
                    xy.Foreground = new SolidColorBrush(Colors.White);
                }
                node = element["AccentColor"];
                Resources.MergedDictionaries[2] = new ResourceDictionary
                {
                    Source = new Uri(node.InnerText)
                };
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            if (File.Exists(Directory.GetCurrentDirectory() + "\\custom.json"))
            {
                List<Shape> objList = new List<Shape>();
                DataContractJsonSerializer jsonSerializer = new DataContractJsonSerializer(objList.GetType(), typeList.ToArray());
                using (FileStream fStream = new FileStream(Directory.GetCurrentDirectory() + "\\custom.json", FileMode.Open))
                {
                    try
                    {
                        objList = jsonSerializer.ReadObject(fStream) as List<Shape>;
                    }
                    catch (Exception ex)
                    {
                        
                    }
                }
                List<Shape> customList = new List<Shape>();
                CustomFigure custom = new CustomFigure(objList);
                custom.Draw(cnv);
                //customFigList.Add(custom);
                shapesWindow.shapesBox.Items.Add(new ShapePropertyControl(custom));
                shapesWindow.shapesBox.SelectedIndex = shapesWindow.shapesBox.Items.Count - 1;
            }
        }

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFile = new SaveFileDialog
            {
                InitialDirectory = "C:\\Projects\\ООП\\CoolPaint",
                Filter = "JSON Files (*.json)|*.json|All files (*.*)|*.*",
                FilterIndex = 0,
                RestoreDirectory = true
            };

            if (saveFile.ShowDialog() == true)
            {
                List<Shape> listShape = new List<Shape>();
                foreach (ShapePropertyControl contr in shapesWindow.shapesBox.Items)
                {
                    if (contr.shape != null)
                        listShape.Add(contr.shape);
                    else
                        listShape.AddRange(contr.custom.list);
                }
                DataContractJsonSerializer jsonSerializer = new DataContractJsonSerializer(listShape.GetType(), typeList.ToArray());
                using (FileStream fStream = new FileStream(saveFile.FileName, FileMode.Create))
                {
                    jsonSerializer.WriteObject(fStream, listShape);
                }
            }
        }

        private void LoadBtn_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog
            {
                InitialDirectory = "C:\\Projects\\ООП\\CoolPaint",
                Filter = "JSON Files (*.json)|*.json|All files (*.*)|*.*",
                FilterIndex = 0,
                RestoreDirectory = true
            };

            if (openFile.ShowDialog() == true)
            {
                List<Shape> listShape = new List<Shape>();
                List<Shape> objList = new List<Shape>();
                foreach (ShapePropertyControl contr in shapesWindow.shapesBox.Items)
                {
                    listShape.Add(contr.shape);
                }
                DataContractJsonSerializer jsonSerializer = new DataContractJsonSerializer(listShape.GetType(), typeList.ToArray());
                using (FileStream fStream = new FileStream(openFile.FileName, FileMode.Open))
                {
                    try
                    {
                        objList = jsonSerializer.ReadObject(fStream) as List<Shape>;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("JSON you loaded is corrupted and raised the following exception: " + ex.Message, "Error");
                    }
                }

                foreach (Shape shape in objList)
                {
                    shape.Draw(cnv);
                    shapesWindow.shapesBox.Items.Add(new ShapePropertyControl(shape));
                    shapesWindow.shapesBox.SelectedIndex = shapesWindow.shapesBox.Items.Count - 1;
                }
            }
        }

        private void GetPlugins()
        {
            DirectoryInfo pluginDir = new DirectoryInfo(pluginPath);
            if (!pluginDir.Exists)
                pluginDir.Create();
            var pluginFiles = Directory.GetFiles(pluginPath, "*.dll");
            try
            {
                foreach (var file in pluginFiles)
                {
                    Assembly assembly = Assembly.LoadFrom(file);

                    var types = assembly.GetTypes().
                        Where(t => t.GetInterfaces().Contains(typeof(IPlugin))); // better than below
                    foreach (var type in types)
                    {
                        var label = new Label()
                        {
                            Content = type.Name
                        };
                        shapeComboBox.Items.Add(label);
                        typeList.Add(type);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Cannot load assemblies.");
            }
        }

        private void GetPluginFactories()
        {
            DirectoryInfo pluginDir = new DirectoryInfo(pluginPath);
            if (!pluginDir.Exists)
                pluginDir.Create();
            var pluginFiles = Directory.GetFiles(pluginPath, "*.dll");
            try
            {
                foreach (var file in pluginFiles)
                {
                    Assembly assembly = Assembly.LoadFrom(file);
                    var types = assembly.GetTypes().
                        Where(t => t.GetInterfaces().
                        Where(i => i.FullName == typeof(IPluginFactory).FullName).Any());
                    foreach (var type in types)
                    {
                        Factory plugin = (Factory)Activator.CreateInstance(type);
                        factoryList.Add(plugin);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Cannot load factory assembly.");
            }
        }

        private void SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            factory = factoryList[shapeComboBox.SelectedIndex];
        }

        private void SettingsButton_Click(object sender, RoutedEventArgs e)
        {
            settingsWindow.ShowDialog();
        }

        private void SaveFigBtn_Click(object sender, RoutedEventArgs e)
        {
            List<Shape> customList = new List<Shape>();
            for (int i = 0; i < shapesWindow.shapesBox.Items.Count; i++)
            {
                if ((shapesWindow.shapesBox.Items[i] as ShapePropertyControl).shape != null)
                    customList.Add((shapesWindow.shapesBox.Items[i] as ShapePropertyControl).shape);
                else
                    customList.AddRange((shapesWindow.shapesBox.Items[i] as ShapePropertyControl).custom.list);
            }

            CustomFigure custom = new CustomFigure(customList);
            cnv.Children.Clear();
            shapesWindow.shapesBox.Items.Clear();
            custom.Draw(cnv);
            customFigList.Add(custom);
            shapesWindow.shapesBox.Items.Add(new ShapePropertyControl(custom));
            shapesWindow.shapesBox.SelectedIndex = shapesWindow.shapesBox.Items.Count - 1;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            try
            {
                XmlTextWriter writer = new XmlTextWriter("config.xml", Encoding.UTF8);
                writer.WriteStartDocument();
                writer.WriteStartElement("root");
                writer.WriteElementString("isLightTheme", settingsWindow.isLight.ToString());
                writer.WriteElementString("AccentColor", Resources.MergedDictionaries[2].Source.ToString());
                writer.WriteEndElement();
                writer.Close();
            }
            catch (Exception ex)
            {
                
            }
            string cwd = Directory.GetCurrentDirectory() + "\\custom.json";
            List<Shape> listShape = new List<Shape>();
            foreach (ShapePropertyControl contr in shapesWindow.shapesBox.Items)
            {
                if (contr.custom != null)
                    listShape.AddRange(contr.custom.list);
            }
            DataContractJsonSerializer jsonSerializer = new DataContractJsonSerializer(listShape.GetType(), typeList.ToArray());
            using (FileStream fStream = new FileStream(cwd, FileMode.Create))
            {
                jsonSerializer.WriteObject(fStream, listShape);
            }
        }

        private void cnv_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            Cursor = Cursors.SizeAll;

            RightButtonDownPos = e.GetPosition(cnv);
        }

        private void cnv_MouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            Cursor = Cursors.Arrow;
        }
    }     
}
