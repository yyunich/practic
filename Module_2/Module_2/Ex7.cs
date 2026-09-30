using System;

struct Train
{
    public string Destination;   // пункт назначения
    public int Number;           // номер поезда
    public string DepartureTime; // время отправления
}

class Ex7
{
    static void Main()
    {
        // Массив из 5 поездов
        Train[] trains = new Train[5];

        // Ввод данных
        Console.WriteLine("Введите данные о 5 поездах:\n");
        for (int i = 0; i < 5; i++)
        {
            Console.WriteLine($"--- Поезд №{i + 1} ---");

            Console.Write("Пункт назначения: ");
            trains[i].Destination = Console.ReadLine();

            Console.Write("Номер поезда: ");
            trains[i].Number = int.Parse(Console.ReadLine());

            Console.Write("Время отправления: ");
            trains[i].DepartureTime = Console.ReadLine();

            Console.WriteLine();
        }

        // Сортировка по номерам поездов (пузырьком)
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4 - i; j++)
            {
                if (trains[j].Number > trains[j + 1].Number)
                {
                    Train temp = trains[j];
                    trains[j] = trains[j + 1];
                    trains[j + 1] = temp;
                }
            }
        }

        // Вывод отсортированного массива
        Console.WriteLine("========== ПОЕЗДА (по номеру) ==========");
        for (int i = 0; i < 5; i++)
        {
            Console.WriteLine($"№{trains[i].Number,-5} | {trains[i].Destination,-20} | {trains[i].DepartureTime}");
        }

        // Поиск по номеру поезда
        Console.Write("\nВведите номер поезда для поиска: ");
        int searchNumber = int.Parse(Console.ReadLine());

        bool found = false;
        for (int i = 0; i < 5; i++)
        {
            if (trains[i].Number == searchNumber)
            {
                Console.WriteLine($"\nНайден поезд:");
                Console.WriteLine($"  Пункт назначения: {trains[i].Destination}");
                Console.WriteLine($"  Номер поезда:     {trains[i].Number}");
                Console.WriteLine($"  Время отправления: {trains[i].DepartureTime}");
                found = true;
            }
        }

        if (!found)
        {
            Console.WriteLine("Поезд с таким номером не найден.");
        }

        // Сортировка по пункту назначения, при равенстве — по времени
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4 - i; j++)
            {
                bool needSwap = false;

                // Сравнение пунктов назначения (по алфавиту)
                int destCompare = string.Compare(trains[j].Destination, trains[j + 1].Destination);

                if (destCompare > 0)
                {
                    needSwap = true;
                }
                else if (destCompare == 0)
                {
                    // Пункты одинаковые — сравниваем время отправления
                    if (string.Compare(trains[j].DepartureTime, trains[j + 1].DepartureTime) > 0)
                    {
                        needSwap = true;
                    }
                }

                if (needSwap)
                {
                    Train temp = trains[j];
                    trains[j] = trains[j + 1];
                    trains[j + 1] = temp;
                }
            }
        }

        // Вывод после второй сортировки
        Console.WriteLine("\n========== ПОЕЗДА (по пункту назначения) ==========");
        for (int i = 0; i < 5; i++)
        {
            Console.WriteLine($"№{trains[i].Number,-5} | {trains[i].Destination,-20} | {trains[i].DepartureTime}");
        }
    }
}