using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;


namespace Comprehensive_Project.basic_Algo
{

    class algo
    {
        public algo()
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
            //DB에서 값 받기

            String dicDB_len_sql = dicDB_len.ToString();
            String dic_sql = dicDB.ToString();
            String con_sql = contentsDB.ToString();
            //DB값을 저장.


            string stmt = "SELECT COUNT(단어) FROM dbo.word";
            string conCount = "SELECT COUNT(word) FROM dbo.contents";
            //string result_count = "SELECT COUNT(word) FROM dbo.score";
            int dic_db_count = 0;
            int con_db_count = 0;

            using (SqlConnection dic_db_Connection = new SqlConnection(connectionString))
            {
                using (SqlCommand dic_db_cmdCount = new SqlCommand(stmt, dic_db_Connection))
                {
                    dic_db_Connection.Open();
                    dic_db_count = (int)dic_db_cmdCount.ExecuteScalar();

                }//카운트 한 값 int로 변환
            }

            using (SqlConnection thisConnection = new SqlConnection(connectionString))
            {
                using (SqlCommand con_db_cmdCount = new SqlCommand(conCount, thisConnection))
                {
                    thisConnection.Open();
                    con_db_count = (int)con_db_cmdCount.ExecuteScalar();
                }//카운트 한 값 int로 변환
            }

            Console.WriteLine("컨텐츠 DB의 단어 수= " + con_db_count + "개");
            Console.WriteLine("사전 DB의 단어 수= " + dic_db_count + "개\n");
            String[,] dic_db_array = new String[dic_db_count, 4];//db카운트 값 만큼 배열 선언
            String[,] con_db_array = new String[con_db_count, 2];//db카운트 값 만큼 배열 선언
            String[,] result_array = new string[con_db_count, 4];//결과 값 넣을 배열
            int dic_count = 0;


            using (SqlCommand command = new SqlCommand(dic_sql, scon))
            {
                using (SqlDataReader dic_reader = command.ExecuteReader())
                {

                    while (dic_reader.Read())
                    {
                        dic_db_array[dic_count, 0] = dic_reader.GetString(0);
                        dic_db_array[dic_count, 1] = dic_reader.GetString(1);//배열에 값 저장
                        dic_db_array[dic_count, 2] = dic_reader.GetString(2);//배열에 값 저장
                        dic_db_array[dic_count, 3] = dic_reader.GetString(3);//배열에 값 저장
                        //Console.Write(dic_db_array[dic_count, 0]+"\t");
                        //Console.Write(dic_db_array[dic_count, 1] + "\t");
                        //Console.Write(dic_db_array[dic_count, 2] + "\t");
                        //Console.Write(dic_db_array[dic_count, 3] + "\n");
                        dic_count += 1;


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
                        String reder_data_0 = (String)reader.GetString(1);
                        con_db_array[con_count, 0] = reder_data_0;
                        String reder_data_1 = (String)reader.GetString(1);
                        con_db_array[con_count, 1] = reder_data_1;
                        con_count += 1;
                        if (con_count >= con_db_count) break;
                    }
                }
            }

            con_count = 0;
            int result_len = 0;
            while (con_count <= con_db_count)
            {
                if (con_count >= con_db_count) break;
                for (int j = 0; j < dic_db_count; j++)
                {
                    string con_key = con_db_array[con_count, 1];
                    string dic_key = dic_db_array[j, 0];
                    if (con_key == dic_key)
                    {

                        result_array[con_count, 0] = dic_db_array[j, 0];
                        result_array[con_count, 1] = dic_db_array[j, 1];
                        result_array[con_count, 2] = dic_db_array[j, 2];
                        result_array[con_count, 3] = dic_db_array[j, 3];
                        result_len++;
                        //Console.WriteLine(result_array[con_count, 1]);
                    }
                }
                con_count++;

            }
            int[] badge_key_array = new int[3];

            for (int i = 0; i < 3; i++)
            {
                badge_key_array[i] = print_rating_count(con_db_count, result_array, i + 1);
                badge_result(badge_key_array[i], i);
            }
            Console.WriteLine("");
        }

        public static int print_rating_count(int con_db_count, String[,] result_array, int col)
        {
            int result_count = 0;
            double score = 0;
            string[,] sum_score = new string[4, 2];
            int badge_count = 0;

            sum_score[1, 0] = "성차별";
            sum_score[2, 0] = "인종차별";
            sum_score[3, 0] = "비속어";


            while (true)
            {
                if (result_count >= con_db_count) break;

                string key = result_array[result_count, col];
                string db_word = result_array[result_count, 0];
                switch (key)
                {
                    case "4등급":
                        score += 0.1;
                        Console.WriteLine(db_word + "  " + sum_score[col, 0] + " " + key + "  " + score + "점");
                        break;
                    case "3등급":
                        score += 0.06;
                        Console.WriteLine(db_word + "  " + sum_score[col, 0] + " " + key + "  " + score + "점");
                        break;
                    case "2등급":
                        score += 0.03;
                        Console.WriteLine(db_word + "  " + sum_score[col, 0] + " " + key + "  " + score + "점");
                        break;
                    case "1등급":
                        score += 0.01;
                        Console.WriteLine(db_word + "  " + sum_score[col, 0] + " " + key + "  " + score + "점");
                        break;
                    default:
                        break;

                }
                result_count++;


            }

            // 0.2점 단위로 등급 판정
            if (score < 0.2)
            {
                sum_score[col, 1] = "0등급";
            }
            else if (score < 0.4)
            {
                sum_score[col, 1] = "1등급";
            }
            else if (score < 0.6)
            {
                sum_score[col, 1] = "2등급";
            }
            else if (score < 0.8)
            {
                sum_score[col, 1] = "3등급";
            }
            else
            {
                sum_score[col, 1] = "4등급";
            }


            if (score >= 1.0)
            {
                score = 1.0;
                Console.WriteLine("\n최고 점수 초과\n과도한 " + sum_score[col, 0] + " 관련 단어가 나온 영상입니다.\n시청에 충분한 주의가 필요합니다.");
                badge_count++;
            }

            //sum_score[col, 1] = score.ToString("F"); //0.2점 단위로 등급 판정 변경 인해 주석처리
            Console.WriteLine("\n");
            //Console.WriteLine("유의미한 단어 수= " + result_len + "개");
            Console.WriteLine("최종" + sum_score[col, 0] + "등급 : " + sum_score[col, 1] + "입니다\n");


            switch (sum_score[col, 0])
            {
                case "성차별":
                    Ratio.sex = score;
                    Ratio.sexStr = sum_score[col, 1];
                    break;
                case "인종차별":
                    Ratio.racism = score;
                    Ratio.racStr = sum_score[col, 1];
                    break;
                case "비속어":
                    Ratio.dirtyWord = score;
                    Ratio.dirtyWordStr = sum_score[col, 1];
                    break;
                default:
                    break;
            }



            return badge_count;
        }


        public string badge_result(int badge_key, int array_key)
        {
            string[] array_badge = new string[3];
            array_badge[0] = "선정성";
            array_badge[1] = "인종차별";
            array_badge[2] = "폭력성";
            if (badge_key == 1)
            {
                Console.Write(array_badge[array_key] + " 태그.\t");
                Ratio.tag = Ratio.tag + " " + array_badge[array_key];
            }
            return array_badge[array_key];
        }
    }
}