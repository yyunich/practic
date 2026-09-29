using System;

class Ex1
{
    static int Gcd(int a, int b)
    {
        while (b != 0)
        {
            int temp = b;
            b = a % b;
            a = temp;
        }
        return a;
    }

    static void Main()
    {
        Console.Write("Введите числитель (неотрицательное число): ");
        int numerator = int.Parse(Console.ReadLine());

        Console.Write("Введите знаменатель (положительное число): ");
        int denominator = int.Parse(Console.ReadLine());

        if (numerator < 0)
        {
            Console.WriteLine("Ошибка: числитель должен быть неотрицательным.");
        }
        else if (denominator <= 0)
        {
            Console.WriteLine("Ошибка: знаменатель должен быть положительным.");
        }
        else
        {
            Console.WriteLine($"\nИсходная дробь: {numerator}/{denominator}");

            int gcd = Gcd(numerator, denominator);
            Console.WriteLine($"НОД({numerator}, {denominator}) = {gcd}");

            int newNumerator = numerator / gcd;
            int newDenominator = denominator / gcd;

            if (newDenominator == 1)
            {
                Console.WriteLine($"Сокращенная дробь: {newNumerator}");
            }
            else
            {
                Console.WriteLine($"Сокращенная дробь: {newNumerator}/{newDenominator}");
            }
        }
    }
}