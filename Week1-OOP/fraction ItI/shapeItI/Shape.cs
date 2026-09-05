using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace shapeItI
{
    public abstract class Shape
    {
        public int Id { get; }

        public ShapeColor Color { get; private set; }

        public Point Position { get; set; }

        public static int Count { get; private set; }

        protected Shape(ShapeColor color, Point position)
        {
            Count++;

            Id = Count;

            Color = color;

            Position = position;
        }

        public abstract double CalculateArea();

        public abstract double CalculatePerimeter();

        public virtual void Display()
        {
            Console.WriteLine(
                $"S{Id} {GetType().Name} {Color} at ({Position.X},{Position.Y}) {CalculateArea():F2}"
            );
        }
    }
    public class Circle : Shape
    {
        public double Radius { get; set; }

        public Circle(
            double radius,
            ShapeColor color,
            Point position)
            : base(color, position)
        {
            Radius = radius;
        }

        public override double CalculateArea()
        {
            return Math.PI * Radius * Radius;
        }

        public override double CalculatePerimeter()
        {
            return 2 * Math.PI * Radius;
        }
    }
    
    public class Triangle : Shape
    {
        public double BaseLength { get; set; }

        public double Height { get; set; }

        public double Side1 { get; set; }

        public double Side2 { get; set; }

        public double Side3 { get; set; }

        public Triangle(
            double baseLength,
            double height,
            double side1,
            double side2,
            double side3,
            ShapeColor color,
            Point position)
            : base(color, position)
        {
            BaseLength = baseLength;
            Height = height;

            Side1 = side1;
            Side2 = side2;
            Side3 = side3;
        }

        public override double CalculateArea()
        {
            return 0.5 * BaseLength * Height;
        }

        public override double CalculatePerimeter()
        {
            return Side1 + Side2 + Side3;
        }
    }

    public class Rectangle : Shape
    {
        public double Width { get; set; }

        public double Height { get; set; }

        public Rectangle(
            double width,
            double height,
            ShapeColor color,
            Point position)
            : base(color, position)
        {
            Width = width;
            Height = height;
        }

        public override double CalculateArea()
        {
            return Width * Height;
        }

        public override double CalculatePerimeter()
        {
            return 2 * (Width + Height);
        }
    }
    public class Square : Rectangle
    {
        public double Side { get; set; }

        public Square(
            double side,
            ShapeColor color,
            Point position)
            : base(side, side, color, position)
        {
            Side = side;
        }
    }
}
