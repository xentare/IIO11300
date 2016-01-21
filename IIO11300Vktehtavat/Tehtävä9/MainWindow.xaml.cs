using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
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

namespace Tehtävä9
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ObservableCollection<Pelaaja> pelaajat = new ObservableCollection<Pelaaja>();
        Pelaaja pelaaja;
        int lastSelectedIndex;
        public MainWindow()
        {
            InitializeComponent();

            pelaajatListbox.ItemsSource = pelaajat;
            pelaajatListbox.DisplayMemberPath = "KokoNimi";
        }

        private void siirtohintaComboBox_Loaded(object sender, RoutedEventArgs e)
        {
            List<string> data = new List<string>();
            data.Add("TPS");
            data.Add("Lukko");
            data.Add("Ässät");
            data.Add("HIFK");
            data.Add("Blues");
            data.Add("HPK");
            data.Add("Tappara");
            data.Add("Ilves");
            data.Add("Sport");
            data.Add("Pelicans");
            data.Add("Kookoo");
            data.Add("SaiPa");
            data.Add("Kärpät");
            data.Add("JYP");
            data.Add("KalPa");

            var comboBox = sender as ComboBox;
            comboBox.ItemsSource = data;
            comboBox.SelectedIndex = 0;

        }
        private void siirtohintaComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var comboBox = sender as ComboBox;
        }

        private void uusiPelaaja_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                pelaaja = new Pelaaja(etunimiTextBox.Text, sukunimiTextBox.Text, double.Parse(siirtohintaTextBox.Text), seuraComboBox.Text);
                if (!pelaajat.Any(p => p.KokoNimi == pelaaja.KokoNimi))
                {
                    pelaajat.Add(pelaaja);
                    statusBarTextBlock.Text = "Pelaaja lisätty!";
                }
                else
                {
                    statusBarTextBlock.Text = "Tämän niminen pelaaja on jo listassa!";
                }
            }
            catch (FormatException error)
            {
                statusBarTextBlock.Text = error.ToString();
            }
        }

        private void pelaajatListbox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (pelaajatListbox.SelectedIndex != -1)
            {
                lastSelectedIndex = pelaajatListbox.SelectedIndex;
                Pelaaja temp = (Pelaaja)pelaajatListbox.SelectedItem;

                etunimiTextBox.Text = temp.Etunimi;
                sukunimiTextBox.Text = temp.Sukunimi;
                seuraComboBox.Text = temp.Seura;
                siirtohintaTextBox.Text = temp.Siirtohinta.ToString();
            }
        }

        private void kirjoitaPelaajaButton_Click(object sender, RoutedEventArgs e)
        {
            string sPath = "E:/pelaajat.txt";

            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.DefaultExt = ".dat";
            saveFileDialog.Filter = "Dat files (.dat)|*.dat";
            saveFileDialog.FileName = "Pelaajat.dat";
            if (saveFileDialog.ShowDialog() == true)
            {
                string path = saveFileDialog.FileName;
                StreamWriter writer = new StreamWriter(path);
                foreach (var item in pelaajatListbox.Items)
                {
                    writer.WriteLine(item.ToString());
                }
                writer.Flush();
                writer.Close();
            }

            statusBarTextBlock.Text = "Pelaajat kirjoitettu tiedostoon!";

            statusBarTextBlock.Text = "Pelaajat tallennettu tiedostoon!" + " " + saveFileDialog.FileName;
        }

        private void talletaPelaajaButton_Click(object sender, RoutedEventArgs e)
        {

            pelaajat[lastSelectedIndex].Etunimi = etunimiTextBox.Text;
            pelaajat[lastSelectedIndex].Sukunimi = sukunimiTextBox.Text;
            pelaajat[lastSelectedIndex].Seura = seuraComboBox.Text;
            pelaajat[lastSelectedIndex].Siirtohinta = double.Parse(siirtohintaTextBox.Text);
            pelaajatListbox.DisplayMemberPath = "";
            pelaajatListbox.DisplayMemberPath = "KokoNimi";

            statusBarTextBlock.Text = "Pelaaja talletettu!";
            statusBarTextBlock.Text = "Pelaajat talletettu!";
        }

        private void poistaPelaajaButton_Click(object sender, RoutedEventArgs e)
        {
            Pelaaja temp = (Pelaaja)pelaajatListbox.SelectedItem;
            pelaajat.Remove(temp);
            statusBarTextBlock.Text = "Pelaaja poistettu!";
            statusBarTextBlock.Text = "Pelaajat poistettu!";
        }

        private void lopetaButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
