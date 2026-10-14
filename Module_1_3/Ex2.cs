using System;

class Ex2
{
    static void Main()
    {
        Console.Write("Введите предельную сумму S: ");
        int s = int.Parse(Console.ReadLine());

        if (s <= 0)
        {
            Console.WriteLine("Ошибка: сумма должна быть положительным числом.");
        }
        else
        {
            Random random = new Random();

            int[] array = new int[s];
            int count = 0;      // фактическое количество элементов
            int sum = 0;        // текущая сумма

            while (true)
            {
                int value = random.Next(1, 10);

                if (sum + value > s)
                {
                    int remaining = s - sum;      // сколько ещё можно добавить
                    if (remaining >= 1)
                    {
                        value = random.Next(1, remaining + 1);  // от 1 до remaining
                        array[count] = value;
                        count++;
                        sum += value;
                    }
                    break;
                }

                array[count] = value;
                count++;
                sum += value;

                if (sum == s)
                {
                    break;
                }
            }

            Console.WriteLine($"\nСоздан массив из {count} элементов:");
            for (int i = 0; i < count; i++)
            {
                Console.Write(array[i] + "  ");
            }
            Console.WriteLine();

            Console.WriteLine($"\nСумма элементов: {sum}");
            Console.WriteLine($"Предельная сумма: {s}");
        }
    }
}