using System;

class Ex3
{
    static void Main()
    {
        Console.Write("Введите количество простых чисел K: ");
        int k = int.Parse(Console.ReadLine());

        Console.WriteLine($"\nПервые {k} простых чисел:");
        Console.WriteLine(new string('-', 40));

        int count = 0;      // количество найденных простых чисел
        int number = 2;     // текущее проверяемое число
        int column = 0;     // счетчик для перевода строки каждые 10 чисел

        while (count < k)
        {
            bool isPrime = true;

            if (number < 2)
            {
                isPrime = false;
            }
            else
            {
                for (int i = 2; i * i <= number; i++)
                {
                    if (number % i == 0)
                    {
                        isPrime = false;
                        break;
                    }
                }
            }

            if (isPrime)
            {
                count++;
                column++;

                Console.Write($"{number,6}");

                if (column % 10 == 0)
                {
                    Console.WriteLine();
                }
            }

            number++;
        }

        if (column % 10 != 0)
        {
            Console.WriteLine();
        }
    }
}