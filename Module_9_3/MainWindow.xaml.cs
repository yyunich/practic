using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace Module_9_3
{
    public partial class MainWindow : Window
    {
        // ObservableCollection автоматически обновляет DataGrid при добавлении/удалении задач
        public ObservableCollection<TaskItem> Tasks { get; set; } = new();

        // Настройки сериализации JSON (с отступами для читаемости файла)
        private JsonSerializerOptions jsonOptions = new JsonSerializerOptions { WriteIndented = true };

        // Таймер для фоновой проверки дедлайнов (чтобы не вешать интерфейс)
        private DispatcherTimer _timer;

        public MainWindow()
        {
            InitializeComponent();
            LoadData(); // Загружаем задачи из файла при старте
            gridTasks.ItemsSource = Tasks; // Привязываем коллекцию к таблице
            UpdateStatusBar(); // Обновляем счетчики задач внизу

            // Запускаем таймер уведомлений (проверка каждую минуту)
            StartNotificationTimer();
        }

        // CRUD
        // Добавление новой задачи
        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            // Валидация: проверяем, введено ли название и выбрана ли дата
            if (string.IsNullOrWhiteSpace(txtTitle.Text) || dpDeadline.SelectedDate == null)
            {
                MessageBox.Show("Введите название задачи и выберите дедлайн!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Генерация нового ID (максимальный + 1) и безопасный парсинг прогресса
            int id = Tasks.Any() ? Tasks.Max(x => x.Id) + 1 : 1;
            string priority = (cmbPriority.SelectedItem as ComboBoxItem)?.Content.ToString() ?? "Низкий";
            int progress = int.TryParse(txtProgress.Text, out int p) ? Math.Clamp(p, 0, 100) : 0;

            Tasks.Add(new TaskItem
            {
                Id = id,
                Title = txtTitle.Text,
                Priority = priority,
                Deadline = dpDeadline.SelectedDate.Value,
                Progress = progress
            });

            ClearInputs();
            UpdateStatusBar();
        }

        // Редактирование выбранной задачи
        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (gridTasks.SelectedItem is TaskItem task && !string.IsNullOrWhiteSpace(txtTitle.Text))
            {
                // Присваиваем новые значения свойствам объекта
                task.Title = txtTitle.Text;
                task.Priority = (cmbPriority.SelectedItem as ComboBoxItem)?.Content.ToString() ?? "Низкий";
                task.Deadline = dpDeadline.SelectedDate ?? task.Deadline;
                task.Progress = int.TryParse(txtProgress.Text, out int p) ? Math.Clamp(p, 0, 100) : task.Progress;

                // Принудительно обновляем таблицу (так как некоторые свойства, например статус, зависят от других)
                gridTasks.Items.Refresh();
                UpdateStatusBar();
            }
        }

        // Удаление задачи
        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (gridTasks.SelectedItem is TaskItem task)
            {
                var result = MessageBox.Show($"Удалить задачу «{task.Title}»?", "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    Tasks.Remove(task);
                    ClearInputs();
                    UpdateStatusBar();
                }
            }
        }

        // При выборе строки в таблице подставляем данные в поля для редактирования
        private void GridTasks_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (gridTasks.SelectedItem is TaskItem task)
            {
                txtTitle.Text = task.Title;
                dpDeadline.SelectedDate = task.Deadline;
                txtProgress.Text = task.Progress.ToString();

                // Синхронизируем ComboBox (Приоритет) с данными задачи
                foreach (ComboBoxItem item in cmbPriority.Items)
                {
                    if (item.Content.ToString() == task.Priority)
                    {
                        cmbPriority.SelectedItem = item;
                        break;
                    }
                }
            }
        }

        // Очистка полей ввода после добавления или удаления
        private void ClearInputs()
        {
            txtTitle.Clear();
            dpDeadline.SelectedDate = null;
            txtProgress.Text = "0";
            cmbPriority.SelectedIndex = 0;
        }

        // Подсчет статистики и вывод в нижнюю панель (StatusBar)
        private void UpdateStatusBar()
        {
            int total = Tasks.Count;
            int done = Tasks.Count(t => t.Progress == 100);
            lblStatus.Text = $"Всего задач: {total} | Выполнено: {done} | В работе: {total - done}";
        }

        // Уведомления
        // Настройка и запуск таймера
        private void StartNotificationTimer()
        {
            _timer = new DispatcherTimer();
            _timer.Interval = TimeSpan.FromMinutes(1); // Проверка раз в минуту
            _timer.Tick += Timer_Tick;
            _timer.Start();

            // Проверяем дедлайны сразу при запуске программы
            CheckDeadlines();
        }

        // Метод, который срабатывает по таймеру
        private void Timer_Tick(object sender, EventArgs e)
        {
            CheckDeadlines();
        }

        // Проверка, не приближается ли дедлайн
        private void CheckDeadlines()
        {
            // Ищем задачи, которые не выполнены, чей дедлайн сегодня или завтра, и по которым еще не было уведомления
            var approachingTasks = Tasks.Where(t =>
                t.Progress < 100 &&
                t.Deadline.Date <= DateTime.Today.AddDays(1) &&
                t.Deadline.Date >= DateTime.Today &&
                !t.IsNotified).ToList();

            foreach (var task in approachingTasks)
            {
                MessageBox.Show($"Внимание! Задача «{task.Title}» должна быть выполнена до {task.Deadline:dd.MM.yyyy}!",
                    "Приближается дедлайн", MessageBoxButton.OK, MessageBoxImage.Warning);

                task.IsNotified = true; // Помечаем, что уведомление отправлено, чтобы окно не выскакивало каждую минуту
            }
        }

        // JSON сохранение
        // Автосохранение при закрытии окна
        private void Window_Closing(object sender, CancelEventArgs e)
        {
            try
            {
                File.WriteAllText("tasks.json", JsonSerializer.Serialize(Tasks, jsonOptions));
            }
            catch { }
        }

        // Загрузка задач из файла при старте
        private void LoadData()
        {
            try
            {
                if (File.Exists("tasks.json"))
                {
                    var loaded = JsonSerializer.Deserialize<ObservableCollection<TaskItem>>(File.ReadAllText("tasks.json"));
                    if (loaded != null) Tasks = loaded;
                }
            }
            catch { }
        }
    }

    // Модель задачи
    // Класс реализует INotifyPropertyChanged для мгновенного обновления UI
    public class TaskItem : INotifyPropertyChanged
    {
        private int _progress;

        public int Id { get; set; }
        public string Title { get; set; }
        public string Priority { get; set; }
        public DateTime Deadline { get; set; }

        public int Progress
        {
            get => _progress;
            set
            {
                _progress = value;
                OnPropertyChanged(); // Уведомляем UI, что прогресс изменился
                OnPropertyChanged(nameof(Status)); // И что статус тоже мог измениться
            }
        }

        // Вычисляемое свойство: статус зависит от прогресса и текущей даты
        public string Status
        {
            get
            {
                if (Progress == 100) return "Выполнена";
                if (Deadline.Date < DateTime.Today) return "Просрочена";
                return "В работе";
            }
        }

        // Флаг, чтобы не показывать уведомление по одной задаче бесконечно
        public bool IsNotified { get; set; } = false;

        public event PropertyChangedEventHandler PropertyChanged;

        // [CallerMemberName] автоматически подставляет имя свойства, из которого вызван метод
        protected void OnPropertyChanged([CallerMemberName] string name = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}