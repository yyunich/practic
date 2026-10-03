using System; // Подключение пространства имён System (Console, Math)

// ==================== АБСТРАКТНЫЙ БАЗОВЫЙ КЛАСС ====================
// abstract означает, что нельзя создать объект new Shap() —
// класс существует только как "шаблон" для наследников.
abstract class Shap
{
    // Абстрактные методы — БЕЗ тела. Наследники ОБЯЗАНЫ их реализовать.
    // Это отличается от virtual: virtual даёт реализацию по умолчанию,
    // а abstract её не даёт вообще — "реализуй сам, я не знаю формулы".
    public abstract double Area();
    public abstract double Perimeter();

    // Обычный (не абстрактный) метод — есть готовая реализация.
    // Наследники могут им пользоваться как есть.
    // Внутри вызываются абстрактные Area() и Perimeter() —
    // благодаря полиморфизму будет выбрана версия конкретного наследника.
    public void PrintInfo(string name)
    {
        // :F2 — формат с 2 знаками после запятой
        Console.WriteLine($"{name}: S = {Area():F2}, P = {Perimeter():F2}");
    }
}

// ==================== НАСЛЕДНИК: КРУГ ====================
// Circ наследует Shap — обязан реализовать оба абстрактных метода
class Circ : Shap
{
    private double r; // Радиус

    public Circ(double r) { this.r = r; }

    // Реализация абстрактного метода Area() из Shap
    public override double Area() { return Math.PI * r * r; }        // S = π·r²

    // Реализация абстрактного метода Perimeter() из Shap
    public override double Perimeter() { return 2 * Math.PI * r; }   // P = 2·π·r
}

// ==================== НАСЛЕДНИК: ПРЯМОУГОЛЬНИК ====================
class Rectang : Shap
{
    private double w, h; // Ширина и высота

    public Rectang(double w, double h) { this.w = w; this.h = h; }

    public override double Area() { return w * h; }              // S = a·b
    public override double Perimeter() { return 2 * (w + h); }   // P = 2·(a+b)
}

// ==================== НАСЛЕДНИК: ТРЕУГОЛЬНИК ====================
class Triang : Shap
{
    private double a, b, c; // Три стороны

    public Triang(double a, double b, double c)
    {
        this.a = a; this.b = b; this.c = c;
    }

    public override double Area()
    {
        // Формула Герона: S = √(p·(p-a)·(p-b)·(p-c))
        double p = (a + b + c) / 2; // Полупериметр
        return Math.Sqrt(p * (p - a) * (p - b) * (p - c));
    }

    public override double Perimeter() { return a + b + c; } // Сумма сторон
}

// ==================== ТОЧКА ВХОДА ====================
class Ex8
{
    static void Main()
    {
        // Массив базового типа Shap, хранящий разные фигуры.
        // Полиморфизм: у каждой фигуры свои Area() и Perimeter().
        Shap[] shapes = {
            new Circ(5),          // элемент 0
            new Rectang(4, 6),    // элемент 1
            new Triang(3, 4, 5)   // элемент 2
        };

        // Параллельный массив имён.
        // Нужен, потому что класс Shap не знает своего "человеческого" названия —
        // он оперирует только математическими формулами.
        string[] names = { "Круг", "Прямоугольник", "Треугольник" };

        // Перебираем фигуры. Обратите внимание на связь двух массивов по индексу i:
        // shapes[i] и names[i] относятся к одной и той же фигуре.
        for (int i = 0; i < shapes.Length; i++)
        {
            // Вызов метода PrintInfo() базового класса.
            // Внутри него вызовутся Area() и Perimeter() того класса,
            // к которому принадлежит объект (Circ / Rectang / Triang).
            shapes[i].PrintInfo(names[i]);
        }
    }
}