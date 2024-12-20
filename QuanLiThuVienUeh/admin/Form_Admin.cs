using QuanLiThuVienUeh.admin;
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
    public partial class Form_Admin : Form
    {
        //Variables
        #region Variables
        bool menuExpandTaiKhoanNhanVien = false; //Biến hiển thị độ mở rộng của button con
        bool menuExpandQuanLiSach = false; //Biến hiển thị độ mở rộng của button con
        bool menuExpandQuanLiDocGia = false; //Biến hiển thị độ mở rộng của button con
        private Form activeForm = null; //Biến thể hiện panel cố định đã có form nào được mở chưa
        int idNhanVien; //Biến chứa idNhanVien
        #endregion

        public Form_Admin(int idNhanVien)
        {
            InitializeComponent();
            this.idNhanVien = idNhanVien;
            panel_ChildTaiKhoanNhanVien.Height = 0; //Mặc định button con của TKNV có height = 0
            panel_ChildQuanLiSach.Height = 0; //Mặc định button con của QLS có height = 0
            panel_ChildQuanLiDocGia.Height = 0; //Mặc định button con của QLDG có height = 0
        }

        #region Search_Function
        private void textBox_Search_TextChanged(object sender, EventArgs e)
        {
            if (textBox_Search.Text.Length != 0)
            {
                label_Search.Text = ""; //Nếu text trong ô textBox được nhập thì xóa label Search
            }
            else if (textBox_Search.Text.Length == 0) label_Search.Text = "Search"; //Nếu text rỗng thì hiện lại label Search
        }

        private void textBox_Search_Click(object sender, EventArgs e)
        {
            if (textBox_Search.Text.Length == 0) label_Search.Text = ""; //TextBox được click thì xóa label Search

        }
        private void labelSearch_Click(object sender, EventArgs e)
        {
            textBox_Search.Focus(); //Nếu click vào label Search thì chuyển Focus vào textBox
            label_Search.Text = ""; //Xóa label Search
        }
        #endregion

        //Functions
        #region Functions
        //Hàm để mở rộng và thu nhỏ các button con
        private void MenuExpand_Transition(ref bool menuExpand, Panel panel, Timer timer)
        {
            if (menuExpand == false)
            {
                panel.Height += 5;
                if (panel.Height >= 100)
                {
                    StopTimer(timer);
                    menuExpand = true;
                }
            }
            else
            {
                panel.Height -= 5;
                if (panel.Height <= 0)
                {
                    StopTimer(timer);
                    menuExpand = false;
                }
            }
        }

        private void StartTimer(Timer timer) //Hàm để Start Timer
        {
            timer.Start();
        }

        private void StopTimer(Timer timer) //Hàm để stop Timer
        {
            timer.Stop();
        }

        //Hàm mở ChildForm vào 1 panel cố định trong giao diện
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
        #endregion

        //Events
        #region Events
        private void timer_TaiKhoanNhanVienTransition_Tick(object sender, EventArgs e)
        {
            MenuExpand_Transition(ref menuExpandTaiKhoanNhanVien, panel_ChildTaiKhoanNhanVien, timer_TaiKhoanNhanVienTransition);
        }

        private void timer_QuanLiSachTransition_Tick(object sender, EventArgs e)
        {
            MenuExpand_Transition(ref menuExpandQuanLiSach, panel_ChildQuanLiSach, timer_QuanLiSachTransition);
        }

        private void timer_QuanLiDocGiaTransition_Tick(object sender, EventArgs e)
        {
            MenuExpand_Transition(ref menuExpandQuanLiDocGia, panel_ChildQuanLiDocGia, timer_QuanLiDocGiaTransition);
        }

        private void button_TaiKhoanNhanVien_Click(object sender, EventArgs e)
        {
            StartTimer(timer_TaiKhoanNhanVienTransition);
            label_CurrentPage.Text = "Tài khoản nhân viên";
            label_CurrentFunction.Text = "";
        }

        private void buttonThongtin_Click(object sender, EventArgs e)
        {
            openChildForm(new ff_ThongTinNhanVien()); //Fill ChildForm vào panel cố định trong giao diện
            label_CurrentPage.Text = "Tài khoản nhân viên";
            label_CurrentFunction.Text = "> Tài khoản nhân viên > Thông tin nhân viên";
        }

        private void buttonChinhSuaTaiKhoan_Click(object sender, EventArgs e)
        {
            openChildForm(new ff_ChinhSuaTaiKhoan()); //Fill ChildForm vào panel cố định trong giao diện
            label_CurrentPage.Text = "Tài khoản nhân viên";
            label_CurrentFunction.Text = "> Tài khoản nhân viên > Chỉnh sửa thông tin nhân viên";
        }

        private void button_QuanLiSach_Click(object sender, EventArgs e)
        {
            StartTimer(timer_QuanLiSachTransition);
            label_CurrentPage.Text = "Quản lí sách";
            label_CurrentFunction.Text = "";
        }

        private void button_ThongTinSach_Click(object sender, EventArgs e)
        {
            openChildForm(new ff_ThongTinSach());
            label_CurrentPage.Text = "Quản lí sách";
            label_CurrentFunction.Text = "> Quản lí sách > Thông tin sách";
        }

        private void button_MuonTraSach_Click(object sender, EventArgs e)
        {
            openChildForm(new ff_MuonTraSach());
            label_CurrentPage.Text = "Quản lí sách";
            label_CurrentFunction.Text = "> Quản lí sách > Mượn trả sách";
        }

        private void button_QuanLiDocGia_Click(object sender, EventArgs e)
        {
            StartTimer(timer_QuanLiDocGiaTransition);
            label_CurrentPage.Text = "Quản lí độc giả";
            label_CurrentFunction.Text = "> Quản lí độc giả";
        }
        private void button_ThongTinDocGia_Click(object sender, EventArgs e)
        {
            openChildForm(new ff_QuanLiDocGia());
            label_CurrentPage.Text = "Quản lí độc giả";
            label_CurrentFunction.Text = "> Quản lí độc giả > Thông tin độc giả";
        }

        private void button_ChinhSuaDocGia_Click(object sender, EventArgs e)
        {
            openChildForm(new ffc_ChinhSuaDocGia());
            label_CurrentPage.Text = "Quản lí độc giả";
            label_CurrentFunction.Text = "> Quản lí độc giả > Chỉnh sửa thông tin độc giả";
        }

        #endregion

        private void Form_Admin_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void button_Logout_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form_Login_Main form = new Form_Login_Main();
            form.Show();
        }
    }
}
