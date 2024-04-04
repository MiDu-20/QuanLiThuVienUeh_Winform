using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLiThuVienUeh.admin
{
    public partial class ff_ThongKe : Form
    {
        private ffc_QuanLiSach _bookService;
        private Form activeForm = null;
        public ff_ThongKe()
        {
            InitializeComponent();
            _bookService = new ffc_QuanLiSach();

        }
        public void openChildForm(Form childForm)
        {
            if (activeForm != null)
            {
                activeForm.Close();
            }
            activeForm = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            panel_ChildForm.Controls.Add(childForm);
            childForm.BringToFront();
            childForm.Show();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            //LoadFirstChart();
            //LoadSecondChart();
            //LoadThirdChart();
        }

        private void btnThongKe1_Click(object sender, EventArgs e)
        {
            ffc_SachMuonTongSach sachMuonTongSoSach = new ffc_SachMuonTongSach();
            openChildForm(sachMuonTongSoSach);
        }

        private void btnThongKe2_Click(object sender, EventArgs e)
        {
            ffc_SachQuaHanSachMuon tongSoSachQuaHanSoSachMuon = new ffc_SachQuaHanSachMuon();
            openChildForm(tongSoSachQuaHanSoSachMuon);
        }

        private void btnThongKe3_Click(object sender, EventArgs e)
        {
            ffc_SachTruoc2000VaSau2000 tongSoSachTruoc2000VaSau2000 = new ffc_SachTruoc2000VaSau2000();
            openChildForm(tongSoSachTruoc2000VaSau2000);
        }

        private void btnThongKe4_Click(object sender, EventArgs e)
        {
            ffc_TheLoaiSach theLoaiSach = new ffc_TheLoaiSach();
            openChildForm(theLoaiSach);
        }
    }
}
