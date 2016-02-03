using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBH4113
{
    class DBH4113
    {
        public static void GetPlayerData()
        {
            try
            {
                string sql = "SELECT etunimi,sukunimi,siirtohinta,seura FROM pelaaja";
                string connStr = Tehtävä9.Properties.Settings.Default.H4113ConnectionString;
                Console.WriteLine(connStr);
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    conn.Open();
                    //using (SqlCommand cmd = new SqlCommand(sql, conn))
                    //{
                    //    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    //    DataSet ds = new DataSet();
                    //    adapter.Fill(ds, "pelaajat");
                    //    return ds.Tables["pelaajat"];
                    //}
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
