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

    static void Main()
    {
        PhoneNotification phone = new PhoneNotification();

        phone.MessageReceived += OnMessageReceived; // подписали обработчик SMS
        phone.CallReceived += OnCallReceived;    // подписали обработчик звонка
        phone.EmailReceived += OnEmailReceived;   // подписали обработчик email

        phone.SendMessage("Привет! Как дела в колледже?");
        phone.SendCall("+375 (33) 333-33-33");
        phone.SendEmail("Кураторский час в 15:00");
    }
}