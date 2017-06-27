namespace Winform
{
    partial class frmMarksixRecord
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMarksixRecord));
            this.tslMaster = new System.Windows.Forms.ToolStrip();
            this.tsbSearch = new System.Windows.Forms.ToolStripMenuItem();
            this.tsbImport = new System.Windows.Forms.ToolStripMenuItem();
            this.tsbStopImport = new System.Windows.Forms.ToolStripMenuItem();
            this.tsbExport = new System.Windows.Forms.ToolStripMenuItem();
            this.tsbExit = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.txtRecordDate = new System.Windows.Forms.TextBox();
            this.txtTimes = new System.Windows.Forms.TextBox();
            this.txtFirstNum = new System.Windows.Forms.TextBox();
            this.txtSecondNum = new System.Windows.Forms.TextBox();
            this.txtThirdNum = new System.Windows.Forms.TextBox();
            this.txtFourthNum = new System.Windows.Forms.TextBox();
            this.txtFifthNum = new System.Windows.Forms.TextBox();
            this.txtSixthNum = new System.Windows.Forms.TextBox();
            this.txtSeventhNum = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.dgvList = new Winform.Common.myDataGridView();
            this.tslMaster.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvList)).BeginInit();
            this.SuspendLayout();
            // 
            // tslMaster
            // 
            this.tslMaster.BackColor = System.Drawing.Color.Transparent;
            this.tslMaster.Dock = System.Windows.Forms.DockStyle.None;
            this.tslMaster.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.tslMaster.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbSearch,
            this.tsbImport,
            this.tsbStopImport,
            this.tsbExport,
            this.tsbExit});
            this.tslMaster.Location = new System.Drawing.Point(7, 116);
            this.tslMaster.Name = "tslMaster";
            this.tslMaster.Padding = new System.Windows.Forms.Padding(0, 0, 2, 0);
            this.tslMaster.Size = new System.Drawing.Size(341, 28);
            this.tslMaster.TabIndex = 2;
            this.tslMaster.Text = "toolStrip1";
            // 
            // tsbSearch
            // 
            this.tsbSearch.Image = global::Winform.Properties.Resources.search;
            this.tsbSearch.Name = "tsbSearch";
            this.tsbSearch.ShortcutKeys = System.Windows.Forms.Keys.F7;
            this.tsbSearch.Size = new System.Drawing.Size(82, 28);
            this.tsbSearch.Text = "查询";
            this.tsbSearch.ToolTipText = "按F7可以执行查询\r\n支持通配符 ? 单个字符 *多个字符\r\n数字和日期支持>、<、=、>=、<=、<>";
            this.tsbSearch.Click += new System.EventHandler(this.tsbSearch_Click);
            // 
            // tsbImport
            // 
            this.tsbImport.Image = global::Winform.Properties.Resources.导入;
            this.tsbImport.Name = "tsbImport";
            this.tsbImport.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.I)));
            this.tsbImport.Size = new System.Drawing.Size(82, 28);
            this.tsbImport.Text = "导入";
            this.tsbImport.Click += new System.EventHandler(this.tsbImport_Click);
            // 
            // tsbStopImport
            // 
            this.tsbStopImport.Name = "tsbStopImport";
            this.tsbStopImport.Size = new System.Drawing.Size(130, 28);
            this.tsbStopImport.Text = "停止导入记录";
            this.tsbStopImport.Visible = false;
            this.tsbStopImport.Click += new System.EventHandler(this.tsbStopImport_Click);
            // 
            // tsbExport
            // 
            this.tsbExport.Image = global::Winform.Properties.Resources.excel;
            this.tsbExport.Name = "tsbExport";
            this.tsbExport.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.E)));
            this.tsbExport.Size = new System.Drawing.Size(82, 28);
            this.tsbExport.Text = "导出";
            this.tsbExport.Click += new System.EventHandler(this.tsbExport_Click);
            // 
            // tsbExit
            // 
            this.tsbExit.Image = global::Winform.Properties.Resources.离开;
            this.tsbExit.Name = "tsbExit";
            this.tsbExit.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.T)));
            this.tsbExit.Size = new System.Drawing.Size(82, 28);
            this.tsbExit.Text = "退出";
            this.tsbExit.Click += new System.EventHandler(this.tsbExit_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tableLayoutPanel1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox1.Size = new System.Drawing.Size(1254, 110);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 14;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 90F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 90F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 75F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 90F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 75F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 90F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 75F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 90F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 75F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 90F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 75F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 90F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 75F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 90F));
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.label2, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.label3, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.label4, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.label5, 4, 1);
            this.tableLayoutPanel1.Controls.Add(this.label7, 8, 1);
            this.tableLayoutPanel1.Controls.Add(this.label8, 10, 1);
            this.tableLayoutPanel1.Controls.Add(this.label9, 12, 1);
            this.tableLayoutPanel1.Controls.Add(this.txtRecordDate, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.txtTimes, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.txtFirstNum, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.txtSecondNum, 3, 1);
            this.tableLayoutPanel1.Controls.Add(this.txtThirdNum, 5, 1);
            this.tableLayoutPanel1.Controls.Add(this.txtFourthNum, 7, 1);
            this.tableLayoutPanel1.Controls.Add(this.txtFifthNum, 9, 1);
            this.tableLayoutPanel1.Controls.Add(this.txtSixthNum, 11, 1);
            this.tableLayoutPanel1.Controls.Add(this.txtSeventhNum, 13, 1);
            this.tableLayoutPanel1.Controls.Add(this.label6, 6, 1);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(12, 22);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(4);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1170, 80);
            this.tableLayoutPanel1.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 11);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 18);
            this.label1.TabIndex = 0;
            this.label1.Text = "记录日期";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(207, 11);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(44, 18);
            this.label2.TabIndex = 0;
            this.label2.Text = "期数";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(24, 51);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(62, 18);
            this.label3.TabIndex = 0;
            this.label3.Text = "第一数";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(189, 51);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(62, 18);
            this.label4.TabIndex = 0;
            this.label4.Text = "第二数";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label5
            // 
            this.label5.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(354, 51);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(62, 18);
            this.label5.TabIndex = 0;
            this.label5.Text = "第三数";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label7
            // 
            this.label7.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(684, 51);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(62, 18);
            this.label7.TabIndex = 0;
            this.label7.Text = "第五数";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label8
            // 
            this.label8.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(849, 51);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(62, 18);
            this.label8.TabIndex = 0;
            this.label8.Text = "第六数";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label9
            // 
            this.label9.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(1014, 51);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(62, 18);
            this.label9.TabIndex = 0;
            this.label9.Text = "第七数";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtRecordDate
            // 
            this.txtRecordDate.Location = new System.Drawing.Point(94, 4);
            this.txtRecordDate.Margin = new System.Windows.Forms.Padding(4);
            this.txtRecordDate.Name = "txtRecordDate";
            this.txtRecordDate.Size = new System.Drawing.Size(79, 28);
            this.txtRecordDate.TabIndex = 1;
            // 
            // txtTimes
            // 
            this.txtTimes.Location = new System.Drawing.Point(259, 4);
            this.txtTimes.Margin = new System.Windows.Forms.Padding(4);
            this.txtTimes.Name = "txtTimes";
            this.txtTimes.Size = new System.Drawing.Size(79, 28);
            this.txtTimes.TabIndex = 1;
            // 
            // txtFirstNum
            // 
            this.txtFirstNum.Location = new System.Drawing.Point(94, 44);
            this.txtFirstNum.Margin = new System.Windows.Forms.Padding(4);
            this.txtFirstNum.Name = "txtFirstNum";
            this.txtFirstNum.Size = new System.Drawing.Size(79, 28);
            this.txtFirstNum.TabIndex = 1;
            // 
            // txtSecondNum
            // 
            this.txtSecondNum.Location = new System.Drawing.Point(259, 44);
            this.txtSecondNum.Margin = new System.Windows.Forms.Padding(4);
            this.txtSecondNum.Name = "txtSecondNum";
            this.txtSecondNum.Size = new System.Drawing.Size(79, 28);
            this.txtSecondNum.TabIndex = 1;
            // 
            // txtThirdNum
            // 
            this.txtThirdNum.Location = new System.Drawing.Point(424, 44);
            this.txtThirdNum.Margin = new System.Windows.Forms.Padding(4);
            this.txtThirdNum.Name = "txtThirdNum";
            this.txtThirdNum.Size = new System.Drawing.Size(79, 28);
            this.txtThirdNum.TabIndex = 1;
            // 
            // txtFourthNum
            // 
            this.txtFourthNum.Location = new System.Drawing.Point(589, 44);
            this.txtFourthNum.Margin = new System.Windows.Forms.Padding(4);
            this.txtFourthNum.Name = "txtFourthNum";
            this.txtFourthNum.Size = new System.Drawing.Size(79, 28);
            this.txtFourthNum.TabIndex = 1;
            // 
            // txtFifthNum
            // 
            this.txtFifthNum.Location = new System.Drawing.Point(754, 44);
            this.txtFifthNum.Margin = new System.Windows.Forms.Padding(4);
            this.txtFifthNum.Name = "txtFifthNum";
            this.txtFifthNum.Size = new System.Drawing.Size(79, 28);
            this.txtFifthNum.TabIndex = 1;
            // 
            // txtSixthNum
            // 
            this.txtSixthNum.Location = new System.Drawing.Point(919, 44);
            this.txtSixthNum.Margin = new System.Windows.Forms.Padding(4);
            this.txtSixthNum.Name = "txtSixthNum";
            this.txtSixthNum.Size = new System.Drawing.Size(79, 28);
            this.txtSixthNum.TabIndex = 1;
            // 
            // txtSeventhNum
            // 
            this.txtSeventhNum.Location = new System.Drawing.Point(1084, 44);
            this.txtSeventhNum.Margin = new System.Windows.Forms.Padding(4);
            this.txtSeventhNum.Name = "txtSeventhNum";
            this.txtSeventhNum.Size = new System.Drawing.Size(79, 28);
            this.txtSeventhNum.TabIndex = 1;
            // 
            // label6
            // 
            this.label6.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(519, 51);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(62, 18);
            this.label6.TabIndex = 0;
            this.label6.Text = "第四数";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dgvList
            // 
            this.dgvList.AllowUserToAddRows = false;
            this.dgvList.AllowUserToDeleteRows = false;
            this.dgvList.AllowUserToResizeColumns = false;
            this.dgvList.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.dgvList.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.Gainsboro;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvList.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvList.ColumnHeadersHeight = 25;
            this.dgvList.Location = new System.Drawing.Point(0, 151);
            this.dgvList.Name = "dgvList";
            this.dgvList.RowTemplate.DefaultCellStyle.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvList.RowTemplate.Height = 30;
            this.dgvList.Size = new System.Drawing.Size(1254, 474);
            this.dgvList.TabIndex = 6;
            this.dgvList.CellMouseEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvList_CellMouseEnter);
            this.dgvList.CellMouseLeave += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvList_CellMouseLeave);
            this.dgvList.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.dgvList_DataBindingComplete);
            // 
            // frmMarksixRecord
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(1254, 625);
            this.Controls.Add(this.dgvList);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.tslMaster);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmMarksixRecord";
            this.Text = "Marksix历史记录";
            this.Load += new System.EventHandler(this.frmMarksixRecord_Load);
            this.tslMaster.ResumeLayout(false);
            this.tslMaster.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvList)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ToolStrip tslMaster;
        private System.Windows.Forms.ToolStripMenuItem tsbImport;
        private System.Windows.Forms.ToolStripMenuItem tsbStopImport;
        private System.Windows.Forms.ToolStripMenuItem tsbSearch;
        private System.Windows.Forms.ToolStripMenuItem tsbExport;
        private System.Windows.Forms.ToolStripMenuItem tsbExit;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtRecordDate;
        private System.Windows.Forms.TextBox txtTimes;
        private System.Windows.Forms.TextBox txtFirstNum;
        private System.Windows.Forms.TextBox txtSecondNum;
        private System.Windows.Forms.TextBox txtThirdNum;
        private System.Windows.Forms.TextBox txtFourthNum;
        private System.Windows.Forms.TextBox txtFifthNum;
        private System.Windows.Forms.TextBox txtSixthNum;
        private System.Windows.Forms.TextBox txtSeventhNum;
        private System.Windows.Forms.Label label6;
        private Common.myDataGridView dgvList;
    }
}