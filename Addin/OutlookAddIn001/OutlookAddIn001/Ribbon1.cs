using Microsoft.Office.Tools.Ribbon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
namespace OutlookAddIn001
{
    public partial class Ribbon1
    {
        private void Ribbon1_Load(object sender, RibbonUIEventArgs e)
        {

        }

        private void button1_Click(object sender, RibbonControlEventArgs e)
        {
            MessageBox.Show("TEST");
        }

        private void button2_Click(object sender, RibbonControlEventArgs e)
        {
            MessageBox.Show("TEST2");

        }
    }
}
