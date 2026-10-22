using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Module_8
{
    public partial class MainWindow : Window
    {
        // Текущие пути для левой и правой панели
        private string currentLeftPath = @"C:\";
        private string currentRightPath = @"C:\";

        public MainWindow()
        {
            InitializeComponent();
            InitDrives();
            LoadDirectory(currentLeftPath, listLeft);
            LoadDirectory(currentRightPath, listRight);
        }

        // Заполнение выпадающих списков доступными дисками
        private void InitDrives()
        {
            // Берем только готовые к работе диски (исключаем пустые CD-ROM и т.д.)
            var drives = DriveInfo.GetDrives().Where(d => d.IsReady).Select(d => d.Name).ToList();
            cmbLeftDrives.ItemsSource = drives;
            cmbLeftDrives.SelectedIndex = 0;
            cmbRightDrives.ItemsSource = drives;
            cmbRightDrives.SelectedIndex = 0;
        }

        // Загрузка содержимого папки в указанный ListView
        private void LoadDirectory(string path, ListView targetList)
        {
            var items = new ObservableCollection<FileSystemItem>();
            try
            {
                DirectoryInfo dirInfo = new DirectoryInfo(path);
                // Сначала добавляем папки, пропуская скрытые системные
                foreach (var d in dirInfo.GetDirectories())
                {
                    if ((d.Attributes & FileAttributes.System) == 0)
                        items.Add(new FolderItem { Name = d.Name, FullPath = d.FullName, LastModified = d.LastWriteTime });
                }

                // Затем добавляем файлы
                foreach (var f in dirInfo.GetFiles())
                    items.Add(new FileItem { Name = f.Name, FullPath = f.FullName, SizeBytes = f.Length, LastModified = f.LastWriteTime });

                targetList.ItemsSource = items;
                lblStatus.Text = $"Левая: {currentLeftPath}  |  Правая: {currentRightPath}";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка доступа к {path}: {ex.Message}");
            }
        }

        // === НАВИГАЦИЯ ===
        private void BtnUpLeft_Click(object sender, RoutedEventArgs e)
        {
            // Получаем родительскую директорию. Если null — мы в корне диска
            DirectoryInfo parent = Directory.GetParent(currentLeftPath);
            if (parent != null) { currentLeftPath = parent.FullName; LoadDirectory(currentLeftPath, listLeft); }
            else System.Media.SystemSounds.Beep.Play();
        }

        private void BtnUpRight_Click(object sender, RoutedEventArgs e)
        {
            DirectoryInfo parent = Directory.GetParent(currentRightPath);
            if (parent != null) { currentRightPath = parent.FullName; LoadDirectory(currentRightPath, listRight); }
            else System.Media.SystemSounds.Beep.Play(); // Звуковой сигнал, если выше некуда
        }
        private void ListLeft_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            // Двойной клик: папка — заходим внутрь, файл — открываем программой по умолчанию
            if (listLeft.SelectedItem is FolderItem folder) { currentLeftPath = folder.FullPath; LoadDirectory(currentLeftPath, listLeft); }
            else if (listLeft.SelectedItem is FileItem file) Process.Start(new ProcessStartInfo(file.FullPath) { UseShellExecute = true });
        }

        private void ListRight_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (listRight.SelectedItem is FolderItem folder) { currentRightPath = folder.FullPath; LoadDirectory(currentRightPath, listRight); }
            else if (listRight.SelectedItem is FileItem file) Process.Start(new ProcessStartInfo(file.FullPath) { UseShellExecute = true });
        }

        private void CmbLeftDrives_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbLeftDrives.SelectedItem != null) { currentLeftPath = cmbLeftDrives.SelectedItem.ToString(); LoadDirectory(currentLeftPath, listLeft); }
        }

        private void CmbRightDrives_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbRightDrives.SelectedItem != null) { currentRightPath = cmbRightDrives.SelectedItem.ToString(); LoadDirectory(currentRightPath, listRight); }
        }

        // КОПИРОВАНИЕ И ПЕРЕМЕЩЕНИЕ
        private void TransferFiles(ListView sourceList, string destPath, bool isMove)
        {
            if (sourceList.SelectedItems.Count == 0) return;
            // Копируем список в отдельную переменную, чтобы избежать ошибок при изменении коллекции
            var itemsToMove = sourceList.SelectedItems.Cast<FileSystemItem>().ToList();
            int successCount = 0;

            foreach (var item in itemsToMove)
            {
                try
                {
                    // Защита: нельзя переместить папку внутрь самой себя
                    if (item is FolderItem && destPath.StartsWith(item.FullPath))
                    {
                        MessageBox.Show($"Нельзя переместить папку «{item.Name}» внутрь самой себя!", "Ошибка");
                        continue;
                    }
                    // Полиморфизм: вызывается нужный метод в зависимости от типа объекта
                    if (isMove) item.MoveTo(destPath);
                    else item.CopyTo(destPath);
                    successCount++;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при обработке «{item.Name}»: {ex.Message}");
                }
            }
            // Обновляем обе панели после операции
            LoadDirectory(currentLeftPath, listLeft);
            LoadDirectory(currentRightPath, listRight);
            lblStatus.Text = $"{(isMove ? "Перемещено" : "Скопировано")} элементов: {successCount}";
        }

        private void BtnCopyRight_Click(object sender, RoutedEventArgs e) => TransferFiles(listLeft, currentRightPath, false);
        private void BtnMoveRight_Click(object sender, RoutedEventArgs e) => TransferFiles(listLeft, currentRightPath, true);
        private void BtnCopyLeft_Click(object sender, RoutedEventArgs e) => TransferFiles(listRight, currentLeftPath, false);
        private void BtnMoveLeft_Click(object sender, RoutedEventArgs e) => TransferFiles(listRight, currentLeftPath, true);

        // === ОПЕРАЦИИ С ФАЙЛАМИ ===
        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            // Определяем, в какой панели сейчас находится фокус (там и будут удаляться файлы)
            ListView activeList = listLeft.IsKeyboardFocusWithin ? listLeft : listRight;
            if (activeList.SelectedItems.Count == 0) return;

            var itemsToDelete = activeList.SelectedItems.Cast<FileSystemItem>().ToList();

            if (MessageBox.Show($"Удалить {itemsToDelete.Count} элемент(ов)?", "Внимание", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                foreach (var item in itemsToDelete)
                {
                    try { item.Delete(); } // Полиморфный вызов
                    catch (Exception ex) { MessageBox.Show($"Ошибка удаления: {ex.Message}"); }
                }
                LoadDirectory(currentLeftPath, listLeft);
                LoadDirectory(currentRightPath, listRight);
            }
        }

        private void BtnCreateFolder_Click(object sender, RoutedEventArgs e)
        {
            ListView activeList = listLeft.IsKeyboardFocusWithin ? listLeft : listRight;
            string activePath = activeList == listLeft ? currentLeftPath : currentRightPath;

            try
            {
                // Генерируем уникальное имя с текущим временем, чтобы папки не перезаписывали друг друга
                string name = $"Новая_папка_{DateTime.Now:HHmmss}";
                Directory.CreateDirectory(Path.Combine(activePath, name));
                LoadDirectory(currentLeftPath, listLeft);
                LoadDirectory(currentRightPath, listRight);
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void BtnRefresh_Click(object sender, RoutedEventArgs e)
        {
            LoadDirectory(currentLeftPath, listLeft);
            LoadDirectory(currentRightPath, listRight);
        }

        // Обработчик нажатия кнопки "Переименовать"
        private void BtnRename_Click(object sender, RoutedEventArgs e)
        {
            ListView activeList = listLeft.IsKeyboardFocusWithin ? listLeft : listRight;
            StartRename(activeList);
        }

        // Обработчик нажатия F2 в списках
        private void List_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.F2)
            {
                StartRename(sender as ListView);
                e.Handled = true;
            }
        }

        private void StartRename(ListView activeList)
        {
            // Переименовывать можно только по одному элементу за раз
            if (activeList.SelectedItems.Count != 1) return;

            if (activeList.SelectedItem is FileSystemItem item)
            {
                item.IsEditing = true; // Включаем режим редактирования (срабатывает DataTrigger в XAML)
            }
        }

        // Когда TextBox загружается, ставим на него фокус и выделяем текст
        private void TextBox_Loaded(object sender, RoutedEventArgs e)
        {
            TextBox tb = sender as TextBox;
            if (tb != null && tb.IsVisible)
            {
                tb.Focus();
                tb.SelectAll();
            }
        }

        // Обработка Enter (сохранить) и Escape (отменить)
        private void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                CommitRename(sender as TextBox);
                e.Handled = true;
            }
            else if (e.Key == Key.Escape)
            {
                CancelRename(sender as TextBox);
                e.Handled = true;
            }
        }

        // Если кликнули мимо поля ввода — сохраняем изменения
        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            CommitRename(sender as TextBox);
        }

        private void CommitRename(TextBox tb)
        {
            if (tb == null) return;
            FileSystemItem item = tb.DataContext as FileSystemItem;
            if (item == null || !item.IsEditing) return;

            string newName = item.Name.Trim();
            item.IsEditing = false; // Выключаем режим редактирования

            // Проверка на пустое имя и запрещенные символы Windows
            if (string.IsNullOrWhiteSpace(newName) || newName.IndexOfAny(Path.GetInvalidFileNameChars()) >= 0)
            {
                LoadDirectory(currentLeftPath, listLeft);
                LoadDirectory(currentRightPath, listRight);
                return;
            }

            try
            {
                string oldName = Path.GetFileName(item.FullPath);
                if (oldName != newName)
                {
                    item.Rename(newName); // Полиморфный вызов метода физического переименования
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка переименования: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                // Обновляем списки в любом случае
                LoadDirectory(currentLeftPath, listLeft);
                LoadDirectory(currentRightPath, listRight);
            }
        }

        private void CancelRename(TextBox tb)
        {
            if (tb == null) return;
            FileSystemItem item = tb.DataContext as FileSystemItem;
            if (item == null || !item.IsEditing) return;

            item.IsEditing = false;
            // Перезагружаем директории, чтобы вернуть старое имя, которое могло измениться через Binding
            LoadDirectory(currentLeftPath, listLeft);
            LoadDirectory(currentRightPath, listRight);
        }
    }
}