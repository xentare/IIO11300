using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
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
        DataTable pelaajatTable;
        public MainWindow()
        {
            InitializeComponent();

            //pelaajatListView.ItemsSource = pelaajat;
            //pelaajatListView.DisplayMemberPath = "KokoNimi";
            InitializeDBData();
        }

        private void InitializeDBData()
        {
            string queryString = "SELECT * FROM pelaajat";
            pelaajatTable = DBH4113.DBH4113.GetPlayerData(queryString).Tables["pelaajat"];
            pelaajatDataGrid.ItemsSource = pelaajatTable.DefaultView;
        }

        /*
        * Haetaan seurat combo boxiin
        */
        private void siirtohintaComboBox_Loaded(object sender, RoutedEventArgs e)
        {
            List<string> data = new List<string>();

            foreach(DataRow row in pelaajatTable.Rows)
            {
                if (!data.Contains(row["seura"]))
                {
                    data.Add(row["seura"].ToString());
                }
            }

            seuraComboBox.ItemsSource = data;
            seuraComboBox.SelectedIndex = 0;
        }

        private void uusiPelaaja_Click(object sender, RoutedEventArgs e)
        {

        }

        private void talletaPelaajaButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void poistaPelaajaButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void kirjoitaPelaajaButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void buttonSaveToDatabase_Click(object sender, RoutedEventArgs e)
        {

        }

        private void lopetaButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void pelaajatDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
        }
    }
}
