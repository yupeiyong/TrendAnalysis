using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Winform.Common
{
    public partial class ExtendDateTimePicker : DateTimePicker
    {
        public ExtendDateTimePicker()
        {
            InitializeComponent();
        }
        public override string Text
        {
            get
            {
                return base.Text;
            }

            set
            {
                base.Text = value;
            }
        }
    }
}
