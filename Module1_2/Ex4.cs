using System;

class Ex4
{
    static void Main()
    {
        Console.Write("Введите количество элементов массива K: ");
        int k = int.Parse(Console.ReadLine());

        Console.Write("Введите нижнюю границу диапазона A: ");
        int a = int.Parse(Console.ReadLine());

        Console.Write("Введите верхнюю границу диапазона B: ");
        int b = int.Parse(Console.ReadLine());

        int[] array = new int[k];
        Random random = new Random();

        for (int i = 0; i < k; i++)
        {
            array[i] = random.Next(a, b);  // Next(a, b) возвращает [a, b)
        }

        Console.WriteLine("\nИсходный массив:");
        for (int i = 0; i < k; i++)
        {
            Console.Write($"[{i}]={array[i]}  ");
        }
        Console.WriteLine();

        int minIndex = 0;
        int maxIndex = 0;

        for (int i = 1; i < k; i++)
        {
            if (array[i] < array[minIndex])
            {
                minIndex = i;
            }

            if (array[i] > array[maxIndex])
            {
                maxIndex = i;
            }
        }

        Console.WriteLine($"\nМинимальный элемент: {array[minIndex]} (индекс {minIndex})");
        Console.WriteLine($"Максимальный элемент: {array[maxIndex]} (индекс {maxIndex})");

        int start = minIndex;
        int end = maxIndex;

        if (start > end)
        {
            int temp = start;
            start = end;
            end = temp;
        }

        Console.WriteLine($"\nЭлементы с {start}-го по {end}-й индекс включительно:");
        for (int i = start; i <= end; i++)
        {
            Console.Write($"[{i}]={array[i]}  ");
        }
        Console.WriteLine();
    }
}