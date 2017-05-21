namespace 趋势分析
{
    partial class frmAnalysisResult
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmAnalysisResult));
            this.dgvList = new Common.myDataGridView();
            this.label26 = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.txtExpectIncome = new System.Windows.Forms.TextBox();
            this.txtAnalysisTimes = new System.Windows.Forms.TextBox();
            this.txtRelityIncome = new System.Windows.Forms.TextBox();
            this.label25 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label24 = new System.Windows.Forms.Label();
            this.txtMultiple = new System.Windows.Forms.TextBox();
            this.label19 = new System.Windows.Forms.Label();
            this.txtWriter = new System.Windows.Forms.TextBox();
            this.txtNum = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.txtWriteDate = new System.Windows.Forms.TextBox();
            this.txtBalance = new System.Windows.Forms.TextBox();
            this.txtOutlay = new System.Windows.Forms.TextBox();
            this.txtNumAndMoney = new System.Windows.Forms.TextBox();
            this.txtModifyDate = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.tslMaster = new System.Windows.Forms.ToolStrip();
            this.tsbSave = new System.Windows.Forms.ToolStripMenuItem();
            this.tsbImport = new System.Windows.Forms.ToolStripMenuItem();
            this.tsbStopImport = new System.Windows.Forms.ToolStripMenuItem();
            this.tsbSearch = new System.Windows.Forms.ToolStripMenuItem();
            this.tsbExport = new System.Windows.Forms.ToolStripMenuItem();
            this.tsbExit = new System.Windows.Forms.ToolStripMenuItem();
            this.msMaster = new System.Windows.Forms.MenuStrip();
            this.文件ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmSave = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmImport = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmStopImport = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmExport = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmSearch = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmExit = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.txtWhatNumber = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvList)).BeginInit();
            this.tslMaster.SuspendLayout();
            this.msMaster.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvList
            // 
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.dgvList.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvList.Location = new System.Drawing.Point(1, 180);
            this.dgvList.Name = "dgvList";
            this.dgvList.RowTemplate.Height = 23;
            this.dgvList.Size = new System.Drawing.Size(817, 239);
            this.dgvList.TabIndex = 7;
            this.dgvList.CellMouseEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvList_CellMouseEnter);
            this.dgvList.CellMouseLeave += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvList_CellMouseLeave);
            this.dgvList.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.dgvList_DataBindingComplete);
            this.dgvList.RowPrePaint += new System.Windows.Forms.DataGridViewRowPrePaintEventHandler(this.dgvList_RowPrePaint);
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Location = new System.Drawing.Point(220, 18);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(53, 12);
            this.label26.TabIndex = 5;
            this.label26.Text = "第几位：";
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Location = new System.Drawing.Point(408, 78);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(41, 12);
            this.label23.TabIndex = 4;
            this.label23.Text = "号码：";
            // 
            // txtExpectIncome
            // 
            this.txtExpectIncome.ForeColor = System.Drawing.Color.Blue;
            this.txtExpectIncome.Location = new System.Drawing.Point(212, 70);
            this.txtExpectIncome.Name = "txtExpectIncome";
            this.txtExpectIncome.Size = new System.Drawing.Size(159, 21);
            this.txtExpectIncome.TabIndex = 3;
            // 
            // txtAnalysisTimes
            // 
            this.txtAnalysisTimes.ForeColor = System.Drawing.Color.Blue;
            this.txtAnalysisTimes.Location = new System.Drawing.Point(51, 13);
            this.txtAnalysisTimes.Name = "txtAnalysisTimes";
            this.txtAnalysisTimes.Size = new System.Drawing.Size(67, 21);
            this.txtAnalysisTimes.TabIndex = 3;
            // 
            // txtRelityIncome
            // 
            this.txtRelityIncome.ForeColor = System.Drawing.Color.Blue;
            this.txtRelityIncome.Location = new System.Drawing.Point(562, 70);
            this.txtRelityIncome.Name = "txtRelityIncome";
            this.txtRelityIncome.Size = new System.Drawing.Size(132, 21);
            this.txtRelityIncome.TabIndex = 3;
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Location = new System.Drawing.Point(504, 108);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(65, 12);
            this.label25.TabIndex = 4;
            this.label25.Text = "修改时间：";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(149, 78);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(53, 12);
            this.label15.TabIndex = 4;
            this.label15.Text = "预期收益";
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Location = new System.Drawing.Point(306, 108);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(65, 12);
            this.label24.TabIndex = 4;
            this.label24.Text = "填写时间：";
            // 
            // txtMultiple
            // 
            this.txtMultiple.ForeColor = System.Drawing.Color.Blue;
            this.txtMultiple.Location = new System.Drawing.Point(168, 13);
            this.txtMultiple.Name = "txtMultiple";
            this.txtMultiple.Size = new System.Drawing.Size(46, 21);
            this.txtMultiple.TabIndex = 3;
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(173, 108);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(53, 12);
            this.label19.TabIndex = 4;
            this.label19.Text = "填写人：";
            // 
            // txtWriter
            // 
            this.txtWriter.ForeColor = System.Drawing.Color.Blue;
            this.txtWriter.Location = new System.Drawing.Point(226, 101);
            this.txtWriter.Name = "txtWriter";
            this.txtWriter.Size = new System.Drawing.Size(64, 21);
            this.txtWriter.TabIndex = 3;
            // 
            // txtNum
            // 
            this.txtNum.ForeColor = System.Drawing.Color.Blue;
            this.txtNum.Location = new System.Drawing.Point(450, 70);
            this.txtNum.Name = "txtNum";
            this.txtNum.Size = new System.Drawing.Size(37, 21);
            this.txtNum.TabIndex = 3;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(503, 78);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(53, 12);
            this.label16.TabIndex = 4;
            this.label16.Text = "实际收益";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(128, 18);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(41, 12);
            this.label18.TabIndex = 4;
            this.label18.Text = "倍率：";
            // 
            // txtWriteDate
            // 
            this.txtWriteDate.ForeColor = System.Drawing.Color.Blue;
            this.txtWriteDate.Location = new System.Drawing.Point(371, 101);
            this.txtWriteDate.Name = "txtWriteDate";
            this.txtWriteDate.Size = new System.Drawing.Size(126, 21);
            this.txtWriteDate.TabIndex = 3;
            // 
            // txtBalance
            // 
            this.txtBalance.ForeColor = System.Drawing.Color.Blue;
            this.txtBalance.Location = new System.Drawing.Point(49, 101);
            this.txtBalance.Name = "txtBalance";
            this.txtBalance.Size = new System.Drawing.Size(94, 21);
            this.txtBalance.TabIndex = 3;
            // 
            // txtOutlay
            // 
            this.txtOutlay.ForeColor = System.Drawing.Color.Blue;
            this.txtOutlay.Location = new System.Drawing.Point(51, 70);
            this.txtOutlay.Name = "txtOutlay";
            this.txtOutlay.Size = new System.Drawing.Size(92, 21);
            this.txtOutlay.TabIndex = 3;
            // 
            // txtNumAndMoney
            // 
            this.txtNumAndMoney.ForeColor = System.Drawing.Color.Blue;
            this.txtNumAndMoney.Location = new System.Drawing.Point(93, 43);
            this.txtNumAndMoney.Name = "txtNumAndMoney";
            this.txtNumAndMoney.Size = new System.Drawing.Size(601, 21);
            this.txtNumAndMoney.TabIndex = 3;
            // 
            // txtModifyDate
            // 
            this.txtModifyDate.ForeColor = System.Drawing.Color.Blue;
            this.txtModifyDate.Location = new System.Drawing.Point(569, 101);
            this.txtModifyDate.Name = "txtModifyDate";
            this.txtModifyDate.Size = new System.Drawing.Size(126, 21);
            this.txtModifyDate.TabIndex = 3;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(10, 18);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(41, 12);
            this.label17.TabIndex = 4;
            this.label17.Text = "期次：";
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(10, 108);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(41, 12);
            this.label22.TabIndex = 4;
            this.label22.Text = "余额：";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(10, 48);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(77, 12);
            this.label20.TabIndex = 4;
            this.label20.Text = "号码及金额：";
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(10, 78);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(41, 12);
            this.label21.TabIndex = 4;
            this.label21.Text = "支出：";
            // 
            // tslMaster
            // 
            this.tslMaster.BackColor = System.Drawing.Color.Transparent;
            this.tslMaster.Dock = System.Windows.Forms.DockStyle.None;
            this.tslMaster.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbSave,
            this.tsbImport,
            this.tsbStopImport,
            this.tsbSearch,
            this.tsbExport,
            this.tsbExit});
            this.tslMaster.Location = new System.Drawing.Point(9, 152);
            this.tslMaster.Name = "tslMaster";
            this.tslMaster.Size = new System.Drawing.Size(307, 25);
            this.tslMaster.TabIndex = 7;
            this.tslMaster.Text = "toolStrip1";
            // 
            // tsbSave
            // 
            this.tsbSave.Image = global::趋势分析.Properties.Resources.保存;
            this.tsbSave.Name = "tsbSave";
            this.tsbSave.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.tsbSave.Size = new System.Drawing.Size(59, 25);
            this.tsbSave.Text = "保存";
            this.tsbSave.Click += new System.EventHandler(this.tsbSave_Click);
            // 
            // tsbImport
            // 
            this.tsbImport.Image = global::趋势分析.Properties.Resources.导入;
            this.tsbImport.Name = "tsbImport";
            this.tsbImport.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.I)));
            this.tsbImport.Size = new System.Drawing.Size(59, 25);
            this.tsbImport.Text = "导入";
            this.tsbImport.Click += new System.EventHandler(this.tsbImport_Click);
            // 
            // tsbStopImport
            // 
            this.tsbStopImport.Image = global::趋势分析.Properties.Resources.停止small;
            this.tsbStopImport.Name = "tsbStopImport";
            this.tsbStopImport.Size = new System.Drawing.Size(107, 25);
            this.tsbStopImport.Text = "停止导入记录";
            this.tsbStopImport.Visible = false;
            this.tsbStopImport.Click += new System.EventHandler(this.tsbStopImport_Click);
            // 
            // tsbSearch
            // 
            this.tsbSearch.Image = global::趋势分析.Properties.Resources.search;
            this.tsbSearch.Name = "tsbSearch";
            this.tsbSearch.ShortcutKeys = System.Windows.Forms.Keys.F7;
            this.tsbSearch.Size = new System.Drawing.Size(59, 25);
            this.tsbSearch.Text = "查询";
            this.tsbSearch.ToolTipText = "按F7可以执行查询\r\n支持通配符 ? 单个字符 *多个字符\r\n数字和日期支持>、<、=、>=、<=、<>";
            this.tsbSearch.Click += new System.EventHandler(this.tsbSearch_Click);
            // 
            // tsbExport
            // 
            this.tsbExport.Image = global::趋势分析.Properties.Resources.excel;
            this.tsbExport.Name = "tsbExport";
            this.tsbExport.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.E)));
            this.tsbExport.Size = new System.Drawing.Size(59, 25);
            this.tsbExport.Text = "导出";
            this.tsbExport.Click += new System.EventHandler(this.tsbExport_Click);
            // 
            // tsbExit
            // 
            this.tsbExit.Image = global::趋势分析.Properties.Resources.离开;
            this.tsbExit.Name = "tsbExit";
            this.tsbExit.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.T)));
            this.tsbExit.Size = new System.Drawing.Size(59, 25);
            this.tsbExit.Text = "退出";
            this.tsbExit.Click += new System.EventHandler(this.tsbExit_Click);
            // 
            // msMaster
            // 
            this.msMaster.BackColor = System.Drawing.Color.Transparent;
            this.msMaster.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.文件ToolStripMenuItem});
            this.msMaster.Location = new System.Drawing.Point(0, 0);
            this.msMaster.Name = "msMaster";
            this.msMaster.Size = new System.Drawing.Size(819, 24);
            this.msMaster.TabIndex = 9;
            this.msMaster.Text = "menuStrip1";
            // 
            // 文件ToolStripMenuItem
            // 
            this.文件ToolStripMenuItem.BackColor = System.Drawing.Color.Transparent;
            this.文件ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmSave,
            this.tsmImport,
            this.tsmStopImport,
            this.tsmExport,
            this.tsmSearch,
            this.tsmExit});
            this.文件ToolStripMenuItem.Name = "文件ToolStripMenuItem";
            this.文件ToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.文件ToolStripMenuItem.Size = new System.Drawing.Size(43, 20);
            this.文件ToolStripMenuItem.Text = "操作";
            // 
            // tsmSave
            // 
            this.tsmSave.Image = global::趋势分析.Properties.Resources.保存;
            this.tsmSave.Name = "tsmSave";
            this.tsmSave.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.tsmSave.Size = new System.Drawing.Size(156, 22);
            this.tsmSave.Text = "保存(&S)";
            this.tsmSave.Click += new System.EventHandler(this.tsbSave_Click);
            // 
            // tsmImport
            // 
            this.tsmImport.Image = global::趋势分析.Properties.Resources.导入;
            this.tsmImport.Name = "tsmImport";
            this.tsmImport.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.I)));
            this.tsmImport.Size = new System.Drawing.Size(156, 22);
            this.tsmImport.Text = "导入(&I)";
            this.tsmImport.Click += new System.EventHandler(this.tsbImport_Click);
            // 
            // tsmStopImport
            // 
            this.tsmStopImport.Image = global::趋势分析.Properties.Resources.停止small;
            this.tsmStopImport.Name = "tsmStopImport";
            this.tsmStopImport.Size = new System.Drawing.Size(156, 22);
            this.tsmStopImport.Text = "停止导入记录";
            this.tsmStopImport.Visible = false;
            this.tsmStopImport.Click += new System.EventHandler(this.tsbStopImport_Click);
            // 
            // tsmExport
            // 
            this.tsmExport.Image = global::趋势分析.Properties.Resources.excel;
            this.tsmExport.Name = "tsmExport";
            this.tsmExport.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.E)));
            this.tsmExport.Size = new System.Drawing.Size(156, 22);
            this.tsmExport.Text = "导出(&E)";
            this.tsmExport.Click += new System.EventHandler(this.tsbExport_Click);
            // 
            // tsmSearch
            // 
            this.tsmSearch.Image = global::趋势分析.Properties.Resources.search;
            this.tsmSearch.Name = "tsmSearch";
            this.tsmSearch.ShortcutKeys = System.Windows.Forms.Keys.F7;
            this.tsmSearch.Size = new System.Drawing.Size(156, 22);
            this.tsmSearch.Text = "查询(&C)";
            this.tsmSearch.Click += new System.EventHandler(this.tsbSearch_Click);
            // 
            // tsmExit
            // 
            this.tsmExit.Image = global::趋势分析.Properties.Resources.离开;
            this.tsmExit.Name = "tsmExit";
            this.tsmExit.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.T)));
            this.tsmExit.Size = new System.Drawing.Size(156, 22);
            this.tsmExit.Text = "退出(&T)";
            this.tsmExit.Click += new System.EventHandler(this.tsbExit_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.BackColor = System.Drawing.Color.Transparent;
            this.groupBox3.Controls.Add(this.label17);
            this.groupBox3.Controls.Add(this.label26);
            this.groupBox3.Controls.Add(this.label21);
            this.groupBox3.Controls.Add(this.label23);
            this.groupBox3.Controls.Add(this.label20);
            this.groupBox3.Controls.Add(this.txtExpectIncome);
            this.groupBox3.Controls.Add(this.label22);
            this.groupBox3.Controls.Add(this.txtAnalysisTimes);
            this.groupBox3.Controls.Add(this.txtModifyDate);
            this.groupBox3.Controls.Add(this.txtRelityIncome);
            this.groupBox3.Controls.Add(this.txtNumAndMoney);
            this.groupBox3.Controls.Add(this.label25);
            this.groupBox3.Controls.Add(this.txtOutlay);
            this.groupBox3.Controls.Add(this.label15);
            this.groupBox3.Controls.Add(this.txtBalance);
            this.groupBox3.Controls.Add(this.label24);
            this.groupBox3.Controls.Add(this.txtWriteDate);
            this.groupBox3.Controls.Add(this.txtWhatNumber);
            this.groupBox3.Controls.Add(this.txtMultiple);
            this.groupBox3.Controls.Add(this.label18);
            this.groupBox3.Controls.Add(this.label19);
            this.groupBox3.Controls.Add(this.label16);
            this.groupBox3.Controls.Add(this.txtWriter);
            this.groupBox3.Controls.Add(this.txtNum);
            this.groupBox3.Location = new System.Drawing.Point(5, 3);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(790, 137);
            this.groupBox3.TabIndex = 10;
            this.groupBox3.TabStop = false;
            // 
            // txtWhatNumber
            // 
            this.txtWhatNumber.ForeColor = System.Drawing.Color.Blue;
            this.txtWhatNumber.Location = new System.Drawing.Point(274, 13);
            this.txtWhatNumber.Name = "txtWhatNumber";
            this.txtWhatNumber.Size = new System.Drawing.Size(64, 21);
            this.txtWhatNumber.TabIndex = 3;
            // 
            // frmAnalysisResult
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Khaki;
            this.ClientSize = new System.Drawing.Size(819, 420);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.msMaster);
            this.Controls.Add(this.tslMaster);
            this.Controls.Add(this.dgvList);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.msMaster;
            this.Name = "frmAnalysisResult";
            this.Text = "frmAnalysisResult";
            this.Activated += new System.EventHandler(this.frmAnalysisResult_Activated);
            this.Deactivate += new System.EventHandler(this.frmAnalysisResult_Deactivate);
            this.Load += new System.EventHandler(this.frmAnalysisResult_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvList)).EndInit();
            this.tslMaster.ResumeLayout(false);
            this.tslMaster.PerformLayout();
            this.msMaster.ResumeLayout(false);
            this.msMaster.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Common.myDataGridView dgvList;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.TextBox txtExpectIncome;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.TextBox txtAnalysisTimes;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.TextBox txtRelityIncome;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.TextBox txtMultiple;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.TextBox txtWriter;
        private System.Windows.Forms.TextBox txtNum;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.TextBox txtWriteDate;
        private System.Windows.Forms.TextBox txtBalance;
        private System.Windows.Forms.TextBox txtOutlay;
        private System.Windows.Forms.TextBox txtNumAndMoney;
        private System.Windows.Forms.TextBox txtModifyDate;
        private System.Windows.Forms.ToolStrip tslMaster;
        private System.Windows.Forms.ToolStripMenuItem tsbSave;
        private System.Windows.Forms.ToolStripMenuItem tsbImport;
        private System.Windows.Forms.ToolStripMenuItem tsbStopImport;
        private System.Windows.Forms.ToolStripMenuItem tsbSearch;
        private System.Windows.Forms.ToolStripMenuItem tsbExport;
        private System.Windows.Forms.ToolStripMenuItem tsbExit;
        private System.Windows.Forms.MenuStrip msMaster;
        private System.Windows.Forms.ToolStripMenuItem 文件ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tsmSave;
        private System.Windows.Forms.ToolStripMenuItem tsmImport;
        private System.Windows.Forms.ToolStripMenuItem tsmStopImport;
        private System.Windows.Forms.ToolStripMenuItem tsmExport;
        private System.Windows.Forms.ToolStripMenuItem tsmSearch;
        private System.Windows.Forms.ToolStripMenuItem tsmExit;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox txtWhatNumber;
    }
}