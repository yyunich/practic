using System;

// Делегат для метода сортировки
delegate void SortMethod(int[] array);

class Ex5
{
    // Сортировка пузырьком
    static void BubbleSort(int[] array)
    {
        for (int i = 0; i < array.Length - 1; i++)
        {
            for (int j = 0; j < array.Length - 1 - i; j++)
            {
                if (array[j] > array[j + 1])
                {
                    int temp = array[j];
                    array[j] = array[j + 1];
                    array[j + 1] = temp;
                }
            }
        }
    }

    // Вывод массива
    static void PrintArray(int[] array)
    {
        for (int i = 0; i < array.Length; i++)
            Console.Write(array[i] + " ");
        Console.WriteLine();
    }

    static void Main()
    {
        // Ввод размера массива
        Console.Write("Введите размер массива N: ");
        int n = int.Parse(Console.ReadLine());

        // Создаём массив
        int[] array = new int[n];
        Random random = new Random();

        // Заполняем случайными числами
        for (int i = 0; i < n; i++)
            array[i] = random.Next(1, 100);

        // Вывод исходного массива
        Console.WriteLine("\n=== ИСХОДНЫЙ МАССИВ ===");
        PrintArray(array);

        while (true)
        {
            Console.WriteLine("\n=== МЕНЮ ===");
            Console.WriteLine("1. Сортировать пузырьком");
            Console.WriteLine("2. Вывести массив");
            Console.WriteLine("3. Выход");
            Console.Write("Выбор: ");

            int choice;
            if (!int.TryParse(Console.ReadLine(), out choice))
            {
                Console.WriteLine("Ошибка: введите число.");
                continue;
            }

            if (choice == 3)
            {
                Console.WriteLine("Выход.");
                break;
            }

            // Выбор делегата через switch
            SortMethod sort = null;
            string methodName = "";

            switch (choice)
            {
                case 1:
                    sort = BubbleSort;
                    methodName = "Сортировка пузырьком";
                    break;
                case 2:
                    Console.WriteLine("\nТекущий массив:");
                    PrintArray(array);
                    Console.WriteLine("\nНажмите Enter...");
                    Console.ReadLine();
                    continue;
                default:
                    Console.WriteLine("Неверный пункт меню.");
                    continue;
            }

            // Замер времени
            DateTime start = DateTime.Now;
            sort(array);   // вызов делегата
            DateTime end = DateTime.Now;

            double ms = (end - start).TotalMilliseconds;

            // Вывод результата
            Console.WriteLine($"\n--- {methodName} ---");
            Console.WriteLine("Отсортированный массив:");
            PrintArray(array);
            Console.WriteLine($"Время выполнения: {ms:F4} мс");

            Console.WriteLine("\nНажмите Enter, чтобы вернуться в меню...");
            Console.ReadLine();
        }
    }
}