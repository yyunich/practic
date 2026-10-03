using System;

delegate void TaskHandler(string taskName);

class Ex3
{
    static void SendNotification(string taskName)
    {
        Console.WriteLine($"Уведомление: задача {taskName} выполнена!");
    }

    static void WriteLog(string taskName)
    {
        Console.WriteLine($"Журнал: [{DateTime.Now:HH:mm:ss}] {taskName} -- ОК");
    }

    static void SendEmail(string taskName)
    {
        Console.WriteLine($"Почта: задача {taskName} -- отправлена по почте!");
    }

    static void Main()
    {
        string[] taskNames = new string[15];
        TaskHandler[] handlers = new TaskHandler[15];
        int count = 0;

        while (true)
        {
            Console.WriteLine("\n=== МЕНЕДЖЕР ЗАДАЧ ===");
            Console.WriteLine("1. Добавить задачу");
            Console.WriteLine("2. Выполнить все задачи");
            Console.WriteLine("3. Выход");
            Console.Write("Выбор: ");
            int choice = int.Parse(Console.ReadLine());

            switch (choice)
            {
                case 1:
                    if (count >= 10)
                    {
                        Console.WriteLine("Список задач выполнен!");
                        break;
                    }
                    Console.Write("Название задачи: ");
                    string name = Console.ReadLine();

                    Console.WriteLine("Выберите обработчик:");
                    Console.WriteLine("  1 — Отправить уведомление");
                    Console.WriteLine("  2 — Записать в журнал");
                    Console.WriteLine("  3 — Отправить email");
                    Console.Write("Выбор: ");
                    int h = int.Parse(Console.ReadLine());

                    TaskHandler handler = null;

                    // Выбор делегата через switch
                    switch (h)
                    {
                        case 1:
                            handler = SendNotification;
                            break;
                        case 2:
                            handler = WriteLog;
                            break;
                        case 3:
                            handler = SendEmail;
                            break;
                        default:
                            Console.WriteLine("Неверный выбор обработчика.");
                            break;
                    }

                    if (handler != null)
                    {
                        taskNames[count] = name;
                        handlers[count] = handler;
                        count++;
                        Console.WriteLine($"✓ Задача «{name}» добавлена");
                    }
                    break;

                case 2:
                    if (count == 0)
                    {
                        Console.WriteLine("Задач нет.");
                        break;
                    }

                    Console.WriteLine("\n--- Выполнение задач ---");
                    for (int i = 0; i < count; i++)
                    {
                        Console.WriteLine($"Задача «{taskNames[i]}»:");
                        handlers[i](taskNames[i]);   // вызов делегата
                    }
                    break;

                case 3:
                    Console.WriteLine("Выход.");
                    return;

                default:
                    Console.WriteLine("Неверный пункт меню.");
                    break;
            }
        }
    }
}