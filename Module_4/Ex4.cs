using System;

interface IBook
{
    bool IsAvailable();
    void Borrow();
}

class Fiction : IBook
{
    public string Title;
    public bool Available = true;

    public bool IsAvailable() => Available;
    public void Borrow() { Available = false; }
}

class Textbook : IBook
{
    public string Title;
    public bool Available = true;

    public bool IsAvailable() => Available;
    public void Borrow() { Available = false; }
}

class Science : IBook
{
    public string Title;
    public bool Available = true;

    public bool IsAvailable() => Available;
    public void Borrow() { Available = false; }
}

class Ex4
{
    static void Main()
    {
        IBook[] books = {
            new Fiction   { Title = "Война и мир" },
            new Textbook  { Title = "Математический анализ" },
            new Science   { Title = "Краткая история времени" },
            new Fiction   { Title = "Мастер и Маргарита" },
            new Textbook  { Title = "Физика" }
        };

        string[] titles = {
            "Война и мир", "Математический анализ",
            "Краткая история времени", "Мастер и Маргарита", "Физика"
        };

        Console.WriteLine("=== КАТАЛОГ БИБЛИОТЕКИ ===\n");
        for (int i = 0; i < books.Length; i++)
        {
            string status = books[i].IsAvailable() ? "доступна" : "выдана";
            Console.WriteLine($"{titles[i]}: {status}");
        }

        Console.WriteLine("\n=== ВЫДАЧА КНИГ ===");
        books[0].Borrow();
        books[3].Borrow();
        Console.WriteLine($"Выдано: {titles[0]}");
        Console.WriteLine($"Выдано: {titles[3]}");

        Console.WriteLine("\n=== КАТАЛОГ ПОСЛЕ ВЫДАЧИ ===\n");
        for (int i = 0; i < books.Length; i++)
        {
            string status = books[i].IsAvailable() ? "доступна" : "выдана";
            Console.WriteLine($"{titles[i]}: {status}");
        }
    }
}