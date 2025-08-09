using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
//
// 1/23
/////in     -> out
///  4      -> 2024/10/04
///  1/2    -> 2024/01/02
/// 50/10/4 -> 1950/10/04
//  49/4/8  -> 2049/04/08
// 2024/01/27
//2025/02/03 V2

namespace Kom_System_Common.CommonClass
{
    public partial class DayTextBox : TextBox
    {
        /// <summary>
        /// マスタから最終範囲を取得
        /// </summary>
        private int modeSw;        //  0:フリー
                                   //  1:コントロール年月/01 ～　コントロール年月/末日　　
                                   //  2:コントロール年月/01 ～　コントロール年月+1月/末日
                                   //  3:                    ～　コントロール年月/末日　　
                                   //  4:                    ～　コントロール年月+1月/末日
                                        //
        private string yyyyMM ="";          // コントロール年月
        private string jcd1="";             // 事業所コード(99)
        private DateTime lastDay;           // 最後の日
        private DateTime fastDay;           // 最初の日
        private bool sendTab = true;        // ENTERでTABを発行する
        private string defDay="";           //       
        public DayTextBox()
        {
            InitializeComponent();
            this.Font = new Font("メイリオ", 10.5f);
        }

        public DayTextBox(IContainer container)
        {
            container.Add(this);

           InitializeComponent();
        }
        public string setdef
        {
            set { defDay =  value; }    //
            get { return defDay  ; }
        }
        /// <summary>
        /// 事業所コード
        /// </summary>
        public string jigyosyoCd
        {
            set { jcd1 = value; getContMast(); }
            get { return jcd1; }
        }
        public bool SendTab
        {
            get { return sendTab; }
            set { sendTab = value; }
        }

        /// <summary>
        /// 末日チェックモード
        /// </summary>
        public int Mode
        {
            set
            {
                modeSw = value;
                if (modeSw == 2) { jcd1 = null; yyyyMM = null; }
                else { getContMast(); }
            }
            get { return modeSw; }

        }
        public string Value
        {
            get
            {
                return this.Text.Replace("/", "");
            }
        }

        protected override void OnCreateControl()
        {
            base.OnCreateControl();
            this.Text = MDYToDMY(defDay);
        }

        protected override void OnCausesValidationChanged(EventArgs e)
        {
            base.OnCausesValidationChanged(e);
            this.Text = MDYToDMY(defDay);
        }

