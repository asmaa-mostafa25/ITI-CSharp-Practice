
using shapeItI;
using System;
class Program
{
    static void Main(string[] args)
    {

        List<Shape> shapes = new List<Shape>();

        shapes.Add(
            new Circle(
                3,
                ShapeColor.Red,
                new Point(10, 0)
            )
        );

        shapes.Add(
            new Rectangle(
                5,
                4,
                ShapeColor.Blue,
                new Point(12, 3)
            )
        );

        shapes.Add(
            new Triangle(
                3,
                4,
                3,
                4,
                5,
                ShapeColor.Green,
                new Point(15, 3)
            )
        );

        shapes.Add(
            new Square(
                4,
                ShapeColor.Black,
                new Point(1, 1)
            )
        );

        Console.WriteLine("--- All shapes ---");

        foreach (Shape shape in shapes)
        {
            shape.Display();
        }

        Console.WriteLine();

        Console.WriteLine($"Shape.Count => {Shape.Count}");
    }
}