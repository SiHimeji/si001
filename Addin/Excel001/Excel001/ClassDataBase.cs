using System;
using System.Collections.Generic;
using System.Linq;
//using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;

namespace cLib
{
    public class ClassDataBase
    {

        ///SQL Server
        ///
        string conn_str = "Data Source = 192.168.1.55; Initial Catalog = si; Connect Timeout = 60; Persist Security Info=True;User ID = si; Password=0251";
        private SqlConnection cn = new SqlConnection();
        private SqlCommand cm = new SqlCommand();
        private SqlDataReader rs;
        public string ErrorMSg;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="strSQL"></param>
        /// <returns></returns>
        public DataTable GetDataTable(string strSQL)
        {
            DataTable dt = new DataTable();

            using (cn = new SqlConnection(conn_str))
            {
                try
                {
                    cn.Open();
                    using (SqlCommand cmd = cn.CreateCommand())
                    {
                        cmd.CommandText = strSQL;
                        // DataAdapterの生成
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        // データベースからデータを取得
                        da.Fill(dt);
                    }

                }
                catch (Exception e)
                {
                    ErrorMSg = e.Message;
                }
            }
            return dt;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="strSQL"></param>
        /// <returns></returns>
        public Boolean EXEC(string strSQL)
        {
            using (cn = new SqlConnection(conn_str))
            {
                try
                {
                    cn.Open();
                    cm.Connection = cn;
                    cm = cn.CreateCommand();
                    cm.CommandText = strSQL;
                    cm.ExecuteNonQuery();
                    cm.Clone();
                    return (true);
                }
                catch (Exception e)
                {
                    ErrorMSg = e.Message;
                    return (false);
                }
            }
        }



    }
}
