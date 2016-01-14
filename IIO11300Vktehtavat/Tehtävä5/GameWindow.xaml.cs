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
        Ellipse snake = new Ellipse();
        Key key;
        double x, y;
        Thread thread;
        List<Ellipse> apples = new List<Ellipse>();

        public GameWindow(MainWindow window)
        {
            InitializeComponent();
            this.window = window;
            thread = new Thread(workingThread);
            thread.Start();
            DrawRectangle();
        }

        public void DrawRectangle()
        {
            SolidColorBrush colorBrush = new SolidColorBrush(Color.FromArgb(255, 0, 155, 0));
            snake.Fill = colorBrush;
            snake.Width = 25;
            snake.Height = 25;
            Canvas.SetTop(snake, 0);
            Canvas.SetLeft(snake, 0);
            canvas.Children.Add(snake);
            createApples();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            window.Show();
        }

        private void canvas_KeyDown(object sender, KeyEventArgs e)
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
                ellipse.Width = 10;
                ellipse.Height = 10;
                Canvas.SetTop(ellipse, r.Next(0, 600));
                Canvas.SetLeft(ellipse, r.Next(0, 1000));
                apples.Add(ellipse);
                canvas.Children.Add(ellipse);
            }
        }



        public void workingThread()
        {
            while (true)
            {
                Dispatcher.Invoke((Action)(() =>
                {
                    y = Canvas.GetTop(snake);
                    x = Canvas.GetLeft(snake);
                    switch (key)
                {
                    case Key.Up: Canvas.SetTop(snake, y - 5); break;
                    case Key.Down: Canvas.SetTop(snake, y + 5); break;
                    case Key.Left: Canvas.SetLeft(snake, x - 5); break;
                    case Key.Right: Canvas.SetLeft(snake, x + 5); break;
                }
                }));
                Thread.Sleep(100);
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
            }
        }
    }
}
