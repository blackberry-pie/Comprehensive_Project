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
        private String insetSql = "INSERT INTO dbo.contents (number,word) VALUES (@number, @word)";
        private String deleteSql = "DELETE FROM dbo.contents";
        private String truncateSql = "TRUNCATE TABLE dbo.contents";
        private String connectionString = "server = hyunsam.asuscomm.com; Database = dictionary; User id = tester; Password = tester";

        public DataBaseAccess(String[] input)
        {
            this.TruncateContentsTable();
            this.InsertData(input);
        }


        public void InsertData(String[] data)
        {




            using (SqlConnection connect = new SqlConnection(connectionString))
            {
                connect.Open();

                using (SqlCommand command = new SqlCommand(insetSql, connect))
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
                Console.WriteLine("sql insert finish");
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
            //delete table
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

    }
}
