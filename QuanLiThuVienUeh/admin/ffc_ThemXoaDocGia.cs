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
using System.Web.UI.WebControls;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace QuanLiThuVienUeh.admin
{
    public partial class ffc_ThemXoaDocGia : Form
    {
        //Variables
        #region Variables
        bool checkInputInsert = true; //Biến check đầu vào khi Insert
        bool checkInputDelete = true; //Biến check đầu vào khi Delete
        #endregion

        public ffc_ThemXoaDocGia()
        {
            InitializeComponent();
            AddDataSourceComboBox();
            AddValueDateTimePicker();
        }

        //Functions
        #region Functions
        //Hàm thêm dữ liệu vào comboBox
        private void AddDataSourceComboBox()
        {
            string[] GioiTinhs = { "Nam", "Nữ" };
            comboBox_GioiTinhInput.DataSource = GioiTinhs;
            comboBox_GioiTinhInput.SelectedIndex = -1;
        }

        //Hàm thêm dữ liệu vào datetimePicker
        private void AddValueDateTimePicker()
        {
            dateTimePicker_NgaySinhInput.Value = DateTime.Now;

        }

        //Hàm check input khi insert
        private void CheckInputForInsertNguoiDung(ref bool check)
        {
            //List chứa các trường bị trống
            List<string> missingFields = new List<string>();

            if (string.IsNullOrWhiteSpace(textBox_HoVaTenInput.Text))
                missingFields.Add("Họ và tên");
            if (string.IsNullOrWhiteSpace(comboBox_GioiTinhInput.Text))
                missingFields.Add("Giới tính");
            if (string.IsNullOrWhiteSpace(textBox_SoDienThoaiInput.Text))
                missingFields.Add("Số điện thoại");
            if (dateTimePicker_NgaySinhInput.Value == null)
                missingFields.Add("Ngày sinh");
            if (string.IsNullOrWhiteSpace(textBox_ChuyenNganhInput.Text))
                missingFields.Add("Chuyên ngành");
            if (string.IsNullOrWhiteSpace(textBox_LopInput.Text))
                missingFields.Add("Lớp");

            if (missingFields.Count == 0)
                check = true;
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
        private string EmailCreating(string HoVaTen, int id) //Hàm tạo email
        {
            #region UnicodeChangingArray
            string[] arr1 = new string[] { "á", "à", "ả", "ã", "ạ", "â", "ấ", "ầ", "ẩ", "ẫ", "ậ", "ă", "ắ", "ằ", "ẳ", "ẵ", "ặ",
            "đ",
            "é","è","ẻ","ẽ","ẹ","ê","ế","ề","ể","ễ","ệ",
            "í","ì","ỉ","ĩ","ị",
            "ó","ò","ỏ","õ","ọ","ô","ố","ồ","ổ","ỗ","ộ","ơ","ớ","ờ","ở","ỡ","ợ",
            "ú","ù","ủ","ũ","ụ","ư","ứ","ừ","ử","ữ","ự",
            "ý","ỳ","ỷ","ỹ","ỵ",};
            string[] arr2 = new string[] { "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a",
            "d",
            "e","e","e","e","e","e","e","e","e","e","e",
            "i","i","i","i","i",
            "o","o","o","o","o","o","o","o","o","o","o","o","o","o","o","o","o",
            "u","u","u","u","u","u","u","u","u","u","u",
            "y","y","y","y","y",};
            #endregion
            for (int i = 0; i < arr1.Length; i++)
            {
                HoVaTen = HoVaTen.Replace(arr1[i], arr2[i]);
                HoVaTen = HoVaTen.Replace(arr1[i].ToUpper(), arr2[i].ToUpper());
            }
            return NameSolving(HoVaTen, id);
        }

        private string NameSolving(string Input, int id) //Hàm xử lí tên
        {
            if (string.IsNullOrEmpty(Input)) return "";
            string[] textsplit = Input.Split(' ');
            string newtext = (textsplit[textsplit.Length - 1] + textsplit[0]).ToLower();
            newtext += $".{id}@st.ueh.edu.vn";
            return newtext;
        }

        private string RandomDefaultPassword()
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

        //Hàm insert dữ liệu
        private void AddNguoiDung()
        {
            DialogResult result = MessageBox.Show($"Bạn có chắc muốn thêm độc giả {textBox_HoVaTenInput.Text} không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                MemoryStream avatarStream = new MemoryStream();
                if (guna2CirclePictureBox_Avatar.Image != null)
                    guna2CirclePictureBox_Avatar.Image.Save(avatarStream, ImageFormat.Png);
                using (QLTVEntities db = new QLTVEntities())
                {
                    NguoiDung nguoidung = new NguoiDung();

                    nguoidung.IDNguoiDung = db.NguoiDung.Max(p => p.IDNguoiDung) + 1;
                    nguoidung.HoTen = textBox_HoVaTenInput.Text;
                    nguoidung.GioiTinh = comboBox_GioiTinhInput.Text;
                    nguoidung.SoDienThoai = textBox_SoDienThoaiInput.Text;
                    nguoidung.NgaySinh = dateTimePicker_NgaySinhInput.Value;
                    nguoidung.ChuyenNganh = textBox_ChuyenNganhInput.Text;
                    nguoidung.Lop = textBox_LopInput.Text;
                    nguoidung.GioiThieu = textBox_GioiThieuInput.Text;
                    nguoidung.LichSuMuon = "Lịch sử mượn";
                    if (guna2CirclePictureBox_Avatar.Image != null)
                        nguoidung.Avatar = avatarStream.ToArray();
                    else nguoidung.Avatar = null;

                    db.NguoiDung.Add(nguoidung);
                    db.SaveChanges();
                    nguoidung.Email = EmailCreating(nguoidung.HoTen, nguoidung.IDNguoiDung);
                    db.SaveChanges();
                    string randomPassword = RandomDefaultPassword();
                    TaiKhoanNguoiDung taiKhoanNguoiDung = new TaiKhoanNguoiDung();
                    taiKhoanNguoiDung.IDTaiKhoanNguoiDung = db.TaiKhoanNguoiDung.Count() + 1;
                    taiKhoanNguoiDung.IDNguoiDung = nguoidung.IDNguoiDung;
                    taiKhoanNguoiDung.Email = nguoidung.Email;
                    taiKhoanNguoiDung.MatKhau = randomPassword;
                    db.TaiKhoanNguoiDung.Add(taiKhoanNguoiDung);
                    db.SaveChanges();
                    MessageBox.Show("Thêm độc giả thành công\nTài khoản đăng nhập của độc giả là: " + taiKhoanNguoiDung.Email.ToString() +"\nMật khẩu là: " + taiKhoanNguoiDung.MatKhau.ToString(), "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else { MessageBox.Show("Thêm độc giả thất bại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information); }
        }

        //Hàm đặt các input về null
        private void ResetControl()
        {
            textBox_HoVaTenInput.Text = null;
            comboBox_GioiTinhInput.SelectedIndex = -1;
            textBox_SoDienThoaiInput.Text = null;
            textBox_ChuyenNganhInput.Text = null;
            textBox_LopInput.Text = null;
            textBox_GioiThieuInput.Text = null;
            dateTimePicker_NgaySinhInput.Value = DateTime.Now;
        }

        //Hàm check input khi delete
        private void CheckInputForDeleteNguoiDung(ref bool check)
        {
            if (!string.IsNullOrWhiteSpace(textBox_IDDeleteInput.Text)) check = true;
            else
            {
                MessageBox.Show($"Trường {textBox_IDDeleteInput.Text} không được để trống", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                check = false;
            }
        }

        //Hàm delete dữ liệu
        private void DeleteNguoiDung()
        {
            DialogResult result = MessageBox.Show($"Bạn có chắc muốn xóa độc giả có ID: {textBox_IDDeleteInput.Text} không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                using (QLTVEntities db = new QLTVEntities())
                {
                    int idDelete = Convert.ToInt32(textBox_IDDeleteInput.Text);
                    if (db.NguoiDung.Where(p => p.IDNguoiDung == idDelete).Count() > 0)
                    {
                        NguoiDung nguoidung = db.NguoiDung.Where(p => p.IDNguoiDung == idDelete).FirstOrDefault();
                        db.NguoiDung.Remove(nguoidung);
                        db.SaveChanges();
                        MessageBox.Show("Xóa độc giả thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                        MessageBox.Show("ID Độc giả không tồn tại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else MessageBox.Show("Xóa độc giả thất bại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        //Hàm đặt các input về null
        private void ResetIdDelete()
        {
            textBox_IDDeleteInput.Text = null;
        }
        #endregion

        //Events
        #region Events
        private void dateTimePicker_NgaySinhInput_ValueChanged(object sender, EventArgs e)
        {
            if (dateTimePicker_NgaySinhInput.Value > DateTime.Now)
            {
                MessageBox.Show("Ngày sinh không được lớn hơn ngày hiện tại", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                dateTimePicker_NgaySinhInput.Value = DateTime.Now;
            }
            else if ((dateTimePicker_NgaySinhInput.Value - DateTime.Now).TotalDays > 50)
            {
                MessageBox.Show("Người này quá lớn tuổi \n Bạn có chắc muốn thêm độc giả ngày sinh này?");
            }
        }

        private void button_SaveInsert_Click(object sender, EventArgs e)
        {
            CheckInputForInsertNguoiDung(ref checkInputInsert);
            if (checkInputInsert == true) AddNguoiDung();
        }

        private void button_ResetInsert_Click(object sender, EventArgs e)
        {
            ResetControl();
        }

        private void button_SaveDelete_Click(object sender, EventArgs e)
        {
            CheckInputForDeleteNguoiDung(ref checkInputDelete);
            if (checkInputDelete == true) DeleteNguoiDung();
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
                System.Drawing.Image avatar = System.Drawing.Image.FromFile(file);
                guna2CirclePictureBox_Avatar.Image = avatar;
            }
        }
        #endregion

        private void textBox_HoVaTenInput_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsDigit(e.KeyChar))
            {
                //e.Handled = true;
                MessageBox.Show("Tên không được chứa số");
            }
        }

        private void comboBox_GioiTinhInput_TextChanged(object sender, EventArgs e)
        {
            // Lấy văn bản nhập vào ComboBox
            string input = comboBox_GioiTinhInput.Text;

            // Tìm kiếm xem có mục nào trong ComboBox khớp với văn bản nhập vào không
            bool found = false;
            foreach (var item in comboBox_GioiTinhInput.Items)
            {
                if (item.ToString().Equals(input, StringComparison.CurrentCultureIgnoreCase))
                {
                    found = true;
                    break;
                }
            }

            // Nếu không tìm thấy khớp, đặt SelectedIndex về -1
            if (!found)
            {
                comboBox_GioiTinhInput.SelectedIndex = -1;
            }
        }

        private void textBox_SoDienThoaiInput_TextChanged(object sender, EventArgs e)
        {
            if (textBox_SoDienThoaiInput.Text.Length > 10) MessageBox.Show("Số điện thoại chỉ chứa 10 số");
            // Lấy văn bản trong TextBox
            string text = textBox_SoDienThoaiInput.Text;
            if (!Regex.IsMatch(text, @"^[0-9]*$"))
            {
                // Nếu văn bản không chỉ chứa số, hiển thị thông báo hoặc xử lý theo ý của bạn
                MessageBox.Show("Vui lòng chỉ nhập số.");
                // Có thể xóa ký tự cuối cùng không phải là số nếu muốn
                textBox_SoDienThoaiInput.Text = text.Substring(0, text.Length - 1);
                // Hoặc có thể xóa toàn bộ văn bản không phải số
                // textBox1.Text = Regex.Replace(text, @"[^0-9]", "");
                // Đặt con trỏ vào cuối TextBox
                textBox_SoDienThoaiInput.SelectionStart = textBox_SoDienThoaiInput.Text.Length;
            }
        }
    }
}

