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

namespace QuanLiThuVienUeh.nguoidung
{
    public partial class ffc_ThongTinSachChiTiet : Form
    {
        int idSach;
        int idNguoiDung;
        public ffc_ThongTinSachChiTiet(int idSach, int idNguoiDung, string formOpen)
        {
            InitializeComponent();
            if(formOpen =="History")
            {
                button_Booking.Visible = false;
                button_Coupon.Visible = false;
            }
               
            this.idSach = idSach;
            this.idNguoiDung = idNguoiDung;
            checkBox_Coupon.Visible = false;
            LoadData(idSach);
        }

        public ffc_ThongTinSachChiTiet(int idSach, int idNguoiDung)
        {
            InitializeComponent();
            this.idSach = idSach;
            this.idNguoiDung = idNguoiDung;
            checkBox_Coupon.Visible = false;
            LoadData(idSach);
        }

        public ffc_ThongTinSachChiTiet(int idSach)
        {
            InitializeComponent();

                button_Booking.Visible = false;
                button_Coupon.Visible = false;
            this.idSach = idSach;
            this.idNguoiDung = idNguoiDung;
            checkBox_Coupon.Visible = false;
            LoadData(idSach);
        }

        private void LoadData(int id)
        {
            using (QLTVEntities db = new QLTVEntities())
            {
                Sach sach = db.Sach.Where(c => c.IDSach == id).FirstOrDefault();
                label_IDInfo.Text = sach.IDSach.ToString();
                label_TenSach.Text = sach.TenSach.ToUpper();
                label_NgonNguInfo.Text = sach.NgonNgu;
                label_TacGiaInfo.Text = sach.TacGia;
                label_TheLoaiInfo.Text = sach.TheLoai;
                label_NhaXuatBanInfo.Text = sach.NhaXuatBan;
                label_NamXuatBanInfo.Text = sach.NamXuatBan.ToString();
                label_PhienBanInfo.Text = sach.PhienBan;
                textBox_GioiThieu.Text = sach.GioiThieu;
                if (sach.Avatar == null) return;
                MemoryStream avatarSteam = new MemoryStream(sach.Avatar.ToArray());
                Image avt = Image.FromStream(avatarSteam);
                if (avt == null) return;
                pictureBox_Avatar.Image = avt;
            }
        }

        private void button_Booking_Click(object sender, EventArgs e)
        {
            using (QLTVEntities db = new QLTVEntities())
            {
                var userTuiDo = db.TuiDo.Where(p => p.IDNguoiDung == idNguoiDung).FirstOrDefault();
                if (userTuiDo == null)
                {
                    MessageBox.Show("Hãy vào mục Reward để cập nhật Coupon trước khi mượn sách", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    var countBook = userTuiDo.CountBookReward;
                    if (db.MuonTraSach.Where(p => p.IDNguoiDung == idNguoiDung && p.TinhTrang == "Đang mượn").Count() < 3 + countBook)
                    {
                        PhieuMuonSach pms = new PhieuMuonSach();
                        pms.IDPhieuMuonSach = db.PhieuMuonSach.Max(s => s.IDPhieuMuonSach) + 1;
                        pms.IDSach = idSach;
                        pms.TenSach = db.Sach.Find(idSach).TenSach;
                        pms.IDNguoiDung = idNguoiDung;
                        pms.HoTen = db.NguoiDung.Find(idNguoiDung).HoTen;
                        if (checkBox_Coupon.Checked == true)
                            pms.Coupon = true;
                        else if (checkBox_Coupon.Checked == false)
                            pms.Coupon = false;
                        db.PhieuMuonSach.Add(pms);
                        db.SaveChanges();
                        MessageBox.Show("Tạo phiếu mượn sách thành công \nKiểm tra phiếu mượn sách ở mục Booking", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else MessageBox.Show("Bạn đã mượn tối đa sách!", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning); ;
                }
            }
        }

        private void button_Coupon_Click(object sender, EventArgs e)
        {
            using (QLTVEntities db = new QLTVEntities())
            {
                var userTuiDo = db.TuiDo.Where(s => s.IDNguoiDung == idNguoiDung).FirstOrDefault();
                if (userTuiDo == null)
                {
                    checkBox_Coupon.Enabled = false;
                    MessageBox.Show("Bạn chưa có coupon \nĐổi coupon ở mục Reward", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (userTuiDo.CountWeekReward == 0)
                {
                    checkBox_Coupon.Enabled = false;
                    MessageBox.Show("Bạn chưa có coupon \nĐổi coupon ở mục Reward", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                    checkBox_Coupon.Visible = true;
            }
        }
    }
}
