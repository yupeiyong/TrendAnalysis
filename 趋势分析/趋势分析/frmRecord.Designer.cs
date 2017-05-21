namespace 趋势分析
{
    partial class frmRecord
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmRecord));
            this.tslMaster = new System.Windows.Forms.ToolStrip();
            this.tsbAdd = new System.Windows.Forms.ToolStripMenuItem();
            this.tsbSave = new System.Windows.Forms.ToolStripMenuItem();
            this.tsbImport = new System.Windows.Forms.ToolStripMenuItem();
            this.tsbStopImport = new System.Windows.Forms.ToolStripMenuItem();
            this.tsbSearch = new System.Windows.Forms.ToolStripMenuItem();
            this.tsbExport = new System.Windows.Forms.ToolStripMenuItem();
            this.tsbExit = new System.Windows.Forms.ToolStripMenuItem();
            this.msMaster = new System.Windows.Forms.MenuStrip();
            this.文件ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmAdd = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmSave = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmImport = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmStopImport = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmExport = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmSearch = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmExit = new System.Windows.Forms.ToolStripMenuItem();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dgvList = new Common.myDataGridView();
            this.tslMaster.SuspendLayout();
            this.msMaster.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvList)).BeginInit();
            this.SuspendLayout();
            // 
            // tslMaster
            // 
            this.tslMaster.BackColor = System.Drawing.Color.LightSkyBlue;
            this.tslMaster.Dock = System.Windows.Forms.DockStyle.None;
            this.tslMaster.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbAdd,
            this.tsbSave,
            this.tsbImport,
            this.tsbStopImport,
            this.tsbSearch,
            this.tsbExport,
            this.tsbExit});
            this.tslMaster.Location = new System.Drawing.Point(2, 80);
            this.tslMaster.Name = "tslMaster";
            this.tslMaster.Size = new System.Drawing.Size(390, 25);
            this.tslMaster.TabIndex = 1;
            this.tslMaster.Text = "toolStrip1";
            // 
            // tsbAdd
            // 
            this.tsbAdd.Image = global::趋势分析.Properties.Resources.新增;
            this.tsbAdd.Name = "tsbAdd";
            this.tsbAdd.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.tsbAdd.Size = new System.Drawing.Size(78, 25);
            this.tsbAdd.Text = "新增(&N)";
            this.tsbAdd.Click += new System.EventHandler(this.tsbAdd_Click);
            // 
            // tsbSave
            // 
            this.tsbSave.Image = global::趋势分析.Properties.Resources.保存;
            this.tsbSave.Name = "tsbSave";
            this.tsbSave.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.tsbSave.Size = new System.Drawing.Size(60, 25);
            this.tsbSave.Text = "保存";
            this.tsbSave.Click += new System.EventHandler(this.tsbSave_Click);
            // 
            // tsbImport
            // 
            this.tsbImport.Image = global::趋势分析.Properties.Resources.导入;
            this.tsbImport.Name = "tsbImport";
            this.tsbImport.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.I)));
            this.tsbImport.Size = new System.Drawing.Size(60, 25);
            this.tsbImport.Text = "导入";
            this.tsbImport.Click += new System.EventHandler(this.tsbImport_Click);
            // 
            // tsbStopImport
            // 
            this.tsbStopImport.Image = global::趋势分析.Properties.Resources.停止small;
            this.tsbStopImport.Name = "tsbStopImport";
            this.tsbStopImport.Size = new System.Drawing.Size(108, 25);
            this.tsbStopImport.Text = "停止导入记录";
            this.tsbStopImport.Visible = false;
            this.tsbStopImport.Click += new System.EventHandler(this.tsbStopImport_Click);
            // 
            // tsbSearch
            // 
            this.tsbSearch.Image = global::趋势分析.Properties.Resources.search;
            this.tsbSearch.Name = "tsbSearch";
            this.tsbSearch.ShortcutKeys = System.Windows.Forms.Keys.F7;
            this.tsbSearch.Size = new System.Drawing.Size(60, 25);
            this.tsbSearch.Text = "查询";
            this.tsbSearch.ToolTipText = "按F7可以执行查询\r\n支持通配符 ? 单个字符 *多个字符\r\n数字和日期支持>、<、=、>=、<=、<>";
            this.tsbSearch.Click += new System.EventHandler(this.tsbSearch_Click);
            // 
            // tsbExport
            // 
            this.tsbExport.Image = global::趋势分析.Properties.Resources.excel;
            this.tsbExport.Name = "tsbExport";
            this.tsbExport.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.E)));
            this.tsbExport.Size = new System.Drawing.Size(60, 25);
            this.tsbExport.Text = "导出";
            this.tsbExport.Click += new System.EventHandler(this.tsbExport_Click);
            // 
            // tsbExit
            // 
            this.tsbExit.Image = global::趋势分析.Properties.Resources.离开;
            this.tsbExit.Name = "tsbExit";
            this.tsbExit.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.T)));
            this.tsbExit.Size = new System.Drawing.Size(60, 25);
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
            this.msMaster.Size = new System.Drawing.Size(994, 25);
            this.msMaster.TabIndex = 2;
            this.msMaster.Text = "menuStrip1";
            // 
            // 文件ToolStripMenuItem
            // 
            this.文件ToolStripMenuItem.BackColor = System.Drawing.Color.Transparent;
            this.文件ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmAdd,
            this.tsmSave,
            this.tsmImport,
            this.tsmStopImport,
            this.tsmExport,
            this.tsmSearch,
            this.tsmExit});
            this.文件ToolStripMenuItem.Name = "文件ToolStripMenuItem";
            this.文件ToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.文件ToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.文件ToolStripMenuItem.Text = "操作";
            // 
            // tsmAdd
            // 
            this.tsmAdd.Image = global::趋势分析.Properties.Resources.新增;
            this.tsmAdd.Name = "tsmAdd";
            this.tsmAdd.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.tsmAdd.Size = new System.Drawing.Size(165, 22);
            this.tsmAdd.Text = "新增(&N)";
            this.tsmAdd.Click += new System.EventHandler(this.tsbAdd_Click);
            // 
            // tsmSave
            // 
            this.tsmSave.Image = global::趋势分析.Properties.Resources.保存;
            this.tsmSave.Name = "tsmSave";
            this.tsmSave.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.tsmSave.Size = new System.Drawing.Size(165, 22);
            this.tsmSave.Text = "保存(&S)";
            this.tsmSave.Click += new System.EventHandler(this.tsbSave_Click);
            // 
            // tsmImport
            // 
            this.tsmImport.Image = global::趋势分析.Properties.Resources.导入;
            this.tsmImport.Name = "tsmImport";
            this.tsmImport.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.I)));
            this.tsmImport.Size = new System.Drawing.Size(165, 22);
            this.tsmImport.Text = "导入(&I)";
            this.tsmImport.Click += new System.EventHandler(this.tsbImport_Click);
            // 
            // tsmStopImport
            // 
            this.tsmStopImport.Image = global::趋势分析.Properties.Resources.停止small;
            this.tsmStopImport.Name = "tsmStopImport";
            this.tsmStopImport.Size = new System.Drawing.Size(165, 22);
            this.tsmStopImport.Text = "停止导入记录";
            this.tsmStopImport.Visible = false;
            this.tsmStopImport.Click += new System.EventHandler(this.tsbStopImport_Click);
            // 
            // tsmExport
            // 
            this.tsmExport.Image = global::趋势分析.Properties.Resources.excel;
            this.tsmExport.Name = "tsmExport";
            this.tsmExport.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.E)));
            this.tsmExport.Size = new System.Drawing.Size(165, 22);
            this.tsmExport.Text = "导出(&E)";
            this.tsmExport.Click += new System.EventHandler(this.tsbExport_Click);
            // 
            // tsmSearch
            // 
            this.tsmSearch.Image = global::趋势分析.Properties.Resources.search;
            this.tsmSearch.Name = "tsmSearch";
            this.tsmSearch.ShortcutKeys = System.Windows.Forms.Keys.F7;
            this.tsmSearch.Size = new System.Drawing.Size(165, 22);
            this.tsmSearch.Text = "查询(&C)";
            this.tsmSearch.Click += new System.EventHandler(this.tsbSearch_Click);
            // 
            // tsmExit
            // 
            this.tsmExit.Image = global::趋势分析.Properties.Resources.离开;
            this.tsmExit.Name = "tsmExit";
            this.tsmExit.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.T)));
            this.tsmExit.Size = new System.Drawing.Size(165, 22);
            this.tsmExit.Text = "退出(&T)";
            this.tsmExit.Click += new System.EventHandler(this.tsbExit_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 14;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.label2, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.label3, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.label4, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.label5, 4, 1);
            this.tableLayoutPanel1.Controls.Add(this.label6, 6, 1);
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
            this.tableLayoutPanel1.Location = new System.Drawing.Point(8, 15);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(780, 53);
            this.tableLayoutPanel1.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(4, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "记录日期";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(138, 7);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 12);
            this.label2.TabIndex = 0;
            this.label2.Text = "期数";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(16, 33);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 0;
            this.label3.Text = "第一数";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(126, 33);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 12);
            this.label4.TabIndex = 0;
            this.label4.Text = "第二数";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label5
            // 
            this.label5.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(236, 33);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(41, 12);
            this.label5.TabIndex = 0;
            this.label5.Text = "第三数";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label6
            // 
            this.label6.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(346, 33);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(41, 12);
            this.label6.TabIndex = 0;
            this.label6.Text = "第四数";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label7
            // 
            this.label7.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(456, 33);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(41, 12);
            this.label7.TabIndex = 0;
            this.label7.Text = "第五数";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label8
            // 
            this.label8.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(566, 33);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(41, 12);
            this.label8.TabIndex = 0;
            this.label8.Text = "第六数";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label9
            // 
            this.label9.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(676, 33);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(41, 12);
            this.label9.TabIndex = 0;
            this.label9.Text = "第七数";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtRecordDate
            // 
            this.txtRecordDate.Location = new System.Drawing.Point(63, 3);
            this.txtRecordDate.Name = "txtRecordDate";
            this.txtRecordDate.Size = new System.Drawing.Size(54, 21);
            this.txtRecordDate.TabIndex = 1;
            // 
            // txtTimes
            // 
            this.txtTimes.Location = new System.Drawing.Point(173, 3);
            this.txtTimes.Name = "txtTimes";
            this.txtTimes.Size = new System.Drawing.Size(54, 21);
            this.txtTimes.TabIndex = 1;
            // 
            // txtFirstNum
            // 
            this.txtFirstNum.Location = new System.Drawing.Point(63, 29);
            this.txtFirstNum.Name = "txtFirstNum";
            this.txtFirstNum.Size = new System.Drawing.Size(54, 21);
            this.txtFirstNum.TabIndex = 1;
            // 
            // txtSecondNum
            // 
            this.txtSecondNum.Location = new System.Drawing.Point(173, 29);
            this.txtSecondNum.Name = "txtSecondNum";
            this.txtSecondNum.Size = new System.Drawing.Size(54, 21);
            this.txtSecondNum.TabIndex = 1;
            // 
            // txtThirdNum
            // 
            this.txtThirdNum.Location = new System.Drawing.Point(283, 29);
            this.txtThirdNum.Name = "txtThirdNum";
            this.txtThirdNum.Size = new System.Drawing.Size(54, 21);
            this.txtThirdNum.TabIndex = 1;
            // 
            // txtFourthNum
            // 
            this.txtFourthNum.Location = new System.Drawing.Point(393, 29);
            this.txtFourthNum.Name = "txtFourthNum";
            this.txtFourthNum.Size = new System.Drawing.Size(54, 21);
            this.txtFourthNum.TabIndex = 1;
            // 
            // txtFifthNum
            // 
            this.txtFifthNum.Location = new System.Drawing.Point(503, 29);
            this.txtFifthNum.Name = "txtFifthNum";
            this.txtFifthNum.Size = new System.Drawing.Size(54, 21);
            this.txtFifthNum.TabIndex = 1;
            // 
            // txtSixthNum
            // 
            this.txtSixthNum.Location = new System.Drawing.Point(613, 29);
            this.txtSixthNum.Name = "txtSixthNum";
            this.txtSixthNum.Size = new System.Drawing.Size(54, 21);
            this.txtSixthNum.TabIndex = 1;
            // 
            // txtSeventhNum
            // 
            this.txtSeventhNum.Location = new System.Drawing.Point(723, 29);
            this.txtSeventhNum.Name = "txtSeventhNum";
            this.txtSeventhNum.Size = new System.Drawing.Size(54, 21);
            this.txtSeventhNum.TabIndex = 1;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.tableLayoutPanel1);
            this.groupBox1.Location = new System.Drawing.Point(2, -1);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(980, 71);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            // 
            // dgvList
            // 
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.dgvList.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvList.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvList.DefaultCellStyle = dataGridViewCellStyle3;
            this.dgvList.Location = new System.Drawing.Point(2, 108);
            this.dgvList.Name = "dgvList";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvList.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.dgvList.RowTemplate.Height = 23;
            this.dgvList.Size = new System.Drawing.Size(990, 408);
            this.dgvList.TabIndex = 0;
            this.dgvList.CellMouseEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvList_CellMouseEnter);
            this.dgvList.CellMouseLeave += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvList_CellMouseLeave);
            this.dgvList.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.dgvList_CellValidating);
            this.dgvList.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.dgvList_DataBindingComplete);
            this.dgvList.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.dgvList_DataError);
            // 
            // frmRecord
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightSkyBlue;
            this.ClientSize = new System.Drawing.Size(994, 518);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.tslMaster);
            this.Controls.Add(this.msMaster);
            this.Controls.Add(this.dgvList);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.msMaster;
            this.Name = "frmRecord";
            this.Text = "记录";
            this.Activated += new System.EventHandler(this.frmRecord_Activated);
            this.Deactivate += new System.EventHandler(this.frmRecord_Deactivate);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmRecord_FormClosing);
            this.Load += new System.EventHandler(this.frmRecord_Load);
            this.tslMaster.ResumeLayout(false);
            this.tslMaster.PerformLayout();
            this.msMaster.ResumeLayout(false);
            this.msMaster.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvList)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Common.myDataGridView dgvList;
        private System.Windows.Forms.ToolStrip tslMaster;
        private System.Windows.Forms.MenuStrip msMaster;
        private System.Windows.Forms.ToolStripMenuItem 文件ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tsmSave;
        private System.Windows.Forms.ToolStripMenuItem tsbSave;
        private System.Windows.Forms.ToolStripMenuItem tsbImport;
        private System.Windows.Forms.ToolStripMenuItem tsbExport;
        private System.Windows.Forms.ToolStripMenuItem tsbSearch;
        private System.Windows.Forms.ToolStripMenuItem tsbExit;
        private System.Windows.Forms.ToolStripMenuItem tsmImport;
        private System.Windows.Forms.ToolStripMenuItem tsmExport;
        private System.Windows.Forms.ToolStripMenuItem tsmSearch;
        private System.Windows.Forms.ToolStripMenuItem tsmExit;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
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
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ToolStripMenuItem tsmStopImport;
        private System.Windows.Forms.ToolStripMenuItem tsbStopImport;
        private System.Windows.Forms.ToolStripMenuItem tsmAdd;
        private System.Windows.Forms.ToolStripMenuItem tsbAdd;

    }
}