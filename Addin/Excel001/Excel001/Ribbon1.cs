using Microsoft.Office.Tools.Ribbon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using cLib;
namespace Excel001
{
    public partial class Ribbon1
    {
        private void Ribbon1_Load(object sender, RibbonUIEventArgs e)
        {

        }

        private void butto抽出_Click(object sender, RibbonControlEventArgs e)
        {
            ClassDataBase cDb = new ClassDataBase();
            DataTable dt;
            string strSQL;
            if (editBox年月.Text != "")
            {
                strSQL = "select  '', format(日付,'MM月dd日') 日付, ＩＤ , 部門 ,顧客名,業務名 ,バッチ名,作業名,作業内容,始,終,休,勤務時間 from dbo.T_パンチ外作業 ";
                strSQL += " where format(日付,'yyyyMM')='" + editBox年月.Text + "'";
                strSQL += " order by 日付,ＩＤ";
                dt = cDb.GetDataTable(strSQL);

                Microsoft.Office.Interop.Excel.Worksheet activeSheet;
                activeSheet = Globals.ThisAddIn.Application.ActiveSheet;
                //行、列の順に指定
                for (int y = 0; y < dt.Rows.Count; y++)
                {
                    for (int x = 0; x < dt.Columns.Count; x++)
                    {
                        switch (x)
                        {
                            case 1:
                                //activeSheet.Cells[y + 1, x + 1].Style.NumberFormat = "m月d日";
                                break;
                            case 3:
                                // activeSheet.Cells[y + 1, x + 1].Style.NumberFormat = "@";
                                break;

                        }
                        activeSheet.Cells[y + 1, x + 1].Value = dt.Rows[y][x];
                    }
                }
            }

        }
    }
}
