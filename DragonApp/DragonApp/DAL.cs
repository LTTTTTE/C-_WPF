using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonApp
{
    public static class DAL
    {
        public static DataTable Exec_sp(string sp_name,List<SqlParameter> sql_params = null)
        {
            string str_connect = "Server=DESKTOP-BNR6PG7;Database=MyLoginApp;Trusted_Connection=True;";

            SqlConnection conn = new SqlConnection();

            DataTable dt = new DataTable();

            try
            {
                conn = new SqlConnection(str_connect);
                conn.Open();

                SqlCommand cmd = new SqlCommand(sp_name, conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddRange(sql_params.ToArray());

                SqlCommand command = conn.CreateCommand();
                SqlDataReader dr = cmd.ExecuteReader();

                dt.Load(dr);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
            return dt;
        }

    }
}
