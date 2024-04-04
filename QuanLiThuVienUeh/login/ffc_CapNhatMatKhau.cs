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

namespace QuanLiThuVienUeh.login
{
    public partial class ffc_CapNhatMatKhau : Form
    {
        //Variables
        #region Variables
        bool showPassword = false;
        int id;
        #endregion
        public ffc_CapNhatMatKhau(int id)
        {
            InitializeComponent();
            this.id = id;
            BindingUsername();
        }

        //Functions
        #region
        private void BindingUsername()
        {
            using (QLTVEntities db = new QLTVEntities())
            {
                TaiKhoanNhanVien taiKhoanNhanVien = db.TaiKhoanNhanVien.Where(r => r.IDNhanVien == id).FirstOrDefault();
                NhanVien nhanvien = db.NhanVien.Find(id);
                TaiKhoanNguoiDung taiKhoanNguoiDung = db.TaiKhoanNguoiDung.Where(r => r.IDNguoiDung == id).FirstOrDefault(); ;
                NguoiDung nguoidung = db.NguoiDung.Find(id);
                if (taiKhoanNhanVien != null) 
                {
                    id = (int)taiKhoanNhanVien.IDNhanVien;
                    label_Ten.Text = nhanvien.HoTen;
                    textBox_Username.Text = taiKhoanNhanVien.Email;
                }
                else if (taiKhoanNguoiDung != null)
                {
                    id = (int)taiKhoanNguoiDung.IDNguoiDung;
                    label_Ten.Text = nguoidung.HoTen;
                    textBox_Username.Text = taiKhoanNguoiDung.Email;
                }
            }
        }

        private string HashPassword(string password)
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
        #endregion

        private void button_SignIn_Click(object sender, EventArgs e)
        {
            if (textBox_Password.Text == null)
            {
                MessageBox.Show("Vui lỏng cập nhật mật khẩu", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (textBox_Password.Text.Length < 8)
            {
                MessageBox.Show("Mật khẩu tối thiểu 8 kí tự", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (textBox_Password.Text.Length > 16)
            {
                MessageBox.Show("Mật khẩu chứa tối đa 16 kí tự, vui lòng nhập lại","Thông báo",MessageBoxButtons.OK,MessageBoxIcon.Warning);
            }
            else
            {
                using (QLTVEntities db = new QLTVEntities())
                {
                    TaiKhoanNhanVien taiKhoanNhanVien = db.TaiKhoanNhanVien.Where(r => r.IDNhanVien == id).FirstOrDefault();
                    TaiKhoanNguoiDung taiKhoanNguoiDung = db.TaiKhoanNguoiDung.Where(r => r.IDNguoiDung == id).FirstOrDefault();
                    if (taiKhoanNhanVien != null)
                    {
                        taiKhoanNhanVien.MatKhau = HashPassword(textBox_Password.Text);
                        db.SaveChanges();
                        if (taiKhoanNhanVien.ChucVu == "Manager")
                        {
                            Form_Admin form = new Form_Admin(id);
                            this.Hide();
                            form.Show();
                        }
                        else if (taiKhoanNhanVien.ChucVu == "Staff")
                        {
                            Form_NhanVien form = new Form_NhanVien(id);
                            this.Hide();
                            form.Show();
                        }
                    }
                    else if (taiKhoanNguoiDung != null)
                    {
                        taiKhoanNguoiDung.MatKhau = HashPassword(textBox_Password.Text);
                        db.SaveChanges();
                        Form_NguoiDung form = new Form_NguoiDung(id); 
                        this.Hide();
                        form.Show();
                    }
                }
            }
        }
    }
}
