using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text.Json.Serialization;

namespace Module_9_2
{
    // Класс "Сотрудник"
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Role { get; set; }

        // Переопределяем ToString(), чтобы в ComboBox (выпадающем списке) 
        // сотрудники отображались красиво: "Иван (Программист)", а не "Module_9_2.Employee".
        public override string ToString() => $"{Name} ({Role})";
    }

    // Класс "Проект"
    // Добавили INotifyPropertyChanged для обновления таблицы при изменении имени или статуса
    public class Project : INotifyPropertyChanged
    {
        // Приватные поля. Нужны, чтобы в сеттерах (set) вызывать OnPropertyChanged
        private string _name;
        private string _status;

        public int Id { get; set; }

        public string Name
        {
            get => _name;
            set { _name = value; OnPropertyChanged(); } // При изменении имени уведомляем интерфейс
        }

        public string Status
        {
            get => _status;
            set { _status = value; OnPropertyChanged(); } // При изменении имени уведомляем интерфейс
        }

        // Для удобного отображения в ComboBox
        public override string ToString() => Name;

        // Стандартная реализация INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        // [CallerMemberName] автоматически подставляет имя свойства, из которого вызван метод.
        // То есть нам не нужно вручную писать OnPropertyChanged("Name") или OnPropertyChanged("Status").
        protected void OnPropertyChanged([CallerMemberName] string name = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }

    // Класс "Задача"
    public class ProjectTask : INotifyPropertyChanged
    {
        private int _progress;

        public int Id { get; set; }
        public string Title { get; set; }
        public int ProjectId { get; set; }
        public int? EmployeeId { get; set; }

        public int Progress
        {
            get => _progress;
            set
            {
                _progress = value;
                OnPropertyChanged();
                // Если прогресс стал 100%, статус должен поменяться на "Выполнена".
                // Поэтому мы принудительно уведомляем интерфейс об изменении свойства Status
                OnPropertyChanged(nameof(Status));
            }
        }

        [JsonIgnore]
        // Атрибут [JsonIgnore] говорит программе: "Не сохраняй это свойство в файл .json".
        // Ведь при загрузке из файла это свойство само пересчитается.
        public string Status => Progress == 100 ? "Выполнена" : "В работе";

        // Эти свойства используются только для отображения в таблице (DataGrid).
        // Они заполняются вручную в коде MainWindow, когда мы подтягиваем данные.
        // В файл JSON их сохранять не нужно.
        [JsonIgnore] public string ProjectName { get; set; }
        [JsonIgnore] public string EmployeeName { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}