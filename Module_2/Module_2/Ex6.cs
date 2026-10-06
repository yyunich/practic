using System;

// Класс Student описывает студента: имя, возраст, средний балл
class Student
{
    // Приватные поля класса — доступны только внутри класса (инкапсуляция)
    private string name;   // Имя студента
    private int age;       // Возраст студента
    private double ball;   // Средний балл

    // Сеттер для имени.
    // this.name — поле класса, name — параметр метода.
    public void SetName(string name)
    {
        this.name = name;
    }

    // Сеттер для возраста с проверкой корректности
    public void SetAge(int age)
    {
        if (age >= 0)
        {
            this.age = age; // Присваиваем, если значение неотрицательное
        }
        else
        {
            // Сообщаем об ошибке, поле остаётся прежним
            Console.WriteLine("Возраст не может быть отрицательным!");
        }
    }

    // Сеттер для среднего балла с проверкой
    public void SetBall(double ball)
    {
        if (ball >= 0)
        {
            this.ball = ball; // Присваиваем, если балл неотрицательный
        }
        else
        {
            Console.WriteLine("Средний балл не может быть отрицательным!");
        }
    }

    // Геттер для имени
    public string GetName()
    {
        return name;
    }

    // Геттер для возраста
    public int GetAge()
    {
        return age;
    }

    // Геттер для среднего балла
    public double GetBall()
    {
        return ball;
    }

    // Метод вывода всей информации о студенте в консоль.
    // Интерполяция строк: $"...{переменная}..." подставляет значения полей.
    public void PrintInfo()
    {
        Console.WriteLine($"Имя: {name}");
        Console.WriteLine($"Возраст: {age}");
        Console.WriteLine($"Средний балл: {ball}");
    }
}

class Ex6
{
    static void Main()
    {
        // Создаём первого студента и заполняем его данные
        Student student1 = new Student();
        student1.SetName("Петя");
        student1.SetAge(17);
        student1.SetBall(8.32);

        // Создаём второго студента
        Student student2 = new Student();
        student2.SetName("Саня");
        student2.SetAge(16);
        student2.SetBall(9.51);

        // Создаём третьего студента
        Student student3 = new Student();
        student3.SetName("Артём");
        student3.SetAge(19);
        student3.SetBall(6.48);

        // Выводим информацию о каждом студенте.
        // \n — символ перевода строки для визуального разделения вывода.
        Console.WriteLine("Информациия о первом студенте: ");
        student1.PrintInfo();

        Console.WriteLine("\nИнформациия о втором студенте: ");
        student2.PrintInfo();

        Console.WriteLine("\nИнформациия о третьем студенте: ");
        student3.PrintInfo();
    }
}