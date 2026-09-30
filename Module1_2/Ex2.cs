using System;

class Ex2
{
    static void Main()
    {
        int[] array = new int[10];
        Random rand = new Random();

        Console.WriteLine("Исходный массив:");
        for (int i = 0; i < array.Length; i++)
        {
            array[i] = rand.Next(-50, 50);
            Console.Write(array[i] + "  ");
        }
        Console.WriteLine();

        int max = array[0];
        for (int i = 1; i < array.Length; i++)
        {
            if (array[i] > max)
                max = array[i];
        }

        Console.WriteLine($"\nМаксимальный элемент: {max}");

        Console.Write("\nВведите целое число для замены: ");

        if (!int.TryParse(Console.ReadLine(), out int newValue))
        {
            Console.WriteLine("Ошибка: введите корректное целое число.");
        }
        for (int i = 0; i < array.Length; i++)
        {
            if (array[i] == max)
            {
                array[i] = newValue;
            }
        }
        Console.WriteLine("Измененный массив:");
        for (int i = 0; i < array.Length; i++)
        {
            Console.Write(array[i] + "  ");
        }
        Console.WriteLine();
    }
}