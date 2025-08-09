using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
////  in      ->   out
////   10     ->  2024/10
////  19/11   ->  2019/11
////  50/1    ->  1950/01
///

namespace Kom_System_Common.CommonClass
{
    public partial class MonTextBox : TextBox
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
        private string yyyyMM;     // コントロール日
        private string jcd1;        // 事業所コード
        private DateTime lastDay;   // 最後の日
        private DateTime fastDay;   // 最初の日
        private bool sendTab = true;     // ENTERでTABを発行する
        public MonTextBox()
        {
            InitializeComponent();
            this.Font = new Font("メイリオ", 10.5f);
        }

        public MonTextBox(IContainer container)
        {
            container.Add(this);

           InitializeComponent();
        }


        /// <summary>
        /// 事業所コード
        /// </summary>
        public string jigyosyoCd
        {
            set { jcd1 = value; getContMast(); }
            get { return jcd1; }
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
        public bool SendTab
        {
            get { return sendTab; }
            set { sendTab = value; }
        }
        /// <summary>
        /// 数字のみ返す
        /// </summary>

        public string Value
        {
            set { this.Text = value; }
            get { return this.Text.Replace("/", ""); }
        }

        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            base.OnKeyPress(e);
            if (e.KeyChar == (char)Keys.Enter)
            {
                mCheck();
                if (sendTab) SendKeys.Send(("({TAB})"));
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
        protected override void OnLeave(EventArgs e)
        {
            base.OnLeave(e);
            mCheck();
        }

        /// <summary>
        ///  ケース
        ///  yyyyMM 6
        ///  yyyyM  5
        ///  yyMM   4
        ///  yyM    3
        ///  MM     2
        ///  M      1
        /// </summary>
        private void mCheck()
        {
            ClassLibSi cLib = new ClassLibSi();
            string mText;
            DateTime day;
            try
            {
                switch (CountChar(this.Text, '/'))
                {
                    case 0:

                        switch (this.Text.Length)
                        {
                            case 6:
                                mText = cLib.Left(this.Text, 4) + "/" + cLib.Right(this.Text, 2) + "/01";
                                day = DateTime.ParseExact(mText, "yyyy/MM/dd", null);
                                if (day != null) this.Text = day.ToString("yyyy/MM");
                                else this.Text = "";
                                break;
                            case 5:
                                mText = cLib.Left(this.Text, 4) + "/0" + cLib.Right(this.Text, 1) + "/01";
                                day = DateTime.ParseExact(mText, "yyyy/MM/dd", null);
                                if (day != null) this.Text = day.ToString("yyyy/MM");
                                else this.Text = "";
                                break;
                            case 4:
                                mText = cLib.Left(DateTime.Now.ToString("yyyy"), 2) + cLib.Left(this.Text, 2) + "/" + cLib.Right(this.Text, 2) + "/01";
                                day = DateTime.ParseExact(mText, "yyyy/MM/dd", null);
                                if (day != null) this.Text = day.ToString("yyyy/MM");
                                else this.Text = "";
                                break;
                            case 3:
                                mText = cLib.Left(DateTime.Now.ToString("yyyy"), 2) + cLib.Left(this.Text, 2) + "/0" + cLib.Right(this.Text, 1) + "/01";
                                day = DateTime.ParseExact(mText, "yyyy/MM/dd", null);
                                if (day != null) this.Text = day.ToString("yyyy/MM");
                                else this.Text = "";
                                break;
                            case 2:
                                mText = DateTime.Now.ToString("yyyy") + "/" + this.Text.ToString() + "/01";
                                day = DateTime.ParseExact(mText, "yyyy/MM/dd", null);
                                if (day != null) this.Text = day.ToString("yyyy/MM");
                                else this.Text = "";
                                break;
                            case 1:
                                mText = DateTime.Now.ToString("yyyy") + "/0" + this.Text.ToString() + "/01";
                                day = DateTime.ParseExact(mText, "yyyy/MM/dd", null);
                                if (day != null) this.Text = day.ToString("yyyy/MM");
                                else this.Text = "";
                                break;
                        }
                        break;
                    case 1:

                        day = DateTime.Parse(this.Text.ToString() + "/01");
                        this.Text = day.ToString("yyyy/MM");
                        break;

                    case 2:
                        day = DateTime.Parse(this.Text.ToString());
                        this.Text = day.ToString("yyyy/MM");
                        break;
                }
                if (!checkDay()) { this.Text = ""; }
            }
            catch (Exception)
            {
                this.Text = "";
            }
        }
        private int CountChar(string s, char c)
        {
            return s.Length - s.Replace(c.ToString(), "").Length;
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
            return false;
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
                    case 1:
                    case 3:
                        lastDay = lastDay.AddMonths(1).AddDays(-1);
                        break;
                    case 2:
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
        /// 
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
                        dy = DateTime.Parse(this.Text + "/01");
                        if (fastDay <= dy && dy <= lastDay) return true;
                        return false;

                    case 3:
                    case 4:
                        dy = DateTime.Parse(this.Text + "/01");
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
        ///
    }
}
