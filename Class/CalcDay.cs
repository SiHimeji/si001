using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace Kom_System_Common.CommonClass
{
    public class CalcDay
    {

        //   select *  from KMCALE  WHERE  KOJCD = 2310
        /// <summary>
        ///  日間の日数を返す
        /// </summary>
        /// <param name="dayFrom"></param>　日から
        /// <param name="dayTo"></param>　　日まで
        /// <param name="sw"></param>　　　 sw=0 休日を含まない　sw=1 休日を含む
        /// <returns></returns>
        public static int calcHikan(DateTime dayFrom, DateTime dayTo,int sw)
        {
            if (sw != 0 && sw != 1)
            {
                throw new ArgumentException("swに0と1以外の値が指定されています", "sw");
            }

            int hikan = (dayTo - dayFrom).Days + 1;

            if (hikan < 1)
            {
                throw new ArgumentException("dayToにdayFromより前の日付が指定されています", "dayFrom,dayTo");
            }

            // 休日を含まない場合
            if (sw == 0)
            { 
                hikan -= calcHolidaykan(dayFrom, dayTo);
            }

            return hikan;
        }
        
        /// <summary>
        /// 日に加算する
        /// </summary>
        /// <param name="day"></param>
        /// <param name="d"></param>　　
        /// <param name="sw"></param>   sw=0 休日を含まない　sw=1 休日を含む
        /// <returns></returns>
        public static DateTime calcHiadd(DateTime day ,int d ,int sw)
        {
            if (d < 1)
            {
                throw new ArgumentException("dには1以上の値を指定してください", "d");
            }

            if (sw != 0 && sw != 1)
            {
                throw new ArgumentException("swに0と1以外の値が指定されています", "sw");
            }

            DateTime dayFrom = day.AddDays(1);
            DateTime dayTo = day.AddDays(d);

            // 休日を含まない場合
            if (sw == 0)
            {
                int holidaykan;
                
                while ((holidaykan = calcHolidaykan(dayFrom, dayTo)) > 0)
                {
                    dayFrom = dayTo.AddDays(1);
                    dayTo = dayFrom.AddDays(holidaykan - 1);
                }
            }

            return dayTo;
        }

        /// <summary>
        /// 日間の休日数を返す
        /// </summary>
        /// <param name="dayFrom"></param>
        /// <param name="dayTo"></param>
        /// <returns></returns>
        private static int calcHolidaykan(DateTime dayFrom, DateTime dayTo) 
        {
            int holidaykan = 0;

            string sql = $@"
                SELECT COUNT(*) 
                FROM KMCALE 
                WHERE KOJCD = 2110 
                  AND KYUKBN = 1 
                  AND YMD BETWEEN {dayFrom.ToString("yyyyMMdd")} AND {dayTo.ToString("yyyyMMdd")}
            ";

            DataTable dt = new DataTable();
            try
            {

                if (Kom_System_Common.CommonClass.SqlSeverControl.DbConnect())
                {
                    Kom_System_Common.CommonClass.SqlSeverControl.ExecuteSqlSelectQuery(sql, ref dt);
                    holidaykan = int.Parse(dt.Rows[0][0].ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error executing query: {ex.Message}");


            }
            finally
            {
                if (Kom_System_Common.CommonClass.SqlSeverControl.sCon.State == System.Data.ConnectionState.Open)
                {
                    Kom_System_Common.CommonClass.SqlSeverControl.DbDisConnect();
                }
            }
            return holidaykan; 
        }
        /// <summary>
        /// マスタの　SGETU を返す
        /// </summary>
        /// <param name="jcd1">事業所コード</param>
        /// <returns></returns>
        public static int GetSgetu(int jcd1)
        {
            int sgetu = 0;
            string query = "SELECT SGETU from KMCONT WHERE  CD1 ='A' AND JCD1 ='"+ jcd1.ToString()+ "'";
            DataTable dt = new DataTable();
            try
            {
                if (SqlSeverControl.DbConnect())
                {
                    SqlSeverControl.ExecuteSqlSelectQuery(query, ref dt);
                    if (dt.Rows.Count > 0)
                    {
                        sgetu = int.Parse(dt.Rows[0]["SGETU"].ToString());
                    }
                    SqlSeverControl.DbDisConnect();
                }
            }
            catch (Exception)
            {

            }
            return sgetu;
        }

        public static int AddSgetu(int sgetu,int m)
        {
            ClassLibSi cLib = new ClassLibSi();
            DateTime dy =  DateTime.Parse( cLib.Left(sgetu.ToString(), 4) + "/" + cLib.Right(sgetu.ToString(), 2)+"/01").AddMonths(m);
            return int.Parse(cLib.Left(dy.ToString("yyyyMMdd"),6));
        }



    }
}
