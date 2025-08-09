using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using System.Security.Cryptography;
using System.Xml.Linq;
using System.Drawing.Text;
using Kom_System_Common.CommonClass.Services;
//using Microsoft.Office.Interop.Excel;

namespace Kom_System_Common.CommonClass
{
    public class FunctionControl
    {
        const int offLeft = 3;          //最初のボタンの左からの位置
        const int OffTop = 3;           //ファンクションボタンの上からの位置
        const int offWidL = 6;         //ファンクション間 隙間大
        const int offWidS = 2;          //ファンクション間 隙間小
        const int wfWidth = 1485;                           //フォームの Width 初期値
        const int wfHeight = 830;                           //フォームの Height 初期値
        const int wdtBtn = 100;         //ファンクションボタンの  幅
        const int htBtn = 38;           //ファンクションボタンの 高さ
        const int htBtn2 = 51;          //ログイン等文字の高さ
        const int wdFtoI = 70;          //ファンクションボタン12とログインボタンの間
        const int wdFtoUp = 10;          //ファンクションボタン12と更新ボタンの間



        private Color defIns = Color.GreenYellow;          //追加のバックカラー
        private Color defUpdate = Color.Yellow;            //更新のバックカラー
        private Color defRef = Color.DodgerBlue;            //更新のバックカラー

        private Font deffont = new Font("メイリオ", 10.5f);//ファンクションボタンの初期フォント

        //int[] leftPos = { 1, 90, 179, 268, 377, 466, 555, 644, 753, 842, 931, 1020 };  //2024/10/07　位置微調整 - 削除CF

        private Button[] myFunction;

        private Label[] MyLavel;
        private Label[] KousinLavel;
        private Form form;
        private string[] FuncText = { "", "", "", "", "", "", "", "", "", "", "", "" };
        private string myLogin;
        private string myGijyosyo;
        private string myGijyosyoNo;
        private string myLoginNo;
        public FunctionControl(Form fm)
        {
            this.form = fm;
        }

        #region "プロパティ"
        /// <summary>
        /// 更新・追加　TEXTの内容を表示　　　追加 2024/10/01 CF
        /// 参照　                            追加 2025/01/30 CF
        ///  空白の場合は Visible = false;
        /// </summary>
        /// 
        public string kousinflg
        {
            get { return this.KousinLavel[0].Text; }     // 
            set
            {
                switch (value)
                {
                    case "0":
                        this.KousinLavel[0].Text = "";
                        this.KousinLavel[0].Visible = false;
                        break;
                    case "1":       //
                        this.KousinLavel[0].Text = "登録";
                        this.KousinLavel[0].Visible = true;
                        this.KousinLavel[0].BackColor = defIns;
                        break;
                    case "2":       //
                        this.KousinLavel[0].Text = "変更";
                        this.KousinLavel[0].Visible = true;
                        this.KousinLavel[0].BackColor = defUpdate;
                        break;
                    case "3":       //追加 2025/01/30
                        this.KousinLavel[0].Text = "参照";
                        this.KousinLavel[0].Visible = true;
                        this.KousinLavel[0].BackColor = defRef;
                        break;
                }

            }
        }
        /// <summary>
        /// /
        /// </summary>
        public string kousin
        {
            get { return this.KousinLavel[0].Text; }     // 
            set
            {
                this.KousinLavel[0].Text = value;
                if (this.KousinLavel[0].Text == "") this.KousinLavel[0].Visible = false;
                else this.KousinLavel[0].Visible = true;
            }    //
        }
        /// <summary>
        /// バックカラーを設定する
        /// </summary>
        public Color kousinBackcolor
        {
            get { return this.KousinLavel[0].BackColor; }     // 
            set { this.KousinLavel[0].BackColor = value; }    //

        }
        public string Gijyosyo
        {
            get { return myGijyosyo; }     // 
            set { myGijyosyo = value; }    //
        }
        public string GijyosyoNo
        {
            get { return myGijyosyoNo; }     // 
            set { myGijyosyoNo = value; }    //
        }

