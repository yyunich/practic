using System;

// Интерфейс "Товар" — что должен уметь любой товар
interface IProduct
{
    double GetCost();   // стоимость партии
    int GetStock();     // остаток на складе
}

// Хлеб: цена × количество
class Bread : IProduct
{
    public double Price; public int Stock;
    public double GetCost() => Price * Stock;
    public int GetStock() => Stock;
}

// Молоко: цена × объём × количество
class Milk : IProduct
{
    public double Price, Volume; public int Stock;
    public double GetCost() => Price * Volume * Stock;
    public int GetStock() => Stock;
}

// Сыр: цена × вес × количество
class Cheese : IProduct
{
    public double Price, Weight; public int Stock;
    public double GetCost() => Price * Weight * Stock;
    public int GetStock() => Stock;
}

class Ex2
{
    static void Main()
    {
        // Массив товаров разных типов через интерфейс
        IProduct[] products = {
            new Bread  { Price = 45.50, Stock = 20 },
            new Milk   { Price = 89.90, Volume = 1.0, Stock = 15 },
            new Cheese { Price = 650.0, Weight = 0.3, Stock = 8 }
        };

        string[] names = { "Хлеб", "Молоко", "Сыр" };

        double total = 0;
        int totalStock = 0;

        for (int i = 0; i < products.Length; i++)
        {
            Console.WriteLine($"{names[i]}: {products[i].GetCost():F2} руб., остаток {products[i].GetStock()} шт.");
            total += products[i].GetCost();
            totalStock += products[i].GetStock();
        }

        Console.WriteLine($"\nИТОГО: {total:F2} руб., {totalStock} шт.");
    }
}