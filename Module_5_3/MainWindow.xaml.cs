using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

namespace Module_5_3
{
    public partial class MainWindow : Window
    {
        // Коллекция задач — WPF сам обновит UI при изменениях
        private ObservableCollection<TaskItem> tasks = new ObservableCollection<TaskItem>();

        public MainWindow()
        {
            InitializeComponent();

            // Привязываем коллекцию к ListBox
            lstTasks.ItemsSource = tasks;

            UpdateStatus();
        }

        // ДОБАВИТЬ ЗАДАЧУ
        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            string text = txtTask.Text.Trim();

            // Проверка: не пусто?
            if (string.IsNullOrEmpty(text))
            {
                MessageBox.Show("Введите текст задачи.", "Внимание",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Добавляем задачу
            tasks.Add(new TaskItem { Text = text, Done = false });

            // Очищаем поле и ставим фокус
            txtTask.Clear();
            txtTask.Focus();

            UpdateStatus();
        }

        // Enter в поле ввода = Добавить
        private void TxtTask_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                BtnAdd_Click(sender, e);
                e.Handled = true;
            }
        }

        // ОТМЕТИТЬ ВЫПОЛНЕННОЙ
        private void BtnToggle_Click(object sender, RoutedEventArgs e)
        {
            if (lstTasks.SelectedItem is TaskItem task)
            {
                task.Done = !task.Done;
                UpdateStatus();
            }
            else
            {
                MessageBox.Show("Выберите задачу в списке.", "Внимание",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        // УДАЛИТЬ ЗАДАЧУ
        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (lstTasks.SelectedItem is TaskItem task)
            {
                tasks.Remove(task);
                UpdateStatus();
            }
            else
            {
                MessageBox.Show("Выберите задачу в списке.", "Внимание",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        // ОЧИСТИТЬ ВСЁ
        private void BtnClear_Click(object sender, RoutedEventArgs e)
        {
            if (tasks.Count == 0) return;

            var result = MessageBox.Show(
                "Удалить все задачи?",
                "Подтверждение",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                tasks.Clear();
                UpdateStatus();
            }
        }

        // ОБНОВЛЕНИЕ СТАТУСА ===
        private void UpdateStatus()
        {
            int total = tasks.Count;
            int done = 0;

            foreach (var t in tasks)
                if (t.Done) done++;

            lblStatus.Text = $"Всего: {total}   Выполнено: {done}   Осталось: {total - done}";
        }
    }

    // Класс задачи — с уведомлением об изменениях (INotifyPropertyChanged)
    public class TaskItem : INotifyPropertyChanged
    {
        private string _text;
        private bool _done;

        // Текст задачи
        public string Text
        {
            get => _text;
            set
            {
                _text = value;
                OnPropertyChanged(nameof(Text));
                OnPropertyChanged(nameof(DisplayText));
            }
        }

        // Выполнена ли задача
        public bool Done
        {
            get => _done;
            set
            {
                _done = value;
                OnPropertyChanged(nameof(Done));
                OnPropertyChanged(nameof(DisplayText));
            }
        }

        // Отображаемый текст: "○ Задача" или "✓ Задача"
        public string DisplayText => (Done ? "✓ " : "○ ") + Text;

        // Реализация INotifyPropertyChanged — WPF использует это для обновления UI
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}