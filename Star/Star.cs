using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Runtime.Serialization;

namespace CoolPaint
{
    [Serializable]
    class Star : Polygon, IPlugin
    {
        public Star(Color color, Point vertex1, Point vertex2) : base(color, vertex1, vertex2)
        {

        }

        protected Star(SerializationInfo info, StreamingContext context) : base(info, context)
        {

        }

        protected override Point[] GeneratePolygon()
        {
            Point[] star = new Point[]
            {
                new Point(Width / 2, 0),
                new Point(0.6 * Width, 0.365 * Height),
                new Point(Width, 0.365 * Height),
                new Point(0.6875 * Width, 0.6 * Height),
                new Point(0.8125 * Width, Height),
                new Point(Width / 2, 0.757 * Height),
                new Point(0.1875 * Width, Height),
                new Point(0.3125 * Width, 0.6 * Height),
                new Point(0, 0.365 * Height),
                new Point(0.3875 * Width, 0.365 * Height),
            };

            return star;
        }
    }

    class StarFactory : Factory, IPluginFactory
    {
        public override Shape FactoryMethod(Color color, Point p1, Point p2)
        {
            return new Star(color, p1, p2);
        }
    }
}
