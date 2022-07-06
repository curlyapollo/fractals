using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Microsoft.Win32;
using Color = System.Drawing.Color;


namespace peer5
{
    /// <summary>
    /// Класс окна.
    /// </summary>
    public partial class MainWindow
    {
        public static uint zoom = 1;
        public readonly double basicWidth;
        public readonly double basicHeight;
        private uint depth;
        private uint leftAngle;
        private uint rightAngle;
        private uint coefficient;
        private uint distance;
        public static Color startColor;
        public static Color finishColor;
        private Fractal currentFractal;
        private bool isMoved;
        private Point basicMouse;
        private bool isChanging = false;
        
        /// <summary>
        /// Конструктор окна.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            RulesBox();
            ZoomLess.IsEnabled = false;
            ZoomMore.IsEnabled = false;
            peer5.Fractal.SetCanvas(Canvas);
            basicWidth = Canvas.Width;
            basicHeight = Canvas.Height;
        }


        /// <summary>
        /// Обработчик кнопки отрисовки фрактала.
        /// </summary>
        /// <param name="sender">Button</param>
        /// <param name="e">Нажатие на кнопку.</param>
        private void Paint(object sender, RoutedEventArgs e)
        {
            try
            {
                if (currentFractal != null)
                {
                    if (startColor == Color.Empty)
                    {
                        MessageBox.Show("Вы не ввели стартовый цвет!");
                        return;
                    }

                    if (finishColor == Color.Empty)
                    {
                        MessageBox.Show("Вы не ввели конечный цвет!");
                        return;
                    }

                    if ((currentFractal.ToString() == "PifagorTree" && (Coefficient.Text == String.Empty ||
                                                                        LeftAngle.Text == String.Empty ||
                                                                        RightAngle.Text == String.Empty))
                        || (currentFractal.ToString() == "Cantor" && DistanceBetweenCuts.Text == String.Empty))
                    {
                        MessageBox.Show("Вы не ввели один из параметров!");
                        return;
                    }

                    currentFractal?.ChangingColor();
                    Canvas.Children.Clear();
                    currentFractal?.Draw(currentFractal.start, currentFractal.finish, depth);
                    isChanging = false;
                    if (ZoomMore.IsEnabled == false && zoom != 5)
                    {
                        ZoomMore.IsEnabled = true;
                    }
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
                return;
            }
        }

        /// <summary>
        /// Вызывает MessageBox с правилами.
        /// </summary>
        private void RulesBox()
        {
            MessageBox.Show(@"ВСЕ ОТВЕТЫ ЗДЕСЬ!!!
Привет!
B этой работе тебе будут представлены 5 видов фракталов:
1. Обдуваемое ветром фрактальное дерево.
2. Кривая Коха.
3. Ковер Серпинского.
4. Треугольник Серпинского.
5. Множество Кантора.
Надеюсь, тебе понравится!

Теперь к правилам:
Про глубину рекурсии: в целях корректной работы на неё 
поставлено ограничение, для каждого фрактала своё:
Дерево Пифагора - 8
Кривая Коха - 6
Ковёр Серпинского - 4
Треугольник Серпинского - 7
Множество Кантора - 7
Соблюдай его, пожалуйста. При каждом изменении глубины фрактал перерисовывается.
Моя программа не предусматривает увеличение зума до рисования, поэтому изначально эта кнопка заблокирована.
Минимальное значение всех параметров - 0. Максимальное: углы - 180, коэффициент 75, расстояние между отрезками - 60.
Чтобы сохранить картинку или сбросить движение картинки нужно вызвать контекстное меню,
нажав правую кнопку мыши на холсте. Максимальное увеличение зума - 5. Значение глубины по умолчанию - 0.
Про градиент: поскольку его не очень понятно, как реализовывать, он у меня реализован по-своему.
В ковре Серпинского градиент нереализуем, поэтому у меня просто средний цвет для двух.
٩(｡•́‿•̀｡)۶");
        }

        /// <summary>
        /// Вызывает MessageBox при некорректном вводе расстояния между итерациями в множестве Кантора.
        /// </summary>
        private void WarningDistance()
        {
            MessageBox.Show(@"Ты ввёл некорректное расстояние между отрезками.
Напомню: ты должна(-ен) ввести целое число от 0 до 60.
Исправляйся!");
        }
        
        /// <summary>
        /// Вызывает MessageBox при некорректном вводе угла в дерефе Пифагора.
        /// </summary>
        private void WarningAngle()
        {
            MessageBox.Show(@"Ты ввёл некорректный угол.
Напомню: ты должна(-ен) ввести целое число от 0 до 180.
Исправляйся!");
        }
        
        /// <summary>
        /// Вызывает MessageBox при некорректном вводе коеффициента в дерефе Пифагора.
        /// </summary>
        private void WarningCoefficient()
        {
            MessageBox.Show(@"Ты ввёл некорректное соотношение длин отрезков.
Напомню: ты должна(-ен) ввести целое число от 0 до 75.
Исправляйся");
        }
        
        /// <summary>
        /// Меняет глубину фрактала.
        /// </summary>
        private void ChangingDepth()
        {
            try
            {
                if (Depth.Text == String.Empty)
                {
                    depth = 0;
                }
                else if (!uint.TryParse(Depth.Text, out depth) || depth > currentFractal?.maxDepth)
                {
                    currentFractal?.WarningDepth();
                    Depth.Text = String.Empty;
                    return;
                }
                currentFractal?.SetDepth(depth);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
                return;
            }
        }
        
        /// <summary>
        /// Обрабатывает изменение глубины фрактала и перерисовывает его.
        /// </summary>
        /// <param name="sender">TextBox</param>
        /// <param name="e">Ввод текста.</param>
        private void DepthTextBoxChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (Depth.Text == String.Empty)
                {
                    depth = 0;
                }
                else if (!uint.TryParse(Depth.Text, out depth) || depth > currentFractal?.maxDepth)
                {
                    currentFractal?.WarningDepth();
                    Depth.Text = String.Empty;
                    return;
                }
                currentFractal?.SetDepth(depth);      
                currentFractal?.ChangingColor();
                if (Canvas.Children != null && isChanging == false)
                {
                    Canvas.Children.Clear();
                    currentFractal?.Draw(currentFractal.start, currentFractal.finish, depth);
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
                return;
            }
        }

        /// <summary>
        /// Перерисовывает дерево Пифагора.
        /// </summary>
        /// <param name="sender">ComboBoxItem</param>
        /// <param name="e">Выбор элемента "Дерево Пифагора".</param>
        private void TreeSelected(object sender, RoutedEventArgs e)
        {
            try
            {
                SettingsForTree.Visibility = Visibility.Visible;
                SettingsForCantor.Visibility = Visibility.Hidden;
                currentFractal = new PifagorTree();
                isChanging = true;
                ChangingDepth();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
                return;
            }
        }

        /// <summary>
        /// Перерисовывает кривую Коха.
        /// </summary>
        /// <param name="sender">ComboBoxItem</param>
        /// <param name="e">Выбор элемента "Кривая Коха".</param>
        private void KochSelected(object sender, RoutedEventArgs e)
        {
            try
            {
                SettingsForCantor.Visibility = Visibility.Hidden;
                SettingsForTree.Visibility = Visibility.Hidden;
                currentFractal = new KochCurve();
                isChanging = true;
                ChangingDepth(); 
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
                return;
            }
        }

        /// <summary>
        /// Перерисовывает ковёр Серпинского.
        /// </summary>
        /// <param name="sender">ComboBoxItem</param>
        /// <param name="e">Выбор элемента "Ковёр Серпинского".</param>
        private void CarpetSelected(object sender, RoutedEventArgs e)
        {
            try
            {
                SettingsForTree.Visibility = Visibility.Hidden;
                SettingsForCantor.Visibility = Visibility.Hidden;
                currentFractal = new Carpet();
                isChanging = true;
                ChangingDepth();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
                return;
            }
        }

        /// <summary>
        /// Перерисовывает треугольник Серпинского.
        /// </summary>
        /// <param name="sender">ComboBoxItem</param>
        /// <param name="e">Выбор элемента "Треугольник Серпинского".</param>
        private void TriangleSelected(object sender, RoutedEventArgs e)
        {
            try
            {
                SettingsForTree.Visibility = Visibility.Hidden;
                SettingsForCantor.Visibility = Visibility.Hidden;
                currentFractal = new Triangle();
                isChanging = true;
                ChangingDepth();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
                return;
            }
        }

        /// <summary>
        /// Перерисовывает множество Кантора.
        /// </summary>
        /// <param name="sender">ComboBoxItem</param>
        /// <param name="e">Выбор элемента "Множество Кантора".</param>
        private void CantorSelected(object sender, RoutedEventArgs e)
        {
            try
            {
                SettingsForTree.Visibility = Visibility.Hidden;
                SettingsForCantor.Visibility = Visibility.Visible;
                currentFractal = new Cantor();
                isChanging = true;
                ChangingDepth();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
                return;
            }
        }

        /// <summary>
        /// Обрабатывает изменения значения левого угла в дереве Пифагора.
        /// </summary>
        /// <param name="sender">TextBox</param>
        /// <param name="e">Изменение TextBox.</param>
        private void LeftAngleTextBoxChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (LeftAngle.Text == String.Empty)
                {
                    return;
                }
                if (!uint.TryParse(LeftAngle.Text, out leftAngle) || leftAngle > 180)
                {
                    WarningAngle();
                    LeftAngle.Text = String.Empty;
                }

                currentFractal?.SetLeftAngle(leftAngle);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
                return;
            }
        }

        /// <summary>
        /// Обрабатывает изменения значения правого угла в дереве Пифагора.
        /// </summary>
        /// <param name="sender">TextBox</param>
        /// <param name="e">Изменение TextBox.</param>
        private void RightAngleTextBoxChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (RightAngle.Text == String.Empty)
                {
                    return;
                }
                if (!uint.TryParse(RightAngle.Text, out rightAngle) || rightAngle > 180)
                {
                    WarningAngle();
                    RightAngle.Text = String.Empty;
                }
                currentFractal?.SetRightAngle(rightAngle);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
                return;
            }
        }

