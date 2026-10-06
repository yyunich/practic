using System;

// Интерфейс
interface IShape
{
    double Area();
    double Perimeter();
}

class Circle : IShape
{
    private double r;
    public Circle(double r) { this.r = r; }

    public double Area() { return Math.PI * r * r; }
    public double Perimeter() { return 2 * Math.PI * r; }
}

class Rectangle : IShape
{
    private double w, h;
    public Rectangle(double w, double h) { this.w = w; this.h = h; }

    public double Area() { return w * h; }
    public double Perimeter() { return 2 * (w + h); }
}

class Triangle : IShape
{
    private double a, b, c;
    public Triangle(double a, double b, double c)
    {
        this.a = a; this.b = b; this.c = c;
    }

    public double Area()
    {
        double p = (a + b + c) / 2;
        return Math.Sqrt(p * (p - a) * (p - b) * (p - c));
    }

    public double Perimeter() { return a + b + c; }
}

class Ex1
{
    static void Main()
    {
        IShape[] shapes = {
            new Circle(5),
            new Rectangle(4, 6),
            new Triangle(3, 4, 5)
        };

        string[] names = { "Круг", "Прямоугольник", "Треугольник" };

        for (int i = 0; i < shapes.Length; i++)
        {
            Console.WriteLine($"--- {names[i]} ---");
            Console.WriteLine($"  Площадь:  {shapes[i].Area():F2}");
            Console.WriteLine($"  Периметр: {shapes[i].Perimeter():F2}");
            Console.WriteLine();
        }
    }
}