        /// <summary>
        /// LOogin者の名前を設定
        /// </summary>
        public string Login
        {
            get { return myLogin; }     // 
            set { myLogin = value; }    //
        }
        public string LoginNo
        {
            get { return myLoginNo; }     // 
            set { myLoginNo = value; }    //
        }

        /// <summary>
        /// ログインユーザーコード
        /// </summary>
        public string LoginUserCode { get; set; }
        #endregion
        #region "ファンクションのプロパティ"
        /// <summary>
        /// フォントサイズを変更する
        /// </summary>
        public float F1_fontSize
        {
            get { return myFunction[0].Font.Size; }
            set { myFunction[0].Font = new Font(myFunction[0].Font.Name, value); }
        }
        public float F2_fontSize
        {
            get { return myFunction[1].Font.Size; }
            set { myFunction[1].Font = new Font(myFunction[1].Font.Name, value); }
        }
        public float F3_fontSize
        {
            get { return myFunction[2].Font.Size; }
            set { myFunction[2].Font = new Font(myFunction[2].Font.Name, value); }
        }
        public float F4_fontSize
        {
            get { return myFunction[3].Font.Size; }
            set { myFunction[3].Font = new Font(myFunction[3].Font.Name, value); }
        }
        public float F5_fontSize
        {
            get { return myFunction[4].Font.Size; }
            set { myFunction[4].Font = new Font(myFunction[4].Font.Name, value); }
        }
        public float F6_fontSize
        {
            get { return myFunction[5].Font.Size; }
            set { myFunction[5].Font = new Font(myFunction[5].Font.Name, value); }
        }
        public float F7_fontSize
        {
            get { return myFunction[6].Font.Size; }
            set { myFunction[6].Font = new Font(myFunction[6].Font.Name, value); }
        }
        public float F8_fontSize
        {
            get { return myFunction[7].Font.Size; }
            set { myFunction[7].Font = new Font(myFunction[7].Font.Name, value); }
        }
        public float F9_fontSize
        {
            get { return myFunction[8].Font.Size; }
            set { myFunction[8].Font = new Font(myFunction[8].Font.Name, value); }
        }
        public float F10_fontSize
        {
            get { return myFunction[9].Font.Size; }
            set { myFunction[9].Font = new Font(myFunction[9].Font.Name, value); }
        }
        public float F11_fontSize
        {
            get { return myFunction[10].Font.Size; }
            set { myFunction[10].Font = new Font(myFunction[10].Font.Name, value); }
        }
        public float F12_fontSize
        {
            get { return myFunction[11].Font.Size; }
            set { myFunction[11].Font = new Font(myFunction[11].Font.Name, value); }
        }
        ////
        /// <summary>
        /// ファンクションのFONTの設定
        public Font F1_Font
        {
            get { return this.myFunction[0].Font; }
            set
            {
                Button bt = this.myFunction[0];
                bt.Font = value;
                this.myFunction[0].Font = new Font(bt.Font.OriginalFontName, bt.Font.Size);
            }
        }
        public Font F2_Font
        {
            get { return this.myFunction[1].Font; }
            set
            {
                Button bt = this.myFunction[1];
                bt.Font = value;
                this.myFunction[1].Font = new Font(bt.Font.OriginalFontName, bt.Font.Size);
            }
        }
        public Font F3_Font
        {
            get { return this.myFunction[2].Font; }
            set
            {
                Button bt = this.myFunction[2];
                bt.Font = value;
                this.myFunction[2].Font = new Font(bt.Font.OriginalFontName, bt.Font.Size);
            }
        }
        public Font F4_Font
        {
            get { return this.myFunction[3].Font; }
            set
            {
                Button bt = this.myFunction[3];
                bt.Font = value;
                this.myFunction[3].Font = new Font(bt.Font.OriginalFontName, bt.Font.Size);
            }
        }
        public Font F5_Font
        {
            get { return this.myFunction[4].Font; }
            set
            {
                Button bt = this.myFunction[4];
                bt.Font = value;
                this.myFunction[4].Font = new Font(bt.Font.OriginalFontName, bt.Font.Size);
            }
        }
        public Font F6_Font
        {
            get { return this.myFunction[5].Font; }
            set
            {
                Button bt = this.myFunction[5];
                bt.Font = value;
                this.myFunction[5].Font = new Font(bt.Font.OriginalFontName, bt.Font.Size);
            }
        }
        public Font F7_Font
        {
            get { return this.myFunction[6].Font; }
            set
            {
                Button bt = this.myFunction[6];
                bt.Font = value;
                this.myFunction[6].Font = new Font(bt.Font.OriginalFontName, bt.Font.Size);
            }
        }
        public Font F8_Font
        {
            get { return this.myFunction[7].Font; }
            set
            {
                Button bt = this.myFunction[7];
                bt.Font = value;
                this.myFunction[7].Font = new Font(bt.Font.OriginalFontName, bt.Font.Size);
            }
        }
        public Font F9_Font
        {
            get { return this.myFunction[8].Font; }
            set
            {
                Button bt = this.myFunction[8];
                bt.Font = value;
                this.myFunction[8].Font = new Font(bt.Font.OriginalFontName, bt.Font.Size);
            }
        }

