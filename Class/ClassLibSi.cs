using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Kom_System_Common.CommonClass
{
    public class ClassLibSi
    {
        /// SI　共通関数 
        /// <summary>
        /// SI　共通関数 
        /// 
        /// 
        #region"アセンブリバージョンの取得"
        /// <returns></returns>
        //アセンブリバージョンの取得
        public string GetAsmVersion()
        {
            System.Reflection.Assembly assm =
            System.Reflection.Assembly.GetExecutingAssembly();
            System.Version ver1 = assm.GetName().Version;
            return (ver1.ToString());
        }
        #endregion
        #region "文字列"
        /// <summary>
        /// 文字列の指定した位置から指定した長さを取得する
        /// </summary>
        /// <param name="str">文字列</param>
        /// <param name="start">開始位置</param>
        /// <param name="len">長さ</param>
        /// <returns>取得した文字列</returns>
        public string Mid(string str, int start, int len)
        {
            if (start <= 0)
            {
                throw new ArgumentException("引数'start'は1以上でなければなりません。");
            }
            if (len < 0)
            {
                throw new ArgumentException("引数'len'は0以上でなければなりません。");
            }
            if (str == null || str.Length < start)
            {
                return "";
            }
            if (str.Length < (start + len))
            {
                return str.Substring(start - 1);
            }
            return str.Substring(start - 1, len);
        }

        /// <summary>
        /// 文字列の指定した位置から末尾までを取得する
        /// </summary>
        /// <param name="str">文字列</param>
        /// <param name="start">開始位置</param>
        /// <returns>取得した文字列</returns>
        public string Mid(string str, int start)
        {
            return Mid(str, start, str.Length);
        }

        /// <summary>
        /// 文字列の先頭から指定した長さの文字列を取得する
        /// </summary>
        /// <param name="str">文字列</param>
        /// <param name="len">長さ</param>
        /// <returns>取得した文字列</returns>
        public string Left(string str, int len)
        {
            if (len < 0)
            {
                throw new ArgumentException("引数'len'は0以上でなければなりません。");
            }
            if (str == null)
            {
                return "";
            }
            if (str.Length <= len)
            {
                return str;
            }
            return str.Substring(0, len);
        }

        /// <summary>
        /// 文字列の末尾から指定した長さの文字列を取得する
        /// </summary>
        /// <param name="str">文字列</param>
        /// <param name="len">長さ</param>
        /// <returns>取得した文字列</returns>
        public string Right(string str, int len)
        {
            if (len < 0)
            {
                throw new ArgumentException("引数'len'は0以上でなければなりません。");
            }
            if (str == null)
            {
                return "";
            }
            if (str.Length <= len)
            {
                return str;
            }
            return str.Substring(str.Length - len, len);
        }
        #endregion
        #region "DataGridView系"
        /// <summary>
        ///        this.DataGridView1.AutoGenerateColumns = False
        ///        this.DataGridView1.DataSource = dt
        ///        
        /// 
        ///        this.DataGridView1.AllowUserToAddRows = False;
        /// </summary>
        /// <param name="dgv"></param>
        /// <param name="ro"></param>
        /// <param name="DataPropertyName"></param>
        /// <param name="HeaderText"></param>
        /// <param name="wid"></param>
        /// <param name="rdonry"></param>
        /// <returns></returns>
        public int SetTextColumn(DataGridView dgv, int ro, string DataPropertyName, string HeaderText, int wid, Boolean rdonry, DataGridViewContentAlignment Alig)
        {
            DataGridViewTextBoxColumn textColumn = new DataGridViewTextBoxColumn();
            textColumn.DataPropertyName = DataPropertyName;
            textColumn.HeaderText = HeaderText;
            textColumn.Name = "Column" + ro.ToString();
            textColumn.DefaultCellStyle.Alignment =Alig;

            dgv.Columns.Add(textColumn);
            dgv.Columns[ro].Width = wid;
            dgv.Columns[ro].ReadOnly = rdonry;
            return (++ro);
        }

        public int SetTextColumn(DataGridView dgv, int ro, string DataPropertyName, string HeaderText, int wid, Boolean rdonry, string msk, DataGridViewContentAlignment Alig)
        {
            DataGridViewMaskedTextBoxColumn textColumn = new DataGridViewMaskedTextBoxColumn();
            textColumn.DataPropertyName = DataPropertyName;
            textColumn.HeaderText = HeaderText;
            textColumn.Name = "Column" + ro.ToString();
            textColumn.Mask = msk;
            textColumn.DefaultCellStyle.Alignment = Alig;
            dgv.Columns.Add(textColumn);
            dgv.Columns[ro].Width = wid;
            dgv.Columns[ro].ReadOnly = rdonry;
            return (++ro);  
        }
        public int SetChekColumn(DataGridView dgv, int ro, string DataPropertyName, string HeaderText, int wid)
        {
            DataGridViewCheckBoxColumn textColumn = new DataGridViewCheckBoxColumn();
            textColumn.DataPropertyName = DataPropertyName;
            textColumn.HeaderText = HeaderText;
            textColumn.Name = "Column" + ro.ToString();
            dgv.Columns.Add(textColumn);
            dgv.Columns[ro].Width = wid;
            return (++ro);
        }
    }
    #endregion

    #region "DataGridViewMaskedTextBoxColumn"

    ////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// DataGridViewMaskedTextBoxCellオブジェクトの列を表します。
    /// </summary>
    public class DataGridViewMaskedTextBoxColumn :
        DataGridViewColumn
    {
        //CellTemplateとするDataGridViewMaskedTextBoxCellオブジェクトを指定して
        //基本クラスのコンストラクタを呼び出す
        public DataGridViewMaskedTextBoxColumn()
            : base(new DataGridViewMaskedTextBoxCell())
        {
        }

        private string maskValue = "";
        /// <summary>
        /// MaskedTextBoxのMaskプロパティに適用する値
        /// </summary>
        public string Mask
        {
            get
            {
                return this.maskValue;
            }
            set
            {
                this.maskValue = value;
            }
        }

        //新しいプロパティを追加しているため、
        // Cloneメソッドをオーバーライドする必要がある
        public override object Clone()
        {
            DataGridViewMaskedTextBoxColumn col =
                (DataGridViewMaskedTextBoxColumn)base.Clone();
            col.Mask = this.Mask;
            return col;
        }

        //CellTemplateの取得と設定
        public override DataGridViewCell CellTemplate
        {
            get
            {
                return base.CellTemplate;
            }
            set
            {
                //DataGridViewMaskedTextBoxCellしか
                // CellTemplateに設定できないようにする
                if (!(value is DataGridViewMaskedTextBoxCell))
                {
                    throw new InvalidCastException(
                        "DataGridViewMaskedTextBoxCellオブジェクトを" +
                        "指定してください。");
                }
                base.CellTemplate = value;
            }
        }
    }

    /// <summary>
    /// MaskedTextBoxで編集できるテキスト情報を
    /// DataGridViewコントロールに表示します。
    /// </summary>
    public class DataGridViewMaskedTextBoxCell :
        DataGridViewTextBoxCell
    {
        //コンストラクタ
        public DataGridViewMaskedTextBoxCell()
        {
        }

        //編集コントロールを初期化する
        //編集コントロールは別のセルや列でも使いまわされるため、初期化の必要がある
        public override void InitializeEditingControl(
            int rowIndex, object initialFormattedValue,
            DataGridViewCellStyle dataGridViewCellStyle)
        {
            base.InitializeEditingControl(rowIndex,
                initialFormattedValue, dataGridViewCellStyle);

            //編集コントロールの取得
            DataGridViewMaskedTextBoxEditingControl maskedBox =
                this.DataGridView.EditingControl as
                DataGridViewMaskedTextBoxEditingControl;
            if (maskedBox != null)
            {
                //Textを設定
                string maskedText = initialFormattedValue as string;
                maskedBox.Text = maskedText != null ? maskedText : "";
                //カスタム列のプロパティを反映させる
                DataGridViewMaskedTextBoxColumn column =
                    this.OwningColumn as DataGridViewMaskedTextBoxColumn;
                if (column != null)
                {
                    maskedBox.Mask = column.Mask;
                }
            }
        }

        //編集コントロールの型を指定する
        public override Type EditType
        {
            get
            {
                return typeof(DataGridViewMaskedTextBoxEditingControl);
            }
        }

        //セルの値のデータ型を指定する
        //ここでは、Object型とする
        //基本クラスと同じなので、オーバーライドの必要なし
        public override Type ValueType
        {
            get
            {
                return typeof(object);
            }
        }

        //新しいレコード行のセルの既定値を指定する
        public override object DefaultNewRowValue
        {
            get
            {
                return base.DefaultNewRowValue;
            }
        }
        //
    }
    ///
    /// <summary>
    /// DataGridViewMaskedTextBoxCellでホストされる
    /// MaskedTextBoxコントロールを表します。
    /// </summary>
    public class DataGridViewMaskedTextBoxEditingControl :
        MaskedTextBox, IDataGridViewEditingControl
    {
        //編集コントロールが表示されているDataGridView
        DataGridView dataGridView;
        //編集コントロールが表示されている行
        int rowIndex;
        //編集コントロールの値とセルの値が違うかどうか
        bool valueChanged;

        //コンストラクタ
        public DataGridViewMaskedTextBoxEditingControl()
        {
            this.TabStop = false;
        }

        #region IDataGridViewEditingControl メンバ

        //編集コントロールで変更されたセルの値
        public object GetEditingControlFormattedValue(
            DataGridViewDataErrorContexts context)
        {
            return this.Text;
        }

        //編集コントロールで変更されたセルの値
        public object EditingControlFormattedValue
        {
            get
            {
                return this.GetEditingControlFormattedValue(
                    DataGridViewDataErrorContexts.Formatting);
            }
            set
            {
                this.Text = (string)value;
            }
        }

        //セルスタイルを編集コントロールに適用する
        //編集コントロールの前景色、背景色、フォントなどをセルスタイルに合わせる
        public void ApplyCellStyleToEditingControl(
            DataGridViewCellStyle dataGridViewCellStyle)
        {
            this.Font = dataGridViewCellStyle.Font;
            this.ForeColor = dataGridViewCellStyle.ForeColor;
            this.BackColor = dataGridViewCellStyle.BackColor;
            switch (dataGridViewCellStyle.Alignment)
            {
                case DataGridViewContentAlignment.BottomCenter:
                case DataGridViewContentAlignment.MiddleCenter:
                case DataGridViewContentAlignment.TopCenter:
                    this.TextAlign = HorizontalAlignment.Center;
                    break;
                case DataGridViewContentAlignment.BottomRight:
                case DataGridViewContentAlignment.MiddleRight:
                case DataGridViewContentAlignment.TopRight:
                    this.TextAlign = HorizontalAlignment.Right;
                    break;
                default:
                    this.TextAlign = HorizontalAlignment.Left;
                    break;
            }
        }

        //編集するセルがあるDataGridView
        public DataGridView EditingControlDataGridView
        {
            get
            {
                return this.dataGridView;
            }
            set
            {
                this.dataGridView = value;
            }
        }

        //編集している行のインデックス
        public int EditingControlRowIndex
        {
            get
            {
                return this.rowIndex;
            }
            set
            {
                this.rowIndex = value;
            }
        }

        //値が変更されたかどうか
        //編集コントロールの値とセルの値が違うかどうか
        public bool EditingControlValueChanged
        {
            get
            {
                return this.valueChanged;
            }
            set
            {
                this.valueChanged = value;
            }
        }

        //指定されたキーをDataGridViewが処理するか、編集コントロールが処理するか
        //Trueを返すと、編集コントロールが処理する
        //dataGridViewWantsInputKeyがTrueの時は、DataGridViewが処理できる
        public bool EditingControlWantsInputKey(
            Keys keyData, bool dataGridViewWantsInputKey)
        {
            //Keys.Left、Right、Home、Endの時は、Trueを返す
            //このようにしないと、これらのキーで別のセルにフォーカスが移ってしまう
            switch (keyData & Keys.KeyCode)
            {
                case Keys.Right:
                case Keys.End:
                case Keys.Left:
                case Keys.Home:
                    return true;
                default:
                    return !dataGridViewWantsInputKey;
            }
        }

        //マウスカーソルがEditingPanel上にあるときのカーソルを指定する
        //EditingPanelは編集コントロールをホストするパネルで、
        //編集コントロールがセルより小さいとコントロール以外の部分がパネルとなる
        public Cursor EditingPanelCursor
        {
            get
            {
                return base.Cursor;
            }
        }

        //コントロールで編集する準備をする
        //テキストを選択状態にしたり、挿入ポインタを末尾にしたりする
        public void PrepareEditingControlForEdit(bool selectAll)
        {
            if (selectAll)
            {
                //選択状態にする
                this.SelectAll();
            }
            else
            {
                //挿入ポインタを末尾にする
                this.SelectionStart = this.TextLength;
            }
        }

        //値が変更した時に、セルの位置を変更するかどうか
        //値が変更された時に編集コントロールの大きさが変更される時はTrue
        public bool RepositionEditingControlOnValueChange
        {
            get
            {
                return false;
            }
        }

        #endregion

        //値が変更された時
        protected override void OnTextChanged(EventArgs e)
        {
            base.OnTextChanged(e);
            //値が変更されたことをDataGridViewに通知する
            this.valueChanged = true;
            this.dataGridView.NotifyCurrentCellDirty(true);
        }
    }
    #endregion
    ///    
}
