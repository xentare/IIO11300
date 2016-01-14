using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using System.Xml.Linq;

namespace Tehtävä7
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            string path = ConfigurationManager.AppSettings["viiniData"];
            List<string> maatList = new List<string>();

            XDocument doc = null;
            try
            {
                doc = XDocument.Load(path);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                Console.WriteLine("");
                MessageBox.Show("Can't find XML filepath, terminating...");
                //this.Close();
            }

            //Haetaan maat XML-tagien sisästä
            var maat = doc.Descendants("maa");
            maatList.Add("Kaikki");
            foreach (var maa in maat)
            {
                if (!maatList.Contains(maa.Value))
                {
                    maatList.Add(maa.Value);
                }
            }
            maatComboBox.ItemsSource = maatList;
            maatComboBox.SelectedIndex = 0;

            //Asetetaan alkuarvot viinilistalle
            XmlDataProvider dp = viinitListView.DataContext as XmlDataProvider;
            dp.Source = new System.Uri(path);
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            XmlDataProvider dp = viinitListView.DataContext as XmlDataProvider;

            if (maatComboBox.Text != "Kaikki")
            {
                dp.XPath = string.Format("viinikellari/wine[contains(maa,\"{0}\")]", maatComboBox.Text);
            }
            else
            {
                dp.XPath = "viinikellari/wine";
            }
        }
    }
}
