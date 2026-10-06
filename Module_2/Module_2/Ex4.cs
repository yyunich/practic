using System;

// Интерфейс IDrawable описывает "контракт":
// любой класс, реализующий этот интерфейс, ОБЯЗАН иметь метод Draw().
// Интерфейс не содержит реализации — только сигнатуру метода.
interface IDrawable
{
    void Draw(); // Метод без тела — реализация будет в классах-наследниках
}

// Класс Circl реализует интерфейс IDrawable
// (обратите внимание: через двоеточие после имени класса — как при наследовании)
class Circl : IDrawable
{
    private double radius; // Приватное поле — радиус

    // Конструктор: принимает радиус
    public Circl(double radius) { this.radius = radius; }

    // Реализация метода Draw() из интерфейса IDrawable.
    // Без этого метода компилятор выдаст ошибку — интерфейс требует его наличия.
    public void Draw()
    {
        Console.WriteLine($"Круг (радиус = {radius})");
        // :F4 — формат вывода с 4 знаками после запятой
        Console.WriteLine($"  Площадь:  {Math.PI * radius * radius:F4}");
        Console.WriteLine($"  Периметр: {2 * Math.PI * radius:F4}");
    }
}

class Rectangl : IDrawable
{
    private double width, height; // Два поля в одной строке

    public Rectangl(double width, double height)
    {
        this.width = width;
        this.height = height;
    }

    // Реализация метода Draw() для прямоугольника
    public void Draw()
    {
        Console.WriteLine($"Прямоугольник {width} x {height}");
        Console.WriteLine($"  Площадь:  {width * height:F4}");
        Console.WriteLine($"  Периметр: {2 * (width + height):F4}");
    }
}

class Triangle : IDrawable
{
    private double a, b, c; // Три стороны треугольника

    public Triangle(double a, double b, double c)
    {
        this.a = a;
        this.b = b;
        this.c = c;
    }

    // Реализация метода Draw() для треугольника
    public void Draw()
    {
        // Полупериметр (нужен для формулы Герона)
        double p = (a + b + c) / 2;

        // Формула Герона: S = √(p·(p-a)·(p-b)·(p-c))
        // Math.Sqrt — извлечение квадратного корня
        double area = Math.Sqrt(p * (p - a) * (p - b) * (p - c));

        Console.WriteLine($"Треугольник ({a}, {b}, {c})");
        Console.WriteLine($"  Площадь:  {area:F4}");
        Console.WriteLine($"  Периметр: {a + b + c:F4}"); // Периметр = сумма сторон
    }
}

class Ex4
{
    static void Main()
    {
        // Полиморфизм через интерфейс:
        // массив типа IDrawable, содержащий объекты разных классов.
        // Все они реализуют IDrawable, поэтому их можно хранить вместе.
        IDrawable[] shapes = {
            new Circl(5),              // элемент 0
            new Rectangl(4, 6),        // элемент 1
            new Triangle(3, 4, 5),     // элемент 2
            new Circl(10),             // элемент 3
            new Rectangl(2.5, 8.5),    // элемент 4
            new Triangle(6, 6, 6)      // элемент 5
        };

        Console.WriteLine("========== РИСУЕМ ФИГУРЫ ==========\n");

        // Перебор всех фигур в массиве
        for (int i = 0; i < shapes.Length; i++)
        {
            Console.WriteLine($"--- Фигура №{i + 1} ---");
            // Полиморфный вызов: во время выполнения выбирается
            // реализация Draw() того класса, к которому принадлежит объект
            shapes[i].Draw();
            Console.WriteLine();
        }
    }
}