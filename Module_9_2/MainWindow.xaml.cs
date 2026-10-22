using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Windows;
using System.Windows.Controls;

namespace Module_9_2
{
    public partial class MainWindow : Window
    {
        // ObservableCollection используется вместо List, потому что она автоматически
        // уведомляет DataGrid (таблицу) об изменениях (добавление, удаление элементов).
        public ObservableCollection<Employee> Employees { get; set; } = new();
        public ObservableCollection<Project> Projects { get; set; } = new();
        public ObservableCollection<ProjectTask> Tasks { get; set; } = new();

        // Настройки для сохранения JSON с отступами (чтобы файл можно было читать глазами)
        private JsonSerializerOptions jsonOptions = new JsonSerializerOptions { WriteIndented = true };

        public MainWindow()
        {
            InitializeComponent();
            LoadData();
            RefreshUI();
        }

        private void RefreshUI()
        {
            // Обновляем таблицы
            gridEmployees.ItemsSource = null; gridEmployees.ItemsSource = Employees;
            gridProjects.ItemsSource = null; gridProjects.ItemsSource = Projects;

            // Обновляем ComboBox
            cmbTaskProject.ItemsSource = null; cmbTaskProject.ItemsSource = Projects;
            cmbTaskEmployee.ItemsSource = null; cmbTaskEmployee.ItemsSource = Employees;

            // Связываем задачи с именами. Берем ID проекта/сотрудника, ищем его имя в списке
            // и записываем в свойства ProjectName и EmployeeName для красивого вывода в таблице.
            foreach (var task in Tasks)
            {
                task.ProjectName = Projects.FirstOrDefault(p => p.Id == task.ProjectId)?.Name ?? "Неизвестно";
                task.EmployeeName = Employees.FirstOrDefault(e => e.Id == task.EmployeeId)?.Name ?? "Не назначен";
            }

            gridTasks.ItemsSource = null; gridTasks.ItemsSource = Tasks;
        }

