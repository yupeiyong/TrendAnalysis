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
    public partial class frmAnalysisResult : Form
    {
        /// <summary>
        /// MDI窗口对象引用
        /// </summary>
        private frmMain frmMdi = null;
        public frmAnalysisResult()
        {
            InitializeComponent();
        }
        #region 处理窗体事件
        private void frmAnalysisResult_Load(object sender, EventArgs e)
        {
            //获取主窗口引用
            frmMdi = this.MdiParent as frmMain;
            //刷新分析结果
            RefreshByRecord();
            txtWhatNumber.Text = "7";
            tsbSearch_Click(null, null);
        }
        private void frmAnalysisResult_Activated(object sender, EventArgs e)
        {
            this.frmMdi.tsbAnalysisResult.BackColor = Color.LimeGreen;
            this.frmMdi.tsslInfo.Text = "就绪";
        }

        private void frmAnalysisResult_Deactivate(object sender, EventArgs e)
        {
            this.frmMdi.tsbAnalysisResult.BackColor = frmMdi.BackColor;
            this.frmMdi.tsslInfo.Text = "就绪";
        }
        #endregion

        #region 处理菜单按钮事件
        private void tsbExit_Click(object sender, EventArgs e)
        {
            Application.DoEvents();
            this.Close();
            this.Dispose();
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
                objs = new object[dgvList.Rows.Count, dgvList.Columns.Count];
            }
            for (int i = 0; i < dgvList.Rows.Count; i++)
            {
                if (dgvList.Rows[i].IsNewRow) { continue; }
                for (int c = 0; c < dgvList.Columns.Count; c++)
                {
                    objs[i, c] = dgvList.Rows[i].Cells[c].Value;
                }
            }
            string[] heads = "期次,第几数,倍率,号码金额,支出,预期收益,记录号码,实际收益,余额,填写人,填写时间,修改时间".Split(new char[] { ',' });
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
                MessageBox.Show("导出错误，" + ex.Message, "失败", MessageBoxButtons.OK, MessageBoxIcon.Error);
                frmMdi.tsslInfo.Text = "导出失败!";
                frmMdi.tsslInfo.BackColor = Color.Yellow;
            }
        }

        private void tsbSearch_Click(object sender, EventArgs e)
        {
            List<Model.Condition> lCondition = new List<Model.Condition>();
            if (txtAnalysisTimes.Text.Trim().Length > 0)
            {
                lCondition.Add(new Model.Condition { paramName = "times", paramValue = txtAnalysisTimes.Text, FieldType = Model.Condition.FieldDbType.String });
            }
            if (txtMultiple.Text.Trim().Length > 0)
            {
                lCondition.Add(new Model.Condition { paramName = "multiple", paramValue = txtMultiple.Text, FieldType = Model.Condition.FieldDbType.Number });
            }
            if (txtWhatNumber.Text.Trim().Length > 0)
            {
                lCondition.Add(new Model.Condition { paramName = "what_number", paramValue = txtWhatNumber.Text, FieldType = Model.Condition.FieldDbType.Number });
            }
            if (txtNumAndMoney.Text.Trim().Length > 0)
            {
                lCondition.Add(new Model.Condition { paramName = "num_money", paramValue = txtNumAndMoney.Text, FieldType = Model.Condition.FieldDbType.String });
            }
            if(txtOutlay.Text.Trim().Length>0)
            {
                lCondition.Add(new Model.Condition { paramName = "outlay", paramValue = txtOutlay.Text, FieldType = Model.Condition.FieldDbType.Number });
            }
            if (txtExpectIncome.Text.Trim().Length > 0)
            {
                lCondition.Add(new Model.Condition { paramName = "expect_income", paramValue = txtExpectIncome.Text, FieldType = Model.Condition.FieldDbType.String });
            }
            if (txtNum.Text.Trim().Length > 0)
            {
                lCondition.Add(new Model.Condition { paramName = "num", paramValue = txtNum.Text, FieldType = Model.Condition.FieldDbType.String });
            }
            if (txtRelityIncome.Text.Trim().Length > 0)
            {
                lCondition.Add(new Model.Condition { paramName = "relity_income", paramValue = txtRelityIncome.Text, FieldType = Model.Condition.FieldDbType.String });
            }
            if (txtBalance.Text.Trim().Length > 0)
            {
                lCondition.Add(new Model.Condition { paramName = "balance", paramValue = txtBalance.Text, FieldType = Model.Condition.FieldDbType.String });
            }
            if (txtWriter.Text.Trim().Length > 0)
            {
                lCondition.Add(new Model.Condition { paramName = "writer", paramValue = txtWriter.Text, FieldType = Model.Condition.FieldDbType.String });
            }
            if (txtWriteDate.Text.Trim().Length > 0)
            {
                lCondition.Add(new Model.Condition { paramName = "write_date", paramValue = txtWriteDate.Text, FieldType = Model.Condition.FieldDbType.String });
            }
            if (txtModifyDate.Text.Trim().Length > 0)
            {
                lCondition.Add(new Model.Condition { paramName = "modify_date", paramValue = txtModifyDate.Text, FieldType = Model.Condition.FieldDbType.String });
            }
            if (lCondition.Count == 0)
            {
                MessageBox.Show("查询条件为空！", "失败", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return;
            }
            BLL.AnalysisBLL bll = new BLL.AnalysisBLL();
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
                    "查询发生错误，" + ex.Message,
                    "错误",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                    );
                frmMdi.tsslInfo.Text = "查询失败！";
                frmMdi.tsslInfo.BackColor = Color.Yellow;
            }
        }

        private void tsbStopImport_Click(object sender, EventArgs e)
        {

        }

        private void tsbImport_Click(object sender, EventArgs e)
        {

        }

        private void tsbSave_Click(object sender, EventArgs e)
        {
            int rowCount = dgvList.Rows.Count;
            if (rowCount == 0 || (dgvList.AllowUserToAddRows && rowCount == 1))
            {
                MessageBox.Show("无保存内容！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                return;
            }
            DialogResult result = MessageBox.Show("确认保存吗！", "确认", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (result == DialogResult.Cancel)
            {
                return;
            }
            //遍历行
            for (int r = 0; r < dgvList.Rows.Count; r++)
            {
                if (dgvList.Rows[r].IsNewRow) { continue; }
                //主键名称
                string keyName = "times";// primaryKey.ColumnName;
                object value = dgvList.Rows[r].Cells[keyName].Value;
                if (value == null || value == DBNull.Value || value.ToString().Trim().Length == 0)
                {
                    MessageBox.Show("单元内容不能为空！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    dgvList.ClearSelection();
                    dgvList.CurrentCell = dgvList.Rows[r].Cells[keyName];
                    dgvList.CurrentCell.Selected = true;
                    dgvList.Focus();
                    return;
                }
            }
            BLL.AnalysisBLL bll = new BLL.AnalysisBLL();
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
            //刷新分析结果
            RefreshByRecord();
            //重新查询
            tsbSearch_Click(null,null);
        }
        #endregion
        /// <summary>
        /// 刷新分析记录（写入实际号码并计算实际收益和余额）
        /// </summary>
        private void RefreshByRecord()
        {
            //分析结果操作对象
            BLL.AnalysisBLL analysisBll = new BLL.AnalysisBLL();
            //获取所有数据
            List<Model.HkRecordAnalysis> lAnalysis = analysisBll.GetData();
            //记录操作对象
            BLL.RecordBLL recordBll = new BLL.RecordBLL();
            //余额
            double balance = 0;
            if (lAnalysis.Count == 0) { return; }
            try
            {
                for (int i = 0; i < lAnalysis.Count; i++)
                {
                    Model.HkRecordAnalysis analysis = lAnalysis[i];
                    if (analysis.Outlay <= 0)
                    {
                        //余额为当前余额
                        analysis.Balance = balance;
                        continue;
                    }
                    else
                    {
                        //号码金额为空
                        if (analysis.NumMoney.Length == 0)
                        {
                            //实际收益-支出
                            analysis.RelityIncome -= analysis.Outlay;
                            //余额＝余额-支出
                            balance = balance - analysis.Outlay;
                            //写入余额
                            analysis.Balance = balance;
                        }
                        else
                        {
                            //操作第几位数字
                            byte whatNumber = analysis.WhatNumber;
                            //当前期次
                            string times = analysis.Times;
                            //按期次获取记录
                            Model.HkRecord record = recordBll.GetDataByTimes(times);
                            //在记录找不到分析的期次
                            if (record.Times == null)
                            {
                                break;
                            }
                            byte number;
                            switch (whatNumber)
                            {
                                case 1:
                                    number = record.FirstNum;
                                    break;
                                case 2:
                                    number = record.SecondNum;
                                    break;
                                case 3:
                                    number = record.ThirdNum;
                                    break;
                                case 4:
                                    number = record.FourthNum;
                                    break;
                                case 5:
                                    number = record.FifthNum;
                                    break;
                                case 6:
                                    number = record.SixthNum;
                                    break;
                                case 7:
                                    number = record.SeventhNum;
                                    break;
                                default:
                                    throw new Exception("期次不正确，必须为1-7之间！");
                            }
                            analysis.RecordNum = number;//记录号码
                            string numMoney = analysis.NumMoney.Replace(" ","").Replace("；",";").Replace("：",":");
                            string[] numMoneys = numMoney.Split(new char[] { ';' });
                            //号码与金额是否有记录号码
                            bool isHasNumber = false;
                            foreach (string item in numMoneys)
                            {
                                int index = item.IndexOf(":");
                                if (index > 0)
                                {
                                    string strNum = item.Substring(0, index);
                                    string strMoney = item.Substring(index + 1);
                                    //分析号码与实际记录一致
                                    if (strNum == number.ToString())
                                    {
                                        double money;
                                        if (double.TryParse(strMoney, out money))
                                        {
                                            double sumMoney = money * analysis.Multiple;
                                            //实际收益
                                            double relityIncome = sumMoney - analysis.Outlay;
                                            analysis.RelityIncome = relityIncome;
                                            isHasNumber = true;
                                            break;
                                        }
                                    }
                                }
                            }
                            if (isHasNumber == false)
                            {
                                //实际收益为负的支出
                                analysis.RelityIncome =- analysis.Outlay;
                            }
                            //余额＝余额+实际收益
                            balance += analysis.RelityIncome;
                            analysis.Balance = balance;
                        }
                    }
                }
                //更新列表
                analysisBll.Update(lAnalysis);
            }
            catch (Exception ex)
            {
                MessageBox.Show("刷新过程发生错误，"+ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                frmMdi.tsslInfo.Text = "刷新失败！";
                frmMdi.tsslInfo.BackColor = Color.Yellow;
            }
        }
        #region 处理DGV控件事件
        private void dgvList_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            string[] heads = "期次,第几数,倍率,号码金额,支出,预期收益,记录号码,实际收益,余额,填写人,填写时间,修改时间".Split(new char[] { ',' });
            for (int i = 0; i < dgvList.Columns.Count; i++)
            {
                //设置列头
                dgvList.Columns[i].HeaderText = heads[i];
            }
        }

        private void dgvList_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {
            int rowIndex = e.RowIndex;
            if (rowIndex < 0 || rowIndex == dgvList.NewRowIndex || rowIndex > dgvList.Rows.Count) { return; }
            dgvList.Rows[rowIndex].DefaultCellStyle.BackColor = Color.MistyRose;
        }

        private void dgvList_CellMouseLeave(object sender, DataGridViewCellEventArgs e)
        {
            int rowIndex = e.RowIndex;
            if (rowIndex < 0 || rowIndex == dgvList.NewRowIndex || rowIndex > dgvList.Rows.Count) { return; }
            dgvList.Rows[rowIndex].DefaultCellStyle.BackColor = Color.Empty;
        }

        private void dgvList_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            //当前行的实际收益
            object value = dgvList.Rows[e.RowIndex].Cells["relity_income"].Value;
            if (value == null || value == DBNull.Value) { return; }
            //实际收益
            double relityIncome;
            if (double.TryParse(value.ToString(), out relityIncome))
            {
                if (relityIncome < 0)
                {
                    //实际收益为负，设置字体为绿色
                    dgvList.Rows[e.RowIndex].DefaultCellStyle.ForeColor = Color.Green;
                }
            }
        }
        #endregion
    }
}
