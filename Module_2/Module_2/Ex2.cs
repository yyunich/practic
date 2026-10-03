using System; // Подключение пространства имён System (Console, Math)

// ==================== БАЗОВЫЙ КЛАСС ====================
// Базовый (родительский) класс Shape — общее описание любой фигуры
class Shape
{
    // Виртуальные методы — будут переопределены в наследниках
    // Ключевое слово virtual позволяет наследникам заменить реализацию (override)
    public virtual double Area()
    {
        return 0; // Базовая реализация: площадь неизвестной фигуры = 0
    }

    public virtual double Perimeter()
    {
        return 0; // Базовая реализация: периметр неизвестной фигуры = 0
    }

    // Виртуальный метод для вывода информации о фигуре.
    // Если наследник не переопределит его, будет использоваться эта версия.
    public virtual void PrintInfo()
    {
        // :F4 — формат вывода числа с 4 знаками после запятой
        Console.WriteLine($"Площадь:   {Area():F4}");
        Console.WriteLine($"Периметр:  {Perimeter():F4}");
    }
}

// ==================== НАСЛЕДНИК: КРУГ ====================
// Класс Circle наследует класс Shape (отношение "является фигурой")
class Circle : Shape
{
    private double radius; // Приватное поле — радиус круга

    // Конструктор: вызывается при создании объекта new Circle(...)
    public Circle(double radius)
    {
        this.radius = radius; // this.radius — поле, radius — параметр
    }

    // Сеттер для изменения радиуса после создания объекта
    public void SetRadius(double radius)
    {
        this.radius = radius;
    }

    // Геттер для получения значения радиуса
    public double GetRadius()
    {
        return radius;
    }

    // Переопределяем Area() для круга: S = π·r²
    // override — заменяем виртуальный метод базового класса
    public override double Area()
    {
        return Math.PI * radius * radius; // Math.PI — константа π из System
    }

    // Переопределяем Perimeter() для круга: P = 2·π·r
    // (обычно называется длиной окружности)
    public override double Perimeter()
    {
        return 2 * Math.PI * radius;
    }

    // Переопределяем PrintInfo() для вывода специфичной информации о круге
    public override void PrintInfo()
    {
        Console.WriteLine($"--- Круг (радиус = {radius}) ---");
        Console.WriteLine($"Площадь:   {Area():F4}");
        Console.WriteLine($"Периметр:  {Perimeter():F4}");
    }
}

// ==================== НАСЛЕДНИК: ПРЯМОУГОЛЬНИК ====================
class Rectangle : Shape
{
    private double width;  // Ширина прямоугольника
    private double height; // Высота прямоугольника

    // Конструктор с двумя параметрами
    public Rectangle(double width, double height)
    {
        this.width = width;
        this.height = height;
    }

    // Сеттер ширины
    public void SetWidth(double width)
    {
        this.width = width;
    }

    // Геттер ширины
    public double GetWidth()
    {
        return width;
    }

    // Сеттер высоты
    public void SetHeight(double height)
    {
        this.height = height;
    }

    // Геттер высоты
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

    // Переопределяем PrintInfo() для прямоугольника
    public override void PrintInfo()
    {
        Console.WriteLine($"--- Прямоугольник ({width} x {height}) ---");
        Console.WriteLine($"Площадь:   {Area():F4}");
        Console.WriteLine($"Периметр:  {Perimeter():F4}");
    }
}

// ==================== ТОЧКА ВХОДА ====================
class Ex2
{
    static void Main()
    {
        // Создаём объекты конкретных фигур
        Circle circle = new Circle(5);         // круг с радиусом 5
        Rectangle rect = new Rectangle(4, 6);  // прямоугольник 4×6

        Console.WriteLine("========== ФИГУРЫ ==========\n");

        // Вызываются переопределённые методы PrintInfo() соответствующих классов
        circle.PrintInfo();
        Console.WriteLine();
        rect.PrintInfo();

        Console.WriteLine("\n========== ЧЕРЕЗ МАССИВ SHAPE ==========\n");

        // Полиморфизм: массив базового типа Shape,
        // но элементы — объекты разных наследников (Circle, Rectangle).
        // При вызове shapes[i].Area() во время выполнения выбирается
        // нужная реализация в зависимости от фактического типа объекта.
        Shape[] shapes = {
            new Circle(3),           // элемент 0
            new Rectangle(5, 8),     // элемент 1
            new Circle(10),          // элемент 2
            new Rectangle(2.5, 4.5)  // элемент 3
        };

        // Перебор всех фигур в массиве
        for (int i = 0; i < shapes.Length; i++)
        {
            Console.WriteLine($"Фигура №{i + 1}:");
            // Вызов Area()/Perimeter() — работает полиморфно:
            // каждый объект использует свою формулу
            Console.WriteLine($"  Площадь:  {shapes[i].Area():F4}");
            Console.WriteLine($"  Периметр: {shapes[i].Perimeter():F4}");
            Console.WriteLine();
        }
    }
}