using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace 趋势分析
{
    public partial class frmRecord : Form
    {
        /// <summary>
        /// 导入的记录条数
        /// </summary>
        private int importCount = 0;
        /// <summary>
        /// 是否停止导入记录
        /// </summary>
        private bool isStopImport = false;
        /// <summary>
        /// 引用主窗口
        /// </summary>
        private frmMain frmMdi = null;
        /// <summary>
        /// 导入趋势分析后台对象
        /// </summary>
        private BackgroundWorker bgwImport = null;
        public frmRecord()
        {
            InitializeComponent();
        }

        private void frmRecord_Load(object sender, EventArgs e)
        {
            //取得MDI窗体的引用
            frmMdi = this.MdiParent as frmMain;
        }

        private void frmRecord_Activated(object sender, EventArgs e)
        {
            frmMdi.tsslInfo.Text = "就绪";
            frmMdi.tsbRecord.BackColor = Color.LimeGreen;
        }

        private void frmRecord_Deactivate(object sender, EventArgs e)
        {
            frmMdi.tsslInfo.Text = "就绪";
            frmMdi.tsbRecord.BackColor = frmMdi.BackColor;
        }

        private void frmRecord_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (bgwImport != null)
            {
                if (bgwImport.CancellationPending == false)
                {
                    bgwImport.CancelAsync();
                }
            }
            Application.DoEvents();
        }

        private void tsbAdd_Click(object sender, EventArgs e)
        {
            BLL.RecordBLL bll = new BLL.RecordBLL();
            //取得一张空表
            DataTable dttab =bll.GetDataByCondition(null);
            if (dttab != null)
            {
                dgvList.DataSource = dttab;
            }
        }

        private void tsbSave_Click(object sender, EventArgs e)
        {
            int rowCount = dgvList.Rows.Count;
            if (rowCount == 0 || (dgvList.AllowUserToAddRows && rowCount == 1))
            {
                MessageBox.Show("无保存内容！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                return;
            }
            DialogResult result= MessageBox.Show("确认保存吗！", "确认", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (result == DialogResult.Cancel)
            {
                return;
            }
            //遍历行
            for (int r = 0; r < dgvList.Rows.Count; r++)
            {
                if (dgvList.Rows[r].IsNewRow) { continue; }
                //从第2列开始遍历列
                for (int c = 1; c < dgvList.Columns.Count; c++)
                {
                    //检查有无空行或空的内容
                    object value = dgvList.Rows[r].Cells[c].Value;
                    if (value == null || value == DBNull.Value || value.ToString().Trim().Length == 0)
                    {
                        MessageBox.Show("单元内容不能为空！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        dgvList.ClearSelection();
                        dgvList.CurrentCell = dgvList.Rows[r].Cells[c];
                        dgvList.CurrentCell.Selected=true;
                        dgvList.Focus();
                        return;
                    }
                }
            }
            BLL.RecordBLL bll = new BLL.RecordBLL();
            DataTable dttab = dgvList.DataSource as DataTable;
            if (dttab != null)
            {
                try
                {
                    bll.SaveData(dttab);
                    MessageBox.Show("保存完成", "完成", MessageBoxButtons.OK, MessageBoxIcon.None);
                    frmMdi.tsslInfo.Text = "保存成功!";
                    frmMdi.tsslInfo.BackColor = frmMdi.BackColor;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("保存错误，" + ex.Message, "失败", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    frmMdi.tsslInfo.Text = "保存失败!";
                    frmMdi.tsslInfo.BackColor = Color.Yellow;
                }
            }
        }

        private void tsbImport_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("确认导入记录数据吗？", "确认", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (result == DialogResult.Cancel)
            {
                return;
            }
            //文件选择窗口
            OpenFileDialog ofd = new OpenFileDialog();
            //筛选条件
            ofd.Filter = "Excel2003(*.xls)|*.xls|Excel2007(*.xlsx)|*.xlsx";
            //文件名
            string fileName = string.Empty;
            if (ofd.ShowDialog(this) == DialogResult.OK)
            {
                fileName = ofd.FileName;
            }
            if (fileName.Length > 0)
            {
                if (bgwImport == null)
                {
                    bgwImport = new BackgroundWorker();
                    //记录完成进度
                    bgwImport.WorkerReportsProgress = true;
                    //支持取消任务
                    bgwImport.WorkerSupportsCancellation = true;
                    //绑定事件
                    bgwImport.DoWork += new DoWorkEventHandler(bgwImport_DoWork);
                    bgwImport.ProgressChanged += new ProgressChangedEventHandler(bgwImport_ProgressChanged);
                    bgwImport.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bgwImport_RunWorkerCompleted);
                }
                //设置工具条和菜单状态
                foreach (ToolStripItem item in msMaster.Items)
                {
                    item.Enabled = false;
                }
                tsmStopImport.Visible =true;
                tsmStopImport.Enabled = true;
                foreach (ToolStripItem item in tslMaster.Items)
                {
                    item.Enabled = false;
                }
                tsbStopImport.Visible = true;
                tsbStopImport.Enabled = true;
                frmMdi.tspbCompletePrecent.Visible = true;
                frmMdi.tspbCompletePrecent.Value = 0;
                frmMdi.tsslInfo.Text = "正在导入记录";
                frmMdi.tsslInfo.BackColor = Color.Yellow;
                //执行异步获取记录列表
                bgwImport.RunWorkerAsync(fileName);
            }
        }

        private void tsbStopImport_Click(object sender, EventArgs e)
        {
            bgwImport.CancelAsync();
            isStopImport = true;
        }

        private void tsbExport_Click(object sender, EventArgs e)
        {
            if (dgvList.Rows.Count == 0) { return; }
            object[,] objs = null;
            if (dgvList.AllowUserToAddRows == true)
            {
                objs = new object[dgvList.Rows.Count - 1, dgvList.Columns.Count];
            }
            else
            {
                objs = new object[dgvList.Rows.Count , dgvList.Columns.Count];
            }
            for (int i = 0; i < dgvList.Rows.Count; i++)
            {
                if (dgvList.Rows[i].IsNewRow) { continue; }
                for (int c = 0; c < dgvList.Columns.Count; c++)
                {
                    objs[i, c] = dgvList.Rows[i].Cells[c].Value;
                }
            }
            string[] heads = "ID,记录日期,期次,第一数,第二数,第三数,第四数,第五数,第六数,第七数".Split(new char[] { ',' });
            ExcelHelper helper = new ExcelHelper();
            try
            {
                helper.WriteToBook(objs, heads);
                MessageBox.Show("导出完成", "完成", MessageBoxButtons.OK, MessageBoxIcon.None);
                frmMdi.tsslInfo.Text = "导出成功!";
                frmMdi.tsslInfo.BackColor = frmMdi.BackColor;
            }
            catch (Exception ex)
            {
                MessageBox.Show("导出错误，"+ex.Message, "失败", MessageBoxButtons.OK, MessageBoxIcon.Error);
                frmMdi.tsslInfo.Text = "导出失败!";
                frmMdi.tsslInfo.BackColor = Color.Yellow;
            }
        }

        private void tsbSearch_Click(object sender, EventArgs e)
        {
            //查询条件列表
            List<Model.Condition> lCondition = new List<Model.Condition>();
            if (txtRecordDate.Text.Trim().Length > 0)
            {
                lCondition.Add(
                    new Model.Condition
                    {
                        paramName = "record_date",
                        paramValue = txtRecordDate.Text,
                        FieldType =Model.Condition.FieldDbType.DateTime
                    });
            }
            if (txtTimes.Text.Trim().Length > 0)
            {
                lCondition.Add(
                    new Model.Condition
                    {
                        paramName = "times",
                        paramValue = txtTimes.Text,
                        FieldType = Model.Condition.FieldDbType.String
                    });
            }
            if (txtFirstNum.Text.Trim().Length > 0)
            {
                lCondition.Add(
                    new Model.Condition
                    {
                        paramName = "first_num",
                        paramValue = txtFirstNum.Text,
                        FieldType = Model.Condition.FieldDbType.Number
                    });
            }
            if (txtSecondNum.Text.Trim().Length > 0)
            {
                lCondition.Add(
                    new Model.Condition
                    {
                        paramName = "second_num",
                        paramValue = txtSecondNum.Text,
                        FieldType = Model.Condition.FieldDbType.Number
                    });
            }
            if (txtThirdNum.Text.Trim().Length > 0)
            {
                lCondition.Add(
                    new Model.Condition
                    {
                        paramName = "third_num",
                        paramValue = txtThirdNum.Text,
                        FieldType = Model.Condition.FieldDbType.Number
                    });
            }
            if (txtFourthNum.Text.Trim().Length > 0)
            {
                lCondition.Add(
                    new Model.Condition
                    {
                        paramName = "fourth_num",
                        paramValue = txtFourthNum.Text,
                        FieldType = Model.Condition.FieldDbType.Number
                    });
            }
            if (txtFifthNum.Text.Trim().Length > 0)
            {
                lCondition.Add(
                    new Model.Condition
                    {
                        paramName = "fifth_num",
                        paramValue = txtFifthNum.Text,
                        FieldType = Model.Condition.FieldDbType.Number
                    });
            }
            if (txtSixthNum.Text.Trim().Length > 0)
            {
                lCondition.Add(
                    new Model.Condition
                    {
                        paramName = "sixth_num",
                        paramValue = txtSixthNum.Text,
                        FieldType = Model.Condition.FieldDbType.Number
                    });
            }
            if (txtSeventhNum.Text.Trim().Length > 0)
            {
                lCondition.Add(
                    new Model.Condition
                    {
                        paramName = "seventh_num",
                        paramValue = txtSeventhNum.Text,
                        FieldType = Model.Condition.FieldDbType.Number
                    });
            }
            if (lCondition.Count == 0)
            {
                MessageBox.Show("查询条件为空！", "失败", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return;
            }
            BLL.RecordBLL bll = new BLL.RecordBLL();
            try
            {
                //按条件取得数据
                DataTable dttab = bll.GetDataByCondition(lCondition);
                if (dttab == null || dttab.Rows.Count == 0)
                {
                    MessageBox.Show(
                        "没有找到符合条件的数据！",
                        "失败",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error
                        );
                    frmMdi.tsslInfo.Text = "查询内容为空！";
                    frmMdi.tsslInfo.BackColor = Color.Yellow;
                }
                else
                {
                    //绑定数据
                    dgvList.DataSource = dttab;
                    frmMdi.tsslInfo.Text = "查询完成！";
                    frmMdi.tsslInfo.BackColor = frmMdi.BackColor;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "查询发生错误，"+ex.Message,
                    "错误",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                    );
                frmMdi.tsslInfo.Text = "查询失败！";
                frmMdi.tsslInfo.BackColor = Color.Yellow;
            }
        }

        private void tsbExit_Click(object sender, EventArgs e)
        {
            Application.DoEvents();
            this.Close();
            this.Dispose();
        }
        #region "处理导入记录后台事件"
        void bgwImport_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (isStopImport)
            {
                MessageBox.Show(
                    "您取消了导入记录！",
                    "取消",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.None
                    );
                frmMdi.tsslInfo.Text = "取消了导入记录";
                frmMdi.tsslInfo.BackColor = Color.Yellow;
            }
            else
            {
                if (importCount > 0)
                {
                    MessageBox.Show(
                        string.Format("成功导入{0}条记录",importCount), 
                        "成功",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.None
                        );
                    frmMdi.tsslInfo.Text = "导入成功";
                    frmMdi.tsslInfo.BackColor =frmMdi.BackColor;
                }
                else
                {
                    MessageBox.Show(
                        string.Format("导入了{0}条记录", importCount),
                        "失败",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error
                        );
                    frmMdi.tsslInfo.Text = string.Format("导入了{0}条记录", importCount);
                    frmMdi.tsslInfo.BackColor = Color.Yellow;
                }
            }
            //设置工具条和菜单状态
            foreach (ToolStripItem item in msMaster.Items)
            {
                item.Enabled = true;
            }
            tsmStopImport.Visible = false;
            foreach (ToolStripItem item in tslMaster.Items)
            {
                item.Enabled = true;
            }
            tsbStopImport.Visible = false;
            try
            {
                frmMdi.tspbCompletePrecent.Visible = false;
            }
            catch { }
        }

        void bgwImport_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            frmMdi.tspbCompletePrecent.Value = e.ProgressPercentage;
        }

        void bgwImport_DoWork(object sender, DoWorkEventArgs e)
        {
            string fileName = e.Argument.ToString();
            //EXCEL程序辅助对象
            ExcelHelper helper = new ExcelHelper();
            //程序不可见
            helper.Visible = false;
            //绑定事件
            helper.GetingRecord += new EventHandler<EventArgs>(helper_GetingRecord);
            try
            {
                //获取文件中的记录列表
                List<Model.HkRecord> lRecord = helper.GetHkRecord(fileName);
                if (lRecord != null && lRecord.Count > 0)
                {
                    //声明一个业务层
                    BLL.RecordBLL bll = new BLL.RecordBLL();
                    importCount = bll.AddRecords(lRecord, AddRecord);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("导入记录时错误，" + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                bgwImport.CancelAsync();
            }
            finally
            {
                helper.Close();
                helper = null;
                GC.Collect();
            }
        }
        /// <summary>
        /// 增加记录，此方法作为业务层添加列表内容的参数
        /// </summary>
        /// <returns>是否停止批量保存</returns>
        private bool AddRecord()
        {
            if (bgwImport.CancellationPending == true)
            {
                return true;
            }
            else
            {
                int value = frmMdi.tspbCompletePrecent.Value + 1;
                int max = frmMdi.tspbCompletePrecent.Maximum;
                if (value > max)
                {
                    value = value - max;
                }
                bgwImport.ReportProgress(value);
                return false;
            }
        }
        void helper_GetingRecord(object sender, EventArgs e)
        {
            ExcelHelper helper = sender as ExcelHelper;
            if (helper != null)
            {
                if (bgwImport.CancellationPending == true)
                {
                    helper.IsStop = true;
                    return;
                }
            }
            int value = frmMdi.tspbCompletePrecent.Value + 1;
            int max = frmMdi.tspbCompletePrecent.Maximum;
            if (value > max)
            {
                value = value - max;
            }
            bgwImport.ReportProgress(value);
        }
        #endregion

        private void dgvList_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            string[] heads = "ID,记录日期,期次,第一数,第二数,第三数,第四数,第五数,第六数,第七数".Split(new char[] { ',' });
            if (dgvList.Columns.Count == heads.Length)
            {
                for (int i = 0; i < dgvList.Columns.Count; i++)
                {
                    dgvList.Columns[i].HeaderText = heads[i];
                }
                //自动调整列宽
                dgvList.AutoResizeColumns();
                //设置ID列只读
                dgvList.Columns["id"].ReadOnly = true;
            }
        }

        private void dgvList_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            MessageBox.Show("数据格式错误！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void dgvList_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            object value = e.FormattedValue;
            if (value == null || value == DBNull.Value||value.ToString().Trim().Length==0) { return; }
            int index = e.ColumnIndex;
            switch (index)
            {
                case 1://日期
                    DateTime dt;
                    if (DateTime.TryParse(value.ToString(), out dt) == false)
                    {
                        MessageBox.Show("输入值必须为日期！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        e.Cancel = true;
                        return;
                    }
                    break;
                case 2://指定格式字符串
                    if (Regex.IsMatch(value.ToString(), @"^\d{7}$") == false)
                    {
                        MessageBox.Show("输入值必须四位年3位期次！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        e.Cancel = true;
                        return;
                    }
                    break;
                case 3://byte类型
                case 4:
                case 5:
                case 6:
                case 7:
                case 8:
                case 9:
                    byte num=0;
                    byte.TryParse(value.ToString(), out num);
                    if (num == 0)
                    {
                        MessageBox.Show("输入值必须为1-255之间的数字！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        e.Cancel = true;
                        return;
                    }
                    break;
            }
        }

        private void dgvList_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {
            int rowIndex = e.RowIndex;
            if (rowIndex < 0 || rowIndex > dgvList.Rows.Count) { return; }
            dgvList.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.MistyRose;
        }

        private void dgvList_CellMouseLeave(object sender, DataGridViewCellEventArgs e)
        {
            int rowIndex = e.RowIndex;
            if (rowIndex < 0 || rowIndex > dgvList.Rows.Count) { return; }
            dgvList.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.Empty;
        }
    }
}
