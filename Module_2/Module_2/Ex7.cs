using System;

// struct — значимый тип (value type), в отличие от class (ссылочного).
// Хранится в стеке (если локальная переменная), копируется целиком при присваивании.
// Хорошо подходит для простых "контейнеров данных" без поведения.
struct Train
{
    public string Destination;   // Пункт назначения (публичное поле)
    public int Number;           // Номер поезда (публичное поле)
    public string DepartureTime; // Время отправления (публичное поле)
}

class Ex7
{
    static void Main()
    {
        // Массив из 5 поездов.
        // Так как Train — структура, элементы массива уже проинициализированы
        // значениями по умолчанию (null для string, 0 для int).
        Train[] trains = new Train[5];

        Console.WriteLine("Введите данные о 5 поездах:\n");
        for (int i = 0; i < 5; i++)
        {
            Console.WriteLine($"--- Поезд №{i + 1} ---");

            Console.Write("Пункт назначения: ");
            // Обращение к полю структуры через индексатор массива.
            // trains[i] возвращает ССЫЛКУ на элемент массива (структуры в массивах
            // можно изменять по индексу, в отличие от локальных переменных).
            trains[i].Destination = Console.ReadLine();

            Console.Write("Номер поезда: ");
            // int.Parse — преобразование строки в число.
            // Если пользователь введёт не число — будет FormatException.
            trains[i].Number = int.Parse(Console.ReadLine());

            Console.Write("Время отправления: ");
            trains[i].DepartureTime = Console.ReadLine();

            Console.WriteLine();
        }

        // СОРТИРОВКА ПО НОМЕРУ (пузырьком)
        // Внешний цикл: количество проходов = N-1 = 4
        for (int i = 0; i < 4; i++)
        {
            // Внутренний цикл: сравнение пар элементов.
            // С каждым проходом "хвост" уже отсортирован,
            // поэтому граница уменьшается: 4 - i
            for (int j = 0; j < 4 - i; j++)
            {
                // Если текущий номер больше следующего — меняем местами
                if (trains[j].Number > trains[j + 1].Number)
                {
                    // Обмен через временную переменную.
                    // Так как Train — структура, копируется ВСЁ содержимое (3 поля).
                    Train temp = trains[j];
                    trains[j] = trains[j + 1];
                    trains[j + 1] = temp;
                }
            }
        }

        // ВЫВОД ПОСЛЕ ПЕРВОЙ СОРТИРОВКИ
        Console.WriteLine("========== ПОЕЗДА (по номеру) ==========");
        for (int i = 0; i < 5; i++)
        {
            // Форматирование с выравниванием:
            // {-5}  — поле шириной 5, выравнивание влево
            // {-20} — поле шириной 20, выравнивание влево
            Console.WriteLine($"№{trains[i].Number,-5} | {trains[i].Destination,-20} | {trains[i].DepartureTime}");
        }

        // ПОИСК ПО НОМЕРУ 
        Console.Write("\nВведите номер поезда для поиска: ");
        int searchNumber = int.Parse(Console.ReadLine());

        bool found = false; // Флаг: найден ли поезд
        for (int i = 0; i < 5; i++)
        {
            if (trains[i].Number == searchNumber)
            {
                Console.WriteLine($"\nНайден поезд:");
                Console.WriteLine($"  Пункт назначения: {trains[i].Destination}");
                Console.WriteLine($"  Номер поезда:     {trains[i].Number}");
                Console.WriteLine($"  Время отправления: {trains[i].DepartureTime}");
                found = true; // Отмечаем, что нашли (продолжаем цикл —
                              // вдруг есть дубликаты номеров)
            }
        }

        // Если флаг остался false — ничего не найдено
        if (!found)
        {
            Console.WriteLine("Поезд с таким номером не найден.");
        }

        // СОРТИРОВКА ПО ПУНКТУ НАЗНАЧЕНИЯ, ЗАТЕМ ПО ВРЕМЕНИ
        // Тот же пузырёк, но с составным условием сравнения
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4 - i; j++)
            {
                bool needSwap = false;

                // string.Compare возвращает:
                //   < 0  — первая строка меньше второй
                //   = 0  — строки равны
                //   > 0  — первая строка больше второй
                // Сравнение по алфавиту (с учётом регистра и текущей культуры)
                int destCompare = string.Compare(trains[j].Destination, trains[j + 1].Destination);

                if (destCompare > 0)
                {
                    // Пункт назначения "больше" — надо поменять
                    needSwap = true;
                }
                else if (destCompare == 0)
                {
                    // Пункты совпадают — вторичный критерий: время отправления.
                    // ВНИМАНИЕ: строковое сравнение работает корректно,
                    // только если время в формате "HH:mm" или "HH:mm:ss"
                    // (одинаковое количество цифр). Иначе "9:00" > "10:00".
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

        // ВЫВОД ПОСЛЕ ВТОРОЙ СОРТИРОВКИ
        Console.WriteLine("\n========== ПОЕЗДА (по пункту назначения) ==========");
        for (int i = 0; i < 5; i++)
        {
            Console.WriteLine($"№{trains[i].Number,-5} | {trains[i].Destination,-20} | {trains[i].DepartureTime}");
        }
    }
}