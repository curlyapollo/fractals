using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace peer5
{
    
    /// <summary>
    /// Класс множества Кантора.
    /// </summary>
    public class Cantor : Fractal
    {
        
        /// <summary>
        /// Вызывает MessageBox при некорректном вводе глубины фрактала.
        /// </summary>
        public override void WarningDepth()
        {
            MessageBox.Show(@"Ты ввёл некорректную для этого фрактала глубину рекурсии.
Напомню: ты должна(-ен) ввести целое число от 0 до 7.
Исправляйся!");
        }

        public override string ToString()
        {
            return "Cantor";
        }

        /// <summary>
        /// Конструктор множества Кантора.
        /// </summary>
        public Cantor()
        {
            start = new Point(CurrentWidth / 9, CurrentHeight / 10);
            finish = new Point(CurrentWidth * 8 / 9, CurrentHeight / 10);
            maxDepth = 7;
            iconicStart = new Point(CurrentWidth / 9, CurrentHeight / 10);
            iconicFinish = new Point(CurrentWidth * 8 / 9, CurrentHeight / 10);
        }

        /// <summary>
        /// Метод отрисовки всех итераций фрактала.
        /// </summary>
        /// <param name="startPoint">Начальная точка.</param>
        /// <param name="finishPoint">Конечная точка.</param>
        /// <param name="currentDepth">Текущая глубина.</param>
        public override void Draw(Point startPoint, Point finishPoint, uint currentDepth)
        {
            for (uint i = 0; i < currentDepth + 1; i++)
            {
                DrawHelper(startPoint, finishPoint, i, i);
            }
        }

        /// <summary>
        /// Помощник для метода отрисовки фрактала.
        /// </summary>
        /// <param name="startPoint">Начальная точка.</param>
        /// <param name="finishPoint">Конечная точка.</param>
        /// <param name="currentDepth">Текущая глубина.</param>
        /// <param name="temporaryLevelColor">Уровень цвета.</param>
        private void DrawHelper(Point startPoint, Point finishPoint, uint currentDepth, uint temporaryLevelColor)
        {
            if (currentDepth == 0)
            {
                Line line = new Line
                {
                    X1 = startPoint.X,
                    X2 = finishPoint.X,
                    Y1 = startPoint.Y,
                    Y2 = finishPoint.Y,
                    Stroke = new SolidColorBrush(colors[(int)temporaryLevelColor]),
                    StrokeThickness = 5
                };
                ourCanvas.Children.Add(line);
            }
            else
            {
                startPoint = new Point(startPoint.X, startPoint.Y + necessaryDistance);
                finishPoint = new Point(finishPoint.X, finishPoint.Y + necessaryDistance);
                var secondPoint = startPoint + (finishPoint - startPoint) / 3;
                var thirdPoint = secondPoint + (finishPoint - startPoint) / 3;
                DrawHelper(startPoint, secondPoint, currentDepth - 1, temporaryLevelColor);
                DrawHelper(thirdPoint, finishPoint, currentDepth - 1, temporaryLevelColor);
            }
        }
    }
}