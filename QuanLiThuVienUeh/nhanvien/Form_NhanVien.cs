using QuanLiThuVienUeh.admin;
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
    public partial class Form_NhanVien : Form
    {
        //Variables
        #region
        int idNhanVien; //Biến chứa idNhanVien
        bool menuExpandTaiKhoanNhanVien = false; //Biến hiển thị độ mở rộng của button con
        bool menuExpandQuanLiSach = false; //Biến hiển thị độ mở rộng của button con
        bool menuExpandQuanLiDocGia = false; //Biến hiển thị độ mở rộng của button con
        private Form activeForm = null; //Biến thể hiện panel cố định đã có form nào được mở chưa
        #endregion

        public Form_NhanVien()
        {
            InitializeComponent();
            panel_ChildTaiKhoan.Height = 0; //Mặc định button con của TKNV có height = 0
            panel_ChildQuanLiSach.Height = 0; //Mặc định button con của QLS có height = 0
        }

        public Form_NhanVien(int idNhanVien)
        {
            InitializeComponent();
            this.idNhanVien = idNhanVien;
            panel_ChildTaiKhoan.Height = 0; //Mặc định button con của TKNV có height = 0
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
        #region Hàm
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

        private void Timer_Start(Timer timer) //Func để Start Timer
        {
            timer.Start();
        }
        #endregion

        //Events
        #region Events
        private void timer_TaiKhoanTransition_Tick(object sender, EventArgs e)
        {
            //Func để mở rộng và thu nhỏ button của TKNV
            MenuExpand_Transition(ref menuExpandTaiKhoanNhanVien, panel_ChildTaiKhoan, timer_TaiKhoanTransition);
        }

        private void timer_QuanLiSachTransition_Tick(object sender, EventArgs e)
        {
            MenuExpand_Transition(ref menuExpandQuanLiSach, panel_ChildQuanLiSach, timer_QuanLiSachTransition);
        }

        private void timer_QuanLiDocGiaTransition_Tick(object sender, EventArgs e)
        {
            MenuExpand_Transition(ref menuExpandQuanLiDocGia, panel_ChildQuanLiDocGia, timer_QuanLiDocGiaTransition);
        }

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
        private void button_TaiKhoan_Click(object sender, EventArgs e)
        {
            Timer_Start(timer_TaiKhoanTransition); //Start Timer của TKNV
            label_CurrentPage.Text = "Tài khoản";
            label_CurrentFunction.Text = "";
        }

        private void button_Thongtin_Click(object sender, EventArgs e)
        {
            label_CurrentPage.Text = "Thông tin nhân viên";
            label_CurrentFunction.Text = "> Tài khoản > Thông tin nhân viên";
            openChildForm(new ffc_ThongTinNhanVienChiTiet(idNhanVien)); //Fill ChildForm vào panel cố định trong giao diện
        }

        private void button_ChinhSuaTaiKhoan_Click(object sender, EventArgs e)
        {
            label_CurrentPage.Text = "Thông tin nhân viên";
            label_CurrentFunction.Text = "> Tài khoản > Đổi mật khẩu";
            openChildForm(new ff_DoiMatKhau(idNhanVien, "Staff")); //Fill ChildForm vào panel cố định trong giao diện
        }

        private void button_LichLamViec_Click(object sender, EventArgs e)
        {
            label_CurrentPage.Text = "Lịch làm việc";
            label_CurrentFunction.Text = "> Lịch làm việc";
            openChildForm(new ffc_NhanVienDatCa(idNhanVien)); //Fill ChildForm vào panel cố định trong giao diện
        }

        private void button_QuanLiSach_Click(object sender, EventArgs e)
        {
            Timer_Start(timer_QuanLiSachTransition);
            label_CurrentPage.Text = "Quản lí sách";
            label_CurrentFunction.Text = "";
        }
        
        private void button_ThongTinSach_Click(object sender, EventArgs e)
        {
            label_CurrentPage.Text = "Thông tin sách";
            label_CurrentFunction.Text = "Quản lí sách > Thông tin sách";
            openChildForm(new ff_ThongTinSach()); //Fill ChildForm vào panel cố định trong giao diện
        }

        private void button_MuonTraSach_Click(object sender, EventArgs e)
        {
            label_CurrentPage.Text = "Mượn trả sách";
            label_CurrentFunction.Text = "Quản lí sách > Mượn trả sách";
            openChildForm(new ff_MuonTraSach()); //Fill ChildForm vào panel cố định trong giao diện
        }

        private void button_QuanLiDocGia_Click(object sender, EventArgs e)
        {
            StartTimer(timer_QuanLiDocGiaTransition);
            label_CurrentPage.Text = "Quản lí độc giả";
            label_CurrentFunction.Text = "Quản lí độc giả";
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

        private void button_ThongKe_Click(object sender, EventArgs e)
        {
            label_CurrentPage.Text = "Thống kê";
            label_CurrentFunction.Text = "Thống kê";
            openChildForm(new ff_ThongKe()); //Fill ChildForm vào panel cố định trong giao diện
        }

        private void button_ThongBao_Click(object sender, EventArgs e)
        {
            label_CurrentPage.Text = "Thông báo";
            label_CurrentFunction.Text = "Thông báo";
            openChildForm(new ff_ThongBaoStaff(idNhanVien)); //Fill ChildForm vào panel cố định trong giao diện
        }
        #endregion
    }
}
