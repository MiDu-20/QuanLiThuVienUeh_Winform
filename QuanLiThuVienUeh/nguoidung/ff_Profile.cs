using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLiThuVienUeh.nguoidung
{
    public partial class ff_Profile : Form
    {
        public ff_Profile(int idNguoiDung)
        {
            InitializeComponent();
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
                label_HoVaTenInfo.Text = nguoidung.HoTen;
                label_GioiTinhInfo.Text = nguoidung.GioiTinh;
                label_NgaySinhInfo.Text = ((DateTime)nguoidung.NgaySinh).ToString();
                label_LopInfo.Text = nguoidung.Lop;
                label_ChuyenNganhInfo.Text = nguoidung.ChuyenNganh;
                label_EmailInfo.Text = nguoidung.Email;
                label_SoDienThoaiInfo.Text = nguoidung.SoDienThoai;
            }
        }
    }
}
