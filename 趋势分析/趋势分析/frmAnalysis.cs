using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Configuration;
using System.Linq.Expressions;
using Analysis;

namespace 趋势分析
{
    public partial class frmAnalysis : Form
    {
        #region 字段
        /// <summary>
        /// 引用主窗口
        /// </summary>
        private frmMain frmMdi = null;
        /// <summary>
        /// 设置对象
        /// </summary>
        private Settings setting = null;
        /// <summary>
        /// 复选框字典，复选框代表的数字为键，复选框引用为值
        /// </summary>
        private Dictionary<string, CheckBox> dictCheckBox = new Dictionary<string, CheckBox>();
        /// <summary>
        /// 数字范围
        /// </summary>
        private readonly string numRange = "1-49";
        /// <summary>
        /// 分析记录对象
        /// </summary>
        private IAnalysisRecord analysisRecord = null;
        #endregion

        #region 方法
        #region 构造函数
        public frmAnalysis()
        {
            InitializeComponent();
        }
        #endregion

        #region 处理窗口事件
        private void frmAnalysis_Load(object sender, EventArgs e)
        {
            //取得MDI窗体的引用
            frmMdi = this.MdiParent as frmMain;
            //获取配置并设置控件
            GetConfiguration();
            //选择最后位数
            this.cboDijiwei.SelectedIndex = cboDijiwei.Items.Count - 1;
            //实例业务对象
            BLL.RecordBLL bll = new BLL.RecordBLL();
            try
            {
                //取最大记录
                Model.HkRecord record = bll.GetMaxRecord();
                //最大记录的记录日期
                DateTime maxDate = record.RecordDate;
                //开始日期
                dtpStartDate.Text = maxDate.ToString("yyyy-MM-dd");
                //结束日期
                dtpEndDate.Text = maxDate.AddYears(-2).ToString("yyyy-MM-dd");
                string times = record.Times;
                //取最大期次
                txtMaxTimes.Text = times;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            //设置文本框
            //SetTextBoxs(this, txt =>
            //{
            //    txt.MouseEnter += txt_MouseEnter;
            //    txt.MouseLeave += txt_MouseLeave;
            //});
            SetControls(this, typeof(TextBox), txt => {
                txt.MouseEnter += txt_MouseEnter;
                txt.MouseLeave += txt_MouseLeave;
            });
            //动态创建数字选择的单选钮及金额文本框,每行排列5组
            CreateSelectNumArray(5);
            //设置单选钮
            //SetRadioButton(tabControl1, rbtn =>
            //{
            //    //rbtn.CheckedChanged += rbtn_CheckedChanged;
            //    rbtn.Click += rbtn_Click;
            //});
            SetControls(tabControl1, typeof(RadioButton), rbtn =>
            {
                rbtn.Click += rbtn_Click;
            });
        }

        /// <summary>
        /// 获取配置参数并设置控件
        /// </summary>
        private void GetConfiguration()
        {
            setting = new Settings();
            //设置按钮的背景颜色
            btnOddBackColor.BackColor = setting.ODD_COLOR;
            btnEvenBackColor.BackColor = setting.EVEN_COLOR;
            btnBigBackColor.BackColor = setting.BIG_COLOR;
            btnSmallBackColor.BackColor = setting.SMALL_COLOR;
            btnContinueColor.BackColor = setting.CONTINUE_CELL_COLOR;
            btnSameColor.BackColor = setting.SAME_CELL_COLOR;
            btnAnotherSameColor.BackColor = setting.SAME_CELL_ANOTHER_COLOR; 
            //设置文本框值
            txtTimeCount.Text = setting.TimeCount.ToString();
            txtCompareTimes.Text = setting.CompareCount.ToString();
            txtWeisu.Text = setting.Weisu.ToString();
            txtTensCompBase.Text = setting.TenBigSmallCompBase.ToString();
            cboUnitsCompBase.Text = setting.UnitsBigSmallCompBase.ToString();
            txtContinueDight.Text = setting.ContinueCount.ToString();
            txtSameDight.Text = setting.SameCount.ToString();
            txtContinueRowCount.Text = setting.ContinueRowsCount.ToString();
            txtSameRowCount.Text = setting.SameRowCount.ToString();
            string analysisType = ConfigurationManager.AppSettings["analysisRecordType"];
        }
        /// <summary>
        /// 创建选择数字数组（01-49）
        /// <param name="everyRowItemCount">每行排列控件项目数，包括复选框、标签、文本框</param>
        /// </summary>
        private void CreateSelectNumArray(int everyRowItemCount)
        {
            if (Regex.IsMatch(this.numRange, @"^\d{1,2}-\d{1,2}$") == false)
            {
                MessageBox.Show("设置数字范围格式错误！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            string[] numRangeArr = this.numRange.Split(new char[] { '-' }, StringSplitOptions.RemoveEmptyEntries);
            int startNum = int.Parse(numRangeArr[0]);
            int endNum = int.Parse(numRangeArr[1]);
            if (startNum >= endNum)
            {
                MessageBox.Show("设置数字范围格式错误！开始数字必须小于结束数字！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (everyRowItemCount > endNum || everyRowItemCount < startNum)
            {
                MessageBox.Show(
                    string.Format(
                        "设置每一行排列控件数量错误，不能小于{0},大于{1}！",
                        endNum,
                        startNum
                    ),
                    "错误",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                    );
                return;
            }
            //第一组复选框标签及文本框总宽度
            int firstWidth = txtMoney01.Left - chk01.Left + txtMoney01.Width + 5;
            int firstHeight = txtMoney01.Top + 8;
            //清空字典
            dictCheckBox.Clear();
            for (int i = startNum; i <= endNum; i++)
            {
                //当前索引
                string index = i.ToString("00");
                if (i == 1)
                {
                    //绑定事件
                    chk01.CheckedChanged += chk_CheckedChanged;
                    txtMoney01.Validating += txt_Validating;
                    txtMoney01.Enabled = false;
                    //添加到字典
                    dictCheckBox.Add(index, chk01);
                    continue;
                }
                //将所有数字按每行5个顺序排列，其对应的复选框、标签、文本框也按这个位置排列
                int xBeisu = 0;//横向倍数
                int yBeisu = 0;//竖向倍数
                int mod = i % everyRowItemCount;//求5的余数,计算当前数字所在列号
                int sang = i / everyRowItemCount;//商，计算当前数字所在行号
                if (mod == 0)//如：5，15，20等等
                {
                    xBeisu = everyRowItemCount;
                    yBeisu = sang - 1;
                }
                else
                {
                    xBeisu = mod;
                    yBeisu = sang;
                }
                //动态添加的复选框
                CheckBox chk = new CheckBox();
                chk.Name = "chk" + index;
                chk.Text = index;
                //x座标
                int x = firstWidth * (xBeisu - 1) + chk01.Left;
                //Y座标
                int y = firstHeight * yBeisu + chk01.Top;
                //复选大小与第一个大小一致
                chk.Size = chk01.Size;
                chk.Location = new Point(x, y);
                //添加事件
                chk.CheckedChanged += chk_CheckedChanged;
                //添加此控件
                groupBox1.Controls.Add(chk);
                //动态添加标签
                Label lbl = new Label();
                lbl.Text = lbl01.Text;
                lbl.Name = "lbl" + index;
                //x座标
                x = firstWidth * (xBeisu - 1) + lbl01.Left;
                //y座标
                y = firstHeight * yBeisu + lbl01.Top;
                //标签大小与第一个标签大小一致
                lbl.Size = lbl01.Size;
                lbl.Location = new Point(x, y);
                //添加此标签
                groupBox1.Controls.Add(lbl);
                //动态添加文本框
                TextBox txt = new TextBox();
                txt.Name = "txtMoney" + index;
                //x座标
                x = firstWidth * (xBeisu - 1) + txtMoney01.Left;
                //y座标
                y = firstHeight * yBeisu + txtMoney01.Top;
                //文本框大小与第一个文本框大小一致
                txt.Size = txtMoney01.Size;
                txt.Location = new Point(x, y);
                txt.Validating += txt_Validating;
                txt.Enabled = false;
                //添加此标签
                groupBox1.Controls.Add(txt);
                //添加到字典
                dictCheckBox.Add(index, chk);
            }
        }

        ///// <summary>
        ///// 设置单选钮
        ///// </summary>
        ///// <param name="ct"></param>
        ///// <param name="act"></param>
        //private void SetRadioButton(Control ct, Action<RadioButton> act)
        //{
        //    foreach (Control control in ct.Controls)
        //    {
        //        if (control is RadioButton)
        //        {
        //            if (act != null)
        //            {
        //                act(control as RadioButton);
        //            }
        //        }
        //        else
        //        {
        //            SetRadioButton(control, act);
        //        }
        //    }
        //}
        private void frmAnalysis_Activated(object sender, EventArgs e)
        {
            this.frmMdi.tsbAnalysis.BackColor = Color.LimeGreen;
            this.frmMdi.tsslInfo.Text = "就绪";
        }
        
        private void frmAnalysis_Deactivate(object sender, EventArgs e)
        {
            this.frmMdi.tsbAnalysis.BackColor = frmMdi.BackColor;
            this.frmMdi.tsslInfo.Text = "就绪";
        }

        private void frmAnalysis_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (setting != null)
            {
                //保存当前设置
                setting.SaveConfig();
            }
        }

        #endregion

        #region 处理单选钮事件
        void rbtn_Click(object sender, EventArgs e)
        {
            RadioButton rbtn = sender as RadioButton;
            if (rbtn == null) { return; }
            //十位奇偶大小状态
            Analysis.State tenState = new State();
            //个位奇偶大小状态
            Analysis.State unitsState = new State();
            //分析数字结果的对象是否为空
            if (analysisRecord == null)
            {
                //按位分析（个位十位）
                if (setting.AnalysisRecordType.Length == 0 || setting.AnalysisRecordType == "")
                {
                    var curAnalysis = new AnalysisRecord(this.numRange);
                    //添加个位比较大小（左右数）的规则
                    curAnalysis.AddCompare(1, new CompareIsInRight(0, setting.UnitsBigSmallCompBase, 9));
                    //添加十位比较大小（左右数）的规则
                    curAnalysis.AddCompare(2, new CompareIsInRight(0, setting.TenBigSmallCompBase, 4));
                    analysisRecord = curAnalysis;
                }
                else//最高位按数字分析
                {
                    var curAnalysis = new AnalysisRecordNum(this.numRange);
                    //添加个位比较大小（左右数）的规则
                    curAnalysis.AddCompare(1, new CompareIsInRight(0, 4, 9));
                    //添加十位比较大小（左右数）的规则
                    curAnalysis.AddHighDigitCompare(new CompareIsInRight(1, setting.TenBigSmallCompBase, 49));
                    analysisRecord = curAnalysis;
                }
            }

            //选择十位奇偶
            if (rbtnTenOdd.Checked)
            {
                tenState.IsOdd = true;
            }
            else if (rbtnTenEven.Checked)
            {
                tenState.IsOdd = false;
            }
            //选择十位大小
            if (rbtnTenBig.Checked)
            {
                tenState.IsInRight = true;
            }else if(rbtnTenSmall.Checked)
            {
                tenState.IsInRight = false;
            }

            //选择个位奇偶
            if (rbtnUnitsOdd.Checked)
            {
                unitsState.IsOdd = true;
            }
            else if (rbtnUnitsEven.Checked)
            {
                unitsState.IsOdd = false;
            }
            if (rbtnUnitsBig.Checked)
            {
                unitsState.IsInRight = true;
            }
            else if (rbtnUnitsSmall.Checked)
            {
                unitsState.IsInRight = false;
            }
            
            //取得字典所有数字（键）
            ICollection<string> nums = dictCheckBox.Keys;
            //数字奇偶大小状态字典
            Dictionary<byte, State> dict = new Dictionary<byte, State>();
            dict.Add(1, unitsState);//添加个位状态
            dict.Add(2, tenState);//添加十位状态
            //获取分析结果
            List<string> lNum = analysisRecord.GetResultString(dict);
            foreach (string num in dictCheckBox.Keys)
            {
                if (lNum.Contains(num))
                {
                    //选择
                    dictCheckBox[num].Checked = true;
                }
                else
                {
                    //不选择
                    dictCheckBox[num].Checked = false;
                }
            }
            //执行文本框内容更改后的重算
            TxtMoneyTextChanged();
        }

        private void rbDate_CheckedChanged(object sender, EventArgs e)
        {
            if (rbDate.Checked)
            {
                dtpStartDate.Enabled = true;
                dtpEndDate.Enabled = true;
                txtMaxTimes.Enabled = false;
                txtTimeCount.Enabled = false;
            }
        }

        private void rbTimes_CheckedChanged(object sender, EventArgs e)
        {
            if (rbTimes.Checked)
            {
                dtpStartDate.Enabled = false;
                dtpEndDate.Enabled = false;
                txtMaxTimes.Enabled = true;
                txtTimeCount.Enabled = true;
            }
        }

        #endregion

        #region 处理复选框事件
        /// <summary>
        /// groupBox1中的复选框选中事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// </summary>
        void chk_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox chk = sender as CheckBox;
            if (chk != null)
            {
                string name = chk.Name;
                name = name.Replace("chk", "");
                if (chk.Checked == true)
                {
                    //选中时字体颜色
                    Color selectedColor = Color.Red;
                    chk.ForeColor = selectedColor;
                    groupBox1.Controls["txtMoney" + name].Enabled = true;
                    groupBox1.Controls["txtMoney" + name].ForeColor = selectedColor;
                    groupBox1.Controls["lbl" + name].ForeColor = selectedColor;
                }
                else
                {
                    //取消选择时的字体颜色
                    Color previousColor = chk.Parent.ForeColor;
                    chk.ForeColor = previousColor;
                    //groupBox1.Controls["txtMoney" + name].ForeColor = previousColor;
                    TextBox txt = (TextBox)groupBox1.Controls["txtMoney" + name];
                    txt.Enabled = false;
                    txt.Text = string.Empty;
                    txt.ForeColor = previousColor;
                    groupBox1.Controls["lbl" + name].ForeColor = previousColor;
                }
                //更改金额与数字
                TxtMoneyTextChanged();
            }
        }
        #endregion

        #region 处理工具条按钮事件
        private void tsbAnalysis_Click(object sender, EventArgs e)
        {
            this.Validate();
            //先清空选择
            tsbClear_Click(null, null);
            //分析数字结果的对象是否为空
            if (analysisRecord == null)
            {
                //按位分析（个位十位）
                if (setting.AnalysisRecordType.Length == 0 || setting.AnalysisRecordType == "AnalysisRecord")
                {
                    var curAnalysis = new AnalysisRecord(this.numRange);
                    //添加个位比较大小（左右数）的规则
                    curAnalysis.AddCompare(1, new CompareIsInRight(0, setting.UnitsBigSmallCompBase, 9));
                    //添加十位比较大小（左右数）的规则
                    curAnalysis.AddCompare(2, new CompareIsInRight(0, setting.TenBigSmallCompBase, 4));
                    analysisRecord = curAnalysis;
                }
                else//最高位按数字分析
                {
                    var curAnalysis = new AnalysisRecordNum(this.numRange);
                    //添加个位比较大小（左右数）的规则
                    curAnalysis.AddCompare(1, new CompareIsInRight(0, 4, 9));
                    //添加十位比较大小（左右数）的规则
                    curAnalysis.AddHighDigitCompare(new CompareIsInRight(1, setting.TenBigSmallCompBase, 49));
                    analysisRecord = curAnalysis;
                }
            }
            try
            {
                List<Model.HkRecord> lRecord = new List<Model.HkRecord>();
                BLL.RecordBLL bll = new BLL.RecordBLL();
                if (rbDate.Checked)
                {
                    DateTime start;
                    if (DateTime.TryParse(dtpStartDate.Text, out start) == false)
                    {
                        MessageBox.Show(
                            dtpStartDate.Text + ",不是有效的开始日期！",
                            "错误",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error
                            );
                        return;
                    }
                    DateTime end;
                    if (DateTime.TryParse(dtpEndDate.Text, out end) == false)
                    {
                        MessageBox.Show(
                            dtpEndDate.Text + ",不是有效的结束日期！",
                            "错误",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error
                            );
                        return;
                    }
                    if (start <= end)
                    {
                        MessageBox.Show(
                            "最大日期不能小于最后日期！",
                            "错误",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error
                            );
                        return;
                    }
                    lRecord = bll.GetDataByRecordDate(start, end);
                }
                else
                {
                    string times = txtMaxTimes.Text;
                    if (Regex.IsMatch(times, @"^\d{7}$") == false)
                    {
                        MessageBox.Show(
                            "最大期次格式比较为4位年3位期次！",
                            "错误",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error
                            );
                        return;
                    }
                    lRecord = bll.GetDataByLessThanTimes(times, (int)setting.TimeCount);
                }
                dgvList.Columns.Clear();
                dgvList.DataSource = lRecord;
                //保留列名
                string baoliuColName;
                //设置绑定之后的DGV状态
                dgvListDataBindingComplete(out baoliuColName);
                //遍历所有行
                for (int i = 0; i < dgvList.Rows.Count; i++)
                {
                    //取指定列的值
                    string num = dgvList.Rows[i].Cells[baoliuColName].Value.ToString();
                    byte b = byte.Parse(num);
                    num = b.ToString("00");
                    //十位是否为奇数
                    bool tenIsOdd = analysisRecord.IsOdd(num, 2);
                    //设置十位奇偶列的符号，如为奇数设置为奇数符号，否则设置为偶数符号
                    dgvList.Rows[i].Cells["dgvcolTenOdd"].Value = tenIsOdd ? setting.OddMark : setting.EvenMark;
                    //十位的数是否为大数(右边数)范围
                    bool tenIsBig = analysisRecord.IsInRight(num,2);
                    //设置十位大小列的符号，如为大数设置为大数符号，否则设置为小数符号
                    dgvList.Rows[i].Cells["dgvcolTenDaxiao"].Value = tenIsBig ? setting.BigMark : setting.SmallMark;
                    //个位是否为奇数
                    bool unitsIsOdd = analysisRecord.IsOdd(num, 1);
                    dgvList.Rows[i].Cells["dgvcolGoOdd"].Value = unitsIsOdd ? setting.OddMark : setting.EvenMark;
                    //个位的数是否为大数范围
                    bool unitsIsBig =analysisRecord.IsInRight(num,1);
                    dgvList.Rows[i].Cells["dgvcolGoDaxiao"].Value = unitsIsBig ? setting.BigMark : setting.SmallMark;
                }
                //各分析列表
                List<string> lTenOdd = new List<string>();
                lTenOdd.Add("0");//待分析位为0
                List<string> lTenDaxiao = new List<string>();
                lTenDaxiao.Add("0");//待分析位为0
                List<string> lGoweiOdd = new List<string>();
                lGoweiOdd.Add("0");//待分析位为0
                List<string> lGoweiDaxiao = new List<string>();
                lGoweiDaxiao.Add("0");//待分析位为0
                for (int i = dgvList.Rows.Count - 1; i >= 0; i--)
                {
                    lTenOdd.Add(dgvList.Rows[i].Cells["dgvcolTenOdd"].Value.ToString());
                    lTenDaxiao.Add(dgvList.Rows[i].Cells["dgvcolTenDaxiao"].Value.ToString());
                    lGoweiOdd.Add(dgvList.Rows[i].Cells["dgvcolGoOdd"].Value.ToString());
                    lGoweiDaxiao.Add(dgvList.Rows[i].Cells["dgvcolGoDaxiao"].Value.ToString());
                }
                //写入十位奇偶数据
                WriteDataToDgv(lTenOdd, dgvTenOddEven);
                //写入十位大小数据
                WriteDataToDgv(lTenDaxiao, dgvTenBigSmall);
                //写入个位奇偶数据
                WriteDataToDgv(lGoweiOdd, dgvUnitsOddEven);
                //写入个痊大小数据
                WriteDataToDgv(lGoweiDaxiao, dgvUnitsBigSmall);
                //下一期次
                string strTimes = dgvList.Rows[dgvList.Rows.Count - 1].Cells["Times"].Value.ToString();
                string curTimes=strTimes.Substring(4);
                byte nextTimes = byte.Parse(curTimes);
                nextTimes += 1;
                txtAnalysisTimes.Text = strTimes.Substring(0, 4) + nextTimes.ToString("000");
                //当前分析的第几位数
                cboWhatNumber.Text = cboDijiwei.Text;
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "分析时发生了错误，" + ex.Message,
                    "错误",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                    );
            }
        }

        private void tsbExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void tsbSave_Click(object sender, EventArgs e)
        {
            Model.HkRecordAnalysis analysis = new Model.HkRecordAnalysis();
            try
            {
                this.Validate();
                if (txtAnalysisTimes.Text.Trim().Length == 0)
                {
                    MessageBox.Show("期次不能为空！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtAnalysisTimes.Focus();
                    return;
                }
                analysis.Times = txtAnalysisTimes.Text;//期次
                int multiple;
                if(int.TryParse(txtMultiple.Text,out multiple))
                {
                    analysis.Multiple = multiple;//倍率
                }
                byte whatNumber;
                if (byte.TryParse(cboWhatNumber.Text, out whatNumber))
                {
                    analysis.WhatNumber = whatNumber;
                }
                analysis.NumMoney = txtNumAndMoney.Text;
                double outlay;
                if (double.TryParse(txtOutlay.Text, out outlay))
                {
                    analysis.Outlay = outlay;
                }
                analysis.ExpectIncome = txtExpectIncome.Text;
                BLL.AnalysisBLL bll = new BLL.AnalysisBLL();
                //执行新增
                bll.Add(analysis);
                this.frmMdi.tsslInfo.Text = "保存分析结果成功";
                this.frmMdi.tsslInfo.BackColor = frmMdi.BackColor;
            }
            catch (Exception ex)
            {
                MessageBox.Show("保存过程中发生错误，"+ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.frmMdi.tsslInfo.Text = "保存失败";
                this.frmMdi.tsslInfo.BackColor = Color.Yellow;
            }
        }

        private void tsbClear_Click(object sender, EventArgs e)
        {
            //清空页面控件数据
            ClearControl(tabPage5);
            //设置单选钮为不选中状态
            SetControls(tabControl1, typeof(RadioButton), rbtn =>
            {
                RadioButton curRbtn = rbtn as RadioButton;
                if (curRbtn != null)
                {
                    curRbtn.Checked = false;
                }
            });
        }

        private void ClearControl(Control ct)
        {
            foreach (Control control in ct.Controls)
            {
                if (control is TextBox)//文本框
                {
                    control.Text = string.Empty;
                }
                else if (control is CheckBox)//复选框
                {
                    ((CheckBox)control).Checked = false;
                }
                else if (control is ComboBox)//组合框
                {
                    ((ComboBox)control).Text = string.Empty;
                }
                else
                {
                    if (control.Controls.Count > 0)
                    {
                        //递归遍历
                        ClearControl(control);
                    }
                }
            }
            txtMultiple.Text = "40";
        }

        private void tsbAnalysisResult_Click(object sender, EventArgs e)
        {
            frmMdi.OpenForm<frmAnalysisResult>();//打开分析结果窗口
        }

        #endregion

        #region 处理按钮事件
        private void btnBackColor_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            if (btn == null) { return; }
            using (var cd = new ColorDialog())
            {
                if (cd.ShowDialog() == DialogResult.OK)
                {
                    Color c = cd.Color;
                    //设置背景颜色
                    btn.BackColor = c;
                    if (btn.Name == btnOddBackColor.Name)
                    {
                        //设置奇数颜色
                        setting.ODD_COLOR = c;
                    }
                    else if (btn.Name == btnEvenBackColor.Name)
                    {
                        //设置偶数颜色
                        setting.EVEN_COLOR = c;
                    }
                    else if (btn.Name == btnBigBackColor.Name)
                    {
                        //设置大数颜色
                        setting.BIG_COLOR = c;
                    }
                    else if (btn.Name == btnSmallBackColor.Name)
                    {
                        //设置小数颜色
                        setting.SMALL_COLOR = c;
                    }
                    else if (btn.Name == btnContinueColor.Name)
                    {
                        //设置连续单元格颜色
                        setting.CONTINUE_CELL_COLOR = c;
                    }
                    else if (btn.Name == btnSameColor.Name)
                    {
                        //设置一致单元格颜色
                        setting.SAME_CELL_COLOR = c;
                    }
                    else if (btn.Name == btnAnotherSameColor.Name)
                    {
                        //设置一致单元格的另一种颜色
                        setting.SAME_CELL_ANOTHER_COLOR = c;
                    }
                }
            }
        }
        #endregion

        #region 处理右键菜单事件
        private void tsmiRemarkContinueCell_Click(object sender, EventArgs e)
        {           
            //先设置回正常显示颜色
            tsmiNormalDisplay_Click(sender, null);
            DataGridView dgv;
            if (TryGetCurrentDgv(sender, out dgv))
            {
                this.Validate();
                dgv.EndEdit();
                //先将DGV控件分组
                Dictionary<int, int> dict = GetArrayOfDgv(dgv);
                //遍历所有分组
                foreach (int startRow in dict.Keys)
                {
                    //结束行
                    int endRow = dict[startRow];
                    //结果字典，键为行号，值为连续单元格数量
                    Dictionary<int, int> resultDict = new Dictionary<int, int>();
                    for (int i = startRow; i <= endRow; i++)
                    {
                        object firstValue = dgv.Rows[i].Cells[1].Value;
                        if (firstValue == null) { continue; }
                        string preValue = firstValue.ToString();
                        //连续数量
                        int continueCount = 1;
                        for (int c = 2; c < dgv.Columns.Count; c++)
                        {
                            if (c == 2 && preValue == "?")
                            {
                                preValue = dgv.Rows[i].Cells[c].Value.ToString();
                            }
                            //当前单元格内容
                            object value = dgv.Rows[i].Cells[c].Value;
                            if (value == null)
                            {
                                break;
                            }
                            //等于前一单元格内容
                            if (value.ToString() == preValue)
                            {
                                //连续数量累加
                                continueCount++;
                            }
                            else
                            {
                                //退出循环
                                break;
                            }
                        }
                        if (continueCount >= setting.ContinueCount)//一致单元格数量需大于设定的连续数量
                        {
                            //添加结果到字典，行号、连续数量键值对
                            resultDict.Add(i,continueCount);
                        }
                    }
                    //此组连续行数大于等于设定的行数
                    if (resultDict.Count >= setting.ContinueRowsCount)
                    {
                        foreach (int row in resultDict.Keys)
                        {
                            int continueCount = resultDict[row];
                            //设置连续单元格的背景颜色
                            for (int c = 1; c < continueCount + 1; c++)
                            {
                                dgv.Rows[row].Cells[c].Style.BackColor = setting.CONTINUE_CELL_COLOR;
                            }
                            //设置当前行标签内容
                            dgv.Rows[row].Tag = "mark";
                        }
                    }
                }
            }
        }

        private void tsmiRemarkSameCell_Click(object sender, EventArgs e)
        {
            //先设置回正常显示颜色
            tsmiNormalDisplay_Click(sender, null);
            DataGridView dgv;
            if (TryGetCurrentDgv(sender, out dgv))
            {
                this.Validate();
                dgv.EndEdit();
                //获取分组后的DGV行号
                Dictionary<int, int> dict = GetArrayOfDgv(dgv);
                foreach (int startRow in dict.Keys)//键为开始行号
                {
                    //当前范围的结束行号
                    int endRow = dict[startRow];
                    //与末行一致的单元格行，键为行号，值为一致的单元格数
                    var sameRowDict = new Dictionary<int, byte>();
                    //与末行一致的另一单元格行，键为行号，值为一致的单元格数
                    var anotherSameRowDict = new Dictionary<int, byte>();
                    //比较的第一个单元格内容，他是首次获取的单元格内容
                    string compareFirst = null; 
                    //遍历当前范围（不包括末行，因为以末尾作为比较参照）
                    for (int i = startRow; i < endRow; i++)
                    {
                        //是否为另一种一致单元格行
                        bool isAnother = false;
                        object value = dgv.Rows[i].Cells[1].Value;
                        if (value == null) { continue; }
                        if (compareFirst == null)
                        {
                            compareFirst = value.ToString();
                            isAnother = false;
                        }
                        else
                        {
                            string curFirstValue = value.ToString();
                            if (curFirstValue == compareFirst)
                            {
                                isAnother = false;
                            }
                            else
                            {
                                isAnother = true;
                            }
                        }
                        
                        byte sameCount = 0;
                        for (int c = 2; c < dgv.Columns.Count; c++)
                        {
                            if (dgv.Rows[i].Cells[c].Value.Equals(dgv.Rows[endRow].Cells[c].Value))
                            {
                                //一致，数量累加
                                sameCount++;
                            }
                            else
                            {
                                //不一致退出循环
                                break;
                            }
                        }
                        if (sameCount >= setting.SameCount)
                        {
                            if (isAnother)
                            {
                                anotherSameRowDict.Add(i, sameCount);
                            }
                            else
                            {
                                sameRowDict.Add(i, sameCount);
                            }
                        }
                    }
                    if (sameRowDict.Count + anotherSameRowDict.Count >= setting.SameRowCount)
                    {
                        //最少一致单元格，设置末尾行的单元格颜色
                        byte minSameCount = 255;
                        foreach (int rowIndex in sameRowDict.Keys)
                        {
                            byte count = sameRowDict[rowIndex];
                            if (count < minSameCount)
                            {
                                minSameCount=count;
                            }
                            //设置单元格背景颜色
                            for (int c = 1; c <= count; c++)
                            {
                                dgv.Rows[rowIndex].Cells[c].Style.BackColor=setting.SAME_CELL_COLOR;
                            }
                            dgv.Rows[rowIndex].Tag = "mark";
                        }
                        //另一种一致单元格，第一列与前一种的第一列为不同的单元格，如前一种为大数，另一种就为小数
                        foreach (int rowIndex in anotherSameRowDict.Keys)
                        {
                            byte count = anotherSameRowDict[rowIndex];
                            if (count < minSameCount)
                            {
                                minSameCount = count;
                            }
                            //设置单元格背景颜色
                            for (int c = 1; c <= count; c++)
                            {
                                dgv.Rows[rowIndex].Cells[c].Style.BackColor = setting.SAME_CELL_ANOTHER_COLOR;
                            }
                            dgv.Rows[rowIndex].Tag = "mark";
                        }
                        //设置末尾行的单元格颜色
                        for (int c = 1; c <= minSameCount; c++)
                        {
                            dgv.Rows[endRow].Cells[c].Style.BackColor = setting.SAME_CELL_COLOR;
                        }
                        dgv.Rows[endRow].Tag = "mark";
                    }
                }
                //int startRowIndex = 0;
                //int endRowIndex = 0;
                //bool isHasStartRow = false;
                //for (int i = 0; i < dgv.Rows.Count; i++)
                //{
                //    if (dgv.Rows[i].IsNewRow) { continue; }
                //    //内容不为且当前没有开始行
                //    if (isHasStartRow == false && dgv.Rows[i].Cells[1].Value != null && dgv.Rows[i].Cells[1].Value.ToString().Trim().Length > 0)
                //    {
                //        isHasStartRow = true;
                //        startRowIndex = i;
                //    }
                //    //内容为空确认为结束行
                //    if (dgv.Rows[i].Cells[1].Value == null || dgv.Rows[i].Cells[1].Value.ToString().Trim().Length == 0)
                //    {
                //        if (isHasStartRow)
                //        {
                //            endRowIndex = i - 1;
                //            //重新设置为false,开始处理新的一级数据
                //            isHasStartRow = false;
                //            SetDgvBackColor(startRowIndex, endRowIndex, dgv);
                //        }
                //    }
                //}
            }
        }

        private void tsmiNormalDisplay_Click(object sender, EventArgs e)
        {
            DataGridView dgv;
            if (TryGetCurrentDgv(sender, out dgv))
            {
                for (int i = 0; i < dgv.Rows.Count; i++)
                {
                    if (dgv.Rows[i].IsNewRow) { continue; }
                    //取消隐藏
                    dgv.Rows[i].Visible = true;
                    //标签置为空
                    dgv.Rows[i].Tag = null;
                    for (int c = 0; c < dgv.Columns.Count; c++)
                    {
                        //颜色设置为空
                        dgv.Rows[i].Cells[c].Style.BackColor = Color.Empty;
                    }
                }
            }
        }

        private void tsmiOnlyDisplayMark_Click(object sender, EventArgs e)
        {
            DataGridView dgv;
            if (TryGetCurrentDgv(sender, out dgv))
            {
                //取得分组的DGV控件
                Dictionary<int, int> dict = GetArrayOfDgv(dgv);
                foreach (int startRow in dict.Keys)
                {
                    //结束行号
                    int endRow = dict[startRow];
                    //是否设置了备注内容
                    bool isTagMark = false;
                    //最后一行必须设置了备注标签
                    if (dgv.Rows[endRow].Tag != null && dgv.Rows[endRow].Tag.ToString() == "mark")
                    {
                        for (int i = startRow; i <= endRow; i++)
                        {
                            if (dgv.Rows[i].Tag != null && dgv.Rows[i].Tag.ToString() == "mark")
                            {
                                isTagMark = true;
                                break;
                            }
                        }
                    }
                    if(isTagMark==false)
                    {
                        for (int i = startRow; i <= endRow; i++)
                        {
                           //设置行隐藏
                            dgv.Rows[i].Visible = false;
                        }
                    }
                }
            }
        }


        private void tsmiRemarkFilterSameCells_Click(object sender, EventArgs e)
        {
            tsmiRemarkSameCell_Click(sender, e);
            tsmiOnlyDisplayMark_Click(sender, e);
        }

        private void tsmiRemarkFilterContinueCells_Click(object sender, EventArgs e)
        {
            tsmiRemarkContinueCell_Click(sender, e);
            tsmiOnlyDisplayMark_Click(sender, e);
        }

        /// <summary>
        /// 将DGV控件按行分级
        /// </summary>
        /// <param name="dgv">待分组的DGV控件</param>
        /// <returns>返回分组字典，键为首行，值为末行</returns>
        private Dictionary<int, int> GetArrayOfDgv(DataGridView dgv)
        {
            Dictionary<int, int> dict = new Dictionary<int, int>();
            //第一个字典项的开始行
            int startRow = 0;
            //每个字典项的结束行
            int endRow = -1;
            for (int i = 0; i < dgv.Rows.Count; i++)
            {
                //当前行第一列的值
                object value = dgv.Rows[i].Cells[0].Value;
                //内容为空，空行，且有开始行或当前行为新增行且有开始行
                if ((dgv.Rows[i].IsNewRow && startRow >=0)||(value == null && startRow >= 0))
                {
                    //设置为结束行,上一非空内容行
                    endRow = i-1;
                    //添加一个组范围到字典
                    dict.Add(startRow, endRow);
                    //设置下一个字典项的开始项为当前的空行
                    startRow =i;
                    //重置结束行
                    endRow = -1;
                }
                else if (i == dgv.Rows.Count - 1 && startRow >= 0)//结束行
                {
                    //设置为结束行,上一非空内容行
                    endRow = i ;
                    //添加一个组范围到字典
                    dict.Add(startRow, endRow);
                }
            }
            return dict;
        }
        #endregion

        #region 处理DGV控件事件
        private bool TryGetCurrentDgv(object sender, out DataGridView dgv)
        {
            dgv = null;
            ToolStripItem tsi = sender as ToolStripItem;
            if (tsi == null) { return false; }
            ContextMenuStrip cms = (ContextMenuStrip)tsi.GetCurrentParent();
            dgv = cms.SourceControl as DataGridView;
            if (dgv == null)
            {
                return false;
            }
            else
            {
                if (dgv.Rows.Count == 0)
                {
                    return false;
                }
                return true;
            }
        }

        //private void SetDgvBackColor(int startRowIndex, int endRowIndex, DataGridView dgv)
        //{
        //    //DGV座标列表字典，键为第一列字符，值为二维DGV座标
        //    Dictionary<string, List<List<DataGridViewPoint>>> dictPoint = new Dictionary<string, List<List<DataGridViewPoint>>>();
        //    for (int i = startRowIndex; i <= endRowIndex - 1; i++)
        //    {
        //        List<DataGridViewPoint> lPoint = new List<DataGridViewPoint>();
        //        //添加第二列的座标
        //        lPoint.Add(new DataGridViewPoint { X = 1, Y = i });
        //        //从第三列开始
        //        for (int c = 2; c < dgv.Columns.Count; c++)
        //        {
        //            //当前行当前列的是否等于最后一行当前列的值
        //            if (dgv.Rows[i].Cells[c].Value.Equals(dgv.Rows[endRowIndex].Cells[c].Value))
        //            {
        //                //将座标添加到列表
        //                lPoint.Add(new DataGridViewPoint { X = c, Y = i });
        //            }
        //            else
        //            {
        //                break;
        //            }
        //        }
        //        //与最后一行一致（完全相同）的单元格数大于等于设定数
        //        if (lPoint.Count >= setting.SameCount)
        //        {
        //            //第二列单元格值
        //            object value = dgv.Rows[i].Cells[1].Value;
        //            string firstCell = string.Empty;
        //            if (value != null && value.ToString().Trim().Length > 0)
        //            {
        //                firstCell = value.ToString().Trim();
        //            }
        //            else
        //            {
        //                MessageBox.Show("第二列单元格内容为空！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //                return;
        //            }
        //            //字典是否包含此字符的键
        //            if (dictPoint.ContainsKey(firstCell))
        //            {
        //                dictPoint[firstCell].Add(lPoint);
        //            }
        //            else
        //            {
        //                //初始化二维座标
        //                List<List<DataGridViewPoint>> llPoint = new List<List<DataGridViewPoint>>();
        //                llPoint.Add(lPoint);
        //                //添加座标到字典
        //                dictPoint.Add(firstCell, llPoint);
        //            }
        //        }
        //    }
        //    if (dictPoint.Count == 0) { return; }
        //    //最大数量的键字符串
        //    string maxCountKey = string.Empty;
        //    //最大数量
        //    int maxCount = 0;
        //    foreach (string firstCell in dictPoint.Keys)
        //    {
        //        List<List<DataGridViewPoint>> llPoint = dictPoint[firstCell];
        //        //是否大于最大数量
        //        if (llPoint.Count > maxCount)
        //        {
        //            maxCount = llPoint.Count;
        //            maxCountKey = firstCell;
        //        }
        //    }
        //    List<List<DataGridViewPoint>> llMaxPoint = dictPoint[maxCountKey];
        //    //如果小于一致行数不设置背景颜色
        //    if (llMaxPoint.Count < setting.SameRowCount - 1) { return; }
        //    //设置最后一行标注颜色的最小数量
        //    int minCount = setting.Weisu;
        //    for (int i = 0; i < llMaxPoint.Count; i++)
        //    {
        //        List<DataGridViewPoint> lPoint = llMaxPoint[i];
        //        if (lPoint.Count < minCount)
        //        {
        //            minCount = lPoint.Count;
        //        }
        //        //是否记录标签到DGV当前行
        //        bool isWriteTag = false;
        //        for (int c = 0; c < lPoint.Count; c++)
        //        {
        //            dgv.Rows[lPoint[c].Y].Cells[lPoint[c].X].Style.BackColor = setting.SAME_CELL_COLOR;
        //            if (isWriteTag == false)
        //            {
        //                dgv.Rows[i].Tag = "mark";
        //                isWriteTag = true;
        //            }

        //        }
        //    }
        //    //设置最后一行的背景色
        //    for (int i = 2; i <= minCount; i++)
        //    {
        //        dgv.Rows[endRowIndex].Cells[i].Style.BackColor = setting.SAME_CELL_COLOR;
        //    }
        //}

        private void dgvListDataBindingComplete(out string baoliuColName)
        {
            //第几数
            byte dijisu = byte.Parse(cboDijiwei.Text);
            baoliuColName = string.Empty;
            switch (dijisu)
            {
                case 1:
                    baoliuColName = "FirstNum";
                    break;
                case 2:
                    baoliuColName = "SecondNum";
                    break;
                case 3:
                    baoliuColName = "ThirdNum";
                    break;
                case 4:
                    baoliuColName = "FourthNum";
                    break;
                case 5:
                    baoliuColName = "FifthNum";
                    break;
                case 6:
                    baoliuColName = "SixthNum";
                    break;
                case 7:
                    baoliuColName = "SeventhNum";
                    break;
            }
            List<string> lColName = new List<string>();
            lColName.Add("FirstNum");
            lColName.Add("SecondNum");
            lColName.Add("ThirdNum");
            lColName.Add("FourthNum");
            lColName.Add("FifthNum");
            lColName.Add("SixthNum");
            lColName.Add("SeventhNum");
            //删除保留列名
            lColName.Remove(baoliuColName);
            //删除不保留的列
            foreach (string colName in lColName)
            {
                dgvList.Columns.Remove(colName);
            }
            dgvList.Columns[baoliuColName].DefaultCellStyle.Format = "00";
            for (int i = 0; i < dgvList.Columns.Count; i++)
            {
                string colName = dgvList.Columns[i].Name;
                switch (colName)
                {
                    case "RecordDate":
                        dgvList.Columns[i].HeaderText = "记录日期";
                        break;
                    case "Times":
                        dgvList.Columns[i].HeaderText = "期次";
                        break;
                    case "FirstNum":
                        dgvList.Columns[i].HeaderText = "第一数";
                        break;
                    case "SecondNum":
                        dgvList.Columns[i].HeaderText = "第二数";
                        break;
                    case "ThirdNum":
                        dgvList.Columns[i].HeaderText = "第三数";
                        break;
                    case "FourthNum":
                        dgvList.Columns[i].HeaderText = "第四数";
                        break;
                    case "FifthNum":
                        dgvList.Columns[i].HeaderText = "第五数";
                        break;
                    case "SixthNum":
                        dgvList.Columns[i].HeaderText = "第六数";
                        break;
                    case "SeventhNum":
                        dgvList.Columns[i].HeaderText = "第七数";
                        break;
                }
            }
            dgvList.Columns.Add("dgvcolTenOdd", "十位奇偶");
            dgvList.Columns.Add("dgvcolTenDaxiao", "十位大小");
            dgvList.Columns.Add("dgvcolGoOdd", "个位奇偶");
            dgvList.Columns.Add("dgvcolGoDaxiao", "个位大小");
            dgvList.AutoResizeColumns();
            for (int i = 3; i < dgvList.Columns.Count; i++)
            {
                //设置每列宽度
                dgvList.Columns[i].Width = 22;
            }
        }

        private void dgvList_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView dgv = sender as DataGridView;
            if (dgv == null) { return; }
            int rowIndex = e.RowIndex;
            if (rowIndex < 0 || rowIndex > dgv.Rows.Count) { return; }
            dgv.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.MistyRose;
        }

        private void dgvList_CellMouseLeave(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView dgv = sender as DataGridView;
            if (dgv == null) { return; }
            int rowIndex = e.RowIndex;
            if (rowIndex < 0 || rowIndex > dgv.Rows.Count) { return; }
            dgv.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.Empty;
        }
        /// 写入数据到DGV控件（十位、个位四个DGV控件）
        /// </summary>
        /// <param name="lArr">二维数组列表</param>
        /// <param name="dgv">待写入数据的控件</param>
        private void WriteDataToDgv(List<string> lArr, DataGridView dgv)
        {
            dgv.Columns.Clear();
            if (lArr == null || lArr.Count == 0) { return; }
            //添加序号列
            dgv.Columns.Add("dgvcolIndex", "序号");
            //按维数添加列
            for (int i = 1; i <=setting.Weisu; i++)
            {
                dgv.Columns.Add("dgvcol" + i.ToString(), i.ToString());
            }
            //实例写入数组到DGV控件对象
            WriteAnalysisToDgv<string> writeToDgv = new WriteAnalysisToDgv<string>(lArr, setting.Weisu, setting.CompareCount);
            //将数组写入DGV控件
            writeToDgv.WriteDataToDgv(dgv);
            //第一列宽度
            dgv.Columns[0].Width = 60;
            //字体颜色
            dgv.Columns[0].DefaultCellStyle.ForeColor = Color.Blue;
            //设置字体
            dgv.Columns[0].DefaultCellStyle.Font = new Font("宋体", 10);
            for (int i = 1; i < dgv.Columns.Count; i++)
            {
                //设置每一列宽度
                dgv.Columns[i].Width = 35;
            }
        }

        private void dgvTenOdd_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            object value = e.FormattedValue;
            if (value == null || value.ToString().Length == 0) { return; }
            //字符串是否为奇数标志
            if (value.ToString() == setting.OddMark)
            {
                //设置字体颜色为奇数颜色
                e.CellStyle.ForeColor = setting.ODD_COLOR;
            }
            else if (value.ToString() == setting.EvenMark)//字符串是否为偶数标志
            {
                //设置字体颜色为偶数颜色
                e.CellStyle.ForeColor = setting.EVEN_COLOR;
            }
        }

        private void dgvGoweiDaxiao_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            object value = e.FormattedValue;
            if (value == null || value.ToString().Length == 0) { return; }
            //字符串是否为大数标志
            if (value.ToString() == setting.BigMark)
            {
                //设置字体颜色为大数颜色
                e.CellStyle.ForeColor = setting.BIG_COLOR;
            }
            else if (value.ToString() == setting.SmallMark)//字符串是否为小数标志
            {
                //设置字体颜色为小数颜色
                e.CellStyle.ForeColor = setting.SMALL_COLOR;
            }
        }

