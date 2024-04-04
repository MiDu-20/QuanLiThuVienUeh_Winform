using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLiThuVienUeh.admin
{
    public partial class ffc_XacNhanTraSach : Form
    {
        #region Variables
        bool checkInputInsert = true; //Biến check input khi Insert
        int coupon; //Biến chứa coupon ưu đãi
        #endregion

        public ffc_XacNhanTraSach()
        {
            InitializeComponent();
            dateTimePicker_NgayTraSach.Value = DateTime.Now;
        }

        //Functions
        #region Functions
        //Hàm check input khi mượn sách
        private void BindingDataByIdMuonTra()
        {
            using (QLTVEntities db = new QLTVEntities())
            {
                if (!string.IsNullOrEmpty(textBox_IDMuonTra.Text)) // Kiểm tra xem textbox có giá trị không
                {
                    if (int.TryParse(textBox_IDMuonTra.Text, out int IDMuonTra)) // Thử chuyển đổi giá trị sang số nguyên
                    {
                        var result = db.MuonTraSach.Find(IDMuonTra);
                        if (result != null)
                        {
                            textBox_IDNguoiDung.Text = result.IDNguoiDung.ToString();
                            textBox_IDSach.Text = result.IDSach.ToString();
                            textBox_HoTen.Text = result.HoTen;
                            textBox_TenSach.Text = result.TenSach;
                        }
                        else
                        {
                            textBox_IDNguoiDung.Text = null;
                            textBox_IDSach.Text = null;
                            textBox_HoTen.Text = null;
                            textBox_TenSach.Text = null;
                        }
                    }
                    else
                    {
                        textBox_IDNguoiDung.Text = null;
                        textBox_IDSach.Text = null; // Nếu không thể chuyển đổi, đặt giá trị textbox kết quả thành null
                        textBox_HoTen.Text = null;
                        textBox_TenSach.Text = null;
                    }
                }
                else
                {
                    textBox_IDNguoiDung.Text = null;
                    textBox_IDSach.Text = null; // Nếu textbox rỗng, đặt giá trị textbox kết quả thành null
                    textBox_HoTen.Text = null;
                    textBox_TenSach.Text = null;
                }
            }
        }

        private void CheckInputForXacNhanTraSach(ref bool check)
        {
            List<string> missingFields = new List<string>();

            if (string.IsNullOrWhiteSpace(textBox_IDMuonTra.Text))
                missingFields.Add("ID Mượn trả");

            if (missingFields.Count == 0) check = true;
            else
            {
                check = false;
                string missingFieldsMessage = "Các trường sau không được để trống:\n";
                foreach (string field in missingFields)
                {
                    missingFieldsMessage += field + "\n";
                }
                MessageBox.Show(missingFieldsMessage, "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void TraSach() //Hàm xử lí trả sách
        {
            int selectedID = Convert.ToInt32(textBox_IDMuonTra.Text);
            using (QLTVEntities db = new QLTVEntities())
            {
                MuonTraSach muonTraSach = db.MuonTraSach.Find(selectedID);
                int idNguoiDung = (int)db.MuonTraSach.Where(s => s.IDMuonTra == selectedID).Select(s => s.IDNguoiDung).FirstOrDefault();
                if (muonTraSach.NgayTraThucTe != null)
                {
                    if (MessageBox.Show($"Sinh viên {muonTraSach.HoTen} đã trả sách \nBạn muốn cập nhật lại ngày trả sách của sinh viên {muonTraSach.HoTen} ?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {

                        muonTraSach.NgayTraThucTe = dateTimePicker_NgayTraSach.Value;
                        if ((dateTimePicker_NgayTraSach.Value - muonTraSach.NgayTraDuKien.Value).TotalDays > 0)
                        {
                            muonTraSach.SoTienPhat = 50000;
                        }
                        else muonTraSach.SoTienPhat = 0;
                        muonTraSach.TinhTrang = "Đã trả";
                        db.SaveChanges();
                        MessageBox.Show("Xác nhận trả sách thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else MessageBox.Show("Xác nhận trả sách thất bại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }    
                else if (muonTraSach.NgayTraThucTe == null)
                {
                    DialogResult result = MessageBox.Show($"Xác nhận trả sách cho sinh viên {muonTraSach.HoTen} ?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result == DialogResult.Yes)
                    {
                        muonTraSach.NgayTraThucTe = dateTimePicker_NgayTraSach.Value;
                        if ((dateTimePicker_NgayTraSach.Value - muonTraSach.NgayTraDuKien.Value).TotalDays > 0)
                        {
                            muonTraSach.SoTienPhat = 50000;
                        }
                        else muonTraSach.SoTienPhat = 0;
                        muonTraSach.TinhTrang = "Đã trả";
                        db.SaveChanges();
                        MessageBox.Show("Xác nhận trả sách thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else MessageBox.Show("Xác nhận trả sách thất bại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void UpdateSoLuongSachSauKhiTra(int idSach) //Hàm cập nhật số lượng sách khi mượn trả
        {
            using (QLTVEntities db = new QLTVEntities())
            {
                Sach sach = db.Sach.Find(idSach);
                sach.SoLuong++;
                db.SaveChanges();
            }
        }

        private void ResetControl() //Hàm đặt input về null
        {
            textBox_IDMuonTra.Text = null;
            textBox_IDNguoiDung.Text = null;
            textBox_IDSach.Text = null;
            dateTimePicker_NgayTraSach.Value = DateTime.Now;
        }
        #endregion

        private void textBox_IDMuonTra_TextChanged(object sender, EventArgs e)
        {
            BindingDataByIdMuonTra();
        }
        private void button_SaveInsert_Click(object sender, EventArgs e)
        {
            CheckInputForXacNhanTraSach(ref checkInputInsert);
            if (checkInputInsert == true)
            {
                TraSach();
                UpdateSoLuongSachSauKhiTra(Convert.ToInt32(textBox_IDSach.Text));
            }
        }

        private void button_ResetInsert_Click(object sender, EventArgs e)
        {
            ResetControl();
        }
    }
}
