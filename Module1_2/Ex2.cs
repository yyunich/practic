using System;

class Ex2
{
    static void Main()
    {
        int[] array = { 5, -3, 12, 7, 12, 0, -8, 15, 4, 9 };

        Console.WriteLine("Исходный массив:");
        for (int i = 0; i < array.Length; i++)
        {
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

        Console.WriteLine("Измененный массив:");
        for (int i = 0; i < array.Length; i++)
        {
            Console.Write(array[i] + "  ");
        }
        Console.WriteLine();
    }
}