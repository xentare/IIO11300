using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H7ADONETConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                string connStr = H7ADONETConsole.Properties.Settings.Default.Tietokanta;
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("SELECT id, asioid, firstname, lastname, date FROM presences", conn);
                    SqlCommand cmd2 = new SqlCommand("DELETE FROM presences WHERE id = 334", conn);
                    SqlCommand cmd3 = new SqlCommand("UPDATE presences SET date = '2016-01-28' WHERE id = 333", conn);
                    SqlDataReader rdr = cmd.ExecuteReader();
                    if (rdr.HasRows)
                    {
                        while (rdr.Read())
                        {
                            Console.WriteLine("Läsnäolosi {0} {1} {2} {3} {4}",rdr.GetInt16(0),rdr.GetString(1),rdr.GetString(2), rdr.GetString(3), rdr.GetDateTime(4));
                        }

                    }
                    rdr.Close();
                    conn.Close();
                    Console.WriteLine("Tietokantayhteys suljettu onnistuneesti.");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                Console.ReadKey();
            }

        }
    }
}
