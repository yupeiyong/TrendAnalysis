using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Excel = Microsoft.Office.Interop.Excel;
using System.Text.RegularExpressions;

namespace 趋势分析
{
	class ExcelHelper
	{
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
        ~ExcelHelper()
        {
            
        }
        /// <summary>
        /// 取得数据
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
                Excel.Worksheet sht = xlBook.Sheets[i];
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
