using System; // Подключение пространства имён System (Console)

// ==================== ДЕЛЕГАТ СОБЫТИЯ ====================
// Делегат — это "тип-указатель на метод". Он описывает сигнатуру
// методов, которые можно будет вызывать через событие.
// TempHandler — метод, принимающий один double и возвращающий void.
delegate void TempHandler(double temp);

// ==================== ИЗДАТЕЛЬ (PUBLISHER) ====================
// Класс TemperatureSensor — источник события.
// Он сообщает всем подписчикам: "температура изменилась".
class TemperatureSensor
{
    // Событие — это обёртка над делегатом с ключевым словом event.
    // Снаружи класса можно ТОЛЬКО подписываться (+=) и отписываться (-=),
    // но НЕЛЬЗЯ вызвать событие напрямую — это защита инкапсуляции.
    public event TempHandler TemperatureChanged;

    // Текущее значение температуры (начальное = 20)
    private double currentTemp = 20;

    // Метод, через который меняется температура.
    // Именно здесь генерируется (raise) событие.
    public void SetTemperature(double newTemp)
    {
        // Событие срабатывает только если значение действительно изменилось
        if (newTemp != currentTemp)
        {
            currentTemp = newTemp;
            Console.WriteLine($"\nДатчик: температура = {newTemp}°C");

            // ?.Invoke — безопасный вызов события.
            // Если подписчиков нет, TemperatureChanged == null,
            // и без ?. было бы NullReferenceException.
            // ?. значит: "вызови Invoke, только если событие не null".
            TemperatureChanged?.Invoke(newTemp);
        }
    }
}

// ==================== ПОДПИСЧИК (SUBSCRIBER) ====================
// Класс Thermostat — реагирует на изменение температуры.
// Он не знает о других подписчиках и о самом датчике —
// просто получает уведомление "температура стала такой-то".
class Thermostat
{
    private string name; // Имя термостата (для различия в выводе)
    private double min;  // Нижняя граница комфортной температуры
    private double max;  // Верхняя граница комфортной температуры

    // Конструктор: задаём имя и диапазон комфорта
    public Thermostat(string name, double min, double max)
    {
        this.name = name;
        this.min = min;
        this.max = max;
    }

    // ОБРАБОТЧИК СОБЫТИЯ — этот метод будет вызван при событии.
    // Сигнатура совпадает с делегатом TempHandler: (double) -> void.
    public void OnTemperatureChanged(double temp)
    {
        // Если температура ниже минимума — холодно
        if (temp < min)
            Console.WriteLine($"  [{name}] ХОЛОДНО -->> включаем отопление");
        // Если выше максимума — жарко
        else if (temp > max)
            Console.WriteLine($"  [{name}] ТЕПЛО -->> выключаем отопление");
        // В пределах диапазона — всё хорошо
        else
            Console.WriteLine($"  [{name}] Комфортно");
    }
}

// ==================== ТОЧКА ВХОДА ====================
class Ex5
{
    static void Main()
    {
        // Создаём издателя (источник событий)
        TemperatureSensor sensor = new TemperatureSensor();

        // Создаём двух подписчиков с разными диапазонами комфорта
        Thermostat t1 = new Thermostat("Термостат-1", 18, 25);
        Thermostat t2 = new Thermostat("Термостат-2", 20, 23); // более узкий диапазон

        // Подписка на событие.
        // += добавляет метод-обработчик в список делегатов события.
        // Теперь при вызове TemperatureChanged?.Invoke(...) будут вызваны
        // ОБА метода — сначала t1.OnTemperatureChanged, потом t2.OnTemperatureChanged.
        sensor.TemperatureChanged += t1.OnTemperatureChanged;
        sensor.TemperatureChanged += t2.OnTemperatureChanged;

        // Изменяем температуру — оба термостата реагируют на каждое изменение.
        // 15 — ниже обоих минимумов
        sensor.SetTemperature(15);
        // 22 — для t1 комфортно, для t2 комфортно
        sensor.SetTemperature(22);
        // 28 — выше обоих максимумов
        sensor.SetTemperature(28);
    }
}