using System;
using System.Numerics;

class Factorial
{
    static void Main()
    {
        Console.Write("Введите число (цифру) для нахождения факториала: ");

        BigInteger n = BigInteger.Parse(Console.ReadLine());

        BigInteger result = 1;

        for (BigInteger i = 1; i <= n; i++)
        {
            result *= i;
        }

        Console.WriteLine($"{n}! = {result}");
    }
}