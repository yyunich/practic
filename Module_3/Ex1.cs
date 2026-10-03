using System;
using System.Globalization;

abstract class Shape
{
    public abstract double Area();
}

class Circ : Shape
{
    private double r;
    public Circ(double r) {  this.r = r; }
    public override double Area() { return Math.PI * r * r; }
}

class Rectang : Shape
{
    private double w, h;
    public Rectang(double w, double h) { this.w = w; this.h = h; }
    public override double Area() { return w * h; }
}

class Triang : Shape
{
    private double a, b, c;
    public Triang(double a, double b, double c) { this.a = a; this.b = b; this.c = c; }
    public override double Area() { double p = (a + b + c) / 2; return Math.Sqrt(p * (p - a) * (p - b) * (p - c)); }
}

delegate double AreaDelegate();

class Ex1
{
    static void Main()
    {
        Shape[] shapes =
        {
            new Circ(10),
            new Rectang(5, 8),
            new Triang(5, 5, 8)
        };

        string[] names = { "Круг", "Прямоугольник", "Треугольник" };

        AreaDelegate[] calculators = new AreaDelegate[shapes.Length];
        for (int i = 0; i < shapes.Length; i++)
        {
            calculators[i] = shapes[i].Area;
        }

        for (int i = 0; i < calculators.Length; i++)
        {
            Console.WriteLine($"{names[i]}: площадь = {calculators[i]():F2}");
        }

    } 
}