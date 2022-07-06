using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace peer5
{
    /// <summary>
    /// Класс ковра Серпинского.
    /// </summary>
    public class Carpet : Fractal
    {
        /// <summary>
        /// Вызывает MessageBox при некорректном вводе глубины фрактала.
        /// </summary>
        public override void WarningDepth()
        {
            MessageBox.Show(@"Ты ввёл некорректную для этого фрактала глубину рекурсии.
Напомню: ты должна(-ен) ввести целое число от 0 до 4.
Исправляйся!");
        }
        
        /// <summary>
        /// Конструктор ковра Серпинского.
        /// </summary>
        public Carpet()
        {
            start = new Point(CurrentWidth / 5, CurrentHeight / 5);
            finish = new Point(4 * CurrentWidth / 5, 4 * CurrentHeight / 5);
            iconicStart = new Point(CurrentWidth / 5, CurrentHeight / 5);
            iconicFinish = new Point(4 * CurrentWidth / 5, 4 * CurrentHeight / 5);
            maxDepth = 4;
        }

        /// <summary>
        /// Метод отрисовки фрактала.
        /// </summary>
        /// <param name="startPoint">Начальная точка.</param>
        /// <param name="finishPoint">Конечная точка.</param>
        /// <param name="currentDepth">Текущая глубина.</param>
        public override void Draw(Point startPoint, Point finishPoint, uint currentDepth)
        {
            if (currentDepth == 0)
            {
                Polygon square = new Polygon();
                square.Points.Add(startPoint);
                square.Points.Add(new Point(startPoint.X, finishPoint.Y));
                square.Points.Add(finishPoint);
                square.Points.Add(new Point(finishPoint.X, startPoint.Y));
                square.Fill = new SolidColorBrush(colors[colors.Count / 2]);
                square.FillRule = FillRule.Nonzero;
                ourCanvas.Children.Add(square);
            }
            else
            {
                for (int i = 0; i < 9; i++)
                {
                    var row = i / 3;
                    var column = i % 3;
                    if (i != 4)
                    {
                        Draw(new Point(startPoint.X + column * (finishPoint.X - startPoint.X) / 3, 
                            startPoint.Y + row * (finishPoint.Y - startPoint.Y) / 3),
                            new Point(startPoint.X + (column + 1) * (finishPoint.X - startPoint.X) / 3, 
                            startPoint.Y + (row + 1) * (finishPoint.Y - startPoint.Y) / 3),
                            currentDepth - 1);
                    }
                }
            }
        }
    }
}