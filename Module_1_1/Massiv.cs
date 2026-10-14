using System;
using System.Numerics;

class Massiv
{
    static void Main()
    {
        int[] numbers = new int[15];
        Random random = new Random();

        Console.WriteLine("Исходный массив:");
        for (int i = 0; i < numbers.Length; i++)
        {
            numbers[i] = random.Next(-50, 51);
            Console.Write(numbers[i] + " ");
        }
        Console.WriteLine();

        int sum = 0;
        int count = 0;

        foreach (int number in numbers)
        {
            if (number > 0)
            {
                sum += number;
                count++;
            }
        }

        Console.WriteLine();
        if (count > 0)
        {
            double average = (double)sum / count;
            Console.WriteLine($"Количество положительных чисел: {count}");
            Console.WriteLine($"Сумма положительных чисел: {sum}");
            Console.WriteLine($"Среднее значение положительных чисел: {average:F2}");
        }
        else
        {
            Console.WriteLine("В массиве нет положительных чисел.");
        }
    }
}