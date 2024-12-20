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
    public partial class ffc_XemThongTinDocGiaChiTiet : Form
    {
        //Variables
        #region
        int idNguoiDung;
        #endregion

        public ffc_XemThongTinDocGiaChiTiet(int idNguoiDung, string chucVu)
        {
            InitializeComponent();
            this.idNguoiDung = idNguoiDung;
            LoadData(idNguoiDung);
        }

        public ffc_XemThongTinDocGiaChiTiet(int idNguoiDung)
        {
            InitializeComponent();
            this.idNguoiDung = idNguoiDung;
            LoadData(idNguoiDung);
        }

        private void LoadData(int idNguoiDung)
        {
            using (QLTVEntities db = new QLTVEntities())
            {
                NguoiDung nguoidung = db.NguoiDung.Where(c => c.IDNguoiDung == idNguoiDung).FirstOrDefault();
                label_NhanVienName.Text = nguoidung.HoTen;
                textBox_GioiThieu.Text = nguoidung.GioiThieu;
                label_IDInfo.Text = nguoidung.IDNguoiDung.ToString();
                label_GioiTinhInfo.Text = nguoidung.GioiTinh;
                label_NgaySinhInfo.Text = nguoidung.NgaySinh.ToString();
                label_LopInfo.Text = nguoidung.Lop;
                label_ChuyenNganhInfo.Text = nguoidung.ChuyenNganh.ToString();
                label_EmailInfo.Text = nguoidung.Email;
                label_SoDienThoaiInfo.Text = nguoidung.SoDienThoai;
                if (nguoidung.Avatar == null) return;
                MemoryStream avatarSteam = new MemoryStream(nguoidung.Avatar.ToArray());
                Image avt = Image.FromStream(avatarSteam);
                if (avt == null) return;
                guna2CirclePictureBox_Avatar.Image = avt;
            }
        }

        private void button_SaveInsert_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show($"Bạn có chắc muốn lưu thay đổi trên không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                using (QLTVEntities db = new QLTVEntities())
                {
                    NguoiDung nguoidung = db.NguoiDung.Find(idNguoiDung);
                    nguoidung.GioiThieu = textBox_GioiThieu.Text;
                    db.SaveChanges();
                    MessageBox.Show("Chỉnh sửa thông tin thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    textBox_GioiThieu.Enabled = false;
                }
            }
        }

        private void button_ResetInsert_Click(object sender, EventArgs e)
        {
            LoadData(idNguoiDung);
        }
    }
}
