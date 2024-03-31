using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace MDK._01._01_PR_30.Classes
{
    public class Connection
    {
#if true
        static readonly string connectionString = "Server=127.0.0.1;Database=CarsBase;Uid=root;Pwd=;";
#else
        static readonly string connectionString = "Server=student.permaviat.ru;Database=ISP_21_2_10;Uid=ISP_21_2_10;Pwd=DSFV988Np#;";
#endif

        public static MySqlConnection GetConnection()
        {
            return new MySqlConnection(connectionString);
        }

        public static bool ExecuteNonQuery(string commandText, Dictionary<string, object> parameters)
        {
            using (MySqlConnection conn = Connection.GetConnection())
            {
                try
                {
                    conn.Open();
                }
                catch
                {
                    return false;
                }

                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = commandText;
                foreach (var param in parameters)

                    cmd.Parameters.AddWithValue(param.Key, param.Value);

                cmd.ExecuteNonQuery();
            }
            return true;
        }
    }
}
