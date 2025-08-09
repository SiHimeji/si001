using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace Kom_System_Common.CommonClass
{
    public partial class dayMaskedTextBox : MaskedTextBox
    {
        public dayMaskedTextBox()
        {
            InitializeComponent();
            this.Mask = "99/99/99";
            this.Font = new Font("メイリオ", 10.5f);

        }

        public dayMaskedTextBox(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }
        /// <summary>
        /// 
        ///  先頭に20を追加
        ///  /を外して数字のみ返す
        ///  
        /// </summary>
        public string value
        {
            set
            {
                this.Text = value;
            }
            get
            {
                return "20"+this.Text.Replace("/","");
            }
        }
        /// <summary>
        /// ロストフォーカスで日付け型に
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLeave(EventArgs e)
        {
            base.OnLeave(e);
            try
            {
                DateTime day = DateTime.Parse(this.Text);
                this.Text = day.ToString("yy/MM/dd");
            }
            catch (Exception)
            {
            }

        }
        /// <summary>
        /// ENTER キーでカーソル位置を進める
        /// 最期はTABへ変換する
        /// </summary>
        /// <param name="e"></param>
        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);
            if (e.KeyCode == Keys.Enter)
            {
                var posStart = this.SelectionStart;
                switch (posStart.ToString())
                {
                    case "0":
                    case "1":
                    case "2":
                        this.SelectionStart = 3;
                        break;
                    case "3":
                    case "4":
                    case "5":
                        this.SelectionStart = 6;
                        break;
                    default:
                        try
                        {
                            DateTime day = DateTime.Parse(this.Text);
                            this.Text = day.ToString("yy/MM/dd");
                            SendKeys.Send(("({TAB})"));
                        }
                        catch
                        {
                            MessageBox.Show("日付エラー");
                        }
                        break;
                }

            }
        }
    }
}
