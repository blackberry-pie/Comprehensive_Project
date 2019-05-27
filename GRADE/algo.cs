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
            SqlConnection con_scon = new SqlConnection(connectionString);
            SqlCommand scom = new SqlCommand();
            //scom.Connection = scon;
            //scom.CommandText = "SELECT * FROM dbo.word";
            scon.Open();
            con_scon.Open();


            StringBuilder dicDB = new StringBuilder();
            StringBuilder contentsDB = new StringBuilder();
            StringBuilder dicDB_len = new StringBuilder();
            StringBuilder conDB_len = new StringBuilder();
            dicDB.Append("SELECT * FROM dbo.word");//DB값 받기
            contentsDB.Append("SELECT * FROM dbo.contents");
            dicDB_len.Append("SELECT COUNT(단어) FROM dbo.word");
            conDB_len.Append("SELECT COUNT(단어) FROM dbo.contents");



            String dicDB_len_sql = dicDB_len.ToString();
            String dic_sql = dicDB.ToString();
            String con_sql = contentsDB.ToString();
            /*
            int db_array_length = Convert.ToInt32(dicDB_len.ExecuteScalar());
            
             * (//int db_array_length = int.Parse(dicDB_len.ToString());

            //String[] db_array = new String[db_array_length];
            //int count = 0;
            */
            Console.WriteLine(dicDB_len_sql);

            string stmt = "SELECT COUNT(단어) FROM dbo.word";
            string conCount = "SELECT COUNT(word) FROM dbo.contents";
            int dic_db_count = 0;
            int con_db_count = 0;

            using (SqlConnection dic_db_Connection = new SqlConnection(connectionString))
            {
                using (SqlCommand dic_db_cmdCount = new SqlCommand(stmt, dic_db_Connection))
                {
                    dic_db_Connection.Open();
                    dic_db_count = (int)dic_db_cmdCount.ExecuteScalar();
               
                }//카운트 한 값 int로 변환
                //dic_db_Connection.Close();
            }

            using (SqlConnection thisConnection = new SqlConnection(connectionString))
            {
                using (SqlCommand con_db_cmdCount = new SqlCommand(conCount, thisConnection))
                {
                    thisConnection.Open();
                    con_db_count = (int)con_db_cmdCount.ExecuteScalar();
                }//카운트 한 값 int로 변환
                //thisConnection.Close();
            }
            
            Console.WriteLine(con_db_count);
            Console.WriteLine(dic_db_count);
            String[,] dic_db_array = new String[dic_db_count, 2];//db카운트 값 만큼 배열 선언
            String[,] con_db_array = new String[con_db_count,2];//db카운트 값 만큼 배열 선언
            String[,] result_array = new string[con_db_count,2];
            int dic_count = 0;

            using (SqlCommand command = new SqlCommand(dic_sql, scon))
            {
                using (SqlDataReader dic_reader = command.ExecuteReader())
                {
                   
                    while (dic_reader.Read())
                    {
                        //Console.WriteLine("{0} {1}", reader.GetString(0), reader.GetString(1));//열 2개
                        //Console.WriteLine("{0}", reader.GetString(0));//열 1개//테이블 출력
                        dic_db_array[dic_count, 0] = dic_reader.GetString(0);
                        dic_db_array[dic_count, 1] = dic_reader.GetString(1);//배열에 값 저장
                        dic_count += 1;
                        //dic_db_array[count++] = reader.GetString(0);

                    }
                    dic_reader.Close();
                }
                
            }
            int con_count = 0;
            using (SqlCommand con_command = new SqlCommand(con_sql, con_scon))
            {
                using (SqlDataReader reader = con_command.ExecuteReader())
                {

                    while (reader.Read())
                    {
                        //Console.WriteLine("{0} {1}", reader.GetString(0), reader.GetString(1));//열 2개
                        //Console.WriteLine("{0}", reader.GetString(0));//열 1개//테이블 출력
                        String reder_data_0 = (String)reader.GetString(1);
                        con_db_array[con_count,0] = reder_data_0;
                        String reder_data_1 = (String)reader.GetString(1);
                        con_db_array[con_count, 1] = reder_data_1;
                        //db_array[count, 1] = reader.GetString(1);//배열에 값 저장
                        con_count += 1;
                        //Console.WriteLine(con_count);
                        //db_array[count++] = reader.GetString(0);
                        if (con_count >= con_db_count) break;
                    }
                }
            }

            con_count = 0;
            while (con_count <= con_db_count)
            {
                if (con_count >= con_db_count) break;
                for (int j = 0; j < dic_db_count; j++)
                {
                    string con_key = con_db_array[con_count,1];
                    string dic_key = dic_db_array[j, 0];
                    if (con_key == dic_key)
                    {

                        result_array[con_count, 1] = dic_db_array[j, 1];
                        result_array[con_count, 0] = dic_db_array[j, 0];
                        Console.WriteLine(result_array[con_count, 1]);
                    }
                }
                //Console.WriteLine(result_array[con_count++]);
                con_count++;


            }
            int result_count = 0;
            int score = 0;
            while (true)
            {
                if (result_count >= con_db_count) break;

                string key = result_array[result_count, 1];
                string db_word = result_array[result_count, 0];
                switch (key)
                {
                    case "4등급":
                        score += 10;
                        Console.WriteLine(db_word + "  " + key + "  " + score + "점");
                        break;
                    case "3등급":
                        score += 6;
                        Console.WriteLine(db_word + "  " + key + "  " + score + "점");
                        break;
                    case "2등급":
                        score += 3;
                        Console.WriteLine(db_word + "  " + key + "  " + score + "점");
                        break;
                    case "1등급":
                        score += 1;
                        Console.WriteLine(db_word + "  " + key + "  " + score + "점");
                        break;
                    default:
                        break;

                }
                
                //Console.Write(score);
                //Console.Write("\t");
                //Console.Write(key);
                //Console.Write("\t");
                result_count++;

            }
            Console.WriteLine("\n");
            Console.WriteLine("최종 등급 점수는 = " + score + "점");


            /*int test_count = 0;
              while (test_count < dic_db_count)
              {
                  Console.Write(dic_db_array[test_count, 0]);
                  Console.Write("\t");
                  Console.Write(dic_db_array[test_count, 1]);
                  Console.Write("\n");

                  test_count += 1;

              }*/


            /*test_count = 0;
              while (test_count < dic_db_count)
              {
                  Console.Write(con_db_array[test_count]);
                  Console.Write("\t");
                  test_count += 1;
                  if (test_count >= con_db_count) break;

              }
              //값 저장확인
              */

        }
    }
}