        // Сотрудники
        private void BtnAddEmp_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtEmpName.Text)) return;

            // Генерация нового ID (максимальный + 1)
            int id = Employees.Any() ? Employees.Max(x => x.Id) + 1 : 1;
            Employees.Add(new Employee { Id = id, Name = txtEmpName.Text, Role = txtEmpRole.Text });

            // Очистка полей и обновление интерфейса
            txtEmpName.Clear(); txtEmpRole.Clear();
            RefreshUI();
        }

        private void BtnDelEmp_Click(object sender, RoutedEventArgs e)
        {
            if (gridEmployees.SelectedItem is Employee emp)
            {
                Employees.Remove(emp);

                // Защита целостности данных: если удаляем сотрудника, 
                // снимаем его со всех задач, где он был назначен.
                foreach (var t in Tasks.Where(t => t.EmployeeId == emp.Id)) t.EmployeeId = null;
                RefreshUI();
            }
        }

        // Проекты
        private void BtnAddProj_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtProjName.Text)) return;
            int id = Projects.Any() ? Projects.Max(x => x.Id) + 1 : 1;
            string status = (cmbProjStatus.SelectedItem as ComboBoxItem)?.Content.ToString() ?? "Активен";

            Projects.Add(new Project { Id = id, Name = txtProjName.Text, Status = status });
            txtProjName.Clear();
            cmbProjStatus.SelectedIndex = 0;
            RefreshUI();
        }

        // Клик по таблице проектов
        private void GridProjects_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (gridProjects.SelectedItem is Project proj)
            {
                // Подставляем данные выбранного проекта в поля ввода
                txtProjName.Text = proj.Name;
                foreach (ComboBoxItem item in cmbProjStatus.Items)
                {
                    if (item.Content.ToString() == proj.Status)
                    {
                        cmbProjStatus.SelectedItem = item;
                        break;
                    }
                }
            }
        }

        // Обновление проекта (в т.ч. смена статуса на "Завершен")
        private void BtnUpdateProj_Click(object sender, RoutedEventArgs e)
        {
            if (gridProjects.SelectedItem is Project proj && !string.IsNullOrWhiteSpace(txtProjName.Text))
            {
                proj.Name = txtProjName.Text;
                proj.Status = (cmbProjStatus.SelectedItem as ComboBoxItem)?.Content.ToString() ?? "Активен";

                RefreshUI();
                txtProjName.Clear();
            }
        }

        private void BtnDelProj_Click(object sender, RoutedEventArgs e)
        {
            if (gridProjects.SelectedItem is Project proj)
            {
                Projects.Remove(proj);

                // Каскадное удаление: при удалении проекта удаляем все его задачи
                var tasksToRemove = Tasks.Where(t => t.ProjectId == proj.Id).ToList();
                foreach (var t in tasksToRemove) Tasks.Remove(t);
                RefreshUI();
            }
        }

        // Задачи
        private void BtnAddTask_Click(object sender, RoutedEventArgs e)
        {
            // Проверка, что введено имя и выбран проект
            if (string.IsNullOrWhiteSpace(txtTaskTitle.Text) || cmbTaskProject.SelectedValue == null) return;

            // Безопасный парсинг прогресса и ограничение значений от 0 до 100
            if (!int.TryParse(txtTaskProgress.Text, out int progress)) progress = 0;
            progress = Math.Clamp(progress, 0, 100);

            int id = Tasks.Any() ? Tasks.Max(x => x.Id) + 1 : 1;

            Tasks.Add(new ProjectTask
            {
                Id = id,
                Title = txtTaskTitle.Text,
                ProjectId = (int)cmbTaskProject.SelectedValue,
                EmployeeId = cmbTaskEmployee.SelectedValue as int?,
                Progress = progress
            });

            txtTaskTitle.Clear(); txtTaskProgress.Text = "0";
            RefreshUI();
        }

        // Клик по таблице задач
        private void GridTasks_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (gridTasks.SelectedItem is ProjectTask task)
            {
                txtTaskTitle.Text = task.Title;
                cmbTaskProject.SelectedValue = task.ProjectId;
                cmbTaskEmployee.SelectedValue = task.EmployeeId;
                txtTaskProgress.Text = task.Progress.ToString();
            }
        }

        // Обновление задачи (изменение прогресса, исполнителя и т.д.)
        private void BtnUpdateTask_Click(object sender, RoutedEventArgs e)
        {
            if (gridTasks.SelectedItem is ProjectTask task && cmbTaskProject.SelectedValue != null)
            {
                if (!int.TryParse(txtTaskProgress.Text, out int progress)) progress = 0;

                task.Title = txtTaskTitle.Text;
                task.ProjectId = (int)cmbTaskProject.SelectedValue;
                task.EmployeeId = cmbTaskEmployee.SelectedValue as int?;
                task.Progress = Math.Clamp(progress, 0, 100); // При изменении статус обновится сам

                RefreshUI();
            }
        }

        private void BtnDelTask_Click(object sender, RoutedEventArgs e)
        {
            if (gridTasks.SelectedItem is ProjectTask task)
            {
                Tasks.Remove(task);
                RefreshUI();
            }
        }

        // Отчёты
        private void BtnGenerateReport_Click(object sender, RoutedEventArgs e)
        {
            // StringBuilder используется для эффективной сборки большого текста
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("================ ОТЧЕТ ПО ПРОЕКТАМ ================\n");

            if (!Projects.Any()) sb.AppendLine("Проектов пока нет.");

            foreach (var proj in Projects)
            {
                var projTasks = Tasks.Where(t => t.ProjectId == proj.Id).ToList();

                // Считаем средний прогресс по всем задачам проекта
                int avgProgress = projTasks.Any() ? (int)projTasks.Average(t => t.Progress) : 0;

                // Если все задачи выполнены, проект считается завершенным
                string displayStatus = avgProgress == 100 && projTasks.Any() ? "ЗАВЕРШЕН" : proj.Status;

                sb.AppendLine($"Проект: {proj.Name} (ID: {proj.Id})");
                sb.AppendLine($"Статус проекта: {displayStatus} | Средний прогресс: {avgProgress}%");

                if (!projTasks.Any()) sb.AppendLine("  (Задач по этому проекту нет)");

                foreach (var t in projTasks)
                {
                    // Ищем имя исполнителя
                    string empName = Employees.FirstOrDefault(emp => emp.Id == t.EmployeeId)?.Name ?? "!!! НЕ НАЗНАЧЕН !!!";

                    // Форматируем строку (t.Title,-25 означает выравнивание по левому краю с шириной 25 символов)
                    sb.AppendLine($"  - [{t.Id}] {t.Title,-25} | Статус: {t.Status,-10} ({t.Progress}%) | Исполнитель: {empName}");
                }
                sb.AppendLine(new string('-', 50));
            }

            txtReport.Text = sb.ToString();
        }

        // JSON сохранение. При закрытии окна автоматически сохраняем все коллекции в файлы
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            try
            {
                File.WriteAllText("employees.json", JsonSerializer.Serialize(Employees, jsonOptions));
                File.WriteAllText("projects.json", JsonSerializer.Serialize(Projects, jsonOptions));
                File.WriteAllText("tasks.json", JsonSerializer.Serialize(Tasks, jsonOptions));
            }
            catch { /* Игнорируем ошибки при закрытии */ }
        }

        // При запуске читаем файлы (если они есть)
        private void LoadData()
        {
            try
            {
                if (File.Exists("employees.json")) Employees = JsonSerializer.Deserialize<ObservableCollection<Employee>>(File.ReadAllText("employees.json")) ?? new();
                if (File.Exists("projects.json")) Projects = JsonSerializer.Deserialize<ObservableCollection<Project>>(File.ReadAllText("projects.json")) ?? new();
                if (File.Exists("tasks.json")) Tasks = JsonSerializer.Deserialize<ObservableCollection<ProjectTask>>(File.ReadAllText("tasks.json")) ?? new();
            }
            catch { /* Если файлы повреждены, начинаем с чистого листа */ }
        }
    }
}