        public Font F10_Font
        {
            get { return this.myFunction[9].Font; }
            set
            {
                Button bt = this.myFunction[9];
                bt.Font = value;
                this.myFunction[9].Font = new Font(bt.Font.OriginalFontName, bt.Font.Size);
            }
        }
        public Font F11_Font
        {
            get { return this.myFunction[10].Font; }
            set
            {
                Button bt = this.myFunction[10];
                bt.Font = value;
                this.myFunction[10].Font = new Font(bt.Font.OriginalFontName, bt.Font.Size);
            }
        }
        public Font F12_Font
        {
            get { return this.myFunction[11].Font; }
            set
            {
                Button bt = this.myFunction[11];
                bt.Font = value;
                this.myFunction[11].Font = new Font(bt.Font.OriginalFontName, bt.Font.Size);
            }
        }
        /// <summary>
        /// Fｘボタンの表示
        /// </summary>
        public string F1
        {
            get { return FuncText[0]; }     // 
            set { FuncText[0] = value; }    //
        }
        public string F2
        {
            get { return FuncText[1]; }     // 
            set { FuncText[1] = value; }    //
        }
        public string F3
        {
            get { return FuncText[2]; }     // 
            set { FuncText[2] = value; }    //
        }        
        public string F4
        {
            get { return FuncText[3]; }     // 
            set { FuncText[3] = value; }    //
        }
        public string F5
        {
            get { return FuncText[4]; }     // 
            set { FuncText[4] = value; }    //
        }
        public string F6
        {
            get { return FuncText[5]; }     // 
            set { FuncText[5] = value; }    //
        }
        public string F7
        {
            get { return FuncText[6]; }     // 
            set { FuncText[6] = value; }    //
        }
        public string F8
        {
            get { return FuncText[7]; }     // 
            set { FuncText[7] = value; }    //
        }

        public string F9
        {
            get { return FuncText[8]; }     // 
            set { FuncText[8] = value; }    //
        }
        public string F10
        {
            get { return FuncText[9]; }     // 
            set { FuncText[9] = value; }    //
        }

        public string F11
        {
            get { return FuncText[10]; }     // 
            set { FuncText[10] = value; }    //
        }

