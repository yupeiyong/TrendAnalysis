using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Winform.Marksix
{
    public partial class frmMarkSixSpecifiedLocationAnalysis : Form
    {
        /// <summary>
        /// 引用主窗口
        /// </summary>
        private frmMain frmMdi = null;

        public frmMarkSixSpecifiedLocationAnalysis()
        {
            InitializeComponent();
        }
        private void frmMarkSixSpecifiedLocationAnalysis_Load(object sender, EventArgs e)
        {
            frmMdi = ParentForm as frmMain;
        }
        private void btnAnalysis_Click(object sender, EventArgs e)
        {

        }

        private void btnPurchase_Click(object sender, EventArgs e)
        {
            var frm = new frmMarkSixSpecifiedLocationPurchase();
            var location = 7;
            if(int.TryParse(cboNumberLocation.Text,out location))
            {
                frm.Text = string.Format("第{0}位 号码清单",location);
                frm.Numbers = new byte[] { 3, 6, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 34, 35, 36, 37, 38, 39, 40, 41, 42, 43, 44, 45, 46, 47, 48, 49, 50, 51, 52, 53, 54, 55, 56, 57, 58, 59, 60, 61, 62, 63, 64, 65, 66, 67, 68, 69, 70, 71, 72, 73, 74, 75, 76, 77, 88, 99 }.ToDictionary(key => key, value => 0D); ;
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    if(frm.Numbers!=null && frm.Numbers.Count > 0)
                    {
                        var resultDict = frm.Numbers.Where(m => m.Value > 0).ToDictionary(m=>m.Key,m=>m.Value);
                        //保存到数据库
                    }
                }
            }
        }


        private void btnAnalysisBefore_Click(object sender, EventArgs e)
        {

        }

    }
}
