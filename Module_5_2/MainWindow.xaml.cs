using System;
using System.IO;
using System.Windows;
using System.Windows.Input;
using Microsoft.Win32;

namespace Module_5_2
{
    public partial class MainWindow : Window
    {
        // Полный путь к файлу (null — если файл ещё не сохранён)
        private string currentFilePath = null;

        public MainWindow()
        {
            InitializeComponent();
        }

        // ОТКРЫТИЕ ФАЙЛА
        private void MenuOpen_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new OpenFileDialog
            {
                Filter = "Текстовые файлы (*.txt)|*.txt|Все файлы (*.*)|*.*",
                Title = "Открыть файл"
            };

            if (dialog.ShowDialog() == true)
            {
                try
                {
                    txtEditor.Text = File.ReadAllText(dialog.FileName);
                    currentFilePath = dialog.FileName;

                    Title = $"Текстовый редактор — {Path.GetFileName(currentFilePath)}";
                    lblFile.Text = Path.GetFileName(currentFilePath);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка чтения файла: {ex.Message}",
                        "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        // СОХРАНЕНИЕ
        private void MenuSave_Click(object sender, RoutedEventArgs e)
        {
            if (currentFilePath != null)
            {
                SaveToFile(currentFilePath);
            }
            else
            {
                MenuSaveAs_Click(sender, e);
            }
        }

        // СОХРАНИТЬ КАК
        private void MenuSaveAs_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new SaveFileDialog
            {
                Filter = "Текстовые файлы (*.txt)|*.txt|Все файлы (*.*)|*.*",
                Title = "Сохранить файл",
                FileName = "Без имени.txt"
            };

            if (dialog.ShowDialog() == true)
            {
                SaveToFile(dialog.FileName);
                currentFilePath = dialog.FileName;

                Title = $"Текстовый редактор — {Path.GetFileName(currentFilePath)}";
                lblFile.Text = Path.GetFileName(currentFilePath);
            }
        }

        // ВСПОМОГАТЕЛЬНЫЙ МЕТОД СОХРАНЕНИЯ
        private void SaveToFile(string path)
        {
            try
            {
                File.WriteAllText(path, txtEditor.Text);
                MessageBox.Show("Файл успешно сохранён.",
                    "Сохранение", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка сохранения: {ex.Message}",
                    "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // ВЫХОД
        private void MenuExit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        // ПРАВКА
        private void MenuUndo_Click(object sender, RoutedEventArgs e) => txtEditor.Undo();
        private void MenuRedo_Click(object sender, RoutedEventArgs e) => txtEditor.Redo();
        private void MenuCut_Click(object sender, RoutedEventArgs e) => txtEditor.Cut();
        private void MenuCopy_Click(object sender, RoutedEventArgs e) => txtEditor.Copy();
        private void MenuPaste_Click(object sender, RoutedEventArgs e) => txtEditor.Paste();
        private void MenuSelectAll_Click(object sender, RoutedEventArgs e) => txtEditor.SelectAll();

        // ОБНОВЛЕНИЕ СЧЁТЧИКА СИМВОЛОВ
        private void TxtEditor_TextChanged(object sender,
            System.Windows.Controls.TextChangedEventArgs e)
        {
            lblChars.Text = $"Символов: {txtEditor.Text.Length}";
        }

        // ГОРЯЧИЕ КЛАВИШИ
        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);

            // Ctrl+O — открыть
            if (e.Key == Key.O && Keyboard.Modifiers == ModifierKeys.Control)
            {
                MenuOpen_Click(null, null);
                e.Handled = true;
            }
            // Ctrl+S — сохранить
            else if (e.Key == Key.S && Keyboard.Modifiers == ModifierKeys.Control)
            {
                MenuSave_Click(null, null);
                e.Handled = true;
            }
        }
    }
}