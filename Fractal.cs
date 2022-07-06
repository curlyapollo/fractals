using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace peer5
{
    /// <summary>
    /// Абстрактный класс фрактала.
    /// </summary>
    public abstract class Fractal
    {
        public uint levelColor = 0;
        public Point start;
        public Point finish;
        public Point iconicStart;
        public Point iconicFinish;
        public uint necessaryDepth = 0;
        protected static uint necessaryLeftAngle = 0;
        protected static uint necessaryRightAngle = 0;
        protected static uint necessaryCoefficicent = 0;
        protected static uint necessaryDistance = 0;
        protected static Canvas ourCanvas;
        public List<Color> colors = new List<Color>();
        public uint maxDepth;
        
        /// <summary>
        /// Формирование списка цветов.
        /// </summary>
        public void ChangingColor()
        {
            colors = new List<Color>();
            byte startR = MainWindow.startColor.R;
            byte startG = MainWindow.startColor.G;
            byte startB = MainWindow.startColor.B;
            byte finishR = MainWindow.finishColor.R;
            byte finishG = MainWindow.finishColor.G;
            byte finishB = MainWindow.finishColor.B;
            if (necessaryDepth == 0)
            {
                colors.Add(Color.FromRgb(startR, startG, startB));
                return;
            }
            for(int i = 0; i <= necessaryDepth; i++)
            {
                var averageR = startR + ((finishR - startR) * i / necessaryDepth);
                var averageG = startG + ((finishG - startG) * i / necessaryDepth);
                var averageB = startB + ((finishB - startB) * i / necessaryDepth);
                colors.Add(Color.FromRgb((byte) averageR, (byte) averageG, (byte) averageB));
            }
        }

        /// <summary>
        /// Вызывает MessageBox при некорректном вводе глубины фрактала.
        /// </summary>
        public abstract void WarningDepth();
        
        /// <summary>
        /// Возвращает текущую ширину Canvas.
        /// </summary>
        protected static double CurrentWidth => ourCanvas.Width * MainWindow.zoom;
        
        /// <summary>
        /// Возвращает текущую высоту Canvas.
        /// </summary>
        protected static double CurrentHeight => ourCanvas.Height * MainWindow.zoom;
        
        /// <summary>
        /// Метод отрисовки фрактала.
        /// </summary>
        /// <param name="startPoint">Начальная точка.</param>
        /// <param name="finishPoint">Конечная точка.</param>
        /// <param name="currentDepth">Текущая глубина.</param>
        public abstract void Draw(Point startPoint, Point finishPoint, uint currentDepth);
        
        /// <summary>
        /// Получение глубины из MainWindow.
        /// </summary>
        /// <param name="currentDepth">Текущая глубина.</param>
        public void SetDepth(uint currentDepth)
        {
            if (currentDepth == necessaryDepth)
            {
                return;
            }
            necessaryDepth = currentDepth;
            
        }
        
        /// <summary>
        /// Получение расстояния между отрезками из MainWindow.
        /// </summary>
        /// <param name="currentDistance">Текущее расстояние.</param>
        public void SetDistance(uint currentDistance)
        {
            if (currentDistance == necessaryDistance)
            {
                return;
            }
            necessaryDistance = currentDistance;
        }
        
        /// <summary>
        /// Получение левого угла из MainWindow.
        /// </summary>
        /// <param name="currentLeftAngle">Текущий левый угол.</param>
        public void SetLeftAngle(uint currentLeftAngle)
        {
            if (currentLeftAngle == necessaryLeftAngle)
            {
                return;
            }
            necessaryLeftAngle = currentLeftAngle;
        }
        
        /// <summary>
        /// Получение текущего правого угла из MainWindow.
        /// </summary>
        /// <param name="currentRightAngle">Текущий правый угол.</param>
        public void SetRightAngle(uint currentRightAngle)
        {
            if (currentRightAngle == necessaryRightAngle)
            {
                return;
            }
            necessaryRightAngle = currentRightAngle;
        }
        
        /// <summary>
        /// Получение текущего коеффициента из MainWindow.
        /// </summary>
        /// <param name="currentCoefficient">Текущий коеффициент.</param>
        public void SetCoefficient(uint currentCoefficient)
        {
            if (currentCoefficient == necessaryCoefficicent)
            {
                return;
            }
            necessaryCoefficicent = currentCoefficient;
        }


        /// <summary>
        /// Получение текущего Canvas из MainWindow.
        /// </summary>
        /// <param name="Canvas"></param>
        public static void SetCanvas(Canvas Canvas) => ourCanvas = Canvas;
    }
}