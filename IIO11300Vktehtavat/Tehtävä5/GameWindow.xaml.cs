using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Tehtävä5
{
    /// <summary>
    /// Interaction logic for GameWindow.xaml
    /// </summary>
    public partial class GameWindow : Window
    {
        MainWindow window;
        Stack<Ellipse> snake = new Stack<Ellipse>();
        Ellipse dot = new Ellipse();
        Line myLine = new Line();
        Key key;
        double x, y;
        Thread thread;
        List<Ellipse> apples = new List<Ellipse>();

        public GameWindow(MainWindow window)
        {
            InitializeComponent();
            this.window = window;
            createApples();
            thread = new Thread(workingThread);
            thread.Start();
            Draw();

        }

        public void Draw()
        {
            Ellipse head = new Ellipse();
            snake.Push(head);
            head.Fill = Brushes.Green;
            head.Width = 50;
            head.Height = 50;
            Canvas.SetTop(head, 0);
            Canvas.SetLeft(head, 0);
            canvas.Children.Add(head);

            dot.Fill = Brushes.Black;
            dot.Width = 5;
            dot.Height = 5;
            Canvas.SetTop(dot, 22.5);
            Canvas.SetLeft(dot, 22.5);
            canvas.Children.Add(dot);

            myLine.Stroke = Brushes.LightSteelBlue;
            myLine.X1 = 0;
            myLine.X2 = 0;
            myLine.Y1 = 0;
            myLine.Y2 = 0;
            myLine.HorizontalAlignment = HorizontalAlignment.Left;
            myLine.VerticalAlignment = VerticalAlignment.Center;
            myLine.StrokeThickness = 2;
            canvas.Children.Add(myLine);

        }

        private void Window_Closed(object sender, EventArgs e)
        {
            window.Show();
        }

        private void GrowSnake()
        {

        }

        private void createApples()
        {
            SolidColorBrush color = new SolidColorBrush(Color.FromArgb(255, 255, 0, 0));
            Random r = new Random();
            for (int i = 0; i < 20; i++)
            {
                Ellipse ellipse = new Ellipse();
                ellipse.Fill = color;
                ellipse.Width = 20;
                ellipse.Height = 20;
                Canvas.SetTop(ellipse, r.Next(0, 600));
                Canvas.SetLeft(ellipse, r.Next(0, 1000));
                apples.Add(ellipse);
                canvas.Children.Add(ellipse);
            }
        }

        private void MoveApple(Ellipse apple)
        {
            Random r = new Random();
            Canvas.SetTop(apple, r.Next(0, 600));
            Canvas.SetLeft(apple, r.Next(0, 1000));
        }

        private void HandleCollasions()
        {
            foreach (Ellipse e in apples)
            {
                double y = Canvas.GetTop(e) + 10;
                double x = Canvas.GetLeft(e) + 10;
                //If the distance between the snake head circle and the apple is less than radius+radius there is a collasion
                if (Math.Abs(Canvas.GetTop(snake.Last()) +25 - y) < 35 && Math.Abs(Canvas.GetLeft(snake.Last()) +25 - x) < 35)
                {
                    MoveApple(e);
                    GrowSnake();
                }
            }
        }

        private void Update()
        {
            switch (key)
            {
                case Key.Up: Canvas.SetTop(snake.Last(), y - 5); break;
                case Key.Down: Canvas.SetTop(snake.Last(), y + 5); break;
                case Key.Left: Canvas.SetLeft(snake.Last(), x - 5); break;
                case Key.Right: Canvas.SetLeft(snake.Last(), x + 5); break;
            }
            int i = 1;
        }

        public void workingThread()
        {
            while (true)
            {
                Dispatcher.Invoke((Action)(() =>
                {
                    y = Canvas.GetTop(snake.Last());
                    x = Canvas.GetLeft(snake.Last());
                    Update();
                    DrawDemos();
                    HandleCollasions();
                }));
                Thread.Sleep(25);
            }
        }

        private void DrawDemos()
        {
            Canvas.SetLeft(dot, Canvas.GetLeft(snake.Last()) + 22.5);
            Canvas.SetTop(dot, Canvas.GetTop(snake.Last()) + 22.5);

            myLine.Y1 = Canvas.GetTop(snake.Last()) + 25;
            myLine.Y2 = Canvas.GetTop(apples[0]) + 10;
            myLine.X1 = Canvas.GetLeft(snake.Last()) + 25;
            myLine.X2 = Canvas.GetLeft(apples[0]) + 10;



        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Up: if(key != Key.Down) key = Key.Up; break;
                case Key.Down: if(key != Key.Up) key = Key.Down; break;
                case Key.Left: if(key != Key.Right) key = Key.Left; break;
                case Key.Right: if(key != Key.Left) key = Key.Right;  break;
            }
        }
    }
}
