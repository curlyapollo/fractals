using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;


namespace peer5
{
    
    /// <summary>
    /// Класс кривой Коха.
    /// </summary>
    public class KochCurve : Fractal
    {
        /// <summary>
        /// Вызывает MessageBox при некорректном вводе глубины фрактала.
        /// </summary>
        public override void WarningDepth()
        {
            MessageBox.Show(@"Ты ввёл некорректную для этого фрактала глубину рекурсии.
Напомню: ты должна(-ен) ввести целое число от 0 до 6.
Исправляйся!");
        }

        /// <summary>
        /// Конструктор кривой Коха.
        /// </summary>
        public KochCurve()
        {
            start = new Point(10, CurrentHeight * 2 / 3);
            finish = new Point(CurrentWidth  - 10, CurrentHeight * 2 / 3);
            maxDepth = 6;
            iconicStart = new Point(10, CurrentHeight * 2 / 3);
            iconicFinish = new Point(CurrentWidth  - 10, CurrentHeight * 2 / 3);
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
        /// <param name="currentDepth">Текущая глубина</param>
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
                    Stroke = new SolidColorBrush(colors[(int) (temporaryLevelColor)]),
                };
                ourCanvas.Children.Add(line);
            }
            else
            {
                var secondPoint = startPoint + (finishPoint - startPoint) / 3;
                var fourthPoint = secondPoint + (finishPoint - startPoint) / 3;
                var thirdPoint = new Point( secondPoint.X + 
                                            Math.Cos(-Math.PI / 3) * (fourthPoint - secondPoint).X -
                                            Math.Sin(-Math.PI / 3) * (fourthPoint - secondPoint).Y,
                    secondPoint.Y + 
                    Math.Sin(-Math.PI / 3) * (fourthPoint - secondPoint).X +
                    Math.Cos(-Math.PI / 3) * (fourthPoint - secondPoint).Y);
                levelColor = currentDepth;
                DrawHelper(startPoint, secondPoint, currentDepth - 1, temporaryLevelColor);
                DrawHelper(secondPoint, thirdPoint, currentDepth - 1, temporaryLevelColor + 1);
                DrawHelper(thirdPoint, fourthPoint, currentDepth - 1, temporaryLevelColor + 1);
                DrawHelper(fourthPoint, finishPoint, currentDepth - 1, temporaryLevelColor);
            }
        }
    }
}