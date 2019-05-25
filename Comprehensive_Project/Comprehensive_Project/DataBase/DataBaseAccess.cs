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
        public DataBaseAccess(String[] input)
        {
            this.InsertData(input);
        }


        public void InsertData(String[] data)
        {

            String connectionString = "server = hyunsam.asuscomm.com; Database = dictionary; User id = tester; Password = tester";


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
                        command.Parameters.Add(new SqlParameter("@word", SqlDbType.VarChar,40));

                        //C#에서 만든 매개변에 실제 값을 대입
                        command.Parameters["@number"].Value = i+1;
                        command.Parameters["@word"].Value = data[i];
                        command.ExecuteNonQuery();
                    }

                    

                }


                connect.Close();
                Console.WriteLine("sql insert finish");
            }
            

        }

        public void DeleteContentsTable()
        {
            //delete table
        }
    }
}
