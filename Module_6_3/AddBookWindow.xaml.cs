using System.Windows;

namespace Module_6_3
{
    public partial class AddBookWindow : Window
    {
        // Публичные свойства — доступны из MainWindow после закрытия диалога
        public string BookTitle { get; private set; }
        public string BookAuthor { get; private set; }
        public int BookYear { get; private set; }

        public AddBookWindow()
        {
            InitializeComponent();
        }

        // OK — сохранить и закрыть
        private void BtnOk_Click(object sender, RoutedEventArgs e)
        {
            // Проверка: не пустые поля
            if (string.IsNullOrWhiteSpace(txtTitle.Text))
            {
                MessageBox.Show("Введите название книги.", "Внимание",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                txtTitle.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtAuthor.Text))
            {
                MessageBox.Show("Введите автора книги.", "Внимание",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                txtAuthor.Focus();
                return;
            }

            // Проверка года
            int year;
            if (!int.TryParse(txtYear.Text, out year) || year < 0 || year > 2100)
            {
                MessageBox.Show("Введите корректный год (например, 1869).", "Внимание",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                txtYear.Focus();
                return;
            }

            // Сохраняем значения в свойства
            BookTitle = txtTitle.Text.Trim();
            BookAuthor = txtAuthor.Text.Trim();
            BookYear = year;

            // DialogResult = true закроет окно и вернёт true в ShowDialog()
            DialogResult = true;
        }

        // Отмена — закрыть без сохранения
        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}