        public string F12
        {
            get { return FuncText[11]; }     // 
            set { FuncText[11] = value; }    //
        }
        ////
        // バックカラーの変更
        ///
        public Color F1_BackColor
        {
            get { return this.myFunction[0].BackColor; }
            set { this.myFunction[0].BackColor = value; }  //
        }
        public Color F2_BackColor
        {
            get { return this.myFunction[1].BackColor; }
            set { this.myFunction[1].BackColor = value; }  //
        }
        public Color F3_BackColor
        {
            get { return this.myFunction[2].BackColor; }
            set { this.myFunction[2].BackColor = value; }  //
        }
        public Color F4_BackColor
        {
            get { return this.myFunction[3].BackColor; }
            set { this.myFunction[3].BackColor = value; }  //
        }
        public Color F5_BackColor
        {
            get { return this.myFunction[4].BackColor; }
            set { this.myFunction[4].BackColor = value; }  //
        }
        public Color F6_BackColor
        {
            get { return this.myFunction[5].BackColor; }
            set { this.myFunction[5].BackColor = value; }  //
        }

        public Color F7_BackColor
        {
            get { return this.myFunction[6].BackColor; }
            set { this.myFunction[6].BackColor = value; }  //
        }
        public Color F8_BackColor
        {
            get { return this.myFunction[7].BackColor; }
            set { this.myFunction[7].BackColor = value; }  //
        }
        public Color F9_BackColor
        {
            get { return this.myFunction[8].BackColor; }
            set { this.myFunction[8].BackColor = value; }  //
        }
        public Color F10_BackColor
        {
            get { return this.myFunction[9].BackColor; }
            set { this.myFunction[9].BackColor = value; }  //
        }
        public Color F11_BackColor
        {
            get { return this.myFunction[10].BackColor; }
            set { this.myFunction[10].BackColor = value; }  //
        }
        public Color F12_BackColor
        {
            get { return this.myFunction[11].BackColor; }
            set { this.myFunction[11].BackColor = value; }  //
        }
        ////
        // ForeColorの変更
        ///
        public Color F1_ForeColor
        {
            get { return this.myFunction[0].ForeColor; }
            set { this.myFunction[0].ForeColor = value; }  //
        }
        public Color F2_ForeColor
        {
            get { return this.myFunction[1].ForeColor; }
            set { this.myFunction[1].ForeColor = value; }  //
        }
        public Color F3_ForeColor
        {
            get { return this.myFunction[2].ForeColor; }
            set { this.myFunction[2].ForeColor = value; }  //
        }
        public Color F4_ForeColor
        {
            get { return this.myFunction[3].ForeColor; }
            set { this.myFunction[3].ForeColor = value; }  //
        }
        public Color F5_ForeColor
        {
            get { return this.myFunction[4].ForeColor; }
            set { this.myFunction[4].ForeColor = value; }  //
        }
        public Color F6_ForeColor
        {
            get { return this.myFunction[5].ForeColor; }
            set { this.myFunction[5].ForeColor = value; }  //
        }
        public Color F7_ForeColor
        {
            get { return this.myFunction[6].ForeColor; }
            set { this.myFunction[6].ForeColor = value; }  //
        }
        public Color F8_ForeColor
        {
            get { return this.myFunction[7].ForeColor; }
            set { this.myFunction[7].ForeColor = value; }  //
        }
        public Color F9_ForeColor
        {
            get { return this.myFunction[8].ForeColor; }
            set { this.myFunction[8].ForeColor = value; }  //
        }
        public Color F10_ForeColor
        {
            get { return this.myFunction[9].ForeColor; }
            set { this.myFunction[9].ForeColor = value; }  //
        }
        public Color F11_ForeColor
        {
            get { return this.myFunction[10].ForeColor; }
            set { this.myFunction[10].ForeColor = value; }  //
        }
        public Color F12_ForeColor
        {
            get { return this.myFunction[11].ForeColor; }
            set { this.myFunction[11].ForeColor = value; }  //
        }
        #endregion

        #region "ファンクション表示"

