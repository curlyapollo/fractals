using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace peer5
{
    
    /// <summary>
    /// Класс треугольника Серпинского.
    /// </summary>
    public class Triangle : Fractal
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
        
        /// <summary>
        /// Конструктор треугольника Серпинского.
        /// </summary>
        public Triangle()
        {
            start = new Point(CurrentWidth / 7, CurrentHeight * 4 / 5);
            finish = new Point(CurrentWidth * 6 / 7, CurrentHeight * 4 / 5);
            maxDepth = 7;
            iconicStart = new Point(CurrentWidth / 7, CurrentHeight * 4 / 5);
            iconicFinish = new Point(CurrentWidth * 6 / 7, CurrentHeight * 4 / 5);
        }

        /// <summary>
        /// Метод отрисовки фрактала.
        /// </summary>
        /// <param name="startPoint">Начальная точка.</param>
        /// <param name="finishPoint">Конечная точка.</param>
        /// <param name="currentDepth">Текущая глубина.</param>
        public override void Draw(Point startPoint, Point finishPoint, uint currentDepth)
        {
            DrawHelper(startPoint, finishPoint, currentDepth, levelColor);
            levelColor = 0;
        }

        /// <summary>
        /// Помощник для метода отрисовки фрактала.
        /// </summary>
        /// <param name="startPoint">Начальная точка.</param>
        /// <param name="finishPoint">Конечная точка.</param>
        /// <param name="currentDepth">Текущая глубина.</param>
        /// <param name="temporaryLevelColor">Уровень цвета.</param>
        public void DrawHelper(Point startPoint, Point finishPoint, uint currentDepth, uint temporaryLevelColor)
        {
            if (currentDepth == 0)
            {
                var highPoint = new Point(
                    startPoint.X +
                    Math.Cos(-Math.PI / 3) * (finishPoint - startPoint).X -
                    Math.Sin(-Math.PI / 3) * (finishPoint - startPoint).Y,
                    startPoint.Y +
                    Math.Sin(-Math.PI / 3) * (finishPoint - startPoint).X +
                    Math.Cos(-Math.PI / 3) * (finishPoint - startPoint).Y);
                Polygon triangle = new Polygon();
                triangle.Stroke = new SolidColorBrush(colors[(int) temporaryLevelColor]);
                triangle.Points.Add(startPoint);
                triangle.Points.Add(highPoint);
                triangle.Points.Add(finishPoint);
                ourCanvas.Children.Add(triangle);
            }
            else
            {
                var highPoint = new Point(
                    startPoint.X +
                    Math.Cos(-Math.PI / 3) * (finishPoint - startPoint).X -
                    Math.Sin(-Math.PI / 3) * (finishPoint - startPoint).Y,
                    startPoint.Y +
                    Math.Sin(-Math.PI / 3) * (finishPoint - startPoint).X +
                    Math.Cos(-Math.PI / 3) * (finishPoint - startPoint).Y);
                var leftPoint = new Point((startPoint.X + highPoint.X) / 2, (startPoint.Y + highPoint.Y) / 2);
                var rightPoint = new Point((finishPoint.X + highPoint.X) / 2, (finishPoint.Y + highPoint.Y) / 2);
                var downPoint = new Point((startPoint.X + finishPoint.X) / 2, (startPoint.Y + finishPoint.Y) / 2);
                DrawHelper(startPoint, downPoint, currentDepth - 1, temporaryLevelColor);
                DrawHelper(downPoint, finishPoint, currentDepth - 1, temporaryLevelColor);
                DrawHelper(leftPoint, rightPoint, currentDepth - 1, temporaryLevelColor + 1);
            }
        }
    }
}
        