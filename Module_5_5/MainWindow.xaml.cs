using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Module_5_5
{
    public partial class MainWindow : Window
    {
        private double firstOperand = 0;
        private string operation = null;
        private bool isNewInput = true;

        public MainWindow()
        {
            InitializeComponent();
        }

        // === ВВОД ЦИФРЫ ===
        private void BtnDigit_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            string digit = btn.Tag.ToString();

            if (isNewInput)
            {
                txtDisplay.Text = digit;
                isNewInput = false;
            }
            else
            {
                if (txtDisplay.Text == "0")
                    txtDisplay.Text = digit;
                else
                    txtDisplay.Text += digit;
            }
        }

        // === ВВОД ЗАПЯТОЙ ===
        private void BtnDot_Click(object sender, RoutedEventArgs e)
        {
            if (isNewInput)
            {
                txtDisplay.Text = "0,";
                isNewInput = false;
            }
            else if (!txtDisplay.Text.Contains(","))
            {
                txtDisplay.Text += ",";
            }
        }

        // === ОПЕРАЦИЯ (+, -, *, /) ===
        private void BtnOp_Click(object sender, RoutedEventArgs e)
        {
            // Если уже была операция и введено второе число — сначала считаем
            if (operation != null && !isNewInput)
            {
                BtnEquals_Click(sender, e);
            }

            Button btn = (Button)sender;

            firstOperand = ParseDisplay();
            operation = btn.Tag.ToString();
            isNewInput = true;

            // Выводим выражение на верхнюю строку (например, "12 +")
            string opSymbol = operation;
            if (opSymbol == "*") opSymbol = "×";
            if (opSymbol == "/") opSymbol = "÷";

            txtExpression.Text = $"{firstOperand} {opSymbol}";
        }

        // === РАВНО ===
        private void BtnEquals_Click(object sender, RoutedEventArgs e)
        {
            if (operation == null) return;

            double secondOperand = ParseDisplay();
            double result = 0;

            string opSymbol = operation;
            if (opSymbol == "*") opSymbol = "×";
            if (opSymbol == "/") opSymbol = "÷";

            // Выводим полное выражение (например, "12 + 5 =")
            txtExpression.Text = $"{firstOperand} {opSymbol} {secondOperand} =";

            switch (operation)
            {
                case "+": result = firstOperand + secondOperand; break;
                case "-": result = firstOperand - secondOperand; break;
                case "*": result = firstOperand * secondOperand; break;
                case "/":
                    if (secondOperand == 0)
                    {
                        MessageBox.Show("Деление на ноль невозможно!", "Ошибка",
                            MessageBoxButton.OK, MessageBoxImage.Warning);
                        txtExpression.Text = ""; // Убираем выражение при ошибке
                        return;
                    }
                    result = firstOperand / secondOperand;
                    break;
            }

            txtDisplay.Text = result.ToString("0.##########");
            operation = null;
            isNewInput = true;
        }

        // === ОЧИСТИТЬ ===
        private void BtnClear_Click(object sender, RoutedEventArgs e)
        {
            txtDisplay.Text = "0";
            txtExpression.Text = ""; // Очищаем и историю
            firstOperand = 0;
            operation = null;
            isNewInput = true;
        }

        // === СМЕНА ЗНАКА ===
        private void BtnSign_Click(object sender, RoutedEventArgs e)
        {
            double val = ParseDisplay();
            txtDisplay.Text = (-val).ToString("0.##########");
        }

        // === ПРОЦЕНТ ===
        private void BtnPercent_Click(object sender, RoutedEventArgs e)
        {
            double val = ParseDisplay();
            txtDisplay.Text = (val / 100).ToString("0.##########");
        }

        // === ПАРСИНГ ===
        private double ParseDisplay()
        {
            string text = txtDisplay.Text.Replace(",", ".");

            if (double.TryParse(text,
                System.Globalization.NumberStyles.Any,
                System.Globalization.CultureInfo.InvariantCulture,
                out double value))
            {
                return value;
            }
            return 0;
        }

        // === ГОРЯЧИЕ КЛАВИШИ ===
        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);

            if (e.Key >= Key.D0 && e.Key <= Key.D9)
            {
                string digit = ((int)e.Key - (int)Key.D0).ToString();
                txtDisplay.Text = isNewInput ? digit : txtDisplay.Text + digit;
                isNewInput = false;
            }
            else if (e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9)
            {
                string digit = ((int)e.Key - (int)Key.NumPad0).ToString();
                txtDisplay.Text = isNewInput ? digit : txtDisplay.Text + digit;
                isNewInput = false;
            }
            else if (e.Key == Key.Add) BtnOpKey("+");
            else if (e.Key == Key.Subtract) BtnOpKey("-");
            else if (e.Key == Key.Multiply) BtnOpKey("*");
            else if (e.Key == Key.Divide) BtnOpKey("/");
            else if (e.Key == Key.Enter) BtnEquals_Click(null, null);
            else if (e.Key == Key.Back)
            {
                if (txtDisplay.Text.Length > 1)
                    txtDisplay.Text = txtDisplay.Text.Substring(0, txtDisplay.Text.Length - 1);
                else
                    txtDisplay.Text = "0";
            }
            else if (e.Key == Key.Escape) BtnClear_Click(null, null);
        }

        private void BtnOpKey(string op)
        {
            firstOperand = ParseDisplay();
            operation = op;
            isNewInput = true;

            string opSymbol = op;
            if (opSymbol == "*") opSymbol = "×";
            if (opSymbol == "/") opSymbol = "÷";

            txtExpression.Text = $"{firstOperand} {opSymbol}";
        }
    }
}