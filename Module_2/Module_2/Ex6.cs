using System;

class Student
{
    private string name;
    private int age;
    private double ball;

    public void SetName (string name )
    {
        this.name = name;
    }

    public void SetAge (int age)
    {
        if (age >= 0) {
            this.age = age; 
        } else {
            Console.WriteLine("Возраст не может быть отрицательным!");
        }
    }

    public void SetBall(double ball)
    {
        if (ball >= 0) {
            this.ball = ball;
        } else {
            Console.WriteLine("Средний балл не может быть отрицательным!");
        }
    }

    public string GetName ()
    {
        return name;
    }

    public int GetAge()
    {
        return age;
    }

    public double GetBall()
    {
        return ball;
    }
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
        Student student1 = new Student();
        student1.SetName("Петя");
        student1.SetAge(17);
        student1.SetBall(8.32);

        Student student2 = new Student();
        student2.SetName("Саня");
        student2.SetAge(16);
        student2.SetBall(9.51);

        Student student3 = new Student();
        student3.SetName("Артём");
        student3.SetAge(19);
        student3.SetBall(6.48);

        Console.WriteLine("Информациия о первом студенте: ");
        student1.PrintInfo();
        Console.WriteLine("\nИнформациия о втором студенте: ");
        student2.PrintInfo();
        Console.WriteLine("\nИнформациия о третьем студенте: ");
        student3.PrintInfo();
    }
}