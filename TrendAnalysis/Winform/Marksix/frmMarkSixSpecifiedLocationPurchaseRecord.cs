using Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TrendAnalysis.DataTransferObject;
using TrendAnalysis.Models;
using TrendAnalysis.Service;

namespace Winform.Marksix
{
    public partial class frmMarkSixSpecifiedLocationPurchaseRecord : Form
    {
        /// <summary>
        /// 引用主窗口
        /// </summary>
        private frmMain frmMdi = null;


        /// <summary>
        /// 事件是否可用，可用方可执行查询
        /// </summary>
        private bool enableEvent = false;

        public frmMarkSixSpecifiedLocationPurchaseRecord()
        {
            InitializeComponent();
        }
        private void frmMarkSixSpecifiedLocationPurchaseRecord_Load(object sender, EventArgs e)
        {
            //取得MDI窗体的引用
            frmMdi = this.MdiParent as frmMain;
            //设置默认页数
            if (tlscombo.Items.Count > 1)
            {
                tlscombo.SelectedIndex = 1;
            }
            bdnPositionItem.Enabled = true;
            bdnPositionItem.Text = "1";
            enableEvent = true;
            tsbSearch_Click(null, e);

        }

        private DataTable Search()
        {
            //每页数量
            var pageSize = 0;
            int.TryParse(tlscombo.Text, out pageSize);
            //页码开始数
            var pageIndex = 1;
            int.TryParse(bdnPositionItem.Text, out pageIndex);
            //开始位置
            var startIndex = pageSize * (pageIndex - 1);
            if (startIndex < 0) startIndex = 0;

            var searchDto = new MarkSixSpecifiedLocationPurchaseSearchDto { StartIndex = startIndex, PageSize = pageSize };
            if (tbStartDateTime.Text.Trim().Length > 0)
            {
                var strDate = tbStartDateTime.Text.Trim();
                DateTime dt;
                if (DateTime.TryParse(strDate, out dt))
                {
                    searchDto.StartDateTime = dt;
                }
            }

            if (tbEndDateTime.Text.Trim().Length > 0)
            {
                var strDate = tbEndDateTime.Text.Trim();
                DateTime dt;
                if (DateTime.TryParse(strDate, out dt))
                {
                    searchDto.EndDateTime = dt;
                }
            }

            searchDto.Times = tbTimes.Text;
            var service = new MarkSixPurchaseService();
            try
            {
                var rows = service.SearchSpecifiedLocation(searchDto);
                if (rows.Count == 0)
                {
                    MessageBox.Show(
                        "没有找到符合条件的数据！",
                        "失败",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error
                        );
                    frmMdi.tsslInfo.Text = "查询内容为空！";
                    frmMdi.tsslInfo.BackColor = Color.Yellow;
                    return null;
                }
                frmMdi.tsslInfo.Text = "查询完成！";
                frmMdi.tsslInfo.BackColor = frmMdi.BackColor;
                var table = rows.ConvertDataTable(properties =>
                {
                    var rowVersionProperty = properties.FirstOrDefault(p => p.Name == nameof(MarkSixSpecifiedLocationPurchase.RowVersion));
                    if (rowVersionProperty != null)
                    {
                        properties.Remove(rowVersionProperty);
                    }
                    var idProperty = properties.FirstOrDefault(p => p.Name == nameof(MarkSixSpecifiedLocationPurchase.Id));
                    if (idProperty != null)
                    {
                        properties.Remove(idProperty);
                    }
                    properties.Insert(0, idProperty);
                });
                dgvMarksixPurchaseRecordList.DataSource = table;

                var pageCount = searchDto.PageCount;
                bdnCountItem.Text = pageCount.ToString();

                if (pageIndex <= 1)
                {
                    bdnMoveFirstItem.Enabled = false;
                    bdnMovePreviousItem.Enabled = false;
                }
                else
                {
                    bdnMoveFirstItem.Enabled = true;
                    bdnMovePreviousItem.Enabled = true;
                }

                if (pageIndex == pageCount)
                {
                    bdnMoveNextItem.Enabled = false;
                    bdnMoveLastItem.Enabled = false;
                }
                else
                {
                    bdnMoveNextItem.Enabled = true;
                    bdnMoveLastItem.Enabled = true;
                }
                return table;
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
                return null;
            }

        }

        private void tsbSearch_Click(object sender, EventArgs e)
        {
            Search();
        }

        private void dgvMarksixPurchaseRecordList_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            //设置ID列只读
            dgvMarksixPurchaseRecordList.Columns[nameof(MarkSixRecord.Id)].ReadOnly = true;
            //设置日期列的显示格式
            dgvMarksixPurchaseRecordList.Columns["录入时间"].DefaultCellStyle.Format = "yyyy-MM-dd";
            dgvMarksixPurchaseRecordList.Columns["修改时间"].DefaultCellStyle.Format = "yyyy-MM-dd";

        }


        private void bdnMoveFirstItem_Click(object sender, EventArgs e)
        {
            //页码开始数
            var pageIndex = 0;
            int.TryParse(bdnPositionItem.Text, out pageIndex);
            if (pageIndex > 1)
            {
                pageIndex = 1;
                bdnPositionItem.Text = pageIndex.ToString();
                Search();
            }
        }



        private void bdnMovePreviousItem_Click(object sender, EventArgs e)
        {
            //页码开始数
            var pageIndex = 0;
            int.TryParse(bdnPositionItem.Text, out pageIndex);
            if (pageIndex > 1)
            {
                pageIndex--;
                bdnPositionItem.Text = pageIndex.ToString();
            }
        }

        private void bdnMoveNextItem_Click(object sender, EventArgs e)
        {
            //页码开始数
            var pageIndex = 0;
            int.TryParse(bdnPositionItem.Text, out pageIndex);

            var pageCount = 0;
            int.TryParse(bdnCountItem.Text, out pageCount);
            if (pageIndex < pageCount)
            {
                pageIndex++;
                bdnPositionItem.Text = pageIndex.ToString();
            }
        }
        private void bdnMoveLastItem_Click(object sender, EventArgs e)
        {
            //页码开始数
            var pageIndex = 0;
            int.TryParse(bdnPositionItem.Text, out pageIndex);

            var pageCount = 0;
            int.TryParse(bdnCountItem.Text, out pageCount);
            if (pageIndex < pageCount)
            {
                pageIndex = pageCount;
                bdnPositionItem.Text = pageIndex.ToString();
            }
        }
        private void bdnPositionItem_TextChanged(object sender, EventArgs e)
        {
            if (!enableEvent) return;
            Search();
        }
        private void tlscombo_TextChanged(object sender, EventArgs e)
        {
            if (!enableEvent) return;
            enableEvent = false;
            bdnPositionItem.Text = "1";
            Search();
            enableEvent = true;
        }

        /// <summary>
        /// 只允许输入数字的控件的输入键事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnlyNumberControl_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsNumber(e.KeyChar) && e.KeyChar != (char)8)
            {
                e.Handled = true;
            }
        }


    }
}
