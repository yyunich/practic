using System;

// Делегат для события
delegate void TempHandler(double temp);

// Издатель — датчик температуры
class TemperatureSensor
{
    public event TempHandler TemperatureChanged;
    private double currentTemp = 20;

    public void SetTemperature(double newTemp)
    {
        if (newTemp != currentTemp)
        {
            currentTemp = newTemp;
            Console.WriteLine($"\nДатчик: температура = {newTemp}°C");
            TemperatureChanged?.Invoke(newTemp);
        }
    }
}

// Подписчик — термостат
class Thermostat
{
    private string name;
    private double min, max;

    public Thermostat(string name, double min, double max)
    {
        this.name = name;
        this.min = min;
        this.max = max;
    }

    public void OnTemperatureChanged(double temp)
    {
        if (temp < min)
            Console.WriteLine($"  [{name}] ХОЛОДНО -->> включаем отопление");
        else if (temp > max)
            Console.WriteLine($"  [{name}] ТЕПЛО -->> выключаем отопление");
        else
            Console.WriteLine($"  [{name}] Комфортно");
    }
}

class Ex5
{
    static void Main()
    {
        TemperatureSensor sensor = new TemperatureSensor();
        Thermostat t1 = new Thermostat("Термостат-1", 18, 25);
        Thermostat t2 = new Thermostat("Термостат-2", 20, 23);

        // Подписка на событие
        sensor.TemperatureChanged += t1.OnTemperatureChanged;
        sensor.TemperatureChanged += t2.OnTemperatureChanged;

        // Изменяем температуру — термостаты реагируют
        sensor.SetTemperature(15);
        sensor.SetTemperature(22);
        sensor.SetTemperature(28);
    }
}