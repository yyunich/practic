using System;
using System.IO;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Microsoft.Win32;

namespace Module_5_4
{
    public partial class MainWindow : Window
    {
        // Текущий масштаб (1.0 = 100%)
        private double zoom = 1.0;

        // Шаг изменения масштаба
        private const double ZoomStep = 1.2;

        // Минимальный и максимальный масштаб
        private const double MinZoom = 0.1;
        private const double MaxZoom = 10.0;

        public MainWindow()
        {
            InitializeComponent();
        }

        // ОТКРЫТИЕ ИЗОБРАЖЕНИЯ
        private void BtnOpen_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new OpenFileDialog
            {
                Filter = "Изображения|*.jpg;*.jpeg;*.png;*.bmp;*.gif|" +
                         "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
                         "PNG (*.png)|*.png|" +
                         "Все файлы (*.*)|*.*",
                Title = "Выберите изображение"
            };

            if (dialog.ShowDialog() == true)
            {
                try
                {
                    // Загружаем изображение
                    var bitmap = new BitmapImage();
                    bitmap.BeginInit();
                    bitmap.UriSource = new Uri(dialog.FileName);
                    bitmap.CacheOption = BitmapCacheOption.OnLoad;   // закрыть файл после загрузки
                    bitmap.EndInit();

                    image.Source = bitmap;

                    // Обновляем информацию
                    Title = $"Просмотр — {Path.GetFileName(dialog.FileName)}";
                    lblFile.Text = dialog.FileName;
                    lblSize.Text = $"{bitmap.PixelWidth} × {bitmap.PixelHeight} px";

                    // Сброс масштаба
                    zoom = 1.0;
                    ApplyZoom();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка загрузки изображения: {ex.Message}",
                        "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        // УВЕЛИЧИТЬ
        private void BtnZoomIn_Click(object sender, RoutedEventArgs e)
        {
            if (image.Source == null) return;

            zoom *= ZoomStep;
            if (zoom > MaxZoom) zoom = MaxZoom;

            ApplyZoom();
        }

        // УМЕНЬШИТЬ
        private void BtnZoomOut_Click(object sender, RoutedEventArgs e)
        {
            if (image.Source == null) return;

            zoom /= ZoomStep;
            if (zoom < MinZoom) zoom = MinZoom;

            ApplyZoom();
        }

        // СБРОС (100%)
        private void BtnReset_Click(object sender, RoutedEventArgs e)
        {
            if (image.Source == null) return;

            zoom = 1.0;
            ApplyZoom();
        }

        // ПО РАЗМЕРУ ОКНА
        private void BtnFit_Click(object sender, RoutedEventArgs e)
        {
            if (image.Source == null) return;

            BitmapSource src = (BitmapSource)image.Source;

            // Считаем, какой масштаб нужен, чтобы картинка влезла в окно
            double scaleX = scrollViewer.ViewportWidth / src.PixelWidth;
            double scaleY = scrollViewer.ViewportHeight / src.PixelHeight;

            // Берём минимальный, чтобы влезло и по ширине, и по высоте
            zoom = Math.Min(scaleX, scaleY);

            // Ограничения
            if (zoom > MaxZoom) zoom = MaxZoom;
            if (zoom < MinZoom) zoom = MinZoom;

            ApplyZoom();
        }

        // ПРИМЕНИТЬ МАСШТАБ
        private void ApplyZoom()
        {
            // LayoutTransform влияет на layout и работает со ScrollViewer
            image.LayoutTransform = new ScaleTransform(zoom, zoom);

            // Обновляем надпись
            lblZoom.Text = $"Масштаб: {zoom * 100:F0}%";
        }

        // КОЛЕСО МЫШИ = МАСШТАБ
        protected override void OnMouseWheel(System.Windows.Input.MouseWheelEventArgs e)
        {
            base.OnMouseWheel(e);

            if (image.Source == null) return;

            // Если Ctrl зажат — масштабируем
            if (System.Windows.Input.Keyboard.Modifiers ==
                System.Windows.Input.ModifierKeys.Control)
            {
                if (e.Delta > 0)
                    BtnZoomIn_Click(null, null);
                else
                    BtnZoomOut_Click(null, null);

                e.Handled = true;
            }
        }
    }
}