        /// <summary>
        /// FUNCTIONの表示
        /// </summary>
        public void FunctionControlDisp()
        {
            /////////////////////////////////////////////////////////////////
            #region ログイン情報の取得
            if (!string.IsNullOrEmpty(this.LoginUserCode))
            {
                // ログインユーザーコードが指定されている場合
                // 名称マスタから事業所、ユーザー名を取得する
                decimal jigyoshoCd = -1;
                if (this.LoginUserCode.Length >= 2)
                {
                    if (decimal.TryParse(this.LoginUserCode.Substring(0,2), out jigyoshoCd))
                    {
                        this.myGijyosyoNo = jigyoshoCd.ToString("00");
                    }
                }
                var jigyoshoData = MeishoMasterService.GetMeishoMaster(MeishoMasterService.NMKBN.JIGYOSHO, jigyoshoCd.ToString(), MeishoMasterService.BRCD.MEISHO);
                if (jigyoshoData != null && jigyoshoData.Rows.Count > 0)
                {
                    this.myGijyosyo = jigyoshoData.Rows[0]["NM"].ToString();
                }

                var userData = MeishoMasterService.GetMeishoMaster(MeishoMasterService.NMKBN.TANTOSHA, this.LoginUserCode, MeishoMasterService.BRCD.MEISHO);
                this.LoginNo = this.LoginUserCode;
                if (userData != null && userData.Rows.Count > 0)
                {
                    this.myLogin = userData.Rows[0]["NM"].ToString();
                }
            }
            #endregion

            this.myFunction = new Button[12];
            for (int i = 0; i < this.myFunction.Length; i++)
            {
                this.myFunction[i] = new Button();

                // コントロールのプロパティ
                this.myFunction[i].Name = "Function" + (i + 1);
                this.myFunction[i].Text = FuncText[i];

                this.myFunction[i].Width = wdtBtn;
                this.myFunction[i].Height = htBtn;
                this.myFunction[i].Top = OffTop;
                this.myFunction[i].Left = offsetLeft(i+1);
                this.myFunction[i].TabStop = false;
                this.myFunction[i].BackColor = Color.LightGray;
                this.myFunction[i].FlatStyle = FlatStyle.Flat;
                this.myFunction[i].Font = deffont;

                // フォームへの追加
                this.form.Controls.Add(this.myFunction[i]);
                // Clickイベントハンドラを追加する
                switch (i)
                {
                    case 0:
                        this.myFunction[i].Click += new EventHandler(Function1_Click);
                        break;
                    case 1:
                        this.myFunction[i].Click += new EventHandler(Function2_Click);
                        break;
                    case 2:
                        this.myFunction[i].Click += new EventHandler(Function3_Click);
                        break;
                    case 3:
                        this.myFunction[i].Click += new EventHandler(Function4_Click);
                        break;
                    case 4:
                        this.myFunction[i].Click += new EventHandler(Function5_Click);
                        break;
                    case 5:
                        this.myFunction[i].Click += new EventHandler(Function6_Click);
                        break;
                    case 6:
                        this.myFunction[i].Click += new EventHandler(Function7_Click);
                        break;
                    case 7:
                        this.myFunction[i].Click += new EventHandler(Function8_Click);
                        break;
                    case 8:
                        this.myFunction[i].Click += new EventHandler(Function9_Click);
                        break;
                    case 9:
                        this.myFunction[i].Click += new EventHandler(Function10_Click);
                        break;
                    case 10:
                        this.myFunction[i].Click += new EventHandler(Function11_Click);
                        break;
                    case 11:
                        this.myFunction[i].Click += new EventHandler(Function12_Click);
                        break;

                }
                this.myFunction[i].BringToFront();
            }

            //前面へ
            ////////////////////////////////////////////////////////////
            // ラベル追加
            //  LOGIN名
            //  system日
            //
            this.MyLavel = new Label[7];
            /////
            this.MyLavel[0] = new Label();
            this.MyLavel[0].Name = "LabelGijyosyo";
            this.MyLavel[0].Text = "事業所　：";          // + myGijyosyo;
            this.MyLavel[0].Width = wdtBtn * 1;         //横幅
            this.MyLavel[0].Height = htBtn2 / 3;        //縦
            this.MyLavel[0].Top = OffTop + ((htBtn2 / 3) * 0);
            this.MyLavel[0].Left = offsetLeft(12) + wdtBtn + wdFtoI;//  
            this.MyLavel[0].Font = deffont;
            this.MyLavel[0].TextAlign = System.Drawing.ContentAlignment.MiddleLeft;            
            this.form.Controls.Add(this.MyLavel[0]);    // フォームへの追加
            this.MyLavel[0].BringToFront();

            ///////
            this.MyLavel[1] = new Label();
            this.MyLavel[1].Name = "LabelLogin";
            this.MyLavel[1].Text = "ログイン：";// + myLogin
            this.MyLavel[1].Width = wdtBtn * 1;         //横幅
            this.MyLavel[1].Height = htBtn2 / 3;         //縦
            this.MyLavel[1].Top = OffTop + ((htBtn2 / 3) * 1);
            this.MyLavel[1].Left = offsetLeft(12) + wdtBtn + wdFtoI;    //  
            this.MyLavel[1].Font = deffont;
            this.MyLavel[1].TextAlign=System.Drawing.ContentAlignment.MiddleLeft;                         
            this.form.Controls.Add(this.MyLavel[1]);// フォームへの追加
            this.MyLavel[1].BringToFront();

            ///////
            this.MyLavel[2] = new Label();
            this.MyLavel[2].Name = "LabelToDay";
            this.MyLavel[2].Text = DateTime.Today.ToShortDateString();
            this.MyLavel[2].Width = wdtBtn * 2;
            this.MyLavel[2].Height = htBtn2 / 3;
            this.MyLavel[2].Top = OffTop + ((htBtn2 / 3) * 2);
            this.MyLavel[2].Left = offsetLeft(12) + wdtBtn + wdFtoI;
            this.MyLavel[2].Font = deffont;
            this.MyLavel[2].TextAlign = System.Drawing.ContentAlignment.MiddleLeft;            
            this.form.Controls.Add(this.MyLavel[2]);// フォームへの追加
            this.MyLavel[2].BringToFront();
            /////////////////

            this.MyLavel[3] = new Label();
            this.MyLavel[3].Name = "LabelGijyosyoNo";
            this.MyLavel[3].Text = myGijyosyoNo;
            this.MyLavel[3].Width = wdtBtn /2;         //横幅
            this.MyLavel[3].Height = htBtn2 / 3;        //縦
            this.MyLavel[3].Top = OffTop + ((htBtn2 / 3) * 0);
            this.MyLavel[3].Left = offsetLeft(12) + wdtBtn + 40 + (wdtBtn * 1);//  
            this.MyLavel[3].Font = deffont;
            this.MyLavel[3].TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.form.Controls.Add(this.MyLavel[3]);    // フォームへの追加
            this.MyLavel[3].BringToFront();

            ///////
            this.MyLavel[4] = new Label();
            this.MyLavel[4].Name = "LabelLoginNo";
            this.MyLavel[4].Text = myLoginNo;
            this.MyLavel[4].Width = wdtBtn /2;         //横幅
            this.MyLavel[4].Height = htBtn2 / 3;         //縦
            this.MyLavel[4].Top = OffTop + ((htBtn2 / 3) * 1);
            this.MyLavel[4].Left = offsetLeft(12) + wdtBtn + 40 + (wdtBtn * 1);//  
            this.MyLavel[4].Font = deffont;
            this.MyLavel[4].TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.form.Controls.Add(this.MyLavel[4]);// フォームへの追加
            this.MyLavel[4].BringToFront();
            ////
            this.MyLavel[5] = new Label();
            this.MyLavel[5].Name = "LabelGijyosyo";
            this.MyLavel[5].Text = myGijyosyo;
            this.MyLavel[5].Width = wdtBtn / 1;         //横幅
            this.MyLavel[5].Height = htBtn2 / 3;        //縦
            this.MyLavel[5].Top = OffTop + ((htBtn2 / 3) * 0);
            this.MyLavel[5].Left = offsetLeft(12) + wdtBtn + 40 + (wdtBtn * 1)+(wdtBtn / 2);//  
            this.MyLavel[5].Font = deffont;
            this.MyLavel[5].TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.form.Controls.Add(this.MyLavel[5]);    // フォームへの追加
            this.MyLavel[5].BringToFront();

            ///////
            this.MyLavel[6] = new Label();
            this.MyLavel[6].Name = "LabelLogin";
            this.MyLavel[6].Text = myLogin;
            this.MyLavel[6].Width = wdtBtn / 1;         //横幅
            this.MyLavel[6].Height = htBtn2 / 3;         //縦
            this.MyLavel[6].Top = OffTop + ((htBtn2 / 3) * 1);
            this.MyLavel[6].Left = offsetLeft(12) + wdtBtn + 40 + (wdtBtn * 1)+(wdtBtn / 2);//  
            this.MyLavel[6].Font = deffont;
            this.MyLavel[6].TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.form.Controls.Add(this.MyLavel[6]);// フォームへの追加
            this.MyLavel[6].BringToFront();
            ////
            ///
            lblKousin();
            //
        }
        /// <summary>
        /// x = 1 to 12
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        private int offsetLeft(int x)
        {
            int ret = offLeft+ ((x - 1) * wdtBtn) + ((x - 1)*offWidS);
            if (x >= 5 ) ret += offWidL;
            if (x >= 9 ) ret += offWidL;

            return ret;
        }
        private void lblKousin()
        {

            this.KousinLavel = new Label[1];

            this.KousinLavel[0] = new Label();
            // コントロールのプロパティ
            this.KousinLavel[0].Name = "lblKkousin";
            this.KousinLavel[0].Text = "";

            this.KousinLavel[0].Width = wdtBtn / 2;             //横幅
            this.KousinLavel[0].Height = htBtn;                 //縦
            this.KousinLavel[0].Top = OffTop;
            this.KousinLavel[0].Left = offsetLeft(12) + wdtBtn + wdFtoUp;   //  
            this.KousinLavel[0].Font = deffont;
            this.KousinLavel[0].BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.KousinLavel[0].TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.KousinLavel[0].Visible = false;

            // フォームへの追加
            this.form.Controls.Add(this.KousinLavel[0]);
            this.KousinLavel[0].BringToFront();


        }
        #endregion

