using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;


namespace basic_Algo
{
    class Program
    {
        static void Main(string[] args)
        {
            string connectionString = "server =hyunsam.asuscomm.com;Database = dictionary;User id = deokjin;Password = s2260827a";

            SqlConnection scon = new SqlConnection(connectionString);
            SqlCommand scom = new SqlCommand();
            //scom.Connection = scon;
            //scom.CommandText = "SELECT * FROM dbo.word";
            scon.Open();

            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT 단어 FROM dbo.word");

            String sql = sb.ToString();

            using (SqlCommand command = new SqlCommand(sql, scon))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        //Console.WriteLine("{0} {1}", reader.GetString(0), reader.GetString(1));//열 2개
                        Console.WriteLine("{0}", reader.GetString(0));//열 1개
                    }
                }
            }

        }
    }
}