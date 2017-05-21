using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Excel = Microsoft.Office.Interop.Excel;
using System.Text.RegularExpressions;

namespace 趋势分析
{
	public class ExcelHelper
	{
        public event EventHandler<EventArgs> GetingRecord;
        /// <summary>
        /// Excel程序
        /// </summary>
        public Excel.Application xlApp = null;
        /// <summary>
        /// 工作簿
        /// </summary>
        public Excel.Workbook xlBook = null;
        /// <summary>
        /// 可见状态
        /// </summary>
        public bool Visible = true;
        /// <summary>
        /// 是否停止
        /// </summary>
        public bool IsStop = false;
        ~ExcelHelper()
        {
            
        }
        protected void OnGetingRecord(EventArgs e)
        {
            if (GetingRecord != null)
            {
                GetingRecord(this, e);
            }
        }
        /// <summary>
        /// 取得记录数据
        /// </summary>
        /// <param name="fileName">包含路径的文件名</param>
        /// <param name="columnCount">要导入数据的列数</param>
        /// <returns></returns>
        public List<Model.HkRecord> GetHkRecord(string fileName)
        {
            //数组列表
            List<Model.HkRecord> lRecord = new List<Model.HkRecord>();
            if (xlApp == null)
            {
                xlApp=new Excel.Application();
                //是否可见
                xlApp.Visible = this.Visible;
            }
            //以只读方式打开文件
            try
            {
                xlBook=xlApp.Workbooks.Open(fileName, ReadOnly: true);
            }
            catch (Exception ex)
            {
                throw new Exception("打开文件失败！"+ex.Message);
            }
            for (int i = 1; i <= xlBook.Sheets.Count; i++)
            {
                //当前序号工作表
                Excel.Worksheet sht = xlBook.Sheets[i];
                //查找最后一个"序号"单元格
                Excel.Range rng = sht.Range[
                    sht.Cells[2, 1], 
                    sht.Cells[sht.UsedRange.Rows.Count, sht.UsedRange.Columns.Count]
                    ].Find("序号");
                if (rng == null)
                {
                    throw new Exception("工作表"+sht.Name+",没有第一个单元格为‘序号’的结束行！");
                }
                //结束行号
                int endRow = rng.Row;
                //记录日期
                DateTime recordDate;
                if (sht.Cells[2, 2].Value == null)
                {
                    throw new Exception("工作表" + sht.Name + ",第二行记录日期为空！");
                }
                if (!DateTime.TryParse(sht.Cells[2, 2].Value.ToString(),out recordDate))
                {
                    throw new Exception("工作表" + sht.Name + ",第二行记录日期格式错误！");
                }
                //遍历所有行
                for (int r = 2; r < endRow; r++)
                {
                    OnGetingRecord(null);
                    //是否停止
                    if (this.IsStop)
                    {
                        return null;
                    }
                    try
                    {
                        //声明记录对象
                        Model.HkRecord record = new Model.HkRecord();
                        //记录日期
                        record.RecordDate = DateTime.Parse(sht.Cells[r, 2].Value.ToString());
                        //记录期次
                        string strTimes = sht.Cells[r, 3].Value.ToString();
                        //期次必须为4位年3位期次
                        if (strTimes != recordDate.Year.ToString() + (r - 1).ToString("000"))
                        {
                            throw new Exception("工作表" + sht.Name + ",第" + r + "行期次错误！" +"必须为4位年3位连续的期次。");
                        }
                        //写入数据到记录对象
                        record.Times = strTimes;
                        record.FirstNum = byte.Parse(sht.Cells[r, 4].Value.ToString());
                        record.SecondNum = byte.Parse(sht.Cells[r, 5].Value.ToString());
                        record.ThirdNum = byte.Parse(sht.Cells[r, 6].Value.ToString());
                        record.FourthNum = byte.Parse(sht.Cells[r, 7].Value.ToString());
                        record.FifthNum = byte.Parse(sht.Cells[r, 8].Value.ToString());
                        record.SixthNum = byte.Parse(sht.Cells[r, 9].Value.ToString());
                        record.SeventhNum = byte.Parse(sht.Cells[r, 10].Value.ToString());
                        //添加到列表
                        lRecord.Add(record);
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("工作表" + sht.Name + ",第"+r+"行格式错误！"+ex.Message);
                    }
                }
            }
            return lRecord;
        }

