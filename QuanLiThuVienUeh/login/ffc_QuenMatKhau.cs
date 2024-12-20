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
    public partial class ffc_QuenMatKhau : Form
    {
        //Variables
        #region Variables
        bool showPassword = false; //Biến show hide password
        int id; //Biến chứa idNhanVien
        string OTP = "";
        #endregion

        public ffc_QuenMatKhau()
        {
            InitializeComponent();
        }

        //Functions
        #region Functions
        private string RandomOTP()
        {
            Random random = new Random();
            StringBuilder stringBuilder = new StringBuilder();

            // Tạo ký tự đầu tiên từ 1 đến 9
            stringBuilder.Append(random.Next(1, 10));

            // Tạo 7 ký tự tiếp theo từ 0 đến 9
            for (int i = 0; i < 7; i++)
            {
                stringBuilder.Append(random.Next(0, 10));
            }

            return stringBuilder.ToString();
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

        public bool VerifyPassword(string password, string hashedPassword)
        {
            string hashedInput = HashPassword(password);
            return hashedInput.Equals(hashedPassword);
        }
        #endregion

        //Events
        #region Events
        private void button_GuiOTP_Click(object sender, EventArgs e)
        {
            using (QLTVEntities db = new QLTVEntities())
            {
                if (!string.IsNullOrWhiteSpace(textBox_Username.Text))
                {
                    if (db.TaiKhoanNhanVien.Where(s => s.Email == textBox_Username.Text).Any())
                    {
                        OTP = RandomOTP();
                        label_OTP.Text = OTP;
                    }
                    else if (db.TaiKhoanNguoiDung.Where(s => s.Email == textBox_Username.Text).Any())
                    {
                        OTP = RandomOTP();
                        label_OTP.Text = OTP;
                    }
                }
            }
        }

        private void button_SignIn_Click(object sender, EventArgs e)
        {
            try
            {
                using (QLTVEntities db = new QLTVEntities())
                {

                    if (db.TaiKhoanNhanVien.Where(r => r.Email == textBox_Username.Text).Any() && textBox_OTP.Text == OTP)
                    {
                        this.Hide();
                        id = (int)db.TaiKhoanNhanVien.Where(r => r.Email == textBox_Username.Text).Select(r => r.IDNhanVien).FirstOrDefault();
                        ffc_CapNhatMatKhau form = new ffc_CapNhatMatKhau(id);
                        form.Show();
                    }
                    else if (db.TaiKhoanNguoiDung.Where(r => r.Email == textBox_Username.Text).Any() && textBox_OTP.Text == OTP)
                    {
                        id = (int)db.TaiKhoanNguoiDung.Where(r => r.Email == textBox_Username.Text).Select(r => r.IDNguoiDung).FirstOrDefault();
                        this.Hide();
                        ffc_CapNhatMatKhau user = new ffc_CapNhatMatKhau(id);
                        user.Show();
                    }                    
                    else
                        MessageBox.Show("Nhập sai Tài khoản/OTP","Cảnh báo",MessageBoxButtons.OK,MessageBoxIcon.Error);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Something went wrong", "Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }
        }
        #endregion
    }
}
