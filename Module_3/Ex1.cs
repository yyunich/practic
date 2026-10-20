using System;
using System.Globalization;

abstract class Shape
{
    public abstract double Area();
}

class Circ : Shape
{
    private double r;

    // Конструктор: принимает радиус и сохраняет его в поле
    public Circ(double r) { this.r = r; }

    // Переопределяем метод Area(): площадь круга = π * r²
    public override double Area() { return Math.PI * r * r; }
}

class Rectang : Shape
{
    private double w, h;

    // Конструктор: принимает ширину и высоту
    public Rectang(double w, double h) { this.w = w; this.h = h; }

    // Переопределяем метод Area(): площадь прямоугольника = ширина * высота
    public override double Area() { return w * h; }
}

class Triang : Shape
{
    private double a, b, c;

    // Конструктор: принимает три стороны
    public Triang(double a, double b, double c) { this.a = a; this.b = b; this.c = c; }

    // Переопределяем метод Area(): формула Герона
    public override double Area()
    {
        double p = (a + b + c) / 2;
        return Math.Sqrt(p * (p - a) * (p - b) * (p - c));
    }
}

// Объявление делегата, который ссылается на метод без параметров, возвращающий double
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

        // Массив делегатов: каждый элемент будет ссылаться на метод Area(), соответствующего объекта из массива shapes.
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