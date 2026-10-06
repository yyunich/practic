using System;

// Класс «Телефонные уведомления».
// Генерирует три вида событий: SMS, звонок, email.
class PhoneNotification
{
    // События — точки оповещения. Action<string> — делегат,
    // описывающий метод без возврата, принимающий одну строку.
    public event Action<string> MessageReceived; // пришло SMS
    public event Action<string> CallReceived;    // входящий звонок
    public event Action<string> EmailReceived;   // пришло письмо

    // Метод-имитация получения SMS.
    public void SendMessage(string text)
    {
        Console.WriteLine($"\n[Уведомление] Сообщение: {text}");

        // ?.Invoke — безопасный вызов: если подписчиков нет,
        // ничего не произойдёт; иначе будут вызваны все обработчики.
        MessageReceived?.Invoke(text);
    }

    // Метод-имитация получения письма.
    public void SendEmail(string subject)
    {
        Console.WriteLine($"\n[Уведомление] Письмо: {subject}");
        EmailReceived?.Invoke(subject);
    }

    // Метод-имитация входящего звонка.
    public void SendCall(string from)
    {
        Console.WriteLine($"\n[Уведомление] Звонок: {from}");
        CallReceived?.Invoke(from);
    }
}

class Ex2
{
    // Обработчик SMS. Метод соответствует делегату Action<string>:
    // принимает строку, ничего не возвращает.
    static void OnMessageReceived(string text)
    {
        Console.WriteLine($"  [SMS] Получено сообщение: \"{text}\"");
    }

    // Обработчик звонка.
    static void OnCallReceived(string from)
    {
        Console.WriteLine($"  [Звонок] Входящий вызов от: {from}");
    }

    // Обработчик email.
    static void OnEmailReceived(string subject)
    {
        Console.WriteLine($"  [Email] Новое письмо: \"{subject}\"");
    }

    // Дополнительный обработчик звонка — имитация журнала вызовов.
    static void LogCall(string from)
    {
        Console.WriteLine($"  [Журнал] Записан номер: {from}");
    }

    static void Main()
    {
        PhoneNotification phone = new PhoneNotification();

        // ---- Подписка на события ----
        // Оператор += присоединяет метод-обработчик к событию.
        // Можно использовать как именованные методы (как здесь),
        // так и лямбда-выражения.

        phone.MessageReceived += OnMessageReceived; // подписали обработчик SMS
        phone.CallReceived += OnCallReceived;    // подписали обработчик звонка
        phone.CallReceived += LogCall;           // второй подписчик на то же событие
        phone.EmailReceived += OnEmailReceived;   // подписали обработчик email

        // Можно также подписаться через лямбда-выражение — компактнее:
        phone.MessageReceived += text =>
            Console.WriteLine($"  [Лог SMS] Длина сообщения: {text.Length} символов");

        // ---- Инициируем события ----
        // Теперь при каждом вызове SendXxx сработают все подписанные обработчики.

        phone.SendMessage("Привет! Как дела в колледже?");
        phone.SendCall("+375 (33) 333-33-33");
        phone.SendEmail("Кураторский час в 15:00");

        // ---- Пример отписки ----
        // Оператор -= удаляет обработчик из списка подписчиков события.
        // После этого LogCall больше не будет вызываться при звонках.
        Console.WriteLine("\n--- Отписываем LogCall от CallReceived ---");
        phone.CallReceived -= LogCall;

        phone.SendCall("+375 (29) 111-22-33"); // LogCall уже не сработает
    }
}