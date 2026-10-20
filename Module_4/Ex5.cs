using System;

interface IDrawing
{
    void DrawLine(int x1, int y1, int x2, int y2);
    void DrawCircle(int x, int y, int r);
    void DrawRectangle(int x, int y, int w, int h);
}

class Canvas : IDrawing
{
    public void DrawLine(int x1, int y1, int x2, int y2)
    {
        Console.WriteLine($"  Линия: ({x1},{y1}) -->> ({x2},{y2})");
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
        IDrawing canvas = new Canvas();

        Console.WriteLine("=== РИСУЕМ НА ХОЛСТЕ ===\n");

        Console.WriteLine("Рисуем домик:");
        canvas.DrawRectangle(100, 200, 80, 60);
        canvas.DrawLine(100, 200, 140, 150);
        canvas.DrawLine(140, 150, 180, 200);
        canvas.DrawRectangle(125, 230, 20, 30);

        Console.WriteLine("\nРисуем солнце:");
        canvas.DrawCircle(300, 100, 40);

        Console.WriteLine("\nРисуем забор:");
        for (int i = 0; i < 5; i++)
        {
            canvas.DrawLine(200 + i * 20, 260, 200 + i * 20, 240);
        }
    }
}