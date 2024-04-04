using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLiThuVienUeh.UserControlUsing
{
    public partial class DayReview : UserControl
    {
        public DayReview()
        {
            InitializeComponent();
        }
        public void ChangeTime(string DateTimeString)
        {
            label1.Text = DateTimeString;
        }
        public Label GetLabel()
        {
            return label1;
        }
    }
}
