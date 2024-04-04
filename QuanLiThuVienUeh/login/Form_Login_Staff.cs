using QuanLiThuVienUeh.login;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLiThuVienUeh
{
    public partial class Form_Login_Staff : Form
    {
        //Variables
        #region Variables
        int idNhanVien; //Biến chứa idNhanVien
        bool showPassword = false; //Biến show hide password
        #endregion

        public Form_Login_Staff()
        {
            InitializeComponent();
        }

        //Functions
        #region Functions
        private string HashPassword(string password) //Hàm hash password
        {
            using (var sha256 = SHA256.Create())
            {
                // Chuyển mật khẩu thành mảng byte
                byte[] bytes = Encoding.UTF8.GetBytes(password);
                // Băm mật khẩu
                byte[] hash = sha256.ComputeHash(bytes);
                // Chuyển mảng byte thành chuỗi hex
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < hash.Length; i++)
                {
                    builder.Append(hash[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }

        public bool VerifyPassword(string password, string hashedPassword) //Hàm xác minh password
        {
            string hashedInput = HashPassword(password);
            return hashedInput.Equals(hashedPassword);
        }
        #endregion

        //Events
        #region Events
        private void button_SHPassword_Click(object sender, EventArgs e)
        {
            if (showPassword == false)
            {
                textBox_Password.UseSystemPasswordChar = false;
                showPassword = true;
            }
            else if (showPassword == true)
            {
                textBox_Password.UseSystemPasswordChar = true;
                showPassword = false;
            }
        }

        private void button_SignIn_Click(object sender, EventArgs e)
        {
            try
            {
                using (QLTVEntities db = new QLTVEntities())
                {
                    string hashedPassword = HashPassword(textBox_Password.Text);
                    if (db.TaiKhoanNhanVien.Where(r => r.Email == textBox_Username.Text && r.MatKhau == textBox_Password.Text && r.ChucVu == "Manager").Count() > 0)
                    {
                        idNhanVien = Convert.ToInt32(db.TaiKhoanNhanVien.Where(r => r.Email == textBox_Username.Text && r.MatKhau == textBox_Password.Text && r.ChucVu == "Manager").Select(r => r.IDNhanVien).FirstOrDefault());
                        this.Hide();
                        ffc_CapNhatMatKhau admin = new ffc_CapNhatMatKhau(idNhanVien);
                        admin.Show();
                    }
                    else if (db.TaiKhoanNhanVien.Where(r => r.Email == textBox_Username.Text && r.MatKhau == hashedPassword && r.ChucVu == "Manager").Count() > 0)
                    {
                        idNhanVien = Convert.ToInt32(db.TaiKhoanNhanVien.Where(r => r.Email == textBox_Username.Text && r.MatKhau == textBox_Password.Text && r.ChucVu == "Manager").Select(r => r.IDNhanVien).FirstOrDefault());
                        this.Hide();
                        Form_Admin admin = new Form_Admin(idNhanVien);
                        admin.Show();
                    }
                    else if (db.TaiKhoanNhanVien.Where(r => r.Email == textBox_Username.Text && r.MatKhau == textBox_Password.Text && r.ChucVu == "Staff").Count() > 0)
                    {
                        idNhanVien = (int)(db.TaiKhoanNhanVien.Where(r => r.Email == textBox_Username.Text && r.MatKhau == textBox_Password.Text && r.ChucVu == "Staff").Select(r => r.IDNhanVien).FirstOrDefault());
                        this.Hide();
                        ffc_CapNhatMatKhau nhanvien = new ffc_CapNhatMatKhau(idNhanVien);
                        nhanvien.Show();
                    }
                    else if (db.TaiKhoanNhanVien.Where(r => r.Email == textBox_Username.Text && r.MatKhau == hashedPassword && r.ChucVu == "Staff").Count() > 0)
                    {
                        this.Hide();
                        idNhanVien = (int)(db.TaiKhoanNhanVien.Where(r => r.Email == textBox_Username.Text && r.MatKhau == hashedPassword && r.ChucVu == "Staff").Select(r => r.IDNhanVien).FirstOrDefault());
                        Form_NhanVien nhanvien = new Form_NhanVien(idNhanVien);
                        nhanvien.Show();
                    }
                    else
                        MessageBox.Show("Nhập sai Tài khoản/Mật khẩu", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Something went wrong", "Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }
        }

        private void linkLabel_ForgotPassword_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Hide();
            ffc_QuenMatKhau quenmk = new ffc_QuenMatKhau();
            quenmk.Show();
        }

        private void Form_Login_Staff_FormClosing(object sender, FormClosingEventArgs e)
        {
            Form_Login_Main formLoginMain = new Form_Login_Main();
            formLoginMain.Show();
        }
        #endregion
    }
}
