using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
namespace Comprehensive_Project.DataBase
{
    class DataBaseAccess
    {
        public DataBaseAccess(String[] input)
        {
           
                String connectionString = "server = hyunsam.asuscomm.com; Database = dictionary; User id = tester; Password = tester";
                SqlConnection scon = new SqlConnection(connectionString);
                SqlCommand scom = new SqlCommand();
            try
            {
                scon.Open();


            }
            catch (Exception e)
            {
                scom.Connection.Close();
                throw;
            }
        }
    }
}
