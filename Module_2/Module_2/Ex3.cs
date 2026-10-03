using System; // Подключение пространства имён System (для Console)

// ==================== КЛАСС AUTHOR ====================
// Класс Author описывает автора книги — самостоятельную сущность
class Author
{
    // Поля автора (приватные — инкапсуляция)
    private string name;      // Имя автора
    private int birthYear;    // Год рождения

    // Конструктор: вызывается при создании new Author(...)
    // Инициализирует поля переданными значениями
    public Author(string name, int birthYear)
    {
        this.name = name;           // this.name — поле, name — параметр
        this.birthYear = birthYear;
    }

    // Методы получения (get) — геттеры, возвращают значения полей
    public string GetName()
    {
        return name;
    }

    public int GetBirthYear()
    {
        return birthYear;
    }

    // Методы установки (set) — сеттеры, изменяют значения полей
    public void SetName(string name)
    {
        this.name = name;
    }

    public void SetBirthYear(int birthYear)
    {
        this.birthYear = birthYear;
    }

    // Вывод информации об авторе в консоль
    public void PrintInfo()
    {
        Console.WriteLine($"Автор: {name} (род. {birthYear})");
    }
}

// ==================== КЛАСС BOOK ====================
// Класс Book описывает книгу. Содержит ссылку на объект Author —
// это пример КОМПОЗИЦИИ ("книга имеет автора", отношение has-a).
class Book
{
    // Поля книги
    private string title;    // Название книги
    private int year;        // Год издания
    private Author author;   // ← КОМПОЗИЦИЯ: поле типа Author (ссылка на объект)

    // Конструктор: принимает объект Author (не строку с именем!)
    // Так книга "знает" о своём авторе через ссылку на объект
    public Book(string title, int year, Author author)
    {
        this.title = title;
        this.year = year;
        this.author = author; // сохраняем ссылку на переданный объект
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

    // Возвращает ссылку на объект Author целиком
    // (а не отдельные поля — их можно получить через методы Author)
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

    // Устанавливает нового автора — заменяет ссылку на объект
    public void SetAuthor(Author author)
    {
        this.author = author;
    }

    // Вывод информации о книге.
    // Для получения данных об авторе вызываем его собственные методы —
    // это пример делегирования ответственности.
    public void PrintInfo()
    {
        Console.WriteLine($"Книга:     {title}");
        Console.WriteLine($"Год:       {year}");
        // Обращаемся к методам объекта author через точку
        Console.WriteLine($"Автор:     {author.GetName()} (род. {author.GetBirthYear()})");
    }
}

// ==================== ТОЧКА ВХОДА ====================
class Ex3
{
    static void Main()
    {
        // Создаём авторов — отдельные объекты класса Author
        Author pushkin = new Author("Александр Пушкин", 1799);
        Author tolstoy = new Author("Лев Толстой", 1828);
        Author bulgakov = new Author("Михаил Булгаков", 1891);

        // Создаём книги — передаём объекты Author (композиция)
        // Обратите внимание: tolstoy — один и тот же объект передаётся
        // в book2 и book4. Это важное свойство композиции через ссылку:
        // при изменении объекта tolstoy изменения будут видны во всех книгах,
        // которые на него ссылаются.
        Book book1 = new Book("Евгений Онегин", 1833, pushkin);
        Book book2 = new Book("Война и мир", 1869, tolstoy);
        Book book3 = new Book("Мастер и Маргарита", 1967, bulgakov);
        Book book4 = new Book("Анна Каренина", 1878, tolstoy); // тот же автор

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