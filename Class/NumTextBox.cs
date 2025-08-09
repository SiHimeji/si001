using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace Kom_System_Common.CommonClass
{
    public partial class NumTextBox : TextBox
    {

        private int _decimal_digits = 2;
        private Boolean _minus_permission = false;

        public void setDecimalDigits(int decimal_digits)
        {
            this._decimal_digits = decimal_digits;
        }

        public void setMinusPermission(Boolean minus_permission)
        {
            this._minus_permission = minus_permission;
        }

        public string Value
        {
            get => base.Text.Replace(",", "");
            set
            {
                base.Text = value;
            }
        }

        public override string Text
        {
            get => base.Text;
            set
            {
                base.Text = "";
                base.Text = value;
            }
        }


        public NumTextBox()
        {
            InitializeComponent();
            this.Font = new Font("メイリオ", 10.5f);
        }

        public NumTextBox(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }

        protected override void OnLayout(LayoutEventArgs levent)
        {
            base.OnLayout(levent);

        }
        protected override void OnKeyPress(KeyPressEventArgs e)

        {
            base.OnKeyPress(e);
            if ((e.KeyChar < '0' || '9' < e.KeyChar) && e.KeyChar != '.' && e.KeyChar != '\b' && e.KeyChar != '\t' && e.KeyChar != (char)Keys.Enter)
            {
                if(this._minus_permission && e.KeyChar == '-')
                {
                    return;
                }
                //イベントをキャンセルする
                e.Handled = true;
            }
        }
        protected override void OnGotFocus(EventArgs e)
        {
            base.OnGotFocus(e);
            delCamma();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLeave(EventArgs e)
        {
            base.OnLeave(e);
            addCamma();
        }

        /// <summary>
        /// カンマ区切り数値に変換
        /// </summary>
        private void addCamma()
        {
            try
            {
                delCamma();
                if (this.Text.IndexOf(".") >= 0){
                    double number = double.Parse(this.Text);
                    this.Text = String.Format("{0:N" + this._decimal_digits.ToString() + "}", number);
                }
                else
                {
                    Int64 number = Int64.Parse(this.Text);
                    this.Text = String.Format("{0:N0}", number);
                }
            }
            catch (Exception)
            {
                this.Text = ""; // ex.Message;
            }
        }

        /// <summary>
        /// 文字列からカンマを削除
        /// </summary>
        private void delCamma()
        {
            this.Text = this.Text.Replace(",", "");
        }

    }
    ///
}
