using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Module_6_2
{
    public partial class MainWindow : Window
    {
        // Файл для сохранения
        private string filePath = "tasks.txt";

        // Список всех задач
        private List<TaskItem> tasks = new List<TaskItem>();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadFromFile();
        }

        // ЧТЕНИЕ ИЗ ФАЙЛА
        // Формат строки: Id|Title|Done
        private void LoadFromFile()
        {
            tasks.Clear();

            if (File.Exists(filePath))
            {
                string[] lines = File.ReadAllLines(filePath);

                foreach (string line in lines)
                {
                    if (string.IsNullOrWhiteSpace(line)) continue;

                    string[] parts = line.Split('|');
                    if (parts.Length < 3) continue;

                    int id;
                    int.TryParse(parts[0], out id);

                    tasks.Add(new TaskItem
                    {
                        Id = id,
                        Title = parts[1],
                        Done = parts[2] == "1"
                    });
                }
            }

            // Обновляем DataGrid
            grid.ItemsSource = null;
            grid.ItemsSource = tasks;

            UpdateStatus();
        }

        // ЗАПИСЬ В ФАЙЛ
        private void SaveToFile()
        {
            List<string> lines = new List<string>();

            foreach (TaskItem t in tasks)
            {
                // "1|Купить хлеб|0"
                lines.Add($"{t.Id}|{t.Title}|{(t.Done ? 1 : 0)}");
            }

            File.WriteAllLines(filePath, lines);
        }

        // ДОБАВИТЬ ЗАДАЧУ
        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            AddTask();
        }

        // Enter в поле ввода = Добавить
        private void TxtTask_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                AddTask();
                e.Handled = true;
            }
        }

        private void AddTask()
        {
            string text = txtTask.Text.Trim();

            if (string.IsNullOrEmpty(text))
            {
                MessageBox.Show("Введите текст задачи.", "Внимание",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Новый ID
            int newId = 1;
            foreach (TaskItem t in tasks)
                if (t.Id >= newId) newId = t.Id + 1;

            // Добавляем задачу
            tasks.Add(new TaskItem
            {
                Id = newId,
                Title = text,
                Done = false
            });

            // Очищаем поле, ставим фокус
            txtTask.Clear();
            txtTask.Focus();

            SaveToFile();
            LoadFromFile();
        }

        // ПЕРЕКЛЮЧИТЬ СТАТУС (выполнена / не выполнена)
        private void BtnToggle_Click(object sender, RoutedEventArgs e)
        {
            TaskItem t = grid.SelectedItem as TaskItem;

            if (t == null)
            {
                MessageBox.Show("Выберите задачу в списке.", "Внимание",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Меняем статус на противоположный
            t.Done = !t.Done;

            // Сохраняем и обновляем
            int selectedIndex = grid.SelectedIndex;
            SaveToFile();
            LoadFromFile();

            // Восстанавливаем выделение
            if (selectedIndex >= 0 && selectedIndex < tasks.Count)
                grid.SelectedIndex = selectedIndex;
        }

        // УДАЛИТЬ ЗАДАЧУ
        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            TaskItem t = grid.SelectedItem as TaskItem;

            if (t == null)
            {
                MessageBox.Show("Выберите задачу в списке.", "Внимание",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Подтверждение
            if (MessageBox.Show($"Удалить задачу «{t.Title}»?", "Подтверждение",
                MessageBoxButton.YesNo) != MessageBoxResult.Yes) return;

            tasks.Remove(t);

            SaveToFile();
            LoadFromFile();
        }

        // ОБНОВИТЬ СТАТУС ВНИЗУ
        private void UpdateStatus()
        {
            int total = tasks.Count;
            int done = 0;

            foreach (TaskItem t in tasks)
                if (t.Done) done++;

            lblStatus.Text = $"Всего: {total}   Выполнено: {done}   Осталось: {total - done}";
        }
    }

    // КЛАСС ЗАДАЧИ
    public class TaskItem
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public bool Done { get; set; }

        // Свойство для отображения в DataGrid
        public string Status
        {
            get { return Done ? "✓ Выполнена" : "○ В работе"; }
        }
    }
}