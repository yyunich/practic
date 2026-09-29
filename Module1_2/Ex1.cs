using System;

class Ex1
{
    static void Main()
    {
        Console.Write("Введите размер массива N: ");
        int n = int.Parse(Console.ReadLine());

        double[] array = new double[n];

        Console.WriteLine($"Введите {n} элементов массива:");
        for (int i = 0; i < n; i++)
        {
            Console.Write($"Элемент [{i}]: ");
            array[i] = double.Parse(Console.ReadLine());
        }

        Console.WriteLine("\nИсходный массив:");
        for (int i = 0; i < n; i++)
        {
            Console.Write(array[i] + "  ");
        }
        Console.WriteLine();

        double maxAbs = Math.Abs(array[0]);
        for (int i = 1; i < n; i++)
        {
            if (Math.Abs(array[i]) > maxAbs)
            {
                maxAbs = Math.Abs(array[i]);
            }
        }

        Console.WriteLine($"\nМаксимальный по модулю элемент: {maxAbs}");

        if (maxAbs == 0)
        {
            Console.WriteLine("\nВсе элементы массива равны нулю. Нормирование невозможно.");
        }
        else
        {
            for (int i = 0; i < n; i++)
            {
                array[i] = array[i] / maxAbs;
            }

            Console.WriteLine("\nНормированный массив:");
            for (int i = 0; i < n; i++)
            {
                Console.Write(array[i] + "  ");
            }
            Console.WriteLine();
        }
    }
}