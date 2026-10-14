using System;
using System.Windows;
using System.Windows.Controls;

namespace Module_5_5
{
    public partial class MainWindow : Window
    {
        // Первое число (левый операнд)
        private double firstOperand = 0;

        // Знак операции: "+", "-", "*", "/"
        private string operation = null;

        // Флаг: следующая цифра — начало нового числа
        private bool isNewInput = true;

        public MainWindow()
        {
            InitializeComponent();
        }

        // ВВОД ЦИФРЫ
        private void BtnDigit_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            string digit = btn.Tag.ToString();

            if (isNewInput)
            {
                // Начинаем новое число
                txtDisplay.Text = digit;
                isNewInput = false;
            }
            else
            {
                // Дописываем к текущему
                if (txtDisplay.Text == "0")
                    txtDisplay.Text = digit;
                else
                    txtDisplay.Text += digit;
            }
        }

        // ВВОД ЗАПЯТОЙ
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

        // ОПЕРАЦИЯ (+, -, *, /)
        private void BtnOp_Click(object sender, RoutedEventArgs e)
        {
            // Если уже была операция и введено второе число — сначала считаем
            if (operation != null && !isNewInput)
            {
                BtnEquals_Click(sender, e);
            }

            Button btn = (Button)sender;

            // Парсим текущее число
            firstOperand = ParseDisplay();
            operation = btn.Tag.ToString();
            isNewInput = true;
        }

        // РАВНО
        private void BtnEquals_Click(object sender, RoutedEventArgs e)
        {
            if (operation == null) return;

            double secondOperand = ParseDisplay();
            double result = 0;

            switch (operation)
            {
                case "+":
                    result = firstOperand + secondOperand;
                    break;
                case "-":
                    result = firstOperand - secondOperand;
                    break;
                case "*":
                    result = firstOperand * secondOperand;
                    break;
                case "/":
                    if (secondOperand == 0)
                    {
                        MessageBox.Show("Деление на ноль невозможно!",
                            "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }
                    result = firstOperand / secondOperand;
                    break;
            }

            txtDisplay.Text = result.ToString("0.##########");
            operation = null;
            isNewInput = true;
        }

        // ОЧИСТИТЬ
        private void BtnClear_Click(object sender, RoutedEventArgs e)
        {
            txtDisplay.Text = "0";
            firstOperand = 0;
            operation = null;
            isNewInput = true;
        }

        // СМЕНА ЗНАКА
        private void BtnSign_Click(object sender, RoutedEventArgs e)
        {
            double val = ParseDisplay();
            txtDisplay.Text = (-val).ToString("0.##########");
        }

        // ПРОЦЕНТ
        private void BtnPercent_Click(object sender, RoutedEventArgs e)
        {
            double val = ParseDisplay();
            txtDisplay.Text = (val / 100).ToString("0.##########");
        }

        // ПАРСИНГ ДИСПЛЕЯ (запятая → точка для double)
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

        // ГОРЯЧИЕ КЛАВИШИ
        protected override void OnKeyDown(System.Windows.Input.KeyEventArgs e)
        {
            base.OnKeyDown(e);

            // Цифры 0-9
            if (e.Key >= System.Windows.Input.Key.D0 &&
                e.Key <= System.Windows.Input.Key.D9)
            {
                string digit = ((int)e.Key - (int)System.Windows.Input.Key.D0).ToString();
                txtDisplay.Text = isNewInput ? digit : txtDisplay.Text + digit;
                isNewInput = false;
            }
            // Numpad 0-9
            else if (e.Key >= System.Windows.Input.Key.NumPad0 &&
                     e.Key <= System.Windows.Input.Key.NumPad9)
            {
                string digit = ((int)e.Key - (int)System.Windows.Input.Key.NumPad0).ToString();
                txtDisplay.Text = isNewInput ? digit : txtDisplay.Text + digit;
                isNewInput = false;
            }
            // Операции
            else if (e.Key == System.Windows.Input.Key.Add) BtnOpKey("+");
            else if (e.Key == System.Windows.Input.Key.Subtract) BtnOpKey("-");
            else if (e.Key == System.Windows.Input.Key.Multiply) BtnOpKey("*");
            else if (e.Key == System.Windows.Input.Key.Divide) BtnOpKey("/");

            // Enter = равно
            else if (e.Key == System.Windows.Input.Key.Enter) BtnEquals_Click(null, null);

            // Backspace — удалить последний символ
            else if (e.Key == System.Windows.Input.Key.Back)
            {
                if (txtDisplay.Text.Length > 1)
                    txtDisplay.Text = txtDisplay.Text.Substring(0, txtDisplay.Text.Length - 1);
                else
                    txtDisplay.Text = "0";
            }

            // Escape = C
            else if (e.Key == System.Windows.Input.Key.Escape) BtnClear_Click(null, null);
        }

        // Вспомогательный метод для операций с клавиатуры
        private void BtnOpKey(string op)
        {
            firstOperand = ParseDisplay();
            operation = op;
            isNewInput = true;
        }
    }
}