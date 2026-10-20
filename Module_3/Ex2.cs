using System;

class PhoneNotification
{
    // Action<string> — делегат, описывающий метод без возврата, принимающий одну строку
    public event Action<string> MessageReceived;
    public event Action<string> CallReceived;   
    public event Action<string> EmailReceived;   

    public void SendMessage(string text)
    {
        Console.WriteLine($"\n[Уведомление] Сообщение: {text}");
        MessageReceived?.Invoke(text);
    }

    public void SendEmail(string subject)
    {
        Console.WriteLine($"\n[Уведомление] Письмо: {subject}");
        EmailReceived?.Invoke(subject);
    }

    public void SendCall(string from)
    {
        Console.WriteLine($"\n[Уведомление] Звонок: {from}");
        CallReceived?.Invoke(from);
    }
}

class Ex2
{
    // Метод соответствует делегату Action<string>: принимает строку, ничего не возвращает
    static void OnMessageReceived(string text)
    {
        Console.WriteLine($"  [SMS] Получено сообщение: \"{text}\"");
    }

    // Обработчик звонка
    static void OnCallReceived(string from)
    {
        Console.WriteLine($"  [Звонок] Входящий вызов от: {from}");
    }

    // Обработчик email
    static void OnEmailReceived(string subject)
    {
        Console.WriteLine($"  [Email] Новое письмо: \"{subject}\"");
    }

    static void Main()
    {
        PhoneNotification phone = new PhoneNotification();

        phone.MessageReceived += OnMessageReceived;
        phone.CallReceived += OnCallReceived;
        phone.EmailReceived += OnEmailReceived;

        phone.SendMessage("Привет! Как дела в колледже?");
        phone.SendCall("+375 (33) 333-33-33");
        phone.SendEmail("Кураторский час в 15:00");
    }
}