        /// <summary>
        /// 将数组写到工作簿
        /// </summary>
        /// <param name="arrayObj">数组</param>
        /// <param name="heads">标题</param>
        public void WriteToBook(object[,] arrayObj, string[] heads)
        {
            if (heads.Length > 256)
            {
                throw new Exception("标题不能256列！");
            }
            if (arrayObj.GetLowerBound(0) > 65535 || arrayObj.GetLowerBound(1) > 256)
            {
                throw new Exception("内容不能超过65535行和256列！");
            }
            if (xlApp == null)
            {
                xlApp=new Excel.Application();
                xlApp.Visible = true;
            }
            //打开一个新工作簿
            Excel.Workbook book = this.xlApp.Workbooks.Add();
            //当前活动工作表
            Excel.Worksheet sht = (Excel.Worksheet)book.ActiveSheet;
            //写入标题
            sht.Range[sht.Cells[1, 1], sht.Cells[1, heads.Length]].Value = heads;
            //写入内容
            sht.Range[sht.Cells[2, 1], sht.Cells[arrayObj.GetUpperBound(0) + 2, arrayObj.GetUpperBound(1)+1]].Value = arrayObj;
            //设置单元格格式
            //设置单元格格式
            //引用写入的单元格区域
            Excel.Range writeRange = sht.Range[sht.Cells[1, 1], sht.Cells[arrayObj.GetUpperBound(0) + 2, heads.Length]];
            //设置单元格格式
            //边线线条格式 左右上下
            writeRange.Borders[Excel.XlBordersIndex.xlEdgeLeft].LineStyle = Excel.XlLineStyle.xlContinuous;
            writeRange.Borders[Excel.XlBordersIndex.xlEdgeRight].LineStyle = Excel.XlLineStyle.xlContinuous;
            writeRange.Borders[Excel.XlBordersIndex.xlEdgeTop].LineStyle = Excel.XlLineStyle.xlContinuous;
            writeRange.Borders[Excel.XlBordersIndex.xlEdgeBottom].LineStyle = Excel.XlLineStyle.xlContinuous;
            //单元格内容线条格式
            writeRange.Borders[Excel.XlBordersIndex.xlInsideVertical].LineStyle = Excel.XlLineStyle.xlContinuous;
            writeRange.Borders[Excel.XlBordersIndex.xlInsideHorizontal].LineStyle = Excel.XlLineStyle.xlContinuous;
            for (int i = 1; i <= heads.Length; i++)
            {
                //自动调整列宽
                ((Excel.Range)sht.Columns[i]).EntireColumn.AutoFit();
                //左边垂直对齐
                ((Excel.Range)sht.Columns[i]).HorizontalAlignment = Excel.Constants.xlLeft;
            }
            //设置行高
            ((Excel.Range)writeRange).RowHeight = 20;
            //设置页面左、右、上、下边距
            sht.PageSetup.LeftMargin = xlApp.InchesToPoints(0.2);
            sht.PageSetup.RightMargin = xlApp.InchesToPoints(0.2);
            sht.PageSetup.TopMargin = xlApp.InchesToPoints(0.4);
            sht.PageSetup.BottomMargin = xlApp.InchesToPoints(0.5);
            //设置页眉
            sht.PageSetup.HeaderMargin = xlApp.InchesToPoints(0.2);
            //设置页脚
            sht.PageSetup.FooterMargin = xlApp.InchesToPoints(0.3);
        }

        public void Close()
        {
            if (this.xlBook != null)
            {
                this.xlBook.Close(false);
                xlBook = null; 
            }
        }
	}
}
