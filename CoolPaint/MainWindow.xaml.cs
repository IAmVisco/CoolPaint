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
        private readonly string pluginPath = System.IO.Path.Combine(Directory.GetCurrentDirectory(), "Plugins");

        private ShapesWindow shapesWindow;
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
        private List<Factory> factoryList = new List<Factory>
        {
            new RectangleFactory(),
            new SquareFactory(),
            new EllipseFactory(),
            new CircleFactory(),
            new TriangleFactory(),
            new HexagonFactory()
        };

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
                    listShape.Add(contr.shape);                
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
            foreach (var file in pluginFiles)
            {
                Assembly assembly = Assembly.LoadFrom(file);
                var types = assembly.GetTypes().
                    Where(t => t.GetInterfaces().
                    Where(i => i.FullName == typeof(IPlugin).FullName).Any());
                foreach(var type in types)
                {
                    shapeComboBox.Items.Add(type.Name);
                    typeList.Add(type);
                }
            }
        }

        private void GetPluginFactories()
        {
            DirectoryInfo pluginDir = new DirectoryInfo(pluginPath);
            if (!pluginDir.Exists)
                pluginDir.Create();
            var pluginFiles = Directory.GetFiles(pluginPath, "*.dll");
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

        private void SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            factory = factoryList[shapeComboBox.SelectedIndex];
        }
    }     
}
