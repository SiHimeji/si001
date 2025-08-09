using System;
using System.Collections.Generic;
using System.Text;
using System.Data.OleDb;
using Microsoft.VisualBasic;
using Oracle.ManagedDataAccess.Client;
using System.IO;
using System.Data;

//********************************************************
// Oracle
//********************************************************
namespace ExcelAddIn1
{
    public class ClassOracle
    {
        //
        private Boolean login;
        private OracleConnection cnn = new OracleConnection();
        private OracleCommand cmd = new OracleCommand();
        private OracleDataReader rs ;
       private string SQL;
        private string ErrorMSg;
        private string[] strSQL;
        private int SQLCNT;
        private string DBMS;
        private string conStr;

        //        public ClassOracle(string DB, string User, string Pass)
        public ClassOracle()
        {
            login = false;
       
        }
        //****************************************************************
        //   
        //  Login
        //
        //****************************************************************

        public void SetOracle(String DB)
        {
            //  conStr =
            //     "user id=CRMSYU;password=E46XPWGW;data source=(DESCRIPTION=(ADDRESS=(PROTOCOL=tcp)(HOST=172.31.37.106)(PORT=1521))(CONNECT_DATA=(SERVICE_NAME=NRDB.WORLD)))";


            if (DB == "SYU")
            {
                conStr =
                   "user id=CRMSYU;password=E46XPWGW;data source=(DESCRIPTION=(ADDRESS=(PROTOCOL=tcp)(HOST=172.31.37.169)(PORT=1521))(CONNECT_DATA=(SERVICE_NAME=NRDB.WORLD)))";
            }
            if (DB == "GQL")
            {
                conStr =
                    "user id=CRMKEN;password=MP8TA24M;data source=(DESCRIPTION=(ADDRESS=(PROTOCOL=tcp)(HOST=172.31.37.169)(PORT=1521))(CONNECT_DATA=(SERVICE_NAME=GQLDB.WORLD)))";
            }

            //
            DBMS = conStr.Substring(0, conStr.IndexOf(";"));

        }
//
        public void OracleLogin()
        {
            if (! login)
            {
                cnn.ConnectionString =conStr;

                try
                {
                    cnn.Open();
                    login = true;
                }
                catch (Exception e)
                {
                    ErrorMSg = e.Message;
                    login = false;
                    Console.WriteLine("Oracle 接続エラー");
                }
            }
        }
        //****************************************************************
        // 
        //  Logout
        //
        //****************************************************************
        public void OracleLogout()
        {
            if (login)
            {
                cnn.Close();
                login = false;
            }
        }
        //****************************************************************
        // 
        //  SQL実行
        //
        //****************************************************************
        public Boolean execSQL(string iSQL)
        {
            try
            {
                if (! login ) OracleLogin();
                cmd.Connection = cnn;
                cmd.CommandText = iSQL;
                cmd.ExecuteNonQuery();

                cmd.CommandText = "COMMIT";
                cmd.ExecuteNonQuery();
                return (true);
            }
            catch (Exception e)
            {
                cmd.CommandText = "ROLLBACK";
                cmd.ExecuteNonQuery();

                ErrorMSg = e.Message;
                return (false);
            }
        }
        //
        //
        public Boolean execSQL()
        {
            int x;
            try
            {
                if (!login) OracleLogin();
                cmd.Connection = cnn;

                for (x = 0; x < SQLCNT; x++)
                {
                    cmd.CommandText = strSQL[x];
                    cmd.ExecuteNonQuery();
                }           
                return (true);
            }
            catch (Exception e)
            {
                cmd.CommandText = "ROLLBACK";
                cmd.ExecuteNonQuery();

                ErrorMSg = e.Message;
                return (false);
            }
        }
        //
        //****************************************************************
        // 
        //  SQL実行
        //
        //****************************************************************
        public Boolean SelectSQL(string iSQL)
        {
            if (!login) OracleLogin();

            cmd.Connection = cnn;
            cmd.CommandText = iSQL;
            try
            {
                rs = cmd.ExecuteReader();
                return (true);
            }
            catch (Exception e)
            {
                ErrorMSg = e.Message;
                return (false);
            }
        }
        //****************************************************************
        // 
        //  SQL実行
        //
        //****************************************************************
        public void SelectSQLClose()
        {
            rs.Close();
            if (login) OracleLogout();
        }
        //
        //****************************************************************
        // 
        // 結果をファイルに書き込み
        //
        //****************************************************************
        //
        public Boolean FileOutPut(string strSQL, string FileName, int x)
        {
            string ln;

            try
            {
                if (!login) OracleLogin();
                if (SelectSQL(strSQL))
                {
                    System.IO.StreamWriter file = new System.IO.StreamWriter(FileName);
                    while (rs.Read())
                    {
                        ln = "";
                        for (int y = 0; y < x; y++)
                        {
                            ln = ln + rs[y].ToString() + ",";

                        }

                        file.WriteLine(ln);
                    }
                    SelectSQLClose();
                    file.Close();
                }
                OracleLogout();
                return (true);
            }
            catch (Exception e)
            {
                ErrorMSg = e.Message;
                OracleLogout();
                return (false);
            }
        }
        //

        //****************************************************************
        // 
        // 結果をListに追加する
        //
        //****************************************************************
        //
        public Boolean GetListAdd(string strSQL, List<string> DataList )
        {
            //string ln;
            int x;
            int y;
            try
            {
                if (!login) OracleLogin();
                if (SelectSQL(strSQL))
                {
                    y = rs.FieldCount;
                    if (rs.Read())
                    {

                        DataList.Clear();
                        for (x = 0; x < y; x++)
                        {
                            DataList.Add(rs[x].ToString());
                        }
                    }

                }
                SelectSQLClose();
                OracleLogout();
                return (true);
            }
            catch (Exception e)
            {
                ErrorMSg = e.Message;
                OracleLogout();
                return (false);
            }
        }
        //
        //結果をDataTableに保存
        //
        public DataTable SelectDT(string strSQL)
        {
            DataTable dt = new DataTable();
            if (!login) OracleLogin();

            using (OracleCommand cmd = cnn.CreateCommand())
                {
                    cmd.CommandText = strSQL;

                    // DataAdapterの生成
                    OracleDataAdapter da = new OracleDataAdapter(cmd);

                    // データベースからデータを取得
                    da.Fill(dt);
                }
            OracleLogout();
            return dt;
        }

        //
        //
    }
}
