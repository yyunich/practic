using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Module_5_1
{
    public partial class MainWindow : Window
    {
        // Текущий режим: "line", "circle", "square"
        private string mode = "line";

        // Начальная точка рисования
        private Point startPoint;

        // Текущая рисуемая фигура (предпросмотр)
        private Shape currentShape = null;

        // Флаг: сейчас идёт рисование?
        private bool isDrawing = false;

        public MainWindow()
        {
            InitializeComponent();
        }

        // КНОПКИ ВЫБОРА РЕЖИМА

        private void BtnLine_Click(object sender, RoutedEventArgs e)
        {
            mode = "line";
            lblMode.Text = "Режим: линия";
        }

        private void BtnCircle_Click(object sender, RoutedEventArgs e)
        {
            mode = "circle";
            lblMode.Text = "Режим: круг";
        }

        private void BtnSquare_Click(object sender, RoutedEventArgs e)
        {
            mode = "square";
            lblMode.Text = "Режим: квадрат";
        }

        private void BtnClear_Click(object sender, RoutedEventArgs e)
        {
            canvas.Children.Clear();
        }

        // РИСОВАНИЕ
        // Начало рисования — мышь нажата
        private void Canvas_MouseDown(object sender, MouseButtonEventArgs e)
        {
            startPoint = e.GetPosition(canvas);
            isDrawing = true;

            // Создаём фигуру в зависимости от режима
            switch (mode)
            {
                case "line":
                    currentShape = new Line
                    {
                        Stroke = Brushes.Black,
                        StrokeThickness = 2,
                        X1 = startPoint.X,
                        Y1 = startPoint.Y,
                        X2 = startPoint.X,
                        Y2 = startPoint.Y
                    };
                    break;

                case "circle":
                    currentShape = new Ellipse
                    {
                        Stroke = Brushes.Black,
                        StrokeThickness = 2,
                        Width = 0,
                        Height = 0
                    };
                    Canvas.SetLeft(currentShape, startPoint.X);
                    Canvas.SetTop(currentShape, startPoint.Y);
                    break;

                case "square":
                    currentShape = new Rectangle
                    {
                        Stroke = Brushes.Black,
                        StrokeThickness = 2,
                        Width = 0,
                        Height = 0
                    };
                    Canvas.SetLeft(currentShape, startPoint.X);
                    Canvas.SetTop(currentShape, startPoint.Y);
                    break;
            }

            // Добавляем фигуру на холст
            canvas.Children.Add(currentShape);
        }

        // Процесс рисования — мышь движется
        private void Canvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (!isDrawing || currentShape == null) return;

            Point currentPoint = e.GetPosition(canvas);

            switch (mode)
            {
                case "line":
                    if (currentShape is Line line)
                    {
                        line.X2 = currentPoint.X;
                        line.Y2 = currentPoint.Y;
                    }
                    break;

                case "circle":
                    if (currentShape is Ellipse ellipse)
                    {
                        // Радиус — расстояние от старта до текущей точки
                        double r = Math.Sqrt(
                            Math.Pow(currentPoint.X - startPoint.X, 2) +
                            Math.Pow(currentPoint.Y - startPoint.Y, 2));

                        ellipse.Width = 2 * r;
                        ellipse.Height = 2 * r;

                        // Центр круга — в startPoint
                        Canvas.SetLeft(ellipse, startPoint.X - r);
                        Canvas.SetTop(ellipse, startPoint.Y - r);
                    }
                    break;

                case "square":
                    if (currentShape is Rectangle rect)
                    {
                        // Сторона квадрата — максимум из dx и dy
                        double size = Math.Max(
                            Math.Abs(currentPoint.X - startPoint.X),
                            Math.Abs(currentPoint.Y - startPoint.Y));

                        rect.Width = size;
                        rect.Height = size;

                        // Позиция — левый верхний угол прямоугольной области
                        Canvas.SetLeft(rect, Math.Min(startPoint.X, currentPoint.X));
                        Canvas.SetTop(rect, Math.Min(startPoint.Y, currentPoint.Y));
                    }
                    break;
            }
        }

        // Конец рисования — мышь отпущена
        private void Canvas_MouseUp(object sender, MouseButtonEventArgs e)
        {
            isDrawing = false;
            currentShape = null;
        }
    }
}