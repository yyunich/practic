using System;

    class Person
    {
        private string name;
        private int age;
        private string addr;

        public void SetName (string name)
        {
            this.name = name;
        }
        public void SetAge(int age)
        {
            if (age >= 0)
            {
                this.age = age;
            }
            else
            {
                Console.WriteLine("Возраст не может быть отрицательным!");
            }
        }

        public void SetAddr(string addr)
        {
            this.addr = addr;
        }

        public string GetName()
        {
            return name;
        }

        public int GetAge()
        {
            return age;
        }

        public string GetAddr()
        {
            return addr;
        }

        public void PrintInfo()
        {
            Console.WriteLine($"Имя: {name}");
            Console.WriteLine($"Возраст: {age}");
            Console.WriteLine($"Адрес: {addr}");
        }
  }

class Ex1
{
    static void Main()
    {
        Person person1 = new Person();
        person1.SetName("Петр");
        person1.SetAge(22);
        person1.SetAddr("ул. Борова, д. 2");

        Person person2 = new Person();
        person2.SetName("Коля");
        person2.SetAge(35);
        person2.SetAddr("ул. Фролова, д. 3, кв. 43");

        Console.WriteLine("Информация о первом человеке: ");
        person1.PrintInfo();

        Console.WriteLine("\nИнформация о втором человеке: ");
        person2.PrintInfo();
    }
}