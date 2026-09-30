using System;

class Author
{
    // Поля автора
    private string name;
    private int birthYear;

    // Конструктор
    public Author(string name, int birthYear)
    {
        this.name = name;
        this.birthYear = birthYear;
    }

    // Методы получения (get)
    public string GetName()
    {
        return name;
    }

    public int GetBirthYear()
    {
        return birthYear;
    }

    // Методы установки (set)
    public void SetName(string name)
    {
        this.name = name;
    }

    public void SetBirthYear(int birthYear)
    {
        this.birthYear = birthYear;
    }

    // Вывод информации об авторе
    public void PrintInfo()
    {
        Console.WriteLine($"Автор: {name} (род. {birthYear})");
    }
}

class Book
{
    // Поля книги
    private string title;
    private int year;
    private Author author;   // ← КОМПОЗИЦИЯ: поле типа Author

    // Конструктор: принимает объект Author
    public Book(string title, int year, Author author)
    {
        this.title = title;
        this.year = year;
        this.author = author;
    }

    // Методы получения (get)
    public string GetTitle()
    {
        return title;
    }

    public int GetYear()
    {
        return year;
    }

    public Author GetAuthor()
    {
        return author;
    }

    // Методы установки (set)
    public void SetTitle(string title)
    {
        this.title = title;
    }

    public void SetYear(int year)
    {
        this.year = year;
    }

    public void SetAuthor(Author author)
    {
        this.author = author;
    }

    // Вывод информации о книге
    public void PrintInfo()
    {
        Console.WriteLine($"Книга:     {title}");
        Console.WriteLine($"Год:       {year}");
        Console.WriteLine($"Автор:     {author.GetName()} (род. {author.GetBirthYear()})");
    }
}

class Ex3
{
    static void Main()
    {
        // Создаём авторов
        Author pushkin = new Author("Александр Пушкин", 1799);
        Author tolstoy = new Author("Лев Толстой", 1828);
        Author bulgakov = new Author("Михаил Булгаков", 1891);

        // Создаём книги — передаём объекты Author (композиция)
        Book book1 = new Book("Евгений Онегин", 1833, pushkin);
        Book book2 = new Book("Война и мир", 1869, tolstoy);
        Book book3 = new Book("Мастер и Маргарита", 1967, bulgakov);
        Book book4 = new Book("Анна Каренина", 1878, tolstoy);   // тот же автор

        // Выводим информацию о книгах
        Console.WriteLine("========== КНИГИ ==========\n");

        Console.WriteLine("--- Книга №1 ---");
        book1.PrintInfo();
        Console.WriteLine();

        Console.WriteLine("--- Книга №2 ---");
        book2.PrintInfo();
        Console.WriteLine();

        Console.WriteLine("--- Книга №3 ---");
        book3.PrintInfo();
        Console.WriteLine();

        Console.WriteLine("--- Книга №4 ---");
        book4.PrintInfo();
    }
}