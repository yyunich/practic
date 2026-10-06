using System;
using System.Globalization;

// Абстрактный базовый класс для всех геометрических фигур.
// Содержит абстрактный метод Area(), который обязаны реализовать наследники.
abstract class Shape
{
    // Абстрактный метод вычисления площади фигуры.
    // Не имеет реализации — каждая фигура вычисляет площадь по-своему.
    public abstract double Area();
}

// Класс «Круг» — наследник Shape.
class Circ : Shape
{
    private double r; // радиус круга

    // Конструктор: принимает радиус и сохраняет его в поле
    public Circ(double r) { this.r = r; }

    // Переопределяем метод Area(): площадь круга = π * r²
    public override double Area() { return Math.PI * r * r; }
}

// Класс «Прямоугольник» — наследник Shape.
class Rectang : Shape
{
    private double w, h; // ширина и высота

    // Конструктор: принимает ширину и высоту
    public Rectang(double w, double h) { this.w = w; this.h = h; }

    // Переопределяем метод Area(): площадь прямоугольника = ширина * высота
    public override double Area() { return w * h; }
}

// Класс «Треугольник» — наследник Shape.
class Triang : Shape
{
    private double a, b, c; // длины трёх сторон треугольника

    // Конструктор: принимает три стороны
    public Triang(double a, double b, double c) { this.a = a; this.b = b; this.c = c; }

    // Переопределяем метод Area(): формула Герона
    // p — полупериметр, площадь = √(p·(p-a)·(p-b)·(p-c))
    public override double Area()
    {
        double p = (a + b + c) / 2;
        return Math.Sqrt(p * (p - a) * (p - b) * (p - c));
    }
}

// Объявление делегата, который ссылается на метод без параметров,
// возвращающий double. Подходит под сигнатуру метода Area().
delegate double AreaDelegate();

class Ex1
{
    static void Main()
    {
        // Массив фигур разных типов — демонстрация полиморфизма.
        // Все элементы приводятся к базовому типу Shape.
        Shape[] shapes =
        {
            new Circ(10),          // круг радиусом 10
            new Rectang(5, 8),     // прямоугольник 5×8
            new Triang(5, 5, 8)    // треугольник со сторонами 5, 5, 8
        };

        // Массив имён фигур для вывода (соответствует порядку в shapes)
        string[] names = { "Круг", "Прямоугольник", "Треугольник" };

        // Массив делегатов: каждый элемент будет ссылаться на метод Area()
        // соответствующего объекта из массива shapes.
        AreaDelegate[] calculators = new AreaDelegate[shapes.Length];

        // Создание делегатов через групповую привязку методов (method group).
        // shapes[i].Area — это ссылка на метод Area() конкретного объекта,
        // то есть делегат «запоминает» нужный экземпляр.
        for (int i = 0; i < shapes.Length; i++)
        {
            calculators[i] = shapes[i].Area;
        }

        // Вызов методов через делегаты и вывод площади с двумя знаками после запятой.
        // Запись calculators[i]() означает: вызвать делегат и получить результат.
        for (int i = 0; i < calculators.Length; i++)
        {
            Console.WriteLine($"{names[i]}: площадь = {calculators[i]():F2}");
        }
    }
}