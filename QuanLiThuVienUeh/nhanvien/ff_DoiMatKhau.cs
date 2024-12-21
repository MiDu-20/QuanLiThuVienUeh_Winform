using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLiThuVienUeh.nhanvien
{
    public partial class ff_DoiMatKhau : Form
    {
        //Variables
        #region
        bool showCurrentPassword = false;
        bool showNewPassword = false;
        bool showVerifyPassword = false;
        bool checkInput = false;
        int id; //Biến chứa id
        string chucvu; //Biến chứa chức vụ
        #endregion

        public ff_DoiMatKhau(int id, string chucvu)
        {
            InitializeComponent();
            this.id = id;
            this.chucvu = chucvu;
            BindingData(id, chucvu);
        }

        //Functions
        #region Functions
        private void HandleShowPassword(TextBox textbox,ref bool showPassword)
        {
            if (showPassword == false)
            {
                textbox.UseSystemPasswordChar = false;
                showPassword = true;
            }
            else if (showPassword == true)
            {
                textbox.UseSystemPasswordChar = true;
                showPassword = false;
            }
        }

        private void BindingData(int id, string chucvu)
        {
            using (QLTVEntities db = new QLTVEntities())
            {
                if (db.TaiKhoanNhanVien.Where(p => p.IDNhanVien == id && p.ChucVu == "Manager").Count() > 0)
                {
                    string email = db.TaiKhoanNhanVien.Where(p => p.IDNhanVien == id && p.ChucVu == "Manager").Select(p => p.Email).FirstOrDefault();
                    textBox_Username.Text = email;
                }
                else if (db.TaiKhoanNhanVien.Where(p => p.IDNhanVien == id && p.ChucVu == "Staff").Count() > 0)
                {
                    string email = db.TaiKhoanNhanVien.Where(p => p.IDNhanVien == id && p.ChucVu == "Staff").Select(p => p.Email).FirstOrDefault();
                    textBox_Username.Text = email;
                }
                else
                {
                    string email = db.TaiKhoanNguoiDung.Where(p => p.IDNguoiDung == id).Select(p => p.Email).FirstOrDefault();
                    textBox_Username.Text = email;
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

        private void CheckInput(ref bool check)
        {
            if (string.IsNullOrWhiteSpace(textBox_CurrentPassword.Text))
                MessageBox.Show("Mật khẩu hiện tại không được để trống", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else if (string.IsNullOrEmpty(textBox_NewPassword.Text))
                MessageBox.Show("Mật khẩu mới không được để trống", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else if (string.IsNullOrEmpty(textBox_VerifyPassword.Text))
                MessageBox.Show("Nhập lại mật khẩu không được để trống", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else if (textBox_NewPassword.Text.Length < 8)
                MessageBox.Show("Mật khẩu mới phải có tối thiểu 8 kí tự");
            else if (textBox_NewPassword.Text.Length > 16)
                MessageBox.Show("Mật khẩu mới chỉ chứa tối đa 16 kí tự");
            else check = true;
        }

        private void HandleChangePasswordAdmin()
        {
            using (QLTVEntities db = new QLTVEntities())
            {
                if (textBox_NewPassword.Text == textBox_VerifyPassword.Text)
                {
                    string currentPassword = HashPassword(textBox_CurrentPassword.Text);
                    string currentPasswordDtb = db.TaiKhoanNhanVien.Where(p => p.IDNhanVien == id && p.ChucVu == "Manager").Select(p => p.MatKhau).FirstOrDefault();
                    string newPassword = HashPassword(textBox_NewPassword.Text);
                    if (currentPassword == currentPasswordDtb)
                    {
                        if (newPassword != currentPasswordDtb)
                        {
                            if (MessageBox.Show("Bạn có chắc muốn đổi mật khẩu", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                            {
                                TaiKhoanNhanVien tk = new TaiKhoanNhanVien();
                                tk.MatKhau = newPassword;
                                MessageBox.Show("Đổi mật khẩu thành công", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                            }
                            else
                                MessageBox.Show("Đổi mật khẩu thất bại", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                        }
                        else
                            MessageBox.Show("Mật khẩu mới không được trùng với mật khẩu hiện tại", "Cảnh bảo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else
                        MessageBox.Show("Mật khẩu hiện tại không chính xác", "Cảnh bảo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                    MessageBox.Show("Nhập lại mật khẩu không chính xác", "Cảnh bảo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void HandleChangePasswordNhanVien()
        {
            using (QLTVEntities db = new QLTVEntities())
            {
                if (textBox_NewPassword.Text == textBox_VerifyPassword.Text)
                {
                    string currentPassword = HashPassword(textBox_CurrentPassword.Text);
                    string currentPasswordDtb = db.TaiKhoanNhanVien.Where(p => p.IDNhanVien == id && p.ChucVu == "Staff").Select(p => p.MatKhau).FirstOrDefault();
                    string newPassword = HashPassword(textBox_NewPassword.Text);
                    if (currentPassword == currentPasswordDtb)
                    {
                        if (newPassword != currentPasswordDtb)
                        {
                            if (MessageBox.Show("Bạn có chắc muốn đổi mật khẩu", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                            {
                                TaiKhoanNhanVien tk = new TaiKhoanNhanVien();
                                tk.MatKhau = newPassword;
                                MessageBox.Show("Đổi mật khẩu thành công", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                            }
                            else
                                MessageBox.Show("Đổi mật khẩu thất bại", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                        }
                        else
                            MessageBox.Show("Mật khẩu mới không được trùng với mật khẩu hiện tại", "Cảnh bảo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else
                        MessageBox.Show("Mật khẩu hiện tại không chính xác", "Cảnh bảo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                    MessageBox.Show("Nhập lại mật khẩu không chính xác", "Cảnh bảo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void HandleChangePasswordNguoiDung()
        {
            using (QLTVEntities db = new QLTVEntities())
            {
                if (textBox_NewPassword.Text == textBox_VerifyPassword.Text)
                {
                    string currentPassword = HashPassword(textBox_CurrentPassword.Text);
                    string currentPasswordDtb = db.TaiKhoanNguoiDung.Where(p => p.IDNguoiDung == id).Select(p => p.MatKhau).FirstOrDefault();
                    string newPassword = HashPassword(textBox_NewPassword.Text);
                    if (currentPassword == currentPasswordDtb)
                    {
                        if (newPassword != currentPasswordDtb)
                        {
                            if (MessageBox.Show("Bạn có chắc muốn đổi mật khẩu", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                            {
                                TaiKhoanNguoiDung tk = new TaiKhoanNguoiDung();
                                tk.MatKhau = newPassword;
                                MessageBox.Show("Đổi mật khẩu thành công", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                            }
                            else
                                MessageBox.Show("Đổi mật khẩu thất bại", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                        }
                        else
                            MessageBox.Show("Mật khẩu mới không được trùng với mật khẩu hiện tại", "Cảnh bảo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else
                        MessageBox.Show("Mật khẩu hiện tại không chính xác", "Cảnh bảo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                    MessageBox.Show("Nhập lại mật khẩu không chính xác", "Cảnh bảo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        #endregion

        private void button_ChangePassword_Click(object sender, EventArgs e)
        {
            CheckInput(ref checkInput);
            if (checkInput == true)
            {
                using (QLTVEntities db = new QLTVEntities())
                {
                    if (db.TaiKhoanNhanVien.Where(p => p.IDNhanVien == id && p.ChucVu == "Manager").Count() > 0)
                    {
                        HandleChangePasswordAdmin();
                    }
                    else if (db.TaiKhoanNhanVien.Where(p => p.IDNhanVien == id && p.ChucVu == "Staff").Count() > 0)
                    {
                        HandleChangePasswordNhanVien();
                    }
                    else
                    {
                        HandleChangePasswordNguoiDung();
                    }
                }
            }  
        }

        private void button_SHCurrentPassword_Click(object sender, EventArgs e)
        {
            HandleShowPassword(textBox_CurrentPassword,ref showCurrentPassword);
        }

        private void button_SHNewPassword_Click(object sender, EventArgs e)
        {
            HandleShowPassword(textBox_NewPassword, ref showNewPassword);
        }

        private void button_SHVerifyPassword_Click(object sender, EventArgs e)
        {
            HandleShowPassword(textBox_VerifyPassword, ref showVerifyPassword);
        }
    }
}
