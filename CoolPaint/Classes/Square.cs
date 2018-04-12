using System;
using System.Windows;
using System.Windows.Media;
using System.Runtime.Serialization;

namespace CoolPaint
{
    [Serializable]
    public class Square : Rectangle
    {
        public override Point P2
        {
            get
            {
                return base.P2;
            }
            set
            {
                base.P2 = SetEqualSides(value);
            }
        }

        public Square(Color color, Point p1, Point p2) : base(color, p1, p2)
        {

        }

        protected Square(SerializationInfo info, StreamingContext context) : base(info, context)
        {

        }

        public Point SetEqualSides(Point v2)
        {
            double height, width;

            if (!revX)
            {
                width = v2.X - P1.X;

                if (width <= 0)
                {
                    return v2;
                }

                if (!revY)
                {
                    height = v2.Y - P1.Y;

                    if (height <= 0)
                    {
                        return v2;
                    }

                    if (height < width)
                    {
                        v2.X = P1.X + height;
                    }
                    else
                    {
                        v2.Y = P1.Y + width;
                    }
                }
                else
                {
                    height = P2.Y - v2.Y;

                    if (height <= 0)
                    {
                        return v2;
                    }

                    if (height < width)
                    {
                        v2.X = P1.X + height;
                    }
                    else
                    {
                        v2.Y = P2.Y - width;
                    }
                }
            }
            else
            {
                width = P2.X - v2.X;

                if (width <= 0)
                {
                    return v2;
                }

                if (!revY)
                {
                    height = v2.Y - P1.Y;

                    if (height <= 0)
                    {
                        return v2;
                    }

                    if (height < width)
                    {
                        v2.X = P2.X - height;
                    }
                    else
                    {
                        v2.Y = P1.Y + width;
                    }
                }
                else
                {
                    height = P2.Y - v2.Y;

                    if (height <= 0)
                    {
                        return v2;
                    }

                    if (height < width)
                    {
                        v2.X = P2.X - height;
                    }
                    else
                    {
                        v2.Y = P2.Y - width;
                    }
                }
            }
            return v2;
        }
    }
}
