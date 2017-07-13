using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Winform.Marksix
{
    public partial class frmMarkSixSpecifiedLocationPurchase : Form
    {
        public Dictionary<byte, double> Numbers { get; set; }

        private Dictionary<byte, TextBox> _textBoxs = new Dictionary<byte, TextBox>();
        public frmMarkSixSpecifiedLocationPurchase()
        {
            InitializeComponent();
        }

        private void frmMarkSixSpecifiedLocationPurchase_Load(object sender, EventArgs e)
        {
            //OnShowDialogEvent(null);
            if (Numbers != null && Numbers.Count > 0)
            {
                //按行列设置文本框和标签
                //顶距
                var marginTop = 20;

                //左边距
                var marginLeft = 15;

                //行高
                var rowHeight = 30;

                //列宽
                var columnWidth = 100;

                //每行显示5个
                var columnCountEveryRow = 5;
                var keys = Numbers.Keys.ToList();
                for (int i = 0, len = keys.Count; i < len; i++)
                {
                    var number = keys[i];
                    var rowIndex = i / columnCountEveryRow;
                    var columnIndex = i % columnCountEveryRow;
                    var yPoint = marginTop + rowIndex * rowHeight;
                    var xPoint = marginLeft + columnIndex * columnWidth;
                    var labelLocation = new Point(xPoint, yPoint + 3);
                    var textboxLocation = new Point(xPoint + 23, yPoint);
                    var label = new Label { Text = number.ToString("00"), Location = labelLocation, Width = 17, Height = 12 };
                    var tb = new TextBox { Location = textboxLocation, Width = 50, Height = 20 };
                    _textBoxs.Add(number, tb);
                    gbNumberList.Controls.Add(label);
                    gbNumberList.Controls.Add(tb);
                }
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (Numbers != null && Numbers.Count > 0)
            {
                var keys = Numbers.Keys.ToList();
                foreach(var key in keys)
                {
                    //购买金额文本框
                    var tb = _textBoxs[key];
                    var amount = 0D;
                    if(double.TryParse(tb.Text,out amount))
                    {
                        Numbers[key] = amount;
                    }
                }
            }
            this.DialogResult = DialogResult.OK;
        }

        //public event EventHandler<EventArgs> ShowDialogEvent;
        //protected void OnShowDialogEvent(EventArgs e)
        //{
        //    if (ShowDialogEvent != null)
        //    {
        //        ShowDialogEvent(this, e);
        //    }
        //}
    }
}
