using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace 趋势分析
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 打开指定窗口
        /// </summary>
        /// <typeparam name="T">窗口类型</typeparam>
        internal Form OpenForm<T>() where T : Form,new()
        {
            //mdi窗口的子窗体
            T sonFrm = null; 
            foreach (Form frm in this.MdiChildren)
            {
                if (frm is T)
                {
                    sonFrm = (T)frm;
                    break;
                }
            }
            if (sonFrm == null)
            {
                sonFrm=new T();
            }
            //指定MDI父窗体
            sonFrm.MdiParent=this;
            sonFrm.Show();
            //窗口最大化
            sonFrm.WindowState = FormWindowState.Maximized;
            return sonFrm;
        }

        private void tsbRecord_Click(object sender, EventArgs e)
        {
            //打开记录窗口
            OpenForm<frmRecord>();
        }


        private void tsbAnalysis_Click(object sender, EventArgs e)
        {
            OpenForm<frmAnalysis>();
        }

        private void tsbExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.DoEvents();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            this.Text = Application.ProductName + Application.ProductVersion;
            //打开记录窗口
            tsbRecord_Click(null, null);
        }

        private void tsbAnalysisResult_Click(object sender, EventArgs e)
        {
            OpenForm<frmAnalysisResult>();
        }
    }
}
