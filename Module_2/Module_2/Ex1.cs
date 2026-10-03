using System; // Подключение пространства имён System для использования Console

// Класс Person описывает человека с именем, возрастом и адресом
class Person
{
    // Приватные поля класса — доступны только внутри класса (инкапсуляция)
    private string name;  // Имя человека
    private int age;      // Возраст человека
    private string addr;  // Адрес человека

    // Публичный метод-сеттер для установки имени
    public void SetName(string name)
    {
        // this.name — поле класса, name — параметр метода
        // Без this присвоение было бы самому себе (параметр = параметр)
        this.name = name;
    }

    // Публичный метод-сеттер для установки возраста
    public void SetAge(int age)
    {
        // Проверка корректности: возраст не может быть отрицательным
        if (age >= 0)
        {
            this.age = age; // Присваиваем значение полю, если оно допустимо
        }
        else
        {
            // Сообщаем пользователю об ошибке, поле остаётся без изменений
            Console.WriteLine("Возраст не может быть отрицательным!");
        }
    }

    // Публичный метод-сеттер для установки адреса
    public void SetAddr(string addr)
    {
        this.addr = addr; // Присваиваем значение полю addr
    }

    // Геттер — возвращает значение поля name
    public string GetName()
    {
        return name;
    }

    // Геттер — возвращает значение поля age
    public int GetAge()
    {
        return age;
    }

    // Геттер — возвращает значение поля addr
    public string GetAddr()
    {
        return addr;
    }

    // Метод для вывода всей информации о человеке в консоль
    public void PrintInfo()
    {
        // Интерполяция строк: подставляет значения полей в шаблон
        Console.WriteLine($"Имя: {name}");
        Console.WriteLine($"Возраст: {age}");
        Console.WriteLine($"Адрес: {addr}");
    }
}

// Класс Ex1 содержит точку входа в программу
class Ex1
{
    // Метод Main — с него начинается выполнение программы
    static void Main()
    {
        // Создаём первый объект класса Person
        Person person1 = new Person();
        person1.SetName("Петр");             // Устанавливаем имя
        person1.SetAge(22);                  // Устанавливаем возраст
        person1.SetAddr("ул. Борова, д. 2"); // Устанавливаем адрес

        // Создаём второй объект класса Person
        Person person2 = new Person();
        person2.SetName("Коля");
        person2.SetAge(35);
        person2.SetAddr("ул. Фролова, д. 3, кв. 43");

        // Выводим информацию о первом человеке
        Console.WriteLine("Информация о первом человеке: ");
        person1.PrintInfo();

        // \n — символ перевода строки для визуального разделения вывода
        Console.WriteLine("\nИнформация о втором человеке: ");
        person2.PrintInfo();
    }
}