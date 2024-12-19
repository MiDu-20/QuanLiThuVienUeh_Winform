using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLiThuVienUeh.admin
{
    public partial class ffc_ThemXoaSach : Form
    {
        //Variables
        #region Variables
        bool checkInputInsert = true; //Biến check đầu vào khi Insert
        bool checkInputDelete = true; //Biến check đầu vào khi Delete
        #endregion

        public ffc_ThemXoaSach()
        {
            InitializeComponent();
            AddComboBoxTheLoai();
            AddComboBoxNhaXuatBan();
        }
        private void AddComboBoxTheLoai() //Thêm dữ liệu vào comboBox Thể loại
        {
            using (QLTVEntities db = new QLTVEntities())
            {
                var distinctTheLoai = db.Sach.Select(s => s.TheLoai).Distinct().ToList();

                // Xóa dữ liệu cũ trong ComboBox (nếu có)
                comboBox1.Items.Clear();

                // Thêm các giá trị distinct vào ComboBox
                foreach (var theLoai in distinctTheLoai)
                {
                    comboBox1.Items.Add(theLoai);
                }
            }
        }
        private void AddComboBoxNhaXuatBan() //Thêm dữ liệu vào comboBox Thể loại
        {
            using (QLTVEntities db = new QLTVEntities())
            {
                var distinctNhaXuatBan = db.Sach.Select(s => s.NhaXuatBan).Distinct().ToList();

                // Xóa dữ liệu cũ trong ComboBox (nếu có)
                comboBox2.Items.Clear();

                // Thêm các giá trị distinct vào ComboBox
                foreach (var theLoai in distinctNhaXuatBan)
                {
                    comboBox2.Items.Add(theLoai);
                }
            }
        }

        //Functions
        #region Functions
        //Hàm check input khi Insert
        private void CheckInputForInsertSach(ref bool check)
        {
            List<string> missingFields = new List<string>();

            if (string.IsNullOrWhiteSpace(textBox_TenSachInsertInput.Text))
                missingFields.Add("Tên sách");
            if (string.IsNullOrWhiteSpace(textBox_TacGiaInsertInput.Text))
                missingFields.Add("Tác giả");
            if (string.IsNullOrWhiteSpace(comboBox1.Text))
                missingFields.Add("Thể loại");
            if (string.IsNullOrWhiteSpace(textBox_SoLuongInsertInput.Text))
                missingFields.Add("Số lượng");
            if (string.IsNullOrWhiteSpace(comboBox2.Text))
                missingFields.Add("Nhà xuất bản");
            if (dateTimePicker_NamXuatBanInsertInput.Value == null)
                missingFields.Add("Năm xuất bản");

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

        private void AddSach() //Hàm insert sách
        {
            DialogResult result = MessageBox.Show($"Bạn có chắc muốn thêm sách {textBox_TenSachInsertInput.Text} không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                MemoryStream avatarStream = new MemoryStream();
                if (pictureBox_Avatar.Image != null)
                    pictureBox_Avatar.Image.Save(avatarStream, ImageFormat.Png);
                using (QLTVEntities db = new QLTVEntities())
                {
                    Sach sach = new Sach();
                    sach.IDSach = db.Sach.Count() + 1;
                    sach.TenSach = textBox_TenSachInsertInput.Text;
                    sach.TacGia = textBox_TacGiaInsertInput.Text;
                    sach.TheLoai = comboBox1.Text;
                    sach.SoLuong = Convert.ToInt32(textBox_SoLuongInsertInput.Text);
                    sach.NhaXuatBan = comboBox2.Text;
                    sach.NamXuatBan = dateTimePicker_NamXuatBanInsertInput.Value;
                    if (pictureBox_Avatar.Image != null)
                        sach.Avatar = avatarStream.ToArray();
                    else sach.Avatar = null;
                    db.Sach.Add(sach);
                    db.SaveChanges();
                    MessageBox.Show("Thêm sách thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else { MessageBox.Show("Thêm sách chưa thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information); }
        }

        private void ResetControl() //Hàm đặt các input về null
        {
            textBox_TenSachInsertInput.Text = null;
            textBox_TacGiaInsertInput.Text = null;
            comboBox1.Text = null;
            textBox_SoLuongInsertInput.Text = null;
            comboBox2.Text = null;
            dateTimePicker_NamXuatBanInsertInput.Value = DateTime.Now;
        }

        //Hàm check input khi Delete
        private void CheckInputForDeleteSach(ref bool check)
        {
            if (!string.IsNullOrWhiteSpace(textBox_IDSachDeleteInput.Text)) check = true;
            else
            {
                MessageBox.Show($"Trường {textBox_IDSachDeleteInput.Text} không được để trống", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                check = false;
            }
        }

        private void DeleteSach() //Hàm delete sách
        {
            DialogResult result = MessageBox.Show($"Bạn có chắc muốn xóa sách có ID: {textBox_IDSachDeleteInput.Text} không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                using (QLTVEntities db = new QLTVEntities())
                {
                    int idDelete = Convert.ToInt32(textBox_IDSachDeleteInput.Text);
                    if (db.Sach.Where(p => p.IDSach == idDelete).Count() > 0)
                    {
                        Sach sach = db.Sach.Where(p => p.IDSach == idDelete).SingleOrDefault();
                        db.Sach.Remove(sach);
                        db.SaveChanges();
                        MessageBox.Show("Xóa sách thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                        MessageBox.Show("ID sách không tồn tại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else MessageBox.Show("Xóa sách thất bại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void ResetIdDelete() //Hàm đặt input về null
        {
            textBox_IDSachDeleteInput.Text = null;
        }
        #endregion

        //Events
        #region Events
        private void button_SaveInsert_Click(object sender, EventArgs e)
        {
            CheckInputForInsertSach(ref checkInputInsert);
            if (checkInputInsert == true) AddSach();
        }

        private void button_ResetInsert_Click(object sender, EventArgs e)
        {
            ResetControl();
        }

        private void button_SaveDelete_Click(object sender, EventArgs e)
        {
            CheckInputForDeleteSach(ref checkInputDelete);
            if (checkInputDelete == true) DeleteSach();
        }

        private void button_ResetDelete_Click(object sender, EventArgs e)
        {
            ResetIdDelete();
        }

        private void button_Upload_Click(object sender, EventArgs e)
        {
            if (openFileDialog_Avatar.ShowDialog() == DialogResult.OK)
            {
                string file = openFileDialog_Avatar.FileName;
                if (string.IsNullOrEmpty(file))
                    return;
                Image avatar = Image.FromFile(file);
                pictureBox_Avatar.Image = avatar;
            }
        }
        #endregion

        private void textBox_TacGiaInsertInput_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsDigit(e.KeyChar))
            {
                //e.Handled = true;
                MessageBox.Show("Tên không được chứa số");
            }
        }

        private void textBox_SoLuongInsertInput_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (textBox_SoLuongInsertInput.Text.Length > 10) MessageBox.Show("Số điện thoại chỉ chứa 10 số");
            // Lấy văn bản trong TextBox
            string text = textBox_SoLuongInsertInput.Text;
            if (!Regex.IsMatch(text, @"^[0-9]*$"))
            {
                // Nếu văn bản không chỉ chứa số, hiển thị thông báo hoặc xử lý theo ý của bạn
                MessageBox.Show("Vui lòng chỉ nhập số.");
                // Có thể xóa ký tự cuối cùng không phải là số nếu muốn
                textBox_SoLuongInsertInput.Text = text.Substring(0, text.Length - 1);
                // Hoặc có thể xóa toàn bộ văn bản không phải số
                // textBox1.Text = Regex.Replace(text, @"[^0-9]", "");
                // Đặt con trỏ vào cuối TextBox
                textBox_SoLuongInsertInput.SelectionStart = textBox_SoLuongInsertInput.Text.Length;
            }
        }

        private void dateTimePicker_NamXuatBanInsertInput_ValueChanged(object sender, EventArgs e)
        {
            if (dateTimePicker_NamXuatBanInsertInput.Value > DateTime.Now)
            {
                MessageBox.Show("Năm xuất bản không được lớn hơn ngày hiện tại");
                dateTimePicker_NamXuatBanInsertInput.Value = DateTime.Now;
            }
        }
    }
}
