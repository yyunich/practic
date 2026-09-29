using System;

class Ex3
{
    static void Main()
    {
        Console.Write("Введите размер матрицы N: ");
        int n = int.Parse(Console.ReadLine());

        if (n <= 0)
        {
            Console.WriteLine("Ошибка: размер должен быть положительным.");
        }
        else
        {
            int[,] matrix = new int[n, n];
            Random random = new Random();

            // Заполнение матрицы случайными числами [-50, 50]
            // Next(-50, 51) возвращает [-50, 50] (верхняя граница не включается)
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    matrix[i, j] = random.Next(-50, 51);
                }
            }

            Console.WriteLine("\nИсходная матрица:");
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    Console.Write($"{matrix[i, j],5}");
                }
                Console.WriteLine();
            }

            int[] sums = new int[n];
            for (int i = 0; i < n; i++)
            {
                int sum = 0;
                for (int j = 0; j < n; j++)
                {
                    sum += matrix[i, j];
                }
                sums[i] = sum;
            }

            Console.WriteLine("\nСуммы строк:");
            for (int i = 0; i < n; i++)
            {
                Console.WriteLine($"Строка {i}: сумма = {sums[i]}");
            }

            // Сортировка строки по возрастанию сумм (метод пузырька)
            // Меняем местами строки и соответствующие суммы
            for (int i = 0; i < n - 1; i++)
            {
                for (int j = 0; j < n - 1 - i; j++)
                {
                    if (sums[j] > sums[j + 1])
                    {
                        // Меняем суммы
                        int tempSum = sums[j];
                        sums[j] = sums[j + 1];
                        sums[j + 1] = tempSum;

                        // Меняем строки матрицы
                        for (int col = 0; col < n; col++) {
                            int temp = matrix[j, col];
                            matrix[j, col] = matrix[j + 1, col];
                            matrix[j + 1, col] = temp;
                        }
                    }
                }
            }

            Console.WriteLine("\nМатрица после сортировки строк по возрастанию сумм:");
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    Console.Write($"{matrix[i, j],5}");
                }
                Console.WriteLine($"   | сумма = {sums[i]}");
            }
        }
    }
}