using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLiThuVienUeh.admin
{
    public partial class ffc_ThongTinNhanVienChiTiet : Form
    {
        #region Variables
        int idNhanVien;
        TextBox textBox_NhanVienName;
        TextBox textBox_IDInfo;
        TextBox textBox_GioiTinhInfo;
        TextBox textBox_ChucVuInfo;
        TextBox textBox_NgaySinhInfo;
        TextBox textBox_EmailInfo;
        TextBox textBox_SoDienThoaiInfo;
        TextBox textBox_NgayNhanViecInfo;
        Label label_NhanVienNameAfter;
        Label label_SoDienThoaiAfter;
        #endregion

        public ffc_ThongTinNhanVienChiTiet(int idNhanVien, string chucVu)
        {
            InitializeComponent();
            if (chucVu == "Manager")
            {
                button_Edit.Visible = false;
                button_SaveInsert.Visible = false;
                button_ResetInsert.Visible = false;
            }
            this.idNhanVien = idNhanVien;
            LoadData(idNhanVien);
        }

        public ffc_ThongTinNhanVienChiTiet(int idNhanVien)
        {
            InitializeComponent();
            this.idNhanVien = idNhanVien;
            LoadData(idNhanVien);
        }

        #region Functions
        private string NgaySinhString(NhanVien nhanvien)
        {
            using (QLTVEntities db = new QLTVEntities())
            {
                return $"{nhanvien.NgaySinh.Value.Date.Day}/{nhanvien.NgaySinh.Value.Date.Month}/{nhanvien.NgaySinh.Value.Date.Year}";
            }
        }

        private string NgayNhanViecString(NhanVien nhanvien)
        {
            using (QLTVEntities db = new QLTVEntities())
            {
                return $"{nhanvien.NgayNhanViec.Value.Date.Day}/{nhanvien.NgayNhanViec.Value.Date.Month}/{nhanvien.NgayNhanViec.Value.Date.Year}";
            }
        }

        private void LoadData(int idNhanVien)
        {
            using (QLTVEntities db = new QLTVEntities())
            {
                NhanVien nhanvien = db.NhanVien.Where(c => c.IDNhanVien == idNhanVien).FirstOrDefault();
                label_NhanVienName.Text = nhanvien.HoTen;
                textBox_GioiThieu.Text = nhanvien.GioiThieu;
                label_IDInfo.Text = nhanvien.IDNhanVien.ToString();
                label_GioiTinhInfo.Text = nhanvien.GioiTinh;
                label_ChucVuInfo.Text = nhanvien.ChucVu;
                label_NgaySinhInfo.Text = NgaySinhString(nhanvien);
                label_EmailInfo.Text = nhanvien.Email;
                label_SoDienThoaiInfo.Text = nhanvien.SoDienThoai;
                label_NgayNhanViecInfo.Text = NgayNhanViecString(nhanvien);
                if (nhanvien.Avatar == null) return;
                MemoryStream avatarSteam = new MemoryStream(nhanvien.Avatar.ToArray());
                Image avt = Image.FromStream(avatarSteam);
                if (avt == null) return;
                guna2CirclePictureBox_Avatar.Image = avt;
            }
        }

        private void LoadDataAfter(int idNhanVien)
        {
            using (QLTVEntities db = new QLTVEntities())
            {
                NhanVien nhanvien = db.NhanVien.Where(c => c.IDNhanVien == idNhanVien).FirstOrDefault();
                label_NhanVienNameAfter.Text = nhanvien.HoTen;
                textBox_GioiThieu.Text = nhanvien.GioiThieu;
                label_IDInfo.Text = nhanvien.IDNhanVien.ToString();
                label_GioiTinhInfo.Text = nhanvien.GioiTinh;
                label_ChucVuInfo.Text = nhanvien.ChucVu;
                label_NgaySinhInfo.Text = NgaySinhString(nhanvien);
                label_EmailInfo.Text = nhanvien.Email;
                label_SoDienThoaiAfter.Text = nhanvien.SoDienThoai;
                label_NgayNhanViecInfo.Text = nhanvien.NgayNhanViec.ToString();
                if (nhanvien.Avatar == null) return;
                MemoryStream avatarSteam = new MemoryStream(nhanvien.Avatar.ToArray());
                Image avt = Image.FromStream(avatarSteam);
                if (avt == null) return;
                guna2CirclePictureBox_Avatar.Image = avt;
            }
        }

        private TextBox ChangeLabelToTextBox(Label label)
        {
            // Tạo TextBox mới
            TextBox textBox = new TextBox();

            // Sao chép dữ liệu từ Label hiện tại
            textBox.Text = label.Text;
            textBox.Font = label.Font;
            textBox.ForeColor = label.ForeColor;
            textBox.BackColor = Color.Gainsboro;

            // Thiết lập vị trí cho TextBox mới
            textBox.Location = label.Location;
            textBox.Size = label.Size;

            // Thêm TextBox mới vào Container và đưa lên trên cùng
            this.Controls.Add(textBox);
            textBox.BringToFront();

            // Xóa Label cũ
            this.Controls.Remove(label);
            return textBox;
        }

        private Label ChangeTextBoxToLabel(TextBox textBox)
        {
            // Tạo Label mới
            Label label = new Label();

            // Sao chép dữ liệu từ Label hiện tại
            label.Text = textBox.Text;
             label.Font = textBox.Font;
            label.ForeColor = textBox.ForeColor;
            label.BackColor = Color.White; ;

            // Thiết lập vị trí cho TextBox mới
            label.Location = textBox.Location;
            label.Size = textBox.Size;

            // Thêm TextBox mới vào Container và đưa lên trên cùng
            this.Controls.Add(label);
            textBox.BringToFront();

            // Xóa Label cũ
            this.Controls.Remove(textBox);
            return label;
        }
        #endregion

        private void button_Edit_Click(object sender, EventArgs e)
        {
            textBox_NhanVienName = ChangeLabelToTextBox(label_NhanVienName);
            textBox_SoDienThoaiInfo = ChangeLabelToTextBox(label_SoDienThoaiInfo);
            textBox_GioiThieu.Enabled = true;
        }

        private void button_SaveInsert_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show($"Bạn có chắc muốn lưu thay đổi trên không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                using (QLTVEntities db = new QLTVEntities())
                {
                    NhanVien nhanvien = db.NhanVien.Find(idNhanVien);
                    nhanvien.HoTen = textBox_NhanVienName.Text;
                    nhanvien.GioiThieu = textBox_GioiThieu.Text;
                    nhanvien.SoDienThoai = textBox_SoDienThoaiInfo.Text;
                    db.SaveChanges();
                    MessageBox.Show("Chỉnh sửa thông tin thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    label_NhanVienNameAfter = ChangeTextBoxToLabel(textBox_NhanVienName);
                    label_SoDienThoaiAfter = ChangeTextBoxToLabel(textBox_SoDienThoaiInfo);
                    textBox_GioiThieu.Enabled = false;
                    LoadDataAfter(idNhanVien);
                }
            }
        }

        private void button_ResetInsert_Click(object sender, EventArgs e)
        {
            label_NhanVienNameAfter = ChangeTextBoxToLabel(textBox_NhanVienName);
            label_SoDienThoaiAfter = ChangeTextBoxToLabel(textBox_SoDienThoaiInfo);
            textBox_GioiThieu.Enabled = false;
            LoadDataAfter(idNhanVien);
        }
    }
}
