using System;

class Cifri
{
    static void Main()
    {
        Console.Write("Введите число (цифру) для нахождения суммы между ними: ");
        int a = int.Parse(Console.ReadLine());
        Console.Write("Введите число (цифру) для нахождения суммы между ними: ");
        int b = int.Parse(Console.ReadLine());

        int result = a + b;

        Console.WriteLine(result);
    }
}