        #region "イベント"
        private void Function1_Click(object sender, EventArgs e)
        {
            SendKeys.Send("{F1}");
        }
        private void Function2_Click(object sender, EventArgs e)
        {
            SendKeys.Send("{F2}");
        }
        private void Function3_Click(object sender, EventArgs e)
        {
            SendKeys.Send("{F3}");
        }
        private void Function4_Click(object sender, EventArgs e)
        {
            SendKeys.Send("{F4}");
        }
        private void Function5_Click(object sender, EventArgs e)
        {
            SendKeys.Send("{F5}");
        }
        private void Function6_Click(object sender, EventArgs e)
        {
            SendKeys.Send("{F6}");
        }
        private void Function7_Click(object sender, EventArgs e)
        {
            SendKeys.Send("{F7}");
        }
        private void Function8_Click(object sender, EventArgs e)
        {
            SendKeys.Send("{F8}");
        }
        private void Function9_Click(object sender, EventArgs e)
        {
            SendKeys.Send("+{F9}");
        }
        private void Function10_Click(object sender, EventArgs e)
        {
            SendKeys.Send("{F10}");
        }
        private void Function11_Click(object sender, EventArgs e)
        {
            SendKeys.Send("{F11}");
        }
        private void Function12_Click(object sender, EventArgs e)
        {
            SendKeys.Send("{F12}");
        }
        #endregion

    }


}
