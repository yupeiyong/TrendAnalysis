namespace 历史记录
{
    partial class frmAddHkRecord
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.dgvList = new System.Windows.Forms.DataGridView();
            this.dgvcolRecordDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvcolTimes = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvcolFirstNum = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvcolSecondNum = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvcolThirdNum = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvcolFourthNum = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvcolFifthNum = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvcolSixthNum = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvcolSeventhNum = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.文件ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmSave = new System.Windows.Forms.ToolStripMenuItem();
            this.tsbSave = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.dgvList)).BeginInit();
            this.toolStrip1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvList
            // 
            this.dgvList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dgvcolRecordDate,
            this.dgvcolTimes,
            this.dgvcolFirstNum,
            this.dgvcolSecondNum,
            this.dgvcolThirdNum,
            this.dgvcolFourthNum,
            this.dgvcolFifthNum,
            this.dgvcolSixthNum,
            this.dgvcolSeventhNum});
            this.dgvList.Location = new System.Drawing.Point(2, 88);
            this.dgvList.Name = "dgvList";
            this.dgvList.RowTemplate.Height = 23;
            this.dgvList.Size = new System.Drawing.Size(990, 428);
            this.dgvList.TabIndex = 0;
            // 
            // dgvcolRecordDate
            // 
            this.dgvcolRecordDate.HeaderText = "记录日期";
            this.dgvcolRecordDate.Name = "dgvcolRecordDate";
            this.dgvcolRecordDate.Width = 120;
            // 
            // dgvcolTimes
            // 
            this.dgvcolTimes.HeaderText = "期数";
            this.dgvcolTimes.Name = "dgvcolTimes";
            // 
            // dgvcolFirstNum
            // 
            this.dgvcolFirstNum.HeaderText = "第一数";
            this.dgvcolFirstNum.Name = "dgvcolFirstNum";
            // 
            // dgvcolSecondNum
            // 
            this.dgvcolSecondNum.HeaderText = "第二数";
            this.dgvcolSecondNum.Name = "dgvcolSecondNum";
            // 
            // dgvcolThirdNum
            // 
            this.dgvcolThirdNum.HeaderText = "第三数";
            this.dgvcolThirdNum.Name = "dgvcolThirdNum";
            // 
            // dgvcolFourthNum
            // 
            this.dgvcolFourthNum.HeaderText = "第四数";
            this.dgvcolFourthNum.Name = "dgvcolFourthNum";
            // 
            // dgvcolFifthNum
            // 
            this.dgvcolFifthNum.HeaderText = "第五数";
            this.dgvcolFifthNum.Name = "dgvcolFifthNum";
            // 
            // dgvcolSixthNum
            // 
            this.dgvcolSixthNum.HeaderText = "第六数";
            this.dgvcolSixthNum.Name = "dgvcolSixthNum";
            // 
            // dgvcolSeventhNum
            // 
            this.dgvcolSeventhNum.HeaderText = "第七数";
            this.dgvcolSeventhNum.Name = "dgvcolSeventhNum";
            // 
            // toolStrip1
            // 
            this.toolStrip1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.toolStrip1.BackColor = System.Drawing.Color.LightSkyBlue;
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbSave});
            this.toolStrip1.Location = new System.Drawing.Point(2, 60);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(56, 25);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.Color.LightSkyBlue;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.文件ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(994, 25);
            this.menuStrip1.TabIndex = 2;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 文件ToolStripMenuItem
            // 
            this.文件ToolStripMenuItem.BackColor = System.Drawing.Color.LightSkyBlue;
            this.文件ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmSave});
            this.文件ToolStripMenuItem.Name = "文件ToolStripMenuItem";
            this.文件ToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.文件ToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.文件ToolStripMenuItem.Text = "文件";
            // 
            // tsmSave
            // 
            this.tsmSave.Name = "tsmSave";
            this.tsmSave.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.tsmSave.Size = new System.Drawing.Size(159, 22);
            this.tsmSave.Text = "保存(&S)";
            // 
            // tsbSave
            // 
            this.tsbSave.Name = "tsbSave";
            this.tsbSave.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.tsbSave.Size = new System.Drawing.Size(44, 25);
            this.tsbSave.Text = "保存";
            // 
            // frmAddHkRecord
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightSkyBlue;
            this.ClientSize = new System.Drawing.Size(994, 518);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.dgvList);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "frmAddHkRecord";
            this.Text = "frmImport";
            ((System.ComponentModel.ISupportInitialize)(this.dgvList)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvList;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvcolRecordDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvcolTimes;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvcolFirstNum;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvcolSecondNum;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvcolThirdNum;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvcolFourthNum;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvcolFifthNum;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvcolSixthNum;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvcolSeventhNum;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripMenuItem tsbSave;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 文件ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tsmSave;

    }
}