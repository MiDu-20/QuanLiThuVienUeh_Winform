using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLiThuVienUeh.admin
{
    public partial class ffc_ThemXoaNhanVien : Form
    {
        //Variables
        #region Variables
        bool checkInputInsert = true; //Biến check đầu vào khi Insert
        bool checkInputDelete = true; //Biến check đầu vào khi Delete
        #endregion

        public ffc_ThemXoaNhanVien()
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

            string[] ChucVus = { "Manager", "Staff" };
            comboBox_ChucVuInput.DataSource = ChucVus;
            comboBox_ChucVuInput.SelectedIndex = -1;
        }

        //Hàm thêm dữ liệu vào datetimePicker
        private void AddValueDateTimePicker()
        {
            dateTimePicker_NgayNhanViecInput.Value = DateTime.Now;
            dateTimePicker_NgaySinhInput.Value = DateTime.Now;
        }

        //Hàm check input khi insert
        private void CheckInputForInsertNhanVien(ref bool check)
        {
            //List chứa các trường bị trống
            List<string> missingFields = new List<string>();

            if (string.IsNullOrWhiteSpace(textBox_HoVaTenInput.Text))
                missingFields.Add("Họ và tên");
            if (string.IsNullOrWhiteSpace(comboBox_ChucVuInput.Text))
                missingFields.Add("Chức vụ");
            if (string.IsNullOrWhiteSpace(comboBox_GioiTinhInput.Text))
                missingFields.Add("Giới tính");
            if (dateTimePicker_NgaySinhInput.Value == null)
                missingFields.Add("Ngày sinh");
            if (string.IsNullOrWhiteSpace(textBox_SoDienThoaiInput.Text))
                missingFields.Add("Số điện thoại");
            if (dateTimePicker_NgayNhanViecInput.Value == null)
                missingFields.Add("Ngày nhận việc");

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
        private string EmailCreating(string HoVaTen) //Hàm tạo email
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
            for (int i = 0; i < arr1.Length-1; i++)
            {
                HoVaTen = HoVaTen.Replace(arr1[i], arr2[i]);
                HoVaTen = HoVaTen.Replace(arr1[i].ToUpper(), arr2[i].ToUpper());
            }
            return NameSolving(HoVaTen);
        }

        private string NameSolving(string Input) //Hàm xử lí tên
        {
            if (string.IsNullOrEmpty(Input)) return "";
            string[] textsplit = Input.Split(' ');
            string newtext = "";
            for (int i = 0; i < textsplit.Length; i++)
            {
                newtext += textsplit[i];
            }
            newtext = (textsplit[textsplit.Length - 1] + textsplit[0]).ToLower();
            newtext += "@ueh.edu.vn";
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
            private void AddNhanVien()
            {
                DialogResult result = MessageBox.Show($"Bạn có chắc muốn thêm nhân viên {textBox_HoVaTenInput.Text} không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    MemoryStream avatarStream = new MemoryStream();
                    if (guna2CirclePictureBox_Avatar.Image != null)
                        guna2CirclePictureBox_Avatar.Image.Save(avatarStream, ImageFormat.Png);
                    using (QLTVEntities db = new QLTVEntities())
                    {
                        NhanVien nhanVien = new NhanVien();

                        nhanVien.IDNhanVien = db.NhanVien.Max(s => s.IDNhanVien) + 1;
                        nhanVien.HoTen = textBox_HoVaTenInput.Text;
                        nhanVien.GioiTinh = comboBox_GioiTinhInput.Text;
                        nhanVien.ChucVu = comboBox_ChucVuInput.Text;
                        nhanVien.NgaySinh = dateTimePicker_NgaySinhInput.Value;
                        nhanVien.Email = EmailCreating(textBox_HoVaTenInput.Text);
                        nhanVien.SoDienThoai = textBox_SoDienThoaiInput.Text;
                        nhanVien.NgayNhanViec = dateTimePicker_NgayNhanViecInput.Value;
                        if (guna2CirclePictureBox_Avatar.Image != null)
                            nhanVien.Avatar = avatarStream.ToArray();
                        else nhanVien.Avatar = null;

                        db.NhanVien.Add(nhanVien);
                        db.SaveChanges();
                        NhanVien nhanVienMoi = db.NhanVien.Find(db.NhanVien.Count() + 1);
                        string randomPassword = RandomDefaultPassword();
                    TaiKhoanNhanVien taiKhoanNhanVien = new TaiKhoanNhanVien()
                    {
                        IDTaiKhoanNhanVien = db.TaiKhoanNhanVien.Max(s => s.IDTaiKhoanNhanVien) + 1,
                            IDNhanVien = nhanVien.IDNhanVien,
                            ChucVu = nhanVien.ChucVu,
                            Email = nhanVien.Email,
                            MatKhau = randomPassword
                        };
                        db.TaiKhoanNhanVien.Add(taiKhoanNhanVien);
                        db.SaveChanges();
                        MessageBox.Show("Thêm nhân viên thành công\nEmail là: "+taiKhoanNhanVien.Email.ToString()+"\nMật khẩu là: "+taiKhoanNhanVien.MatKhau.ToString(), "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else { MessageBox.Show("Thêm nhân viên thất bại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information); }
            }

        //Hàm đặt các input về null
        private void ResetControl()
        {
            textBox_HoVaTenInput.Text = null;
            comboBox_ChucVuInput.SelectedIndex = -1;
            comboBox_GioiTinhInput.SelectedIndex = -1;
            dateTimePicker_NgayNhanViecInput.Value = DateTime.Now;
            textBox_SoDienThoaiInput.Text = null;
            dateTimePicker_NgaySinhInput.Value = DateTime.Now;
        }

        //Hàm check input khi delete
        private void CheckInputForDeleteNhanVien(ref bool check)
        {
            if (!string.IsNullOrWhiteSpace(textBox_IDDeleteInput.Text)) check = true;
            else
            {
                MessageBox.Show($"Trường {textBox_IDDeleteInput.Text} không được để trống", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                check = false;
            }
        }

        //Hàm delete dữ liệu
        private void DeleteNhanVien()
        {
            DialogResult result = MessageBox.Show($"Bạn có chắc muốn xóa nhân viên có ID: {textBox_IDDeleteInput.Text} không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                using (QLTVEntities db = new QLTVEntities())
                {
                    int idDelete = Convert.ToInt32(textBox_IDDeleteInput.Text);
                    NhanVien nhanVien = db.NhanVien.Where(p => p.IDNhanVien == idDelete).SingleOrDefault();
                    db.NhanVien.Remove(nhanVien);
                    db.SaveChanges();
                    MessageBox.Show("Xóa nhân viên thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else MessageBox.Show("Xóa nhân viên thất bại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            if (dateTimePicker_NgaySinhInput.Value > dateTimePicker_NgayNhanViecInput.Value)
            {
                MessageBox.Show("Ngày sinh không được lớn hơn ngày nhận việc của nhân viên", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                dateTimePicker_NgaySinhInput.Value = dateTimePicker_NgayNhanViecInput.Value;
            }
        }

        private void dateTimePicker_NgayNhanViecInput_ValueChanged(object sender, EventArgs e)
        {
            if (dateTimePicker_NgayNhanViecInput.Value < dateTimePicker_NgaySinhInput.Value)
            {
                MessageBox.Show("Ngày nhận việc không được nhỏ hơn ngày sinh của nhân viên", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                dateTimePicker_NgayNhanViecInput.Value = dateTimePicker_NgaySinhInput.Value;
            }
        }

        private void button_SaveInsert_Click(object sender, EventArgs e)
        {
            CheckInputForInsertNhanVien(ref checkInputInsert);
            if (checkInputInsert == true) AddNhanVien();
        }

        private void button_ResetInsert_Click(object sender, EventArgs e)
        {
            ResetControl();
        }

        private void button_SaveDelete_Click(object sender, EventArgs e)
        {
            CheckInputForDeleteNhanVien(ref checkInputDelete);
            if (checkInputDelete == true) DeleteNhanVien();
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
                guna2CirclePictureBox_Avatar.Image = avatar;
            }
        }
        #endregion
    }
}
