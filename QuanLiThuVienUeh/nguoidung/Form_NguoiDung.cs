using QuanLiThuVienUeh.admin;
using QuanLiThuVienUeh.nguoidung;
using QuanLiThuVienUeh.nhanvien;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLiThuVienUeh
{
    public partial class Form_NguoiDung : Form
    {
        #region Variables
        int idNguoiDung;
        private Form activeForm = null; //Biến thể hiện panel cố định đã có form nào được mở chưa
        #endregion

        public Form_NguoiDung() 
        {
            InitializeComponent();
        }

        public Form_NguoiDung(int idNguoiDung)
        {
            InitializeComponent();
            this.idNguoiDung = idNguoiDung;
        }

        #region Search Function
        private void textBox_SearchFunction_TextChanged(object sender, EventArgs e)
        {
            if (textBox_SearchFunction.Text.Length != 0)
            {
                label_SearchFunction.Text = ""; //Nếu text trong ô textBox được nhập thì xóa label Search
            }
            else if (textBox_SearchFunction.Text.Length == 0) label_SearchFunction.Text = "Search or type"; //Nếu text rỗng thì hiện lại label Search
        }

        private void textBox_SearchFunction_Click(object sender, EventArgs e)
        {
            if (textBox_SearchFunction.Text.Length == 0) label_SearchFunction.Text = ""; //TextBox được click thì xóa label Search
        }

        private void label_SearchFunction_Click(object sender, EventArgs e)
        {
            textBox_SearchFunction.Focus(); //Nếu click vào label Search thì chuyển Focus vào textBox
            label_SearchFunction.Text = ""; //Xóa label Search
        }
        #endregion

        //Functions
        #region Functions
        public void openChildForm(Form childForm) //Func mở ChildForm vào 1 panel cố định trong giao diện
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
        #endregion

        //Events
        #region Events
        private void button_Home_Click(object sender, EventArgs e)
        {
            openChildForm(new ff_Home(idNguoiDung));
        }

        private void button_Profile_Click(object sender, EventArgs e)
        {
            openChildForm(new ffc_XemThongTinDocGiaChiTiet(idNguoiDung));
        }

        private void button_Searching_Click(object sender, EventArgs e)
        {
            openChildForm(new ff_Searching(idNguoiDung));
        }

        private void button_Booking_Click(object sender, EventArgs e)
        {
            openChildForm(new ff_Booking(idNguoiDung));
        }

        private void button_History_Click(object sender, EventArgs e)
        {
            openChildForm(new ff_History(idNguoiDung));
        }

        private void button_Reward_Click(object sender, EventArgs e)
        {
            openChildForm(new ff_Reward(idNguoiDung));
        }

        private void button_Games_Click(object sender, EventArgs e)
        {
            openChildForm(new ff_Game(idNguoiDung));
        }

        private void button_Noti_Click(object sender, EventArgs e)
        {
            openChildForm(new ff_ThongBaoNguoiDung(idNguoiDung));
        }
        private void button_ChangePassword_Click(object sender, EventArgs e)
        {
            openChildForm(new ff_DoiMatKhau(idNguoiDung, ""));
        }

        private void button_Logout_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form_Login_Main form = new Form_Login_Main();
            form.Show();
        }
        #endregion
    }
}
