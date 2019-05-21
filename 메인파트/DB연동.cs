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
            StringBuilder db_len = new StringBuilder();
            sb.Append("SELECT * FROM dbo.word");
            db_len.Append("SELECT COUNT(단어) FROM dbo.word");



            String db_len_sql = db_len.ToString();
            String sql = sb.ToString();
            /*
            int db_array_length = Convert.ToInt32(db_len.ExecuteScalar());
            
             * (//int db_array_length = int.Parse(db_len.ToString());

            //String[] db_array = new String[db_array_length];
            //int count = 0;
            */
            Console.WriteLine(db_len_sql);

            string stmt = "SELECT COUNT(단어) FROM dbo.word";
            int db_count = 0;

            using (SqlConnection thisConnection = new SqlConnection(connectionString))
            {
                using (SqlCommand cmdCount = new SqlCommand(stmt, thisConnection))
                {
                    thisConnection.Open();
                    db_count = (int)cmdCount.ExecuteScalar();
                }//카운트 한 값 int로 변환
            }
            Console.WriteLine(db_count);
            String[,] db_array = new String[db_count, 2];//db카운트 값 만큼 배열 선언
            int count = 0;

            using (SqlCommand command = new SqlCommand(sql, scon))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                   
                    while (reader.Read())
                    {
                        //Console.WriteLine("{0} {1}", reader.GetString(0), reader.GetString(1));//열 2개
                        //Console.WriteLine("{0}", reader.GetString(0));//열 1개//테이블 출력
                        db_array[count, 0] = reader.GetString(0);
                        db_array[count, 1] = reader.GetString(1);//배열에 값 저장
                        count+=1;
                        //db_array[count++] = reader.GetString(0);
                    }
                }
            }

            int test_count = 0;
            while(test_count < db_count)
            {
                Console.Write(db_array[test_count,0]);
                Console.Write("\t");
                Console.Write(db_array[test_count,1]);
                Console.Write("\n");

                test_count += 1;
                                
            }
            //값 저장확인
        }
    }
}