namespace 趋势分析
{
    partial class frmMain
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.tslMaster = new System.Windows.Forms.ToolStrip();
            this.tsbRecord = new System.Windows.Forms.ToolStripMenuItem();
            this.tsbAnalysis = new System.Windows.Forms.ToolStripMenuItem();
            this.tsbAnalysisResult = new System.Windows.Forms.ToolStripMenuItem();
            this.tsbExit = new System.Windows.Forms.ToolStripMenuItem();
            this.msMaster = new System.Windows.Forms.MenuStrip();
            this.文件ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmRecord = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmAnalysis = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmExit = new System.Windows.Forms.ToolStripMenuItem();
            this.ssMaster = new System.Windows.Forms.StatusStrip();
            this.tsslInfo = new System.Windows.Forms.ToolStripStatusLabel();
            this.tspbCompletePrecent = new System.Windows.Forms.ToolStripProgressBar();
            this.tslMaster.SuspendLayout();
            this.msMaster.SuspendLayout();
            this.ssMaster.SuspendLayout();
            this.SuspendLayout();
            // 
            // tslMaster
            // 
            this.tslMaster.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.tslMaster.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbRecord,
            this.tsbAnalysis,
            this.tsbAnalysisResult,
            this.tsbExit});
            this.tslMaster.Location = new System.Drawing.Point(0, 34);
            this.tslMaster.Name = "tslMaster";
            this.tslMaster.Padding = new System.Windows.Forms.Padding(0, 0, 2, 0);
            this.tslMaster.Size = new System.Drawing.Size(936, 28);
            this.tslMaster.TabIndex = 1;
            this.tslMaster.Text = "toolStrip1";
            // 
            // tsbRecord
            // 
            this.tsbRecord.Image = global::趋势分析.Properties.Resources.记录;
            this.tsbRecord.Name = "tsbRecord";
            this.tsbRecord.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.R)));
            this.tsbRecord.Size = new System.Drawing.Size(82, 28);
            this.tsbRecord.Text = "记录";
            this.tsbRecord.Click += new System.EventHandler(this.tsbRecord_Click);
            // 
            // tsbAnalysis
            // 
            this.tsbAnalysis.Image = global::趋势分析.Properties.Resources.计算;
            this.tsbAnalysis.Name = "tsbAnalysis";
            this.tsbAnalysis.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.A)));
            this.tsbAnalysis.Size = new System.Drawing.Size(82, 28);
            this.tsbAnalysis.Text = "分析";
            this.tsbAnalysis.Click += new System.EventHandler(this.tsbAnalysis_Click);
            // 
            // tsbAnalysisResult
            // 
            this.tsbAnalysisResult.Image = global::趋势分析.Properties.Resources.分析结果;
            this.tsbAnalysisResult.Name = "tsbAnalysisResult";
            this.tsbAnalysisResult.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.I)));
            this.tsbAnalysisResult.Size = new System.Drawing.Size(118, 28);
            this.tsbAnalysisResult.Text = "分析结果";
            this.tsbAnalysisResult.Click += new System.EventHandler(this.tsbAnalysisResult_Click);
            // 
            // tsbExit
            // 
            this.tsbExit.Image = global::趋势分析.Properties.Resources.离开;
            this.tsbExit.Name = "tsbExit";
            this.tsbExit.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.E)));
            this.tsbExit.Size = new System.Drawing.Size(82, 28);
            this.tsbExit.Text = "退出";
            this.tsbExit.Click += new System.EventHandler(this.tsbExit_Click);
            // 
            // msMaster
            // 
            this.msMaster.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.msMaster.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.文件ToolStripMenuItem});
            this.msMaster.Location = new System.Drawing.Point(0, 0);
            this.msMaster.Name = "msMaster";
            this.msMaster.Padding = new System.Windows.Forms.Padding(9, 3, 0, 3);
            this.msMaster.Size = new System.Drawing.Size(936, 34);
            this.msMaster.TabIndex = 3;
            this.msMaster.Text = "menuStrip1";
            // 
            // 文件ToolStripMenuItem
            // 
            this.文件ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmRecord,
            this.tsmAnalysis,
            this.toolStripMenuItem1,
            this.tsmExit});
            this.文件ToolStripMenuItem.Name = "文件ToolStripMenuItem";
            this.文件ToolStripMenuItem.Size = new System.Drawing.Size(58, 28);
            this.文件ToolStripMenuItem.Text = "文件";
            // 
            // tsmRecord
            // 
            this.tsmRecord.Image = ((System.Drawing.Image)(resources.GetObject("tsmRecord.Image")));
            this.tsmRecord.Name = "tsmRecord";
            this.tsmRecord.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.R)));
            this.tsmRecord.Size = new System.Drawing.Size(224, 30);
            this.tsmRecord.Text = "记录(&R)";
            this.tsmRecord.Click += new System.EventHandler(this.tsbRecord_Click);
            // 
            // tsmAnalysis
            // 
            this.tsmAnalysis.Image = global::趋势分析.Properties.Resources.计算;
            this.tsmAnalysis.Name = "tsmAnalysis";
            this.tsmAnalysis.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.A)));
            this.tsmAnalysis.Size = new System.Drawing.Size(224, 30);
            this.tsmAnalysis.Text = "分析(&A)";
            this.tsmAnalysis.Click += new System.EventHandler(this.tsbAnalysis_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Image = global::趋势分析.Properties.Resources.分析结果;
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.I)));
            this.toolStripMenuItem1.Size = new System.Drawing.Size(224, 30);
            this.toolStripMenuItem1.Text = "分析结果";
            this.toolStripMenuItem1.Click += new System.EventHandler(this.tsbAnalysisResult_Click);
            // 
            // tsmExit
            // 
            this.tsmExit.Name = "tsmExit";
            this.tsmExit.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.E)));
            this.tsmExit.Size = new System.Drawing.Size(224, 30);
            this.tsmExit.Text = "退出(&E)";
            this.tsmExit.Click += new System.EventHandler(this.tsbExit_Click);
            // 
            // ssMaster
            // 
            this.ssMaster.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.ssMaster.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsslInfo,
            this.tspbCompletePrecent});
            this.ssMaster.Location = new System.Drawing.Point(0, 606);
            this.ssMaster.Name = "ssMaster";
            this.ssMaster.Padding = new System.Windows.Forms.Padding(2, 0, 21, 0);
            this.ssMaster.Size = new System.Drawing.Size(936, 30);
            this.ssMaster.TabIndex = 5;
            this.ssMaster.Text = "statusStrip1";
            // 
            // tsslInfo
            // 
            this.tsslInfo.Name = "tsslInfo";
            this.tsslInfo.Size = new System.Drawing.Size(46, 25);
            this.tsslInfo.Text = "就绪";
            // 
            // tspbCompletePrecent
            // 
            this.tspbCompletePrecent.Maximum = 1000;
            this.tspbCompletePrecent.Name = "tspbCompletePrecent";
            this.tspbCompletePrecent.Size = new System.Drawing.Size(300, 24);
            this.tspbCompletePrecent.Step = 1;
            this.tspbCompletePrecent.Visible = false;
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(936, 636);
            this.Controls.Add(this.ssMaster);
            this.Controls.Add(this.tslMaster);
            this.Controls.Add(this.msMaster);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.msMaster;
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "frmMain";
            this.Text = "趋势分析";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMain_FormClosing);
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.tslMaster.ResumeLayout(false);
            this.tslMaster.PerformLayout();
            this.msMaster.ResumeLayout(false);
            this.msMaster.PerformLayout();
            this.ssMaster.ResumeLayout(false);
            this.ssMaster.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip tslMaster;
        private System.Windows.Forms.MenuStrip msMaster;
        private System.Windows.Forms.ToolStripMenuItem 文件ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tsmAnalysis;
        private System.Windows.Forms.ToolStripMenuItem tsmRecord;
        private System.Windows.Forms.ToolStripMenuItem tsmExit;
        private System.Windows.Forms.StatusStrip ssMaster;
        internal System.Windows.Forms.ToolStripStatusLabel tsslInfo;
        internal System.Windows.Forms.ToolStripMenuItem tsbAnalysis;
        internal System.Windows.Forms.ToolStripMenuItem tsbRecord;
        internal System.Windows.Forms.ToolStripMenuItem tsbExit;
        internal System.Windows.Forms.ToolStripProgressBar tspbCompletePrecent;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        internal System.Windows.Forms.ToolStripMenuItem tsbAnalysisResult;

    }
}

