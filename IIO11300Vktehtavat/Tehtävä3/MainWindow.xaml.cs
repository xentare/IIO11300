using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;

namespace Tehtävä3
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            txtBoxResultFile.Text = ConfigurationManager.AppSettings["resultFileName"];

        }

        private void btnGetFiles_Click(object sender, RoutedEventArgs e)
        {
            findFiles(txtBoxFolder.Text);
        }

        private void findFiles(string folder)
        {
            try
            {
                string[] dirs = Directory.GetFiles(folder, "*.txt");
                List<string> list = new List<string>();
                list = dirs.ToList();
                listBoxFiles.ItemsSource = list;
                txtBlockStatusBar.Text = "Found " + dirs.Count().ToString() + "files";
            }
            catch (DirectoryNotFoundException e)
            {
                txtBlockStatusBar.Text = e.Message;
            }
            catch(Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }
        }

        private void btnCombineFiles_Click(object sender, RoutedEventArgs e)
        {
            string result = "";
            string path = txtBoxResultFile.Text;
            List<string> selected = new List<string>();

            foreach(string i in listBoxFiles.SelectedItems)
            {
                selected.Add(i);
            }
            foreach(string i in selected)
            {
                string lines = File.ReadAllText(i, Encoding.UTF8);
                result += lines;
                result += "\r\n";
            }

            File.WriteAllText(path, result);

        }
    }
}
