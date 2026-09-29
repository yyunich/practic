using System;
using System.Numerics;

class Proverka
{
    static void Main()
    {
        Console.Write("Введите число: ");
        int number = int.Parse(Console.ReadLine());

        bool isPrime = true;

        if (number < 2)
        {
            isPrime = false;
        }
        else
        {
            // Проверяем все делители от 2 до number-1
            for (int i = 2; i < number; i++)
            {
                if (number % i == 0)
                {
                    isPrime = false;
                    break;
                }
            }
        }

        if (isPrime)
            Console.WriteLine($"{number} — простое число.");
        else
            Console.WriteLine($"{number} — не является простым числом.");
    }
}