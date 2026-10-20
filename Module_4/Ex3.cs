using System;

interface IStudent
{
    double GetAverage(); 
    int GetCourse(); 
}

class FirstYear : IStudent
{
    public string Name; public double Avg;
    public double GetAverage() => Avg;
    public int GetCourse() => 1;
}

class SecondYear : IStudent
{
    public string Name; public double Avg;
    public double GetAverage() => Avg;
    public int GetCourse() => 2;
}

class ThirdYear : IStudent
{
    public string Name; public double Avg;
    public double GetAverage() => Avg;
    public int GetCourse() => 3;
}

class Ex3
{
    static void Main()
    {
        IStudent[] students = {
            new FirstYear  { Name = "Иван Петров",     Avg = 4.5 },
            new SecondYear { Name = "Мария Сидорова",  Avg = 4.8 },
            new ThirdYear  { Name = "Алексей Иванов",  Avg = 4.2 },
            new FirstYear  { Name = "Ольга Кузнецова", Avg = 3.9 },
            new SecondYear { Name = "Дмитрий Смирнов", Avg = 5.0 }
        };

        string[] names = {
            "Иван Петров", "Мария Сидорова", "Алексей Иванов",
            "Ольга Кузнецова", "Дмитрий Смирнов"
        };

        double total = 0;

        for (int i = 0; i < students.Length; i++)
        {
            Console.WriteLine($"{names[i]}: {students[i].GetCourse()} курс, средний балл {students[i].GetAverage():F1}");
            total += students[i].GetAverage();
        }

        Console.WriteLine($"\nСредний балл по всем студентам: {total / students.Length:F2}");
    }
}