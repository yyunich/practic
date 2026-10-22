using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;

namespace Module_8
{
    // Базовый абстрактный класс. Наследуем INotifyPropertyChanged для обновления UI при переименовании.
    public abstract class FileSystemItem : INotifyPropertyChanged
    {
        private string _name;
        public string Name
        {
            get => _name;
            set { _name = value; OnPropertyChanged(nameof(Name)); }
        }

        private bool _isEditing;
        // Флаг, указывающий, что элемент сейчас редактируется (для показа TextBox вместо текста)
        public bool IsEditing
        {
            get => _isEditing;
            set { _isEditing = value; OnPropertyChanged(nameof(IsEditing)); }
        }

        public string FullPath { get; set; }
        public DateTime LastModified { get; set; }

        // Абстрактные свойства: каждый наследник (файл/папка) задает их по-своему
        public abstract string Icon { get; }
        public abstract string SizeDisplay { get; }

        // Абстрактные методы (Полиморфизм): логика зависит от конкретного типа
        public abstract void Delete();
        public abstract void CopyTo(string destinationFolder);
        public abstract void MoveTo(string destinationFolder);
        public abstract void Rename(string newName);

        // Реализация INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    // Класс Папки (наследник)
    public class FolderItem : FileSystemItem
    {
        public override string Icon => "📁";
        public override string SizeDisplay => "<ПАПКА>";

        public override void Delete() => Directory.Delete(FullPath, true);

        // Переименование папки: физическое перемещение на диске
        public override void Rename(string newName)
        {
            string parentDir = Path.GetDirectoryName(FullPath);
            string newPath = Path.Combine(parentDir, newName);
            Directory.Move(FullPath, newPath);
            Name = newName; // Переименование папки
            FullPath = newPath;
        }

        public override void CopyTo(string destinationFolder)
        {
            string targetPath = Path.Combine(destinationFolder, Name);
            CopyDirectory(FullPath, targetPath);
        }

        public override void MoveTo(string destinationFolder)
        {
            string targetPath = Path.Combine(destinationFolder, Name);
            Directory.Move(FullPath, targetPath);
        }
        
        // Инкапсуляция: скрытый рекурсивный метод для копирования папки со всем содержимым
        private void CopyDirectory(string sourceDir, string targetDir)
        {
            Directory.CreateDirectory(targetDir);
            // Копируем все файлы текущей директории
            foreach (var file in Directory.GetFiles(sourceDir))
                File.Copy(file, Path.Combine(targetDir, Path.GetFileName(file)), true);
            // Рекурсивно заходим в подпапки
            foreach (var dir in Directory.GetDirectories(sourceDir))
                CopyDirectory(dir, Path.Combine(targetDir, Path.GetFileName(dir)));
        }
    }

    // Класс Файла (наследник)
    public class FileItem : FileSystemItem
    {
        public long SizeBytes { get; set; }

        public override string Icon => "📄";
        
        // Красивое отображение размера файла
        public override string SizeDisplay
        {
            get
            {
                if (SizeBytes < 1024) return $"{SizeBytes} Б";
                if (SizeBytes < 1024 * 1024) return $"{SizeBytes / 1024.0:F1} КБ";
                return $"{SizeBytes / (1024.0 * 1024.0):F1} МБ";
            }
        }

        public override void Delete() => File.Delete(FullPath);

        // Переименование файла
        public override void Rename(string newName)
        {
            string parentDir = Path.GetDirectoryName(FullPath);
            string newPath = Path.Combine(parentDir, newName);
            File.Move(FullPath, newPath);
            Name = newName; // Переименование файла
            FullPath = newPath;
        }

        public override void CopyTo(string destinationFolder)
        {
            string targetPath = Path.Combine(destinationFolder, Name);
            File.Copy(FullPath, targetPath, true); // true = перезаписать, если существует
        }

        public override void MoveTo(string destinationFolder)
        {
            string targetPath = Path.Combine(destinationFolder, Name);
            File.Move(FullPath, targetPath);
        }
    }
}   