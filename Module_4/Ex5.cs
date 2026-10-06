using System;

// Интерфейс "Рисунок"
interface IDrawing
{
    void DrawLine(int x1, int y1, int x2, int y2);   // линия
    void DrawCircle(int x, int y, int r);            // круг
    void DrawRectangle(int x, int y, int w, int h);  // прямоугольник
}

// Класс "Холст" — реализует интерфейс
class Canvas : IDrawing
{
    public void DrawLine(int x1, int y1, int x2, int y2)
    {
        Console.WriteLine($"  Линия: ({x1},{y1}) → ({x2},{y2})");
    }

    public void DrawCircle(int x, int y, int r)
    {
        Console.WriteLine($"  Круг: центр ({x},{y}), радиус {r}");
    }

    public void DrawRectangle(int x, int y, int w, int h)
    {
        Console.WriteLine($"  Прямоугольник: ({x},{y}), {w}×{h}");
    }
}

class Ex5
{
    static void Main()
    {
        // Создаём холст через интерфейс
        IDrawing canvas = new Canvas();

        Console.WriteLine("=== РИСУЕМ НА ХОЛСТЕ ===\n");

        Console.WriteLine("Рисуем домик:");
        canvas.DrawRectangle(100, 200, 80, 60);   // стены
        canvas.DrawLine(100, 200, 140, 150);      // крыша (левая)
        canvas.DrawLine(140, 150, 180, 200);      // крыша (правая)
        canvas.DrawRectangle(125, 230, 20, 30);   // дверь

        Console.WriteLine("\nРисуем солнце:");
        canvas.DrawCircle(300, 100, 40);

        Console.WriteLine("\nРисуем забор:");
        for (int i = 0; i < 5; i++)
        {
            canvas.DrawLine(200 + i * 20, 260, 200 + i * 20, 240);
        }
    }
}