using System;

interface IDrawable
{
    void Draw();
}

class Circl : IDrawable
{
    private double radius;

    public Circl(double radius) { this.radius = radius; }

    public void Draw()
    {
        Console.WriteLine($"Круг (радиус = {radius})");
        Console.WriteLine($"  Площадь:  {Math.PI * radius * radius:F4}");
        Console.WriteLine($"  Периметр: {2 * Math.PI * radius:F4}");
    }
}

class Rectangl : IDrawable
{
    private double width, height;

    public Rectangl(double width, double height)
    {
        this.width = width;
        this.height = height;
    }

    public void Draw()
    {
        Console.WriteLine($"Прямоугольник {width} x {height}");
        Console.WriteLine($"  Площадь:  {width * height:F4}");
        Console.WriteLine($"  Периметр: {2 * (width + height):F4}");
    }
}

class Triangle : IDrawable
{
    private double a, b, c;

    public Triangle(double a, double b, double c)
    {
        this.a = a;
        this.b = b;
        this.c = c;
    }

    public void Draw()
    {
        double p = (a + b + c) / 2;
        double area = Math.Sqrt(p * (p - a) * (p - b) * (p - c));

        Console.WriteLine($"Треугольник ({a}, {b}, {c})");
        Console.WriteLine($"  Площадь:  {area:F4}");
        Console.WriteLine($"  Периметр: {a + b + c:F4}");
    }
}

class Ex4
{
    static void Main()
    {
        IDrawable[] shapes = {
            new Circl(5),
            new Rectangl(4, 6),
            new Triangle(3, 4, 5),
            new Circl(10),
            new Rectangl(2.5, 8.5),
            new Triangle(6, 6, 6)
        };

        Console.WriteLine("========== РИСУЕМ ФИГУРЫ ==========\n");

        for (int i = 0; i < shapes.Length; i++)
        {
            Console.WriteLine($"--- Фигура №{i + 1} ---");
            shapes[i].Draw();
            Console.WriteLine();
        }
    }
}