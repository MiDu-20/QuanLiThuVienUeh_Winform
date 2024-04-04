using QuanLiThuVienUeh.login;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLiThuVienUeh
{
    public partial class Form_Login_Student : Form
    {
        //Variables
        #region Variables
        int idNguoiDung; //Biến chứa idNguoiDung
        bool showPassword = false; //Biến show hide password
        #endregion

        public Form_Login_Student()
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
                string hashedPassword = HashPassword(textBox_Password.Text);
                using (QLTVEntities db = new QLTVEntities())
                {
                    if (db.TaiKhoanNguoiDung.Where(r => r.Email == textBox_Username.Text && r.MatKhau == textBox_Password.Text).Count() > 0)
                    {
                        idNguoiDung = (int)db.TaiKhoanNguoiDung.Where(r => r.Email == textBox_Username.Text && r.MatKhau == textBox_Password.Text).Select(r => r.IDNguoiDung).FirstOrDefault();
                        this.Hide();
                        ffc_CapNhatMatKhau admin = new ffc_CapNhatMatKhau(idNguoiDung);
                        admin.Show();
                    }
                    else if (db.TaiKhoanNguoiDung.Where(r => r.Email == textBox_Username.Text && r.MatKhau == hashedPassword).Count() > 0)
                    {
                        idNguoiDung = (int)db.TaiKhoanNguoiDung.Where(r => r.Email == textBox_Username.Text && r.MatKhau == textBox_Password.Text).Select(r => r.IDNguoiDung).FirstOrDefault();
                        this.Hide();
                        Form_NguoiDung admin = new Form_NguoiDung(idNguoiDung);
                        admin.Show();
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
        private void Form_Login_Student_FormClosing(object sender, FormClosingEventArgs e)
        {
            Form_Login_Main formLoginMain = new Form_Login_Main();
            formLoginMain.Show();
        }
        #endregion
    }
}
