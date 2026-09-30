using System;

class Shape
{
    // Виртуальные методы — будут переопределены в наследниках
    public virtual double Area()
    {
        return 0;
    }

    public virtual double Perimeter()
    {
        return 0;
    }

    // Виртуальный метод для вывода информации о фигуре
    public virtual void PrintInfo()
    {
        Console.WriteLine($"Площадь:   {Area():F4}");
        Console.WriteLine($"Периметр:  {Perimeter():F4}");
    }
}

class Circle : Shape
{
    private double radius;

    // Конструктор
    public Circle(double radius)
    {
        this.radius = radius;
    }

    public void SetRadius(double radius)
    {
        this.radius = radius;
    }

    public double GetRadius()
    {
        return radius;
    }

    // Переопределяем Area() для круга: S = π·r²
    public override double Area()
    {
        return Math.PI * radius * radius;
    }

    // Переопределяем Perimeter() для круга: P = 2·π·r
    public override double Perimeter()
    {
        return 2 * Math.PI * radius;
    }

    public override void PrintInfo()
    {
        Console.WriteLine($"--- Круг (радиус = {radius}) ---");
        Console.WriteLine($"Площадь:   {Area():F4}");
        Console.WriteLine($"Периметр:  {Perimeter():F4}");
    }
}

class Rectangle : Shape
{
    private double width;
    private double height;

    // Конструктор
    public Rectangle(double width, double height)
    {
        this.width = width;
        this.height = height;
    }

    public void SetWidth(double width)
    {
        this.width = width;
    }

    public double GetWidth()
    {
        return width;
    }

    public void SetHeight(double height)
    {
        this.height = height;
    }

    public double GetHeight()
    {
        return height;
    }

    // Переопределяем Area() для прямоугольника: S = a·b
    public override double Area()
    {
        return width * height;
    }

    // Переопределяем Perimeter() для прямоугольника: P = 2·(a + b)
    public override double Perimeter()
    {
        return 2 * (width + height);
    }

    public override void PrintInfo()
    {
        Console.WriteLine($"--- Прямоугольник ({width} x {height}) ---");
        Console.WriteLine($"Площадь:   {Area():F4}");
        Console.WriteLine($"Периметр:  {Perimeter():F4}");
    }
}

class Ex2
{
    static void Main()
    {
        // Создаём объекты
        Circle circle = new Circle(5);
        Rectangle rect = new Rectangle(4, 6);

        // Выводим информацию
        Console.WriteLine("========== ФИГУРЫ ==========\n");

        circle.PrintInfo();
        Console.WriteLine();
        rect.PrintInfo();

        Console.WriteLine("\n========== ЧЕРЕЗ МАССИВ SHAPE ==========\n");

        // Полиморфизм: массив базового типа, содержащий разные фигуры
        Shape[] shapes = {
            new Circle(3),
            new Rectangle(5, 8),
            new Circle(10),
            new Rectangle(2.5, 4.5)
        };

        for (int i = 0; i < shapes.Length; i++)
        {
            Console.WriteLine($"Фигура №{i + 1}:");
            Console.WriteLine($"  Площадь:  {shapes[i].Area():F4}");
            Console.WriteLine($"  Периметр: {shapes[i].Perimeter():F4}");
            Console.WriteLine();
        }
    }
}