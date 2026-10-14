using System;

class Ex6
{
    static void Main()
    {
        double[] array = new double[10];
        Random random = new Random();

        for (int i = 0; i < 10; i++)
        {
            // NextDouble() возвращает [0, 1), умножаем на 20 → [0, 20), вычитаем 10 → [-10, 10)
            array[i] = random.NextDouble() * 20 - 10;
        }

        Console.WriteLine("Исходный массив:");
        for (int i = 0; i < 10; i++)
        {
            Console.WriteLine($"[{i}] = {array[i]:F4}");
        }

        int[] indices = new int[10];
        for (int i = 0; i < 10; i++)
        {
            indices[i] = i;
        }

        for (int i = 0; i < 9; i++)
        {
            for (int j = i + 1; j < 10; j++)
            {
                if (array[indices[i]] > array[indices[j]])
                {
                    int temp = indices[i];
                    indices[i] = indices[j];
                    indices[j] = temp;
                }
            }
        }

        Console.WriteLine("\nМассив индексов (по возрастанию значений):");
        for (int i = 0; i < 10; i++)
        {
            Console.WriteLine($"{i}-й по порядку: индекс {indices[i]}, значение {array[indices[i]]:F4}");
        }
    }
}