        protected override void OnLayout(LayoutEventArgs levent)
        {
            base.OnLayout(levent);
        }
        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            base.OnKeyPress(e);
            if (e.KeyChar == (char)Keys.Enter)
            {
                checkText();
                if(sendTab)SendKeys.Send(("({TAB})"));
            }
            else
            {
                if ((e.KeyChar < '0' || '9' < e.KeyChar) && e.KeyChar != '/' && e.KeyChar != '\b' && e.KeyChar != '\t')
                {
                    //イベントをキャンセルする
                    e.Handled = true;
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLeave(EventArgs e)
        {
            checkText();
            base.OnLeave(e);
        
        }
        /// <summary>
        /// ALL数字の処理
        /// </summary>
        private void allSuuji()
        {
            ClassLibSi cLib = new ClassLibSi();
            DateTime day;
            string dText;
            switch (this.Text.Length)
            {
                case 8:
                    day = DateTime.ParseExact(this.Text, "yyyyMMdd", null);
                    if (day != null) this.Text = day.ToString("yyyy/MM/dd");
                    if (!checkDay()) { this.Text = ""; }
                    break;
                case 7:
                    dText = cLib.Left(this.Text, 4) + "/" + cLib.Mid(this.Text, 5, 2) + "/0" + cLib.Right(this.Text, 1);
                    day = DateTime.ParseExact(dText, "yyyy/MM/dd", null);
                    if (day == null)
                    {
                        dText = cLib.Left(this.Text, 4) + "/0" + cLib.Mid(this.Text, 5, 1) + "/" + cLib.Right(this.Text, 2);
                        day = DateTime.ParseExact(dText, "yyyy/MM/dd", null);
                    }
                    if (day != null) this.Text = day.ToString("yyyy/MM/dd");
                    if (!checkDay()) { this.Text = ""; }
                    break;
                case 6:
                    dText = cLib.Left(DateTime.Now.ToString("yyyy"), 2) + cLib.Left(this.Text, 2) + "/" + cLib.Mid(this.Text, 3, 2) + "/" + cLib.Right(this.Text, 2);
                    day = DateTime.ParseExact(dText, "yyyy/MM/dd", null);
                    if (day != null) this.Text = day.ToString("yyyy/MM/dd");
                    if (!checkDay()) { this.Text = ""; }
                    break;
                case 5:
                    dText = cLib.Left(DateTime.Now.ToString("yyyy"), 2) + cLib.Left(this.Text, 2) + "/0" + cLib.Mid(this.Text, 3, 1) + "/" + cLib.Right(this.Text, 2);
                    day = DateTime.ParseExact(dText, "yyyy/MM/dd", null);
                    if (day == null)
                    {
                        dText = cLib.Left(DateTime.Now.ToString("yyyy"), 2) + cLib.Left(this.Text, 2) + "/" + cLib.Mid(this.Text, 3, 2) + "/0" + cLib.Right(this.Text, 1);
                        day = DateTime.ParseExact(dText, "yyyy/MM/dd", null);
                    }
                    if (day != null) this.Text = day.ToString("yyyy/MM/dd");
                    if (!checkDay()) { this.Text = ""; }
                    break;
                case 4:
                    dText = DateTime.Now.ToString("yyyy") + "/" + cLib.Left(this.Text, 2) + "/" + cLib.Right(this.Text, 2);
                    day = DateTime.ParseExact(dText, "yyyy/MM/dd", null);
                    if (day != null) this.Text = day.ToString("yyyy/MM/dd");
                    if (!checkDay()) { this.Text = ""; }
                    break;
                case 3:
                    dText = DateTime.Now.ToString("yyyy") + "/0" + cLib.Left(this.Text, 1) + "/" + cLib.Right(this.Text, 2);
                    day = DateTime.ParseExact(dText, "yyyy/MM/dd", null);
                    if (day == null)
                    {
                        dText = DateTime.Now.ToString("yyyy") + "/" + cLib.Left(this.Text, 2) + "/0" + cLib.Right(this.Text, 1);
                        day = DateTime.ParseExact(dText, "yyyy/MM/dd", null);
                    }
                    if (day != null) this.Text = day.ToString("yyyy/MM/dd");
                    if (!checkDay()) { this.Text = ""; }
                    break;
                case 2:
                    if (this.Text == "00" || this.Text == "99")
                    {
                        day = DateTime.Parse(DateTime.Now.ToString("yyyy/MM/dd"));
                        this.Text = day.ToString("yyyy/MM/") + this.Text;
                    }
                    else
                    {
                        dText = DateTime.Now.ToString("yyyy/MM") + "/" + cLib.Left(this.Text, 2);
                        day = DateTime.ParseExact(dText, "yyyy/MM/dd", null);
                        if (day == null)
                        {
                            dText = DateTime.Now.ToString("yyyy") + "/0" + cLib.Left(this.Text, 1) + "/0" + cLib.Right(this.Text, 1);
                            day = DateTime.ParseExact(dText, "yyyy/MM/dd", null);
                        }

                        if (day != null) this.Text = day.ToString("yyyy/MM/dd");
                        if (!checkDay()) { this.Text = ""; }
                    }
                    break;
                case 1:

                    dText = DateTime.Now.ToString("yyyy/MM") + "/0" + cLib.Right(this.Text, 1);
                    day = DateTime.ParseExact(dText, "yyyy/MM/dd", null);

                    if (day != null) this.Text = day.ToString("yyyy/MM/dd");
                    if (!checkDay()) { this.Text = ""; }

                    break;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void checkText()
        {

            DateTime day;
            try
            {
                if (this.Text.Trim().Length > 0)
                {

                    if (this.Text.Trim() == "0000/00/00" || this.Text.Trim() == "9999/99/99") return;
                        switch (CountChar(this.Text, '/'))
                    {
                        case 0:
                            //数字のみ
                            if (this.Text.All(char.IsDigit))
                            {
                                allSuuji();
                            }
                            else
                            {
                                day = DateTime.Parse(DateTime.Now.ToString("yyyy/MM/") + this.Text.ToString());
                                this.Text = day.ToString("yyyy/MM/dd");
                                if (!checkDay()) { this.Text = ""; }
                            }
                            break;
                        case 1:
                            if (this.Text.Substring(this.Text.Length - 2, 2) == "00" || this.Text.Substring(this.Text.Length - 2, 2) == "99")
                            {
                                day = DateTime.Parse(DateTime.Now.ToString("yyyy/") + this.Text.ToString().Replace("00", "01").Replace("99", "01"));

                                this.Text = day.ToString("yyyy/MM/") + this.Text.Substring(this.Text.Length - 2, 2);
                                if (!checkDay()) { this.Text = ""; }
                            }
                            else
                            {
                                day = DateTime.Parse(DateTime.Now.ToString("yyyy/") + this.Text.ToString());
                                this.Text = day.ToString("yyyy/MM/dd");
                                if (!checkDay()) { this.Text = ""; }
                            }
                            break;
                        case 2:
                            if (this.Text.Substring(this.Text.Length - 2, 2) == "00" || this.Text.Substring(this.Text.Length - 2, 2) == "99")
                            {
                                day = DateTime.Parse(Sura(this.Text) + "/" + Surasura(this.Text) + "/01");

                                this.Text = day.ToString("yyyy/MM/") + this.Text.Substring(this.Text.Length - 2, 2);
                                if (!checkDay()) { this.Text = ""; }
                            }
                            else
                            {
                                if (this.Text.Substring(0, 1) == "/")
                                {
                                    day = DateTime.Parse(DateTime.Now.ToString("yyyy") + "/" + Surasura(this.Text) + "/" + SuraE(this.Text));
                                    this.Text = day.ToString("yyyy/MM/dd");
                                    if (!checkDay()) { this.Text = ""; }

                                }
                                else
                                {
                                    day = DateTime.Parse(this.Text.ToString());
                                    this.Text = day.ToString("yyyy/MM/dd");
                                    if (!checkDay()) { this.Text = ""; }
                                }
                            }
                            break;
                    }
                }
                else
                {
                    if (this.Text == "") this.Text = defDay;
                    //this.Text = DateTime.Now.ToString("yyyy/MM/dd");
                    if (!checkDay()) { this.Text = defDay; }
                }
            }
            catch (Exception)
            {
#if DEBUG
                this.Text = ""; // ex.Message;
#else

                this.Text = "Error";
#endif
            }
        }
        /// <summary>
        /// 文字の数を数える
        /// </summary>
        /// <param name="s"></param>
        /// <param name="c"></param>
        /// <returns></returns>
        private int CountChar(string s, char c)
        {
            return s.Length - s.Replace(c.ToString(), "").Length;
        }
        private string Sura(string s)
        {
            int x = 0;
            int x0 = 0;
            for (x = 0; x < s.Length; x++)
            {
                if (s.Substring(x, 1) == "/")
                {
                    if (x0 == 0)
                    {
                        x0 = x;
                        break;
                    }
                }
            }
            if (x0 < 1)
            {
                return DateTime.Now.ToString("yyyy");
            }
            return s.Substring(0, x0);
        }
        /// <summary>
        ///  /間の数字を戻す
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        private string Surasura(string s)
        {
            Boolean fst = true;
            int x = 0;
            int x0 = 0;
            int x1 = 0;
            for (x = 0; x < s.Length; x++)
            {
                if (s.Substring(x, 1) == "/")
                {
                    if (fst) { x0 = x; fst = false; }
                    else x1 = x;
                }
            }
            if (x1 - x0 - 1 < 1)
            {
                return DateTime.Now.ToString("MM");
            }
            return s.Substring(x0 + 1, x1 - x0 - 1);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        private string SuraE(string s)
        {
            int x = 0;
            int x0 = 0;
            for (x = s.Length - 1; x > 0; x--)
            {
                if (s.Substring(x, 1) == "/")
                {
                    x0 = x;
                    break;
                }
            }
            if (s.Length - x0 - 1 < 1)
            {
                return DateTime.Now.ToString("dd");
            }
            return s.Substring(x0 + 1, s.Length - x0 - 1);
        }
        //
        /// <summary>
        ///  コントロールマスタ読み込み
        /// </summary>
        /// <returns></returns>
        private Boolean getContMast()
        {
            if (modeSw > 0 && jcd1 != null)
            {
                try
                {
                    DataTable dt = new DataTable();
                    string sql = "SELECT SGETU FROM KMCONT WHERE JCD1 ='" + jcd1 + "' AND CD1='A' ";
                    if (Kom_System_Common.CommonClass.SqlSeverControl.DbConnect())
                    {
                        Kom_System_Common.CommonClass.SqlSeverControl.ExecuteSqlSelectQuery(sql, ref dt);
                        foreach (DataRow dtax in dt.Rows)
                        {
                            yyyyMM = (dtax[0].ToString());
                        }
                        setLastDay();
                    }

                }
                catch (Exception)
                {
                    modeSw = 0;
                    return false;
                }
                finally
                {
                    if (Kom_System_Common.CommonClass.SqlSeverControl.sCon.State == System.Data.ConnectionState.Open)
                    {
                        Kom_System_Common.CommonClass.SqlSeverControl.DbDisConnect();
                    }
                }
            }
            return true;
        }
        /// <summary>
        /// 最終日をセットする
        /// </summary>
        /// <returns></returns>
        private Boolean setLastDay()
        {
            ClassLibSi cLib = new ClassLibSi();
            try
            {
                fastDay = DateTime.Parse(cLib.Left(yyyyMM, 4) + "/" + cLib.Right(yyyyMM, 2) + "/01");
                lastDay = DateTime.Parse(cLib.Left(yyyyMM, 4) + "/" + cLib.Right(yyyyMM, 2) + "/01");

                switch (modeSw)
                {
                    case 0:
                        break;
                    case 1:
                        lastDay = lastDay.AddMonths(1).AddDays(-1);
                        break;
                    case 2:
                        lastDay = lastDay.AddMonths(2).AddDays(-1);
                        break;
                    case 3:
                        lastDay = lastDay.AddMonths(1).AddDays(-1);
                        break;
                    case 4:
                        lastDay = lastDay.AddMonths(2).AddDays(-1);
                        break;
                }
                return true;
            }
            catch (Exception)
            {
                return false;

            }
        }
        /// <summary>
        /// /
        /// </summary>
        /// <returns></returns>
        private Boolean checkDay()
        {
            try
            {
                DateTime dy;
                switch (modeSw)
                {
                    case 0:
                        return true;
                    case 1:
                    case 2:
                        dy = DateTime.Parse(this.Text);
                        if (fastDay <= dy && dy <= lastDay) return true;
                        return false;

                    case 3:
                    case 4:
                        dy = DateTime.Parse(this.Text);
                        if (dy <= lastDay) return true;
                        return false;
                    default:
                        return false;
                }

            }
            catch (Exception)
            {
            }
            return false;
        }

        private string MDYToDMY(string input)
        {

            string regex = @"^[0-9]{4}/[0-9]{2}/[0-9]{2}$";
            try
            {

                if (Regex.IsMatch(input, regex))
                {
                    return input;
                }
                return "";

            }
            catch (RegexMatchTimeoutException)
            {
                return  "";
            }
        }
        /////
    }
    ///
}
