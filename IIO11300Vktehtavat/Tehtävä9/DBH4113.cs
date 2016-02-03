using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBH4113
{
    class DBH4113
    {
        public static DataSet GetPlayerData(string queryString)
        {
            DataSet dataset = new DataSet();
            using(OleDbConnection connection = new OleDbConnection(Tehtävä9.Properties.Settings.Default.H4113ConnectionString) )
            {
                OleDbDataAdapter adapter = new OleDbDataAdapter();
                adapter.SelectCommand = new OleDbCommand(queryString, connection);

                connection.Open();

                adapter.Fill(dataset, "pelaajat");

                return dataset;
            }
        }
    }
}
