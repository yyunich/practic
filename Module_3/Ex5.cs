using System;

// Делегат для метода сортировки: принимает массив int[], ничего не возвращает.
// Любой метод с такой сигнатурой подойдёт (пузырёк, вставки, выбор и т.д.).
delegate void SortMethod(int[] array);

class Ex5
{
    // Сортировка пузырьком: соседние элементы меняются местами, если стоят не по порядку.
    // Внешний цикл — количество проходов, внутренний — сравнения в текущем проходе.
    static void BubbleSort(int[] array)
    {
        for (int i = 0; i < array.Length - 1; i++)
        {
            for (int j = 0; j < array.Length - 1 - i; j++)
            {
                if (array[j] > array[j + 1])
                {
                    // Обмен двух соседних элементов.
                    int temp = array[j];
                    array[j] = array[j + 1];
                    array[j + 1] = temp;
                }
            }
        }
    }

    // Вывод массива в одну строку через пробел.
    static void PrintArray(int[] array)
    {
        for (int i = 0; i < array.Length; i++)
            Console.Write(array[i] + " ");
        Console.WriteLine();
    }

    static void Main()
    {
        // Запрос размера массива.
        Console.Write("Введите размер массива N: ");
        int n = int.Parse(Console.ReadLine());

        // Создание массива и генератора случайных чисел.
        int[] array = new int[n];
        Random random = new Random();

        // Заполнение случайными числами от 1 до 99.
        for (int i = 0; i < n; i++)
            array[i] = random.Next(1, 100);

        // Показ исходного массива.
        Console.WriteLine("\n=== ИСХОДНЫЙ МАССИВ ===");
        PrintArray(array);

        while (true)
        {
            Console.WriteLine("\n=== МЕНЮ ===");
            Console.WriteLine("1. Сортировать пузырьком");
            Console.WriteLine("2. Вывести массив");
            Console.WriteLine("3. Выход");
            Console.Write("Выбор: ");

            // Безопасный ввод номера пункта меню.
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

            // Переменная-делегат: сюда будет сохранён выбранный метод сортировки.
            SortMethod sort = null;
            string methodName = ""; // название для вывода

            switch (choice)
            {
                // Выбор сортировки пузырьком.
                case 1:
                    sort = BubbleSort; // присваиваем метод делегату
                    methodName = "Сортировка пузырьком";
                    break;

                // Пункт 2 — просто показать текущий массив и вернуться в меню.
                case 2:
                    Console.WriteLine("\nТекущий массив:");
                    PrintArray(array);
                    Console.WriteLine("\nНажмите Enter...");
                    Console.ReadLine();
                    continue; // возвращаемся в начало while

                default:
                    Console.WriteLine("Неверный пункт меню.");
                    continue;
            }

            // Замер времени выполнения сортировки.
            DateTime start = DateTime.Now;
            sort(array);   // вызов метода через делегат
            DateTime end = DateTime.Now;

            // Разница во времени в миллисекундах.
            double ms = (end - start).TotalMilliseconds;

            // Вывод результата и затраченного времени.
            Console.WriteLine($"\n--- {methodName} ---");
            Console.WriteLine("Отсортированный массив:");
            PrintArray(array);
            Console.WriteLine($"Время выполнения: {ms:F4} мс");

            Console.WriteLine("\nНажмите Enter, чтобы вернуться в меню...");
            Console.ReadLine();
        }
    }
}