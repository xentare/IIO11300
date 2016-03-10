using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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

namespace H8ADONETWpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            GetData();
        }

        private void GetData()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(H8ADONETWpf.Properties.Settings.Default.Tietokanta))
                {
                    string sql = "SELECT id, asioid, firstname, lastname, date FROM presences";
                    SqlDataAdapter da = new SqlDataAdapter(sql, conn);
                    DataTable dt = new DataTable("presences");
                    da.Fill(dt);

                    myGrid.DataContext = dt;
                    conn.Close();
                }    
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