        /// <summary>
        /// Обрабатывает изменения расстояния между отрезками в множестве Кантора.
        /// </summary>
        /// <param name="sender">TextBox</param>
        /// <param name="e">Изменение TextBox.</param>
        private void DistanceBetweenCutsTextBoxChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (DistanceBetweenCuts.Text == String.Empty)
                {
                    return;
                }
                if (!uint.TryParse(DistanceBetweenCuts.Text, out distance) || distance > 60)
                {
                    WarningDistance();
                    DistanceBetweenCuts.Text = String.Empty;
                }
                currentFractal?.SetDistance(distance);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
                return;
            }
        }

        /// <summary>
        /// Обрабатывает изменения значения коэффициента в дереве Пифагора.
        /// </summary>
        /// <param name="sender">TextBox</param>
        /// <param name="e">Изменение TextBox.</param>
        private void CoefficientTextBoxChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (Coefficient.Text == String.Empty)
                {
                    return;
                }
                if (!uint.TryParse(Coefficient.Text, out coefficient) || coefficient > 75)
                {
                    WarningCoefficient();
                    Coefficient.Text = String.Empty;
                }
                currentFractal?.SetCoefficient(coefficient);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
                return;
            }
        }

        /// <summary>
        /// Обработчик нажатия на кнопку уменьшения значения зума.
        /// </summary>
        /// <param name="sender">Button</param>
        /// <param name="e">Нажатие на кнопку.</param>
        private void ZoomLessClick(object sender, RoutedEventArgs e)
        {
            try
            {
                ZoomMore.IsEnabled = true;
                if (zoom <= 2)
                {
                    ZoomLess.IsEnabled = false;
                }
                zoom -= 1;
                ScaleTransform scaling = new ScaleTransform
                {
                    ScaleX = zoom,
                    ScaleY = zoom
                };
                Canvas.RenderTransform = scaling;
                Canvas.Width = basicWidth * zoom;
                Canvas.Height = basicHeight * zoom;
                if (zoom == 1)
                {
                    ScrollViewer.HorizontalScrollBarVisibility = ScrollBarVisibility.Hidden;
                    ScrollViewer.VerticalScrollBarVisibility = ScrollBarVisibility.Hidden;
                }
                else
                {
                    ScrollViewer.HorizontalScrollBarVisibility = ScrollBarVisibility.Auto;
                    ScrollViewer.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;
                }
                MessageBox.Show($"Zoom: {zoom}");
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
                return;
            }
        }

        /// <summary>
        /// Обработчик нажатия на кнопку увелечения значения зума.
        /// </summary>
        /// <param name="sender">Button</param>
        /// <param name="e">Нажатие на кнопку.</param>
        private void ZoomMoreClick(object sender, RoutedEventArgs e)
        {
            try
            {
                if (zoom >= 4)
                {
                    ZoomMore.IsEnabled = false;
                }
                ZoomLess.IsEnabled = true;
                zoom += 1;
                ScaleTransform scaling = new ScaleTransform
                {
                    ScaleX = zoom,
                    ScaleY = zoom
                };
                Canvas.RenderTransform = scaling;
                Canvas.Width = basicWidth * zoom;
                Canvas.Height = basicHeight * zoom;
                if (zoom == 1)
                {
                    ScrollViewer.HorizontalScrollBarVisibility = ScrollBarVisibility.Hidden;
                    ScrollViewer.VerticalScrollBarVisibility = ScrollBarVisibility.Hidden;
                }
                else
                {
                    ScrollViewer.HorizontalScrollBarVisibility = ScrollBarVisibility.Auto;
                    ScrollViewer.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;
                }
                MessageBox.Show($"Zoom: {zoom}");
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
                return;
            }
        }
        
        /// <summary>
        /// Обработчик захвата мышью изображения.
        /// </summary>
        /// <param name="sender">Мышь</param>
        /// <param name="e">Нажатие кнопки мыши.</param>
        private void CanvasMouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                if (currentFractal != null && Canvas.Children != null 
                                           && e.LeftButton == MouseButtonState.Pressed && isChanging == false)
                {
                    isMoved = true;
                
                    basicMouse = e.GetPosition(this);
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
                return;
            }
        }

        /// <summary>
        /// Обработчик отпускания мышью изображения.
        /// </summary>
        /// <param name="sender">Мышь</param>
        /// <param name="e">Отпускание мыши.</param>
        private void CanvasMouseUp(object sender, MouseButtonEventArgs e)
        {
            try
            {
                if (e.LeftButton == MouseButtonState.Released)
                {
                    isMoved = false;
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
                return;
            }
        }

        /// <summary>
        /// Обработчик движения картинки по холсту.
        /// </summary>
        /// <param name="sender">Мышь</param>
        /// <param name="e">Движение мыши.</param>
        private void CanvasMouseMove(object sender, MouseEventArgs e)
        {
            try
            {
                if (isMoved)
                {
                    var temporaryVector = e.GetPosition(this) - basicMouse;
                    currentFractal.start = currentFractal.iconicStart + temporaryVector;
                    currentFractal.finish = currentFractal.iconicFinish + temporaryVector;
                    Canvas.Children.Clear();
                    currentFractal?.Draw(currentFractal.start, currentFractal.finish, depth);
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
                return;
            }
        }

        /// <summary>
        /// Обработчик нажатия на кнопку сохранения картинки.
        /// </summary>
        /// <param name="sender">MenuItem</param>
        /// <param name="e">Выбор элемента меню.</param>
        private void SaveClick(object sender, RoutedEventArgs e)
        {
            try
            {
                SaveFileDialog save = new SaveFileDialog();
                save.Title = "Сохранение файла";
                save.ShowDialog();
            
 
                var rtb = new RenderTargetBitmap((int)Canvas.Width, (int)Canvas.Height, 96d, 96d, PixelFormats.Pbgra32);
                Canvas.Measure(new Size((int)Canvas.Width, (int)Canvas.Height));
                Canvas.Arrange(new Rect(new Size((int)Canvas.Width, (int)Canvas.Height)));
                rtb.Render(Canvas);
                    
                PngBitmapEncoder bufferSave = new PngBitmapEncoder();
                bufferSave.Frames.Add((BitmapFrame.Create(rtb)));
                using(var fs=File.OpenWrite(save.FileName))
                {
                    bufferSave.Save(fs);              
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
        }

        /// <summary>
        /// Обработчик нажатия на кнопку сброса сдвига.
        /// </summary>
        /// <param name="sender">MenuItem</param>
        /// <param name="e">Выбор элемента меню.</param>
        private void ResetMoveClick(object sender, RoutedEventArgs e)
        {
            try
            {
                if (currentFractal != null && isChanging == false)
                {
                    currentFractal.start = currentFractal.iconicStart;
                    currentFractal.finish = currentFractal.iconicFinish;
                    Canvas.Children.Clear();
                    currentFractal?.Draw(currentFractal.start, currentFractal.finish, depth);
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
                return;
            }
        }

        /// <summary>
        /// Обработчик выбора стартового цвета.
        /// </summary>
        /// <param name="sender">ComboBox</param>
        /// <param name="e">Выбор элемента ComboBox.</param>
        private void StartColorSelectedItem(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                switch (StartColor.SelectedIndex)
                {
                    case 0:
                        startColor = Color.Red;
                        break;
                    case 1:
                        startColor = Color.Orange;
                        break;
                    case 2:
                        startColor = Color.Yellow;
                        break;
                    case 3:
                        startColor = Color.Green;
                        break;
                    case 4:
                        startColor = Color.Blue;
                        break;
                    case 5:
                        startColor = Color.Black;
                        break;
                    default:
                        startColor = Color.BlueViolet;
                        break;
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
                return;
            }
        }

        /// <summary>
        /// Обработчик выбора конечного цвета.
        /// </summary>
        /// <param name="sender">ComboBox</param>
        /// <param name="e">Выбор элемента ComboBox.</param>
        private void FinishColorSelectedItem(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                switch (FinishColor.SelectedIndex)
                {
                    case 0:
                        finishColor = Color.Red;
                        break;
                    case 1:
                        finishColor = Color.Orange;
                        break;
                    case 2:
                        finishColor = Color.Yellow;
                        break;
                    case 3:
                        finishColor = Color.Green;
                        break;
                    case 4:
                        finishColor = Color.Blue;
                        break;
                    case 5:
                        finishColor = Color.Black;
                        break;
                    default:
                        finishColor = Color.BlueViolet;
                        break;
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
                return;
            }
        }

        /// <summary>
        /// Обработчик нажатия на кнопку "Правила".
        /// </summary>
        /// <param name="sender">Button</param>
        /// <param name="e">Нажатие на кнопку.</param>
        private void Rules(object sender, RoutedEventArgs e)
        {
            RulesBox();
        }
    }
}