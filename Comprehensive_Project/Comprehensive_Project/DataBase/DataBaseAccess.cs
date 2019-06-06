using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Threading.Tasks;
using System.Data.SqlClient;
namespace Comprehensive_Project.DataBase
{
    class DataBaseAccess
    {
        private readonly String insetContentsSql = "INSERT INTO dbo.contents (number,word) VALUES (@number, @word)";
        private readonly String insetResultSql = "INSERT INTO dbo.result (number, link, 성차별, 인종차별, 비속어, 태그,videoname) VALUES (@number, @link, @성차별, @인종차별, @비속어, @태그,@videoname)";
        private readonly String deleteSql = "DELETE FROM dbo.contents";
        private readonly String truncateSql = "TRUNCATE TABLE dbo.contents";
        private readonly String truncateSqlResultTable = "TRUNCATE TABLE dbo.result";
        private readonly String connectionString = "server = hyunsam.asuscomm.com; Database = dictionary; User id = tester; Password = tester";

        public DataBaseAccess()
        {
 
        }
        public void InsertParserData(String[] data)
        {
            using (SqlConnection connect = new SqlConnection(connectionString))
            {
                connect.Open();

                using (SqlCommand command = new SqlCommand(insetContentsSql, connect))
                {
                    command.CommandType = CommandType.Text;

                    for (int i = 0; i < data.Length; i++)
                    {
                        command.Parameters.Clear();
                        //매개변수 객체 생성(C#에서의) -> SQL @변수와 대응됨
                        command.Parameters.Add(new SqlParameter("@number", SqlDbType.Int));
                        command.Parameters.Add(new SqlParameter("@word", SqlDbType.VarChar, 40));

                        //C#에서 만든 매개변에 실제 값을 대입
                        command.Parameters["@number"].Value = i + 1;
                        command.Parameters["@word"].Value = data[i];
                        command.ExecuteNonQuery();
                    }
                }
                connect.Close();
                //Console.WriteLine("sql insert finish");
            }
        }
        public void InsertRatioData()
        {
            using (SqlConnection connect = new SqlConnection(connectionString))
            {
                connect.Open();
                using (SqlCommand command = new SqlCommand(insetResultSql, connect))
                {
                    command.CommandType = CommandType.Text;
                    command.Parameters.Clear();

                    command.Parameters.Add(new SqlParameter("@number", SqlDbType.Int));
                    command.Parameters.Add(new SqlParameter("@link", SqlDbType.VarChar, 200));
                    command.Parameters.Add(new SqlParameter("@성차별", SqlDbType.Float));
                    command.Parameters.Add(new SqlParameter("@인종차별", SqlDbType.Float));
                    command.Parameters.Add(new SqlParameter("@비속어", SqlDbType.Float));
                    command.Parameters.Add(new SqlParameter("@태그", SqlDbType.VarChar, 20));
                    command.Parameters.Add(new SqlParameter("@videoname", SqlDbType.VarChar, 300));


                    command.Parameters["@number"].Value = 1;
                    command.Parameters["@link"].Value = Ratio.link;
                    command.Parameters["@성차별"].Value = Ratio.sex;
                    command.Parameters["@인종차별"].Value = Ratio.racism;
                    command.Parameters["@비속어"].Value = Ratio.dirtyWord;
                    command.Parameters["@태그"].Value = Ratio.tag;
                    command.Parameters["@videoname"].Value = Ratio.GetvideoName();

                    command.ExecuteNonQuery();
                }
                connect.Close();
            }

        }
        public void DeleteContentsTable()//컨텐츠 테이블 초기화
        {
            //delete table
            using (SqlConnection connect = new SqlConnection(connectionString))
            {
                connect.Open();

                using (SqlCommand command = new SqlCommand(deleteSql, connect))
                {
                    command.Parameters.Clear();
                    command.CommandType = CommandType.Text;
                    command.ExecuteNonQuery();
                }
                connect.Close();
            }
        }
        public void TruncateContentsTable()//컨텐츠 테이블 초기화
        {
            //truncate table
            using (SqlConnection connect = new SqlConnection(connectionString))
            {
                connect.Open();

                using (SqlCommand command = new SqlCommand(truncateSql, connect))
                {
                    command.Parameters.Clear();
                    command.CommandType = CommandType.Text;
                    command.ExecuteNonQuery();
                }
                connect.Close();
            }
        }
        public void TruncateResultTable()
        {
            using (SqlConnection connect = new SqlConnection(connectionString))
            {
                connect.Open();

                using (SqlCommand command = new SqlCommand(truncateSqlResultTable, connect))
                {
                    command.Parameters.Clear();
                    command.CommandType = CommandType.Text;
                    command.ExecuteNonQuery();
                }
                connect.Close();
            }
        }

    }
}