        private void dgvList_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            object value = e.FormattedValue;
            if (value == null || value.ToString().Length == 0) { return; }
            switch (e.ColumnIndex)
            {
                case 4://十位奇偶列号
                    if (value.ToString() == setting.OddMark)
                    {
                        e.CellStyle.ForeColor = setting.ODD_COLOR;
                    }
                    else if (value.ToString() == setting.EvenMark)
                    {
                        e.CellStyle.ForeColor = setting.EVEN_COLOR;
                    }
                    break;
                case 5://十位大小列号
                    if (value.ToString() == setting.BigMark)
                    {
                        e.CellStyle.ForeColor = setting.BIG_COLOR;
                    }
                    else if (value.ToString() == setting.SmallMark)
                    {
                        e.CellStyle.ForeColor = setting.SMALL_COLOR;
                    }
                    break;
                case 6://个位奇偶列号
                    if (value.ToString() == setting.OddMark)
                    {
                        e.CellStyle.ForeColor = setting.ODD_COLOR;
                    }
                    else if (value.ToString() == setting.EvenMark)
                    {
                        e.CellStyle.ForeColor = setting.EVEN_COLOR;
                    }
                    break;
                case 7://个位大小列号
                    if (value.ToString() == setting.BigMark)
                    {
                        e.CellStyle.ForeColor = setting.BIG_COLOR;
                    }
                    else if (value.ToString() == setting.SmallMark)
                    {
                        e.CellStyle.ForeColor = setting.SMALL_COLOR;
                    }
                    break;
                default:
                    return;
            }
        }

