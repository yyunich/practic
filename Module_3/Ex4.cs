using System;

delegate bool DataFilter(string item);

class Ex4
{
    static void Main()
    {
        string[] data = {
            "14.02.2025: Совещание с клиентом",
            "20.12.2024: Отчёт по продажам",
            "08.03.2025: Презентация продукта",
            "15.01.2025: Планирование бюджета",
            "14.02.2025: Отчёт по маркетингу",
            "10.11.2024: Анализ конкурентов"
        };

        Console.WriteLine("=== ИСХОДНЫЕ ДАННЫЕ ===");
        for (int i = 0; i < data.Length; i++)
            Console.WriteLine($"  {data[i]}");

        while (true)
        {
            Console.WriteLine("\n=== ВЫБОР ФИЛЬТРА ===");
            Console.WriteLine("1. По ключевому слову");
            Console.WriteLine("2. По дате (ДД.ММ.ГГГГ)");
            Console.WriteLine("3. По длине строки");
            Console.WriteLine("4. Выход");
            Console.Write("Выбор: ");

            int choice;
            if (!int.TryParse(Console.ReadLine(), out choice))
            {
                Console.WriteLine("Ошибка: введите число.");
                continue;
            }

            if (choice == 4)
            {
                Console.WriteLine("Выход.");
                break;
            }

            DataFilter filter = null;

            switch (choice)
            {
                case 1:
                    Console.Write("Ключевое слово: ");
                    string keyword = Console.ReadLine();
                    filter = item => item.ToLower().Contains(keyword.ToLower());
                    break;

                case 2:
                    Console.Write("Дата (ДД.ММ.ГГГГ): ");
                    string date = Console.ReadLine();
                    filter = item => item.StartsWith(date);
                    break;

                case 3:
                    Console.Write("Минимальная длина: ");
                    int minLength;
                    while (!int.TryParse(Console.ReadLine(), out minLength))
                    {
                        Console.Write("Ошибка! Введите число: ");
                    }
                    filter = item => item.Length >= minLength;
                    break;

                default:
                    Console.WriteLine("Неверный пункт меню.");
                    continue;
            }

            // Фильтрация — БЕЗ повторного ввода!
            Console.WriteLine("\n--- Результат фильтрации ---");
            int found = 0;

            for (int i = 0; i < data.Length; i++)
            {
                if (filter(data[i]))   // никаких вопросов внутри
                {
                    Console.WriteLine($"OK -- {data[i]}");
                    found++;
                }
            }

            if (found == 0)
                Console.WriteLine("  Ничего не найдено.");
            else
                Console.WriteLine($"\nВсего найдено: {found}");

            Console.WriteLine("\nНажмите Enter, чтобы вернуться в меню...");
            Console.ReadLine();
        }
    }
}