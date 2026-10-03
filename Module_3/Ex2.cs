using System;

class PhoneNotification
{
    public event Action<string> MessageReceived;
    public event Action<string> CallReceived;
    public event Action<string> EmailReceived;

    public void SendMessage(string text)
    {
        Console.WriteLine($"\n[Уведомление] Сообщение: {text}");
        CallReceived?.Invoke(text);
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
    static void Main()
    {
        PhoneNotification phone = new PhoneNotification();

        phone.MessageReceived += text => Console.WriteLine($"SMS: {text}");
        phone.CallReceived += from => Console.WriteLine($"Входящий звонок: {from}");
        phone.EmailReceived += subject => Console.WriteLine($"EMAIL: {subject}");

        phone.SendMessage("Привет! Как дела в колледже? =)");
        phone.SendCall("+375 (33) 333-33-33");
        phone.SendEmail("Кураторский час в 15:00");
    }
}