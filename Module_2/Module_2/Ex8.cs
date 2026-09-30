using System;

// Базовый класс
abstract class Shap
{
    public abstract double Area();
    public abstract double Perimeter();

    public void PrintInfo(string name)
    {
        Console.WriteLine($"{name}: S = {Area():F2}, P = {Perimeter():F2}");
    }
}

// Круг
class Circ : Shap
{
    private double r;
    public Circ(double r) { this.r = r; }

    public override double Area() { return Math.PI * r * r; }
    public override double Perimeter() { return 2 * Math.PI * r; }
}

// Прямоугольник
class Rectang : Shap
{
    private double w, h;
    public Rectang(double w, double h) { this.w = w; this.h = h; }

    public override double Area() { return w * h; }
    public override double Perimeter() { return 2 * (w + h); }
}

// Треугольник (по трём сторонам)
class Triang : Shap
{
    private double a, b, c;
    public Triang(double a, double b, double c)
    {
        this.a = a; this.b = b; this.c = c;
    }

    public override double Area()
    {
        double p = (a + b + c) / 2;
        return Math.Sqrt(p * (p - a) * (p - b) * (p - c));
    }

    public override double Perimeter() { return a + b + c; }
}

class Ex8
{
    static void Main()
    {
        Shap[] shapes = {
            new Circ(5),
            new Rectang(4, 6),
            new Triang(3, 4, 5)
        };

        string[] names = { "Круг", "Прямоугольник", "Треугольник" };

        for (int i = 0; i < shapes.Length; i++)
        {
            shapes[i].PrintInfo(names[i]);
        }
    }
}