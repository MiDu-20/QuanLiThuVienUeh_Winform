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
    public partial class UserDay : UserControl
    {
        public UserDay()
        {
            InitializeComponent();
            WireAllControls(this);
        }
        private void WireAllControls(Control cont)
        {
            foreach (Control ctl in cont.Controls)
            {
                ctl.Click += ctl_Click;
                if (ctl.HasChildren)
                {
                    WireAllControls(ctl);
                }
            }
        }
        private void ctl_Click(object sender, EventArgs e)
        {
            this.InvokeOnClick(this, EventArgs.Empty);

        }
        public void days(int numday)
        {
            labelDay.Text = numday.ToString();
        }
        public string labelText()
        {
            string text = labelDay.Text;
            return text;
        }
    }
}