        #endregion

        #region 处理文本框事件
        private void txtTensCompBase_Validating(object sender, CancelEventArgs e)
        {
            TextBox txt = sender as TextBox;
            if (txt == null)
            {
                return;
            }
            string[] numRangeArr = numRange.Split(new char[] { '-' }, StringSplitOptions.RemoveEmptyEntries);
            ////开始数
            //startNum = uint.Parse(numRangeArr[0]);
            ///结尾数
            uint endNum = uint.Parse(numRangeArr[1]);

            uint value;
            if (uint.TryParse(txt.Text, out value)==false)
            {
                txt.SelectAll();
                MessageBox.Show("必须为非负整数！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                e.Cancel = true;
                return;
            }
            if (value > endNum)
            {
                txt.SelectAll();
                MessageBox.Show("不能大于范围尾数！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                e.Cancel = true;
                return;
            }
            setting.TenBigSmallCompBase = (byte)value;
        }

        /// <summary>
        /// groupBox1中的文本框验证事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txt_Validating(object sender, CancelEventArgs e)
        {
            TextBox txt = sender as TextBox;
            if (txt == null) { return; }
            if (txt.Text.Trim().Length == 0)
            {
                return;
            }
            if (txtMultiple.Text.Trim().Length == 0)
            {
                MessageBox.Show("请先输入倍率！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                MessageBox.Show("金额必须为非负整数或小数！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtMultiple.Focus();
                return;
            }
            string value = txt.Text.Trim();
            double money;
            if (double.TryParse(value, out money))
            {
                if (Math.Abs(money) != money)
                {
                    e.Cancel = true;
                    MessageBox.Show("金额必须为非负整数或小数！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                //执行文本框内容更改后的重算
                TxtMoneyTextChanged();
            }
            else
            {
                e.Cancel = true;
                MessageBox.Show("金额必须为整数或小数！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void TxtMoneyTextChanged()
        {
            string[] numRangeArr = this.numRange.Split(new char[] { '-' }, StringSplitOptions.RemoveEmptyEntries);
            int startNum = int.Parse(numRangeArr[0]);
            int endNum = int.Parse(numRangeArr[1]);
            //选择的数字
            string selectedNum = string.Empty;
            //总金额
            double sumMoney = 0;
            //最小最大预期收益
            double minYuqi = 0;
            double maxYuqi = 0;
            for (int i = startNum; i <= endNum; i++)
            {
                //当前索引
                string index = i.ToString("00");
                CheckBox chk = (CheckBox)groupBox1.Controls["chk" + index.ToString()];
                if (chk.Checked)
                {
                    TextBox txt = (TextBox)groupBox1.Controls["txtMoney" + index.ToString()];
                    if (txt.Text.Trim().Length > 0)
                    {
                        string value = txt.Text;
                        selectedNum += index + ":"+value+";";
                        double curMoney = double.Parse(value);
                        //金额累加
                        sumMoney += curMoney;
                        //当前金额的收益
                        double shouyi = curMoney * double.Parse(txtMultiple.Text);
                        if (minYuqi == 0 && maxYuqi == 0)
                        {
                            minYuqi = shouyi;
                            maxYuqi = shouyi;
                        }
                        else if (shouyi < minYuqi)
                        {
                            minYuqi = shouyi;
                        }
                        else if (shouyi > maxYuqi)
                        {
                            maxYuqi = shouyi;
                        }
                    }
                    else
                    {
                        selectedNum += index + ",";
                    }
                }
            }
            if (selectedNum.Length > 1)
            {
                selectedNum = selectedNum.Substring(0, selectedNum.Length - 1);
                txtNumAndMoney.Text = selectedNum;
            }
            else
            {
                txtNumAndMoney.Text = string.Empty;
            }
            if (sumMoney > 0)
            {
                txtOutlay.Text = sumMoney.ToString();
            }
            if (minYuqi > 0&&maxYuqi==minYuqi)
            {
                txtExpectIncome.Text = maxYuqi.ToString();
            }
            else if (minYuqi > 0 && maxYuqi > 0)
            {
                txtExpectIncome.Text = minYuqi.ToString() + "-" + maxYuqi.ToString();
            }
        }

        private void txt_MouseEnter(object sender, EventArgs e)
        {
            TextBox txt = sender as TextBox;
            if (txt == null) { return; }
            string message = string.Empty;
            if (txt.Name == txtMaxTimes.Name)
            {
                message = "待分析记录的上一期次";
            }
            else if (txt.Name == txtOddMark.Name)
            {
                message = "设置奇数显示的符号";
            }
            else if (txt.Name == txtTimeCount.Name)
            {
                message = "分析记录的最后期次";
            }
            else if (txt.Name == txtEvenMark.Name)
            {
                message = "设置偶数显示的符号";
            }
            else if (txt.Name == txtBigMark.Name)
            {
                message = "设置大数显示的符号";
            }
            else if (txt.Name == txtSmallMark.Name)
            {
                message = "设置小数显示的符号";
            }
            else if (txt.Name == txtContinueDight.Name)
            {
                message = "设置连续符号的位数";
            }
            else if (txt.Name == txtSameDight.Name)
            {
                message = "设置一致单元格的位数";
            }
            else if (txt.Name == txtWeisu.Name)
            {
                message = "设置维数数量";
            }
            else if (txt.Name == txtTimeCount.Name)
            {
                message = "取比较期数";
            }
            else if (txt.Name == txtCompareTimes.Name)
            {
                message = "比较次数";
            }
            else if (txt.Name == txtContinueRowCount.Name)
            {
                message = "连续行数";
            }
            else if (txt.Name == txtSameRowCount.Name)
            {
                message = "一致行数";
            }
            else if (txt.Name == txtTensCompBase.Name)
            {
                message = "分析十位数大小的比较基数";
            }
            frmMdi.tsslInfo.Text = message;
            frmMdi.tsslInfo.BackColor = Color.Yellow;
        }

        private void txt_MouseLeave(object sender, EventArgs e)
        {
            frmMdi.tsslInfo.Text = "就绪";
            frmMdi.tsslInfo.BackColor = frmMdi.BackColor;
        }

        //private void SetTextBoxs(Control ct, Action<TextBox> act)
        //{
        //    foreach (Control control in ct.Controls)
        //    {
        //        if (control is TextBox)
        //        {
        //            if (act != null)
        //            {
        //                act(control as TextBox);
        //            }
        //        }
        //        else
        //        {
        //            SetTextBoxs(control, act);
        //        }
        //    }
        //}

        private void txtTimeCount_Validating(object sender, CancelEventArgs e)
        {
            TextBox txt = sender as TextBox;
            if (txt == null)
            {
                return;
            }
            uint value;
            if (uint.TryParse(txt.Text, out value))
            {
                if (value == 0)
                {
                    txt.SelectAll();
                    MessageBox.Show("分析期数不能为空或0！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    e.Cancel = true;
                    return;
                }
            }
            else
            {
                txt.SelectAll();
                MessageBox.Show("分析期数必须为非负整数！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                e.Cancel = true;
                return;
            }
            setting.TimeCount = value;
        }

        private void txtSameDigit_Validating(object sender, CancelEventArgs e)
        {
            TextBox txt = sender as TextBox;
            if (txt == null)
            {
                return;
            }
            byte value;
            if (byte.TryParse(txt.Text, out value))
            {
                if (value == 0)
                {
                    txt.SelectAll();
                    MessageBox.Show("不能为空或0！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    e.Cancel = true;
                    return;
                }
                if (value > setting.Weisu)
                {
                    txt.SelectAll();
                    MessageBox.Show("不能大于维数！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    e.Cancel = true;
                    return;
                }
            }
            else
            {
                txt.SelectAll();
                MessageBox.Show("必须为1-255之间的整数！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                e.Cancel = true;
                return;
            }
            setting.SameCount = value;
        }

        private void txtContinueDigit_Validating(object sender, CancelEventArgs e)
        {
            TextBox txt = sender as TextBox;
            if (txt == null)
            {
                return;
            }
            byte value;
            if (byte.TryParse(txt.Text, out value))
            {
                if (value == 0)
                {
                    txt.SelectAll();
                    MessageBox.Show("不能为空或0！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    e.Cancel = true;
                    return;
                }
                if (value > setting.Weisu)
                {
                    txt.SelectAll();
                    MessageBox.Show("不能大于维数！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    e.Cancel = true;
                    return;
                }
            }
            else
            {
                txt.SelectAll();
                MessageBox.Show("必须为1-255之间的整数！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                e.Cancel = true;
                return;
            }
            setting.ContinueCount = value;
        }

        private void txtWeisu_Validating(object sender, CancelEventArgs e)
        {
            TextBox txt = sender as TextBox;
            if (txt == null)
            {
                return;
            }
            byte value;
            if (byte.TryParse(txt.Text, out value))
            {
                if (value == 0)
                {
                    txt.SelectAll();
                    MessageBox.Show("不能为空或0！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    e.Cancel = true;
                    return;
                }
                if (value < setting.ContinueCount || value < setting.SameCount)
                {
                    txt.SelectAll();
                    MessageBox.Show("维数不能小于连续位数或一致位数！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    e.Cancel = true;
                    return;
                }
            }
            else
            {
                txt.SelectAll();
                MessageBox.Show("必须填入1-255之间的整数！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                e.Cancel = true;
                return;
            }
            setting.Weisu = value;
        }

        private void txtCompareTimes_Validating(object sender, CancelEventArgs e)
        {
            TextBox txt = sender as TextBox;
            if (txt == null)
            {
                return;
            }
            uint value;
            if (uint.TryParse(txt.Text, out value))
            {
                if (value == 0)
                {
                    txt.SelectAll();
                    MessageBox.Show("分析期数不能为空或0！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    e.Cancel = true;
                    return;
                }
            }
            else
            {
                txt.SelectAll();
                MessageBox.Show("分析期数必须为非负整数！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                e.Cancel = true;
                return;
            }
            setting.CompareCount = value;
        }

        private void txtContinueRowCount_Validating(object sender, CancelEventArgs e)
        {
            TextBox txt = sender as TextBox;
            if (txt == null)
            {
                return;
            }
            uint value;
            if (uint.TryParse(txt.Text, out value))
            {
                if (value == 0)
                {
                    txt.SelectAll();
                    MessageBox.Show("连续行数不能为空或0！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    e.Cancel = true;
                    return;
                }
            }
            else
            {
                txt.SelectAll();
                MessageBox.Show("连续行数必须为非负整数！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                e.Cancel = true;
                return;
            }
            setting.ContinueRowsCount = value;
        }

        private void txtSameRowCount_Validating(object sender, CancelEventArgs e)
        {
            TextBox txt = sender as TextBox;
            if (txt == null)
            {
                return;
            }
            uint value;
            if (uint.TryParse(txt.Text, out value))
            {
                if (value == 0)
                {
                    txt.SelectAll();
                    MessageBox.Show("一致行数不能为空或0！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    e.Cancel = true;
                    return;
                }
            }
            else
            {
                txt.SelectAll();
                MessageBox.Show("一致行数必须为非负整数！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                e.Cancel = true;
                return;
            }
            setting.SameRowCount = value;
        }

        private void txtOutlay_Validating(object sender, CancelEventArgs e)
        {
            string value =txtOutlay.Text;
            if (value.Trim().Length == 0)
            {
                return;
            }
            float f;
            if (float.TryParse(value, out f))
            {
                if (Math.Abs(f) != f)
                {
                    MessageBox.Show("不能为负数！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    e.Cancel = true;
                    return;
                }
            }
            else
            {
                MessageBox.Show("输入数据错误，必须为小数或整数！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                e.Cancel = true;
                return;
            }
        }

        private void txtMultiple_Validating(object sender, CancelEventArgs e)
        {
            string value = txtMultiple.Text;
            if (value.Trim().Length == 0)
            {
                return;
            }
            float f;
            if (float.TryParse(value, out f))
            {
                if (Math.Abs(f) != f)
                {
                    MessageBox.Show("不能为负数！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    e.Cancel = true;
                    return;
                }
            }
            else
            {
                MessageBox.Show("输入数据错误，必须为小数或整数！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                e.Cancel = true;
                return;
            }
        }

        private void txtMarks_Validating(object sender, CancelEventArgs e)
        {
            TextBox txt = sender as TextBox;
            if (txt == null) { return; }
            if (txt.Text.Trim().Length == 0) { return; }
            if (txt.Name == txtOddMark.Name)
            {
                setting.OddMark = txt.Text; ;
            }
            else if (txt.Name == txtEvenMark.Name)
            {
                setting.EvenMark = txt.Text;
            }
            else if (txt.Name == txtBigMark.Name)
            {
                setting.BigMark = txt.Text;
            }
            else if (txt.Name == txtSmallMark.Name)
            {
                setting.SmallMark = txt.Text;
            }
        }

        #endregion

        #region 处理组合框事件
        private void cboUnitsCompBase_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox cbo = sender as ComboBox;
            if (cbo != null)
            {
                //写入个位比较基数
                setting.UnitsBigSmallCompBase = byte.Parse(cbo.Text);
            }
        }
        #endregion

        #region 公共方法
        /// <summary>
        /// 设置控件，绑定控件事件、设置控件格式
        /// </summary>
        /// <param name="parent">父控件</param>
        /// <param name="controlType">控件类型</param>
        /// <param name="act">针对控件的委托方法</param>
        private void SetControls(Control parent, Type controlType, Action<Control> act)
        {
            //控件类型是否等于指定的类型
            if (parent.GetType().Equals(controlType))
            {
                //委托方法不为空
                if (act != null)
                {
                    //执行委托方法
                    act(parent);
                }
            }
            //是否包含子控件
            if (parent.Controls.Count > 0)
            {
                //遍历子控件
                foreach (Control ct in parent.Controls)
                {
                    SetControls(ct, controlType, act);
                }
            }
        }
        #endregion
        #endregion

    }
}
