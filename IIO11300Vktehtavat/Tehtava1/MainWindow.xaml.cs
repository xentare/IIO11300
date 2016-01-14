using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Tehtävä1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private bool isOkay(string syote, TextBox sender)
        {
            double luku = 0;
            if (syote.Length > 0 && double.TryParse(syote, out luku))
            {
                return true;

            }
            else
            {
                MessageBox.Show("Tarkista syöte!");
                sender.Focus();
                return false;
            }
        }
        private void button_Click(object sender, RoutedEventArgs e)
        {
            try {
                int width = Int32.Parse(windowWidth.Text);
                int height = Int32.Parse(windowHeight.Text);
                int frame = Int32.Parse(frameWidth.Text);

                int winArea = (width - frame) * (height - frame);

                windowArea.Text = (winArea).ToString();
                frameRing.Text = (width * 2 + height * 2).ToString();
                frameArea.Text = (width * height - winArea).ToString();

            }
            catch (Exception a)
            {
                Console.WriteLine("{0} Exception caught.", a);
                MessageBox.Show("Vain kokonaislukuja");
            }
        }

    }
}
