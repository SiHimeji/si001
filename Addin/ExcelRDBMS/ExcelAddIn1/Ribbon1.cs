using Microsoft.Office.Tools.Ribbon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ExcelAddIn1
{
    public partial class Ribbon1
    {
        private void Ribbon1_Load(object sender, RibbonUIEventArgs e)
        {


        }
        private void button抽出_Click(object sender, RibbonControlEventArgs e)
        {
            ClassOracle classOracle = new ClassOracle();
            string quary = $@"SELECT   FROM ";



        }


    }
}
