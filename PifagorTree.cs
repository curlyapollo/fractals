using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace peer5
{
    
    /// <summary>
    /// Класс дерева Пифагора.
    /// </summary>
    public class PifagorTree : Fractal
    {
        
        /// <summary>
        /// Вызывает MessageBox при некорректном вводе глубины фрактала.
        /// </summary>
        public override void WarningDepth()
        {
            MessageBox.Show(@"Ты ввёл некорректную для этого фрактала глубину рекурсии.
Напомню: ты должна(-ен) ввести целое число от 0 до 8.
Исправляйся!");
        }
        
        /// <summary>
        /// Конструктор дерева Пифагора.
        /// </summary>
        public PifagorTree()
        {
            start = new Point(CurrentWidth / 2.0, CurrentHeight * 9.0 / 10.0);
            finish = new Point(CurrentWidth / 2.0, CurrentHeight * 7.0 / 10.0);
            maxDepth = 8;
            iconicStart = new Point(CurrentWidth / 2.0, CurrentHeight * 9.0 / 10.0);
            iconicFinish = new Point(CurrentWidth / 2.0, CurrentHeight * 7.0 / 10.0);
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

        public override string ToString()
        {
            return "PifagorTree";
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
                Line line = new Line
                {
                    X1 = startPoint.X,
                    X2 = finishPoint.X,
                    Y1 = startPoint.Y,
                    Y2 = finishPoint.Y,
                    Stroke = new SolidColorBrush(colors[(int)temporaryLevelColor]),
                };
                ourCanvas.Children.Add(line);
            }
            else
            {
                var firstFinish = new Point(
                    ((finishPoint.X - startPoint.X) * Math.Cos(necessaryLeftAngle / 180.0 * Math.PI) +
                     (finishPoint.Y - startPoint.Y) * Math.Sin(necessaryLeftAngle / 180.0 * Math.PI)) *
                    (necessaryCoefficicent / 100.0)
                    + finishPoint.X,
                    (-(finishPoint.X - startPoint.X) * Math.Sin(necessaryLeftAngle / 180.0 * Math.PI) +
                     (finishPoint.Y - startPoint.Y) * Math.Cos(necessaryLeftAngle / 180.0 * Math.PI)) *
                    (necessaryCoefficicent / 100.0)
                    + finishPoint.Y);
                var secondFinish = new Point(
                    ((finishPoint.X - startPoint.X) * Math.Cos(necessaryRightAngle / 180.0 * Math.PI) -
                     (finishPoint.Y - startPoint.Y) * Math.Sin(necessaryRightAngle / 180.0 * Math.PI)) *
                    (necessaryCoefficicent / 100.0)
                    + finishPoint.X,
                    ((finishPoint.X - startPoint.X) * Math.Sin(necessaryRightAngle / 180.0 * Math.PI) +
                     (finishPoint.Y - startPoint.Y) * Math.Cos(necessaryRightAngle / 180.0 * Math.PI)) *
                    (necessaryCoefficicent / 100.0)
                    + finishPoint.Y);
                DrawHelper(startPoint, finishPoint, currentDepth - 1, levelColor);
                DrawHelper(finishPoint, firstFinish, currentDepth - 1, levelColor + 1);
                DrawHelper(finishPoint, secondFinish, currentDepth - 1, levelColor + 1);
            }
        }
    }
}