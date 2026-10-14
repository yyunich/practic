using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Windows;
using System.Windows.Media;

namespace Module_6_4
{
    public partial class MainWindow : Window
    {
        private string filePath = "finance.txt";
        private List<Operation> operations = new List<Operation>();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // Устанавливаем сегодняшнюю дату
            dpDate.SelectedDate = DateTime.Today;

            LoadFromFile();
        }

        // ЧТЕНИЕ ИЗ ФАЙЛА
        // Формат: Id|Date|Description|Amount|Type
        private void LoadFromFile()
        {
            operations.Clear();

            if (File.Exists(filePath))
            {
                string[] lines = File.ReadAllLines(filePath);

                foreach (string line in lines)
                {
                    if (string.IsNullOrWhiteSpace(line)) continue;

                    string[] parts = line.Split('|');
                    if (parts.Length < 5) continue;

                    int id;
                    int.TryParse(parts[0], out id);

                    double amount;
                    double.TryParse(parts[3],
                        NumberStyles.Any,
                        CultureInfo.InvariantCulture,
                        out amount);

                    operations.Add(new Operation
                    {
                        Id = id,
                        Date = parts[1],
                        Description = parts[2],
                        Amount = amount,
                        Type = parts[4]
                    });
                }
            }

            // Сортируем по дате
            operations.Sort((a, b) => string.Compare(a.Date, b.Date));

            grid.ItemsSource = null;
            grid.ItemsSource = operations;

            UpdateTotals();
        }

        // ЗАПИСЬ В ФАЙЛ
        private void SaveToFile()
        {
            List<string> lines = new List<string>();

            foreach (Operation op in operations)
            {
                lines.Add($"{op.Id}|{op.Date}|{op.Description}|" +
                          $"{op.Amount.ToString(CultureInfo.InvariantCulture)}|{op.Type}");
            }

            File.WriteAllLines(filePath, lines);
        }

        // ДОБАВИТЬ ДОХОД
        private void BtnIncome_Click(object sender, RoutedEventArgs e)
        {
            AddOperation("income");
        }

        // ДОБАВИТЬ РАСХОД
        private void BtnExpense_Click(object sender, RoutedEventArgs e)
        {
            AddOperation("expense");
        }

        private void AddOperation(string type)
        {
            // Проверка описания
            if (string.IsNullOrWhiteSpace(txtDesc.Text))
            {
                MessageBox.Show("Введите описание операции.", "Внимание",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                txtDesc.Focus();
                return;
            }

            // Проверка суммы
            double amount;
            if (!double.TryParse(txtAmount.Text.Replace(',', '.'),
                NumberStyles.Any,
                CultureInfo.InvariantCulture,
                out amount) || amount <= 0)
            {
                MessageBox.Show("Введите корректную сумму (положительное число).",
                    "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);
                txtAmount.Focus();
                return;
            }

            // Проверка даты
            if (dpDate.SelectedDate == null)
            {
                MessageBox.Show("Выберите дату.", "Внимание",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Новый ID
            int newId = 1;
            foreach (Operation op in operations)
                if (op.Id >= newId) newId = op.Id + 1;

            // Добавляем операцию
            operations.Add(new Operation
            {
                Id = newId,
                Date = dpDate.SelectedDate.Value.ToString("yyyy-MM-dd"),
                Description = txtDesc.Text.Trim(),
                Amount = amount,
                Type = type
            });

            // Очищаем поля
            txtDesc.Clear();
            txtAmount.Clear();
            txtDesc.Focus();

            SaveToFile();
            LoadFromFile();
        }

        // УДАЛИТЬ ЗАПИСЬ
        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            Operation op = grid.SelectedItem as Operation;

            if (op == null)
            {
                MessageBox.Show("Выберите запись в списке.", "Внимание",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (MessageBox.Show($"Удалить запись «{op.Description}»?",
                "Подтверждение", MessageBoxButton.YesNo) != MessageBoxResult.Yes)
                return;

            operations.Remove(op);

            SaveToFile();
            LoadFromFile();
        }

        // ИТОГИ ВНИЗУ
        private void UpdateTotals()
        {
            double income = 0;
            double expense = 0;

            foreach (Operation op in operations)
            {
                if (op.Type == "income")
                    income += op.Amount;
                else
                    expense += op.Amount;
            }

            double balance = income - expense;

            lblIncome.Text = income.ToString("N2") + " Br";
            lblExpense.Text = expense.ToString("N2") + " Br";
            lblBalance.Text = balance.ToString("N2") + " Br";

            // Цвет баланса
            lblBalance.Foreground = balance >= 0 ? Brushes.Green : Brushes.Red;
        }
    }
    // КЛАСС ОПЕРАЦИИ
    public class Operation
    {
        public int Id { get; set; }
        public string Date { get; set; }
        public string Description { get; set; }
        public double Amount { get; set; }
        public string Type { get; set; }   // "income" / "expense"

        // Отображаемый тип
        public string TypeDisplay
        {
            get { return Type == "income" ? "💰 Доход" : "💸 Расход"; }
        }

        // Отображаемая сумма со знаком
        public string AmountDisplay
        {
            get
            {
                string sign = Type == "income" ? "+" : "−";
                return $"{sign}{Amount:N2} Br";
            }
        }
    }
}