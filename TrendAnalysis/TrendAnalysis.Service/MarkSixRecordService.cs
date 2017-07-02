using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrendAnalysis.Data;
using TrendAnalysis.DataTransferObject;
using TrendAnalysis.Models;

namespace TrendAnalysis.Service
{
    /// <summary>
    /// MarkSix记录操作类
    /// </summary>
    public class MarkSixRecordService
    {
        public event EventHandler<EventArgs> ImportingEvent;

        public event EventHandler<EventArgs> BeforeImportEvent;

        /// <summary>
        /// 记录条数
        /// </summary>
        public int RecordCount { get; set; }


        /// <summary>
        /// 已导入条数
        /// </summary>
        public int ImportedCount { get; set; } = 0;
        /// <summary>
        /// 是否停止导入
        /// </summary>
        public bool IsStopImporting { get; set; }

        public List<MarkSixRecord> Search(MarkSixRecordSearchDto dto)
        {
            using(var dao=new TrendDbContext())
            {
                var source = dao.Set<MarkSixRecord>().AsQueryable();

                if (dto.StartDateTime.HasValue || dto.EndDateTime.HasValue)
                {
                    if (!dto.StartDateTime.HasValue)
                    {
                        dto.StartDateTime = DateTime.MinValue.AddDays(1);
                    }
                    if (!dto.EndDateTime.HasValue)
                    {
                        dto.EndDateTime = DateTime.MaxValue.AddDays(-1);
                    }
                    dto.StartDateTime = dto.StartDateTime.Value.Date.AddDays(-1);
                    dto.EndDateTime = dto.EndDateTime.Value.Date.AddDays(1);
                    source = source.Where(m => m.AwardingDate > dto.StartDateTime.Value && m.AwardingDate < dto.EndDateTime);
                }

                if (!string.IsNullOrWhiteSpace(dto.Times))
                {
                    source = source.Where(m => m.Times.Contains(dto.Times));
                }
                if (dto.IsGetTotalCount)
                {
                    dto.TotalCount = source.Count();
                }
                return source.OrderBy(m=>m.Times).Skip(dto.StartIndex).Take(dto.PageSize).ToList();
            }
        }
        /// <summary>
        /// 导出
        /// </summary>
        /// <returns></returns>
        public List<MarkSixRecord> Export(DataTable table)
        {
            FileInfo newFile = new FileInfo(@"d:\test.xlsx");
            if (newFile.Exists)
            {
                newFile.Delete();
                newFile = new FileInfo(@"d:\test.xlsx");
            }
            using (var package = new ExcelPackage(newFile))
            {
                var workbook=package.Workbook;
                var sht = workbook.Worksheets.Add("MarksixRecord");
                sht.Cells[1, 1].LoadFromDataTable(table, true);
                package.Save();
            }
                return null;
        }

        protected void OnImportingEvent(EventArgs e)
        {
            if (ImportingEvent != null)
            {
                ImportingEvent(this, e);
            }
        }

        protected void OnBeforeImportEvent(EventArgs e)
        {
            if (BeforeImportEvent != null)
            {
                BeforeImportEvent(this, e);
            }
        }
        /// <summary>
        /// 导入
        /// </summary>
        public int Import(string fileName)
        {
            //数组列表
            List<MarkSixRecord> records = new List<MarkSixRecord>();
            using (var stream = new FileStream(fileName, FileMode.Open))
            using(var package=new ExcelPackage(stream))
            {
                var workbook = package.Workbook;
                for (int i = 1,len=workbook.Worksheets.Count; i <= len; i++)
                {
                    //当前序号工作表
                    var sht = workbook.Worksheets[i];
                    var startRow=sht.Dimension.Start.Row;
                    var endRow = sht.Dimension.End.Row;
                    var startColumn = sht.Dimension.Start.Column;
                    var endColumn = sht.Dimension.End.Column;
                    var cell=sht.Cells[startRow, startColumn, endRow, endColumn].FirstOrDefault(c => c.Text == "序号");
                    //查找最后一个"序号"单元格
                    if (cell == null)
                    {
                        throw new Exception("工作表" + sht.Name + ",没有第一个单元格为‘序号’的结束行！");
                    }
                    //记录日期
                    DateTime recordDate;
                    if (sht.Cells[2, 2].Value == null)
                    {
                        throw new Exception("工作表" + sht.Name + ",第二行记录日期为空！");
                    }
                    if (!DateTime.TryParse(sht.Cells[2, 2].Value.ToString(), out recordDate))
                    {
                        throw new Exception("工作表" + sht.Name + ",第二行记录日期格式错误！");
                    }
                    //遍历所有行
                    for (int r = 2; r < endRow; r++)
                    {

                        try
                        {
                            //声明记录对象
                            var record = new MarkSixRecord();
                            //开奖日期
                            record.AwardingDate = DateTime.Parse(sht.Cells[r, 2].Value.ToString());
                            //记录期次
                            string strTimes = sht.Cells[r, 3].Value.ToString();
                            //期次必须为4位年3位期次
                            if (strTimes != recordDate.Year.ToString() + (r - 1).ToString("000"))
                            {
                                throw new Exception("工作表" + sht.Name + ",第" + r + "行期次错误！" + "必须为4位年3位连续的期次。");
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
                            records.Add(record);
                        }
                        catch (Exception ex)
                        {
                            throw new Exception("工作表" + sht.Name + ",第" + r + "行格式错误！" + ex.Message);
                        }
                    }
                }
            }
            if (records.Count > 0)
            {
                this.RecordCount = records.Count;
                OnBeforeImportEvent(null);
                using(var dao=new TrendDbContext())
                {
                    //var timeses = records.Select(r => r.Times).ToList();
                    //var originalRecords = dao.Set<MarkSixRecord>().Where(m => timeses.Any(times => times == m.Times));
                    //dao.Set<MarkSixRecord>().RemoveRange(originalRecords);
                    //dao.Set<MarkSixRecord>().AddRange(records);
                    foreach (var record in records)
                    {
                        //记录已导入数量
                        ImportedCount++;
                        //处理正在导入事件
                        OnImportingEvent(null);
                        if (IsStopImporting)
                            return 0;
                        var originalRecord = dao.Set<MarkSixRecord>().FirstOrDefault(m => m.Times == record.Times);
                        if (originalRecord != null)
                        {
                            originalRecord.AwardingDate = record.AwardingDate;
                            originalRecord.FirstNum = record.FirstNum;
                            originalRecord.SecondNum = record.SecondNum;
                            originalRecord.ThirdNum = record.ThirdNum;
                            originalRecord.FourthNum = record.FourthNum;
                            originalRecord.FifthNum = record.FifthNum;
                            originalRecord.SixthNum = record.SixthNum;
                            originalRecord.SeventhNum = record.SeventhNum;
                            dao.Entry(originalRecord).State = System.Data.Entity.EntityState.Modified;
                        }
                        else
                        {
                            dao.Set<MarkSixRecord>().Add(record);
                        }
                    }


                    dao.SaveChanges();
                }
            }
            return records.Count();
        }

        /// <summary>
        /// 通过网络抓取
        /// </summary>
        public void NetworkCapture()
        {

        }
    }
}
