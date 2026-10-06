using System;

// Интерфейс "Книга"
interface IBook
{
    bool IsAvailable();   // доступна ли книга
    void Borrow();        // выдать книгу
}

// Художественная книга
class Fiction : IBook
{
    public string Title;
    public bool Available = true;

    public bool IsAvailable() => Available;
    public void Borrow() { Available = false; }
}

// Учебник
class Textbook : IBook
{
    public string Title;
    public bool Available = true;

    public bool IsAvailable() => Available;
    public void Borrow() { Available = false; }
}

// Научная литература
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

        // Выводим состояние всех книг
        Console.WriteLine("=== КАТАЛОГ БИБЛИОТЕКИ ===\n");
        for (int i = 0; i < books.Length; i++)
        {
            string status = books[i].IsAvailable() ? "доступна" : "выдана";
            Console.WriteLine($"{titles[i]}: {status}");
        }

        // Выдаём две книги
        Console.WriteLine("\n=== ВЫДАЧА КНИГ ===");
        books[0].Borrow();
        books[3].Borrow();
        Console.WriteLine($"Выдано: {titles[0]}");
        Console.WriteLine($"Выдано: {titles[3]}");

        // Проверяем состояние после выдачи
        Console.WriteLine("\n=== КАТАЛОГ ПОСЛЕ ВЫДАЧИ ===\n");
        for (int i = 0; i < books.Length; i++)
        {
            string status = books[i].IsAvailable() ? "доступна" : "выдана";
            Console.WriteLine($"{titles[i]}: {status}");
        }
    }
}