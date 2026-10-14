using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Input;

namespace Module_6_3
{
    public partial class MainWindow : Window
    {
        private string filePath = "library.txt";
        private List<Book> books = new List<Book>();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // При первом запуске — создаём файл с начальными книгами
            if (!File.Exists(filePath))
            {
                var initial = new List<string>
                {
                    "1|Война и мир|Лев Толстой|1869|1",
                    "2|Мастер и Маргарита|Михаил Булгаков|1967|1",
                    "3|Преступление и наказание|Фёдор Достоевский|1866|1",
                    "4|Евгений Онегин|Александр Пушкин|1833|1",
                    "5|Анна Каренина|Лев Толстой|1878|1"
                };
                File.WriteAllLines(filePath, initial);
            }

            LoadFromFile();
        }

        // ЧТЕНИЕ ИЗ ФАЙЛА
        // Формат: Id|Title|Author|Year|Available
        private void LoadFromFile()
        {
            books.Clear();

            if (File.Exists(filePath))
            {
                string[] lines = File.ReadAllLines(filePath);

                foreach (string line in lines)
                {
                    if (string.IsNullOrWhiteSpace(line)) continue;

                    string[] parts = line.Split('|');
                    if (parts.Length < 5) continue;

                    int id, year;
                    int.TryParse(parts[0], out id);
                    int.TryParse(parts[3], out year);

                    books.Add(new Book
                    {
                        Id = id,
                        Title = parts[1],
                        Author = parts[2],
                        Year = year,
                        Available = parts[4] == "1"
                    });
                }
            }

            grid.ItemsSource = null;
            grid.ItemsSource = books;
            UpdateStatus();
        }

        // ЗАПИСЬ В ФАЙЛ
        private void SaveToFile()
        {
            List<string> lines = new List<string>();

            foreach (Book b in books)
            {
                lines.Add($"{b.Id}|{b.Title}|{b.Author}|{b.Year}|{(b.Available ? 1 : 0)}");
            }

            File.WriteAllLines(filePath, lines);
        }

        // ПОИСК
        private void BtnSearch_Click(object sender, RoutedEventArgs e)
        {
            Search();
        }

        private void TxtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                Search();
                e.Handled = true;
            }
        }

        private void Search()
        {
            string query = txtSearch.Text.ToLower().Trim();

            if (string.IsNullOrEmpty(query))
            {
                LoadFromFile();
                return;
            }

            // Фильтруем по названию и автору
            var filtered = new List<Book>();
            foreach (Book b in books)
            {
                if (b.Title.ToLower().Contains(query) ||
                    b.Author.ToLower().Contains(query))
                {
                    filtered.Add(b);
                }
            }

            grid.ItemsSource = null;
            grid.ItemsSource = filtered;

            lblStatus.Text = $"Найдено: {filtered.Count}";
        }

        private void BtnShowAll_Click(object sender, RoutedEventArgs e)
        {
            txtSearch.Clear();
            LoadFromFile();
        }

        // АРЕНДОВАТЬ КНИГУ
        private void BtnBorrow_Click(object sender, RoutedEventArgs e)
        {
            Book b = grid.SelectedItem as Book;

            if (b == null)
            {
                MessageBox.Show("Выберите книгу в списке.", "Внимание",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (!b.Available)
            {
                MessageBox.Show($"Книга «{b.Title}» уже арендована.", "Внимание",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            b.Available = false;
            SaveToFile();
            LoadFromFile();

            MessageBox.Show($"Книга «{b.Title}» успешно арендована.", "Готово",
                MessageBoxButton.OK, MessageBoxImage.Information);
        }

        // ВЕРНУТЬ КНИГУ
        private void BtnReturn_Click(object sender, RoutedEventArgs e)
        {
            Book b = grid.SelectedItem as Book;

            if (b == null)
            {
                MessageBox.Show("Выберите книгу в списке.", "Внимание",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (b.Available)
            {
                MessageBox.Show($"Книга «{b.Title}» уже доступна.", "Внимание",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            b.Available = true;
            SaveToFile();
            LoadFromFile();

            MessageBox.Show($"Книга «{b.Title}» возвращена в библиотеку.", "Готово",
                MessageBoxButton.OK, MessageBoxImage.Information);
        }

        // ДОБАВИТЬ КНИГУ
        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            // Создаём окно для ввода
            var dialog = new AddBookWindow();

            if (dialog.ShowDialog() == true)
            {
                // Новый ID
                int newId = 1;
                foreach (Book b in books)
                    if (b.Id >= newId) newId = b.Id + 1;

                books.Add(new Book
                {
                    Id = newId,
                    Title = dialog.BookTitle,
                    Author = dialog.BookAuthor,
                    Year = dialog.BookYear,
                    Available = true
                });

                SaveToFile();
                LoadFromFile();
            }
        }

        // СТАТУС ВНИЗУ
        private void UpdateStatus()
        {
            int total = books.Count;
            int available = 0;

            foreach (Book b in books)
                if (b.Available) available++;

            lblStatus.Text = $"Всего книг: {total}   Доступно: {available}   Арендовано: {total - available}";
        }
    }

    // КЛАСС КНИГИ
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public int Year { get; set; }
        public bool Available { get; set; }

        // Отображаемый статус
        public string Status
        {
            get { return Available ? "✓ Доступна" : "✗ Арендована"; }
        }
    }
}