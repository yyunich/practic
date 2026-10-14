using System;

class Ex5
{
    static void Main()
    {
        Console.Write("Введите количество элементов массива K: ");
        int k = int.Parse(Console.ReadLine());

        char[] alphabet = {
            'а', 'б', 'в', 'г', 'д', 'е', 'ё', 'ж', 'з', 'и', 'й',
            'к', 'л', 'м', 'н', 'о', 'п', 'р', 'с', 'т', 'у', 'ф',
            'х', 'ц', 'ч', 'ш', 'щ', 'ъ', 'ы', 'ь', 'э', 'ю', 'я'
        };

        char[] consonants = {
            'б', 'в', 'г', 'д', 'ж', 'з', 'й', 'к', 'л', 'м', 'н',
            'п', 'р', 'с', 'т', 'ф', 'х', 'ц', 'ч', 'ш', 'щ'
        };

        char[] array = new char[k];
        Random random = new Random();

        for (int i = 0; i < k; i++)
        {
            array[i] = alphabet[random.Next(alphabet.Length)];
        }

        Console.WriteLine("\nИсходный массив:");
        for (int i = 0; i < k; i++)
        {
            Console.Write(array[i] + "  ");
        }
        Console.WriteLine();

        int consonantsCount = 0;
        for (int i = 0; i < k; i++)
        {
            for (int j = 0; j < consonants.Length; j++)
            {
                if (array[i] == consonants[j])
                {
                    consonantsCount++;
                    break;
                }
            }
        }

        char[] consonantsArray = new char[consonantsCount];

        int index = 0;
        for (int i = 0; i < k; i++)
        {
            for (int j = 0; j < consonants.Length; j++)
            {
                if (array[i] == consonants[j])
                {
                    consonantsArray[index] = array[i];
                    index++;
                    break;
                }
            }
        }

        Console.WriteLine($"\nМассив согласных букв (всего {consonantsCount}):");
        if (consonantsCount == 0)
        {
            Console.WriteLine("Согласных букв нет.");
        }
        else
        {
            for (int i = 0; i < consonantsCount; i++)
            {
                Console.Write(consonantsArray[i] + "  ");
            }
            Console.WriteLine();
        }
    }
}