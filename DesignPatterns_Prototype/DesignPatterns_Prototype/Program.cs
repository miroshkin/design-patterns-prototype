using System;
using System.IO;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace DesignPatterns_Prototype
{
    class Program
    {
        //original source https://metanit.com/sharp/patterns/2.4.php
        static void Main(string[] args)
        {
            Console.WriteLine("Design Patterns - Prototype!");
            
            Circle figure = new Circle(30, 50, 60);
            Circle clonedFigure=figure.DeepCopy() as Circle;
            figure.Point.X = 100;
            figure.GetInfo();
            clonedFigure.GetInfo();
 
            Console.Read();

        }
    }

    interface IFigure
    {
        IFigure Clone();
        void GetInfo();
    }

    [Serializable]
    class Point
    {
        public int X { get; set; }
        public int Y { get; set; }
    }

    [Serializable]
    class Circle : IFigure
    {
        int radius;
        public Point Point { get; set; }
        public Circle(int r, int x, int y)
        {
            radius = r;
            this.Point = new Point { X = x, Y = y };
        }
 
        public IFigure Clone()
        {
            return this.MemberwiseClone() as IFigure;
        }
 
        public object DeepCopy()
        {
            object figure = null;
            using (MemoryStream tempStream = new MemoryStream())
            {
                BinaryFormatter binaryFormatter = new BinaryFormatter(null,
                    new StreamingContext(StreamingContextStates.Clone));
 
                binaryFormatter.Serialize(tempStream, this);
                tempStream.Seek(0, SeekOrigin.Begin);
 
                figure = binaryFormatter.Deserialize(tempStream);
            }
            return figure;
        }
        public void GetInfo()
        {
            Console.WriteLine("Circle with radius = {0} and center in ({1}, {2})", radius, Point.X, Point.Y);
        }
    }
}
