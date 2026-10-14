using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace Module_6_1
{
    public partial class MainWindow : Window
    {
        // Путь к файлу (в папке с программой)
        private string filePath = "employees.txt";

        // Список всех сотрудников
        private List<Employee> employees = new List<Employee>();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadFromFile();
        }

        // ЧТЕНИЕ ИЗ ФАЙЛА
        private void LoadFromFile()
        {
            employees.Clear();

            // Если файла нет — ничего не делаем
            if (!File.Exists(filePath))
            {
                grid.ItemsSource = employees;
                return;
            }

            // Читаем все строки
            string[] lines = File.ReadAllLines(filePath);

            foreach (string line in lines)
            {
                if (string.IsNullOrWhiteSpace(line)) continue;

                // Разбиваем строку по |
                string[] parts = line.Split('|');
                if (parts.Length < 5) continue;

                employees.Add(new Employee
                {
                    Id = int.Parse(parts[0]),
                    Name = parts[1],
                    Position = parts[2],
                    Department = parts[3],
                    Salary = double.Parse(parts[4])
                });
            }

            grid.ItemsSource = null;
            grid.ItemsSource = employees;
        }

        // ЗАПИСЬ В ФАЙЛ
        private void SaveToFile()
        {
            List<string> lines = new List<string>();

            foreach (Employee emp in employees)
            {
                lines.Add($"{emp.Id}|{emp.Name}|{emp.Position}|{emp.Department}|{emp.Salary}");
            }

            File.WriteAllLines(filePath, lines);
        }

        // ДОБАВИТЬ
        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBox.Show("Введите ФИО.");
                return;
            }

            // Определяем новый ID
            int newId = 1;
            foreach (Employee emp in employees)
                if (emp.Id >= newId) newId = emp.Id + 1;

            // Создаём сотрудника
            double salary;
            double.TryParse(txtSalary.Text, out salary);

            employees.Add(new Employee
            {
                Id = newId,
                Name = txtName.Text,
                Position = txtPosition.Text,
                Department = txtDepartment.Text,
                Salary = salary
            });

            SaveToFile();
            LoadFromFile();
            ClearInputs();
        }

        // ИЗМЕНИТЬ
        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            Employee emp = grid.SelectedItem as Employee;
            if (emp == null)
            {
                MessageBox.Show("Выберите сотрудника.");
                return;
            }

            emp.Name = txtName.Text;
            emp.Position = txtPosition.Text;
            emp.Department = txtDepartment.Text;
            double.TryParse(txtSalary.Text, out double salary);
            emp.Salary = salary;

            SaveToFile();
            LoadFromFile();
            ClearInputs();
        }

        // УДАЛИТЬ
        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            Employee emp = grid.SelectedItem as Employee;
            if (emp == null)
            {
                MessageBox.Show("Выберите сотрудника.");
                return;
            }

            if (MessageBox.Show($"Удалить {emp.Name}?", "Подтверждение",
                MessageBoxButton.YesNo) != MessageBoxResult.Yes) return;

            employees.Remove(emp);

            SaveToFile();
            LoadFromFile();
            ClearInputs();
        }

        // ВЫБОР СТРОКИ → заполняем поля
        private void Grid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Employee emp = grid.SelectedItem as Employee;
            if (emp != null)
            {
                txtName.Text = emp.Name;
                txtPosition.Text = emp.Position;
                txtDepartment.Text = emp.Department;
                txtSalary.Text = emp.Salary.ToString();
            }
        }

        private void ClearInputs()
        {
            txtName.Clear();
            txtPosition.Clear();
            txtDepartment.Clear();
            txtSalary.Clear();
        }
    }

    // Класс сотрудника
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Position { get; set; }
        public string Department { get; set; }
        public double Salary { get; set; }
    }
}