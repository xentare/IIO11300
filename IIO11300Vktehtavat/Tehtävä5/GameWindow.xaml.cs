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
        List<Rectangle> snake = new List<Rectangle>();
        Key key;
        double x, y;
        int score;
        Thread thread;
        Vector snakeSize = new Vector();
        Vector appleSize = new Vector();
        SolidColorBrush color =  new SolidColorBrush(Color.FromArgb(255, 255, 0, 0));
        List<Rectangle> apples = new List<Rectangle>();
        int speed;
        bool paused;

        public GameWindow(MainWindow window)
        {
            InitializeComponent();
            this.window = window;
            thread = new Thread(workingThread);
            thread.Start();
            snakeSize.X = 20;
            snakeSize.Y = 20;
            appleSize.X = 20;
            appleSize.Y = 20;
            speed = 250;
            createApples();
            Initialize();
        }

        public void Initialize()
        {
            for (int i = 2; i > 0; i--)
            {
                Rectangle head = new Rectangle();
                head.Fill = Brushes.Green;
                head.Width = snakeSize.X;
                head.Height = snakeSize.Y;
                Canvas.SetTop(head, 0);
                Canvas.SetLeft(head, i*snakeSize.X);
                snake.Add(head);
                canvas.Children.Add(head);
            }
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            window.Show();
            thread.Abort();
        }

        private void GrowSnake()
        {
            Rectangle body = new Rectangle();
            body.Fill = Brushes.Green;
            body.Width = snakeSize.X;
            body.Height = snakeSize.Y;
            Canvas.SetTop(body, y);
            Canvas.SetLeft(body, x);
            snake.Add(body);
            canvas.Children.Add(body);
        }

        private void createApples()
        {
            Random r = new Random();
            for (int i = 0; i < 20; i++)
            {
                Rectangle rect = new Rectangle();
                rect.Fill = color;
                rect.Width = appleSize.X;
                rect.Height = appleSize.Y;
                Canvas.SetTop(rect, r.Next(0, 600/(int)appleSize.Y)*appleSize.Y);
                Canvas.SetLeft(rect, r.Next(0, 1000/(int)appleSize.X)*appleSize.X);
                apples.Add(rect);
                canvas.Children.Add(rect);
            }
        }

        private void MoveApple(Rectangle apple)
        {
            Random r = new Random();
            Canvas.SetTop(apple, r.Next(0, 600/20)*20);
            Canvas.SetLeft(apple, r.Next(0, 1000/20)*20);
        }

        private void HandleCollisions()
        {
            foreach (Rectangle e in apples)
            {
                double y = Canvas.GetTop(e);
                double x = Canvas.GetLeft(e);
                //Apples
                if (Canvas.GetTop(snake[0]) == y && Canvas.GetLeft(snake[0]) == x)
                {
                    MoveApple(e);
                    GrowSnake();
                    speed = speed - 10;
                    score++;
                    Title = score.ToString();
                }
                //Walls
                if(Canvas.GetTop(snake[0]) >= canvas.Height || Canvas.GetTop(snake[0]) < 0 || Canvas.GetLeft(snake[0]) >= canvas.Width || Canvas.GetLeft(snake[0]) < 0 )
                {
                    GameOver();
                }
                //Self
                for(int i = 1; i < snake.Count; i++)
                {
                    if(Canvas.GetTop(snake[0]) == Canvas.GetTop(snake[i]) && Canvas.GetLeft(snake[0]) == Canvas.GetLeft(snake[i]))
                    {
                        //GameOver();
                        Console.WriteLine("Collision");
                    }
                }

            }
        }

        private void Update()
        {
            y = Canvas.GetTop(snake.Last());
            x = Canvas.GetLeft(snake.Last());
            Rectangle head = new Rectangle();
            Canvas.SetTop(head, y);
            Canvas.SetLeft(head, x);
            head.Fill = color;
            head.Width = snakeSize.X;
            head.Height = snakeSize.Y;
            switch (key)
            { 
                case Key.Up: Canvas.SetTop(head, y - 20); break;
                case Key.Down: Canvas.SetTop(head, y + 20); break;
                case Key.Left: Canvas.SetLeft(head, x - 20); break;
                case Key.Right: Canvas.SetLeft(head, x + 20); break;
            }
            snake.Add(head);
            snake.RemoveAt(snake.Count-1);
        }

        private void GameOver()
        {
            Close();
            thread.Abort();
        }

        public void workingThread()
        {
            while (true)
            {
                if (!paused)
                {
                        try {
                        Dispatcher.Invoke((Action)(() =>
                        {
                            y = Canvas.GetTop(snake.Last());
                            x = Canvas.GetLeft(snake.Last());
                            HandleCollisions();
                            Update();
                        }));
                    }
                    catch (Exception e)
                    {

                    }
                }
                Thread.Sleep(speed);
            }
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Up: if(key != Key.Down) key = Key.Up; break;
                case Key.Down: if(key != Key.Up) key = Key.Down; break;
                case Key.Left: if(key != Key.Right) key = Key.Left; break;
                case Key.Right: if(key != Key.Left) key = Key.Right;  break;
                case Key.P: paused = !paused; break;
                case Key.Escape: Close(); thread.Abort(); break;
            }
        }
    }
}
