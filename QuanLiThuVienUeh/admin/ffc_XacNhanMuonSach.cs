using QuanLiThuVienUeh.nguoidung;
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
    public partial class ffc_XacNhanMuonSach : Form
    {
        //Variables
        #region Variables
        bool checkInputInsert = true; //Biến check input khi Insert
        int pageNumber = 1; //Biến thể hiện trang hiện tại
        int numberRecord = 5; //Biến thể hiện số dòng hiển thị
        int totalRecord = 0; //Biến chứa tổng số dòng trong bảng
        int lastPageNumber = 0; //Biến thể hiện trang cuối cùng trong bảng
        List<Button> buttonChangePageList = new List<Button>(); //List chứa các button phân trang
        int coupon; //Biến chứa coupon đổi thưởng
        #endregion

        public ffc_XacNhanMuonSach()
        {
            InitializeComponent();
            LoadData();
            dateTimePicker_NgayMuonSach.Value = DateTime.Now;
        }

        //Functions
        #region Functions
        private void ResetLabelTextToNull(Label label) //Đặt text của lable về null
        {
            label.Text = null;
        }

        private void SetLabelText(Label label, string text) //Set text cho label
        {
            label.Text = text;
        }

        private void FocusTextBox(TextBox textBox) //Focus vào textBox
        {
            textBox.Focus();
        }

        private void SearchNhanViensByGeneral() //Hàm tìm kiếm nhân viên 1 cách tổng quát
        {
            using (QLTVEntities db = new QLTVEntities())
            {
                var result = db.PhieuMuonSach.Where(c => c.IDNguoiDung.ToString().Contains(textBox_SearchName.Text)
                                                     || c.HoTen.Contains(textBox_SearchName.Text)
                                                     || c.IDSach.ToString().Contains(textBox_SearchName.Text)
                                                     || c.TenSach.Contains(textBox_SearchName.Text));
                if (result != null)
                {
                    totalRecord = result.Count();
                    lastPageNumber = (int)Math.Ceiling((double)totalRecord / numberRecord);
                    result = result.OrderBy(s => s.IDPhieuMuonSach)
                                 .Skip((pageNumber - 1) * numberRecord)
                                 .Take(numberRecord);
                    dataGridView_PhieuMuonSach.DataSource = result.Select(s => new
                    {
                        s.IDPhieuMuonSach,
                        s.IDNguoiDung,
                        s.HoTen,
                        s.IDSach,
                        s.TenSach,
                        s.Coupon,
                    }).ToList();
                    AdjustRowHeight();
                    AdjustColumnWidth();
                    ChangeHeader();
                }
            }
        }

        private void LoadData() //Hàm để hiển thị dữ liệu
        {
            using (QLTVEntities db = new QLTVEntities())
            {
                totalRecord = db.PhieuMuonSach.Count(); //Lấy ra tổng số dòng trong bảng
            }
            lastPageNumber = (int)Math.Ceiling((double)totalRecord / numberRecord); //Công thức tính trang cuối cùng trong bảng
            dataGridView_PhieuMuonSach.DataSource = LoadRecord(pageNumber, numberRecord); //Hiển thị lên dataGridView
            AdjustRowHeight(); //Customize lại height các dòng
            AdjustColumnWidth(); //Customize lại width các cột
            ChangeHeader(); //Thay đổi tiêu đề hiển thị trên dataGridView

        }

        List<object> LoadRecord(int page, int recordNum) //Hàm phân trang 
        {
            List<object> result = new List<object>();
            using (QLTVEntities db = new QLTVEntities())
            {
                result = db.PhieuMuonSach.OrderBy(e => e.IDPhieuMuonSach)
                    .Skip((page - 1) * recordNum)
                    .Take(recordNum)
                    .Select(s => new
                    {
                        s.IDPhieuMuonSach,
                        s.IDNguoiDung,
                        s.HoTen,
                        s.IDSach,
                        s.TenSach,
                        s.Coupon,
                    }).ToList<object>();
            }
            return result;
        }

        public void AdjustRowHeight() //Hàm customize lại height các dòng
        {
            //Biến thể hiện height của các dòng sao cho bằng nhau
            int desiredHeight = dataGridView_PhieuMuonSach.Height / (dataGridView_PhieuMuonSach.Rows.Count + 1);
            if (dataGridView_PhieuMuonSach.Rows.Count > 0 && dataGridView_PhieuMuonSach.Rows.Count < 5)
            {
                foreach (DataGridViewRow row in dataGridView_PhieuMuonSach.Rows)
                {
                    row.Height = 50;
                }
            }
            else
            {
                // Thiết lập chiều cao cho mỗi dòng
                foreach (DataGridViewRow row in dataGridView_PhieuMuonSach.Rows)
                {
                    row.Height = desiredHeight;
                }
            }
        }

        private void AdjustColumnWidth() //Hàm customize lại width các dòng
        {
            if (dataGridView_PhieuMuonSach.Columns.Count > 0)
            {
                dataGridView_PhieuMuonSach.Columns[0].Width = dataGridView_PhieuMuonSach.Width * 10 / 100;
                dataGridView_PhieuMuonSach.Columns[1].Width = dataGridView_PhieuMuonSach.Width * 17 / 100;
                dataGridView_PhieuMuonSach.Columns[2].Width = dataGridView_PhieuMuonSach.Width * 23 / 100;
                dataGridView_PhieuMuonSach.Columns[3].Width = dataGridView_PhieuMuonSach.Width * 10 / 100;
                dataGridView_PhieuMuonSach.Columns[4].Width = dataGridView_PhieuMuonSach.Width * 30 / 100;
                dataGridView_PhieuMuonSach.Columns[5].Width = dataGridView_PhieuMuonSach.Width * 10 / 100;
            }
        }

        private void ChangeHeader() //Hàm thay đổi tiêu đề hiển thị trên dataGridView
        {
            if (dataGridView_PhieuMuonSach.Columns.Count > 0)
            {
                dataGridView_PhieuMuonSach.Columns[0].HeaderText = "STT";
                dataGridView_PhieuMuonSach.Columns[1].HeaderText = "MSSV";
                dataGridView_PhieuMuonSach.Columns[2].HeaderText = "Họ tên";
                dataGridView_PhieuMuonSach.Columns[3].HeaderText = "ID Sách";
                dataGridView_PhieuMuonSach.Columns[4].HeaderText = "Tên sách";
                dataGridView_PhieuMuonSach.Columns[5].HeaderText = "Coupon";
            }
        }

        private void AddButtonChangePageList() //Hàm thêm các button phân trang vào list phân trang
        {
            buttonChangePageList.Add(button_ChangePage1);
            buttonChangePageList.Add(button_ChangePage2);
            buttonChangePageList.Add(button_ChangePage3);
            buttonChangePageList.Add(button_ReturnFirstPage);
            buttonChangePageList.Add(button_ReturnLastPage);
        }

        //Hàm tạo thứ tự cho các button phân trang căn cứ vào trang hiện tại
        private void CreateOrderForButtonChangePageByPageNumber(int pageNumber)
        {
            button_ChangePage1.Text = (pageNumber - 1).ToString();
            button_ChangePage2.Text = pageNumber.ToString();
            button_ChangePage3.Text = (pageNumber + 1).ToString();
        }

        //Hàm đặt các button phân trang về mặc định: 1 2 3
        private void SetDefaultButtonChangePageText()
        {
            button_ChangePage1.Text = "1";
            button_ChangePage2.Text = "2";
            button_ChangePage3.Text = "3";
        }

        //Hàm tạo thứ tự cho các button phân trang căn cứ vào trang cuối cùng
        private void CreateOrderForButtonChangePageByLastPageNumber(int lastPageNumber)
        {
            button_ChangePage1.Text = (lastPageNumber - 2).ToString();
            button_ChangePage2.Text = (lastPageNumber - 1).ToString();
            button_ChangePage3.Text = lastPageNumber.ToString();
        }

        private void ResetColorButton() //Hàm đặt lại màu của các button phân trang
        {
            AddButtonChangePageList();
            foreach (Button button in buttonChangePageList)
            {
                button.BackColor = Color.White;
                button.ForeColor = Color.Black;
            }
        }

        private void HighlightButtonCurrentPage(object obj) //Hàm hightlight button phân trang được truyền vào
        {
            Button sender = obj as Button;
            sender.BackColor = Color.FromArgb(0, 95, 105);
            sender.ForeColor = Color.White;
        }
        private void BindingDataSelected()
        {
            int selectedID = Convert.ToInt32(dataGridView_PhieuMuonSach.SelectedCells[0].OwningRow.Cells["IDPhieuMuonSach"].Value.ToString());
            using (QLTVEntities db = new QLTVEntities())
            {
                PhieuMuonSach tk = db.PhieuMuonSach.Find(selectedID);
                textBox_IDNguoiDung.Text = tk.IDNguoiDung.ToString();
                textBox_HoVaTenNguoiDung.Text = tk.HoTen;
                textBox_IDSach.Text = tk.IDSach.ToString();
                textBox_TenSach.Text = tk.TenSach;
                if (tk.Coupon == true)
                    checkBox_Coupon.CheckState = CheckState.Checked;
                else if (tk.Coupon == false)
                    checkBox_Coupon.CheckState = CheckState.Unchecked;
            }
        }

        private void CheckInputForXacNhanMuonSach(ref bool check)
        {
            List<string> missingFields = new List<string>();

            if (string.IsNullOrWhiteSpace(textBox_IDNguoiDung.Text))
                missingFields.Add("ID Người dùng");
            if (string.IsNullOrWhiteSpace(textBox_HoVaTenNguoiDung.Text))
                missingFields.Add("Họ và tên");
            if (string.IsNullOrWhiteSpace(textBox_IDSach.Text))
                missingFields.Add("ID Sách");
            if (string.IsNullOrWhiteSpace(textBox_TenSach.Text))
                missingFields.Add("Tên sách");
            if (dateTimePicker_NgayMuonSach.Value == null)
                missingFields.Add("Ngày mượn sách");

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

        private void MuonSach() //Hàm xử lí mượn sách
        {
            DialogResult result = MessageBox.Show($"Xác nhận mượn sách cho sinh viên {textBox_HoVaTenNguoiDung.Text} ?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                using (QLTVEntities db = new QLTVEntities())
                {
                    int selectedID = Convert.ToInt32(dataGridView_PhieuMuonSach.SelectedCells[0].OwningRow.Cells["IDPhieuMuonSach"].Value.ToString());
                    if (db.PhieuMuonSach.Find(selectedID).Coupon == true)
                        coupon = 7;
                    else coupon = 0;
                    MuonTraSach muontrasach = new MuonTraSach();
                    muontrasach.IDMuonTra = Convert.ToInt32(db.MuonTraSach.Count() + 1);
                    muontrasach.IDNguoiDung = Convert.ToInt32(textBox_IDNguoiDung.Text);
                    muontrasach.HoTen = textBox_HoVaTenNguoiDung.Text;
                    muontrasach.IDSach = Convert.ToInt32(textBox_IDSach.Text);
                    muontrasach.TenSach = textBox_TenSach.Text;
                    muontrasach.NgayMuon = dateTimePicker_NgayMuonSach.Value;
                    muontrasach.NgayTraDuKien = dateTimePicker_NgayMuonSach.Value.AddDays(7 + coupon);
                    muontrasach.SoTienPhat = 0;
                    muontrasach.TinhTrang = "Đang mượn";
                    PhieuMuonSach pms = db.PhieuMuonSach.Find(selectedID);
                    db.PhieuMuonSach.Remove(pms);
                    db.MuonTraSach.Add(muontrasach);
                    db.SaveChanges();
                    MessageBox.Show("Xác nhận mượn sách thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    
                    
                }
            }
            else { MessageBox.Show("Xác nhận mượn sách thất bại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information); }
        }

        private void UpdateSoLuongSachSauKhiMuon(int idSach) //Hàm cập nhật số lượng sách khi mượn trả
        {
            using (QLTVEntities db = new QLTVEntities())
            {
                Sach sach = db.Sach.Find(idSach);
                sach.SoLuong--;
                db.SaveChanges();
            }
        }

        private void ResetControl() //Hàm đặt input về null
        {
            textBox_IDNguoiDung.Text = null;
            dateTimePicker_NgayMuonSach.Value = DateTime.Now;
            textBox_HoVaTenNguoiDung.Text = null;
            textBox_IDSach.Text = null;
            textBox_TenSach.Text = null;
        }
        #endregion

        //Events
        #region Events
        private void ffc_XacNhanMuonSach_Load(object sender, EventArgs e)
        {
            AdjustColumnWidth();
            AdjustRowHeight();
        }

        private void textBox_SearchName_TextChanged(object sender, EventArgs e)
        {
            if (textBox_SearchName.Text.Length != 0)
            {
                ResetLabelTextToNull(label_SearchName);//Nếu text trong ô textBox được nhập thì xóa label Search
                pageNumber = 1;
                SetDefaultButtonChangePageText();
                ResetColorButton();
                HighlightButtonCurrentPage(button_ChangePage1);
                SearchNhanViensByGeneral();
            }
            else
            {
                SetLabelText(label_SearchName, "Search by id, name, email..."); //Nếu text rỗng thì hiện lại label Search
                pageNumber = 1;
                SetDefaultButtonChangePageText();
                ResetColorButton();
                HighlightButtonCurrentPage(button_ChangePage1);
                LoadData();
            }
        }

        private void textBox_SearchName_Click(object sender, EventArgs e)
        {
            if (textBox_SearchName.Text.Length == 0)
                ResetLabelTextToNull(label_SearchName); //TextBox được click thì xóa label Search
        }

        private void label_SearchName_Click(object sender, EventArgs e)
        {
            FocusTextBox(textBox_SearchName); //Nếu click vào label Search thì chuyển Focus vào textBox
            ResetLabelTextToNull(label_SearchName); //Xóa label Search
        }

        private void button_ChangePage1_Click(object sender, EventArgs e)
        {
            pageNumber = Convert.ToInt32(button_ChangePage1.Text);

            if (button_ChangePage1.Text != "1")
            {
                if (textBox_SearchName.Text != null)
                {
                    SearchNhanViensByGeneral();
                    CreateOrderForButtonChangePageByPageNumber(pageNumber);
                    ResetColorButton();
                    HighlightButtonCurrentPage(button_ChangePage2);
                }
                else
                {
                    LoadData();
                    CreateOrderForButtonChangePageByPageNumber(pageNumber);
                    ResetColorButton();
                    HighlightButtonCurrentPage(button_ChangePage2);
                }
            }
            else
            {
                if (textBox_SearchName.Text != null)
                {
                    SearchNhanViensByGeneral();
                    ResetColorButton();
                    HighlightButtonCurrentPage(button_ChangePage1);
                }
                else
                {
                    LoadData();
                    ResetColorButton();
                    HighlightButtonCurrentPage(button_ChangePage1);
                }
            }
        }

        private void button_ChangePage2_Click(object sender, EventArgs e)
        {
            if (lastPageNumber == 1) return;
            pageNumber = Convert.ToInt32(button_ChangePage2.Text);
            if (textBox_SearchName.Text != null)
            {
                SearchNhanViensByGeneral();
                ResetColorButton();
                HighlightButtonCurrentPage(sender);
            }
            else
            {
                LoadData();
                ResetColorButton();
                HighlightButtonCurrentPage(sender);
            }
        }

        private void button_ChangePage3_Click(object sender, EventArgs e)
        {
            if (lastPageNumber <= 2) return;
            pageNumber = Convert.ToInt32(button_ChangePage3.Text);

            if (lastPageNumber > pageNumber)
            {
                if (textBox_SearchName.Text != null)
                {
                    SearchNhanViensByGeneral();
                    CreateOrderForButtonChangePageByPageNumber(pageNumber);
                    ResetColorButton();
                    HighlightButtonCurrentPage(button_ChangePage2);
                }
                else
                {
                    LoadData();
                    CreateOrderForButtonChangePageByPageNumber(pageNumber);
                    ResetColorButton();
                    HighlightButtonCurrentPage(button_ChangePage2);
                }
            }
            else
            {
                if (textBox_SearchName.Text != null)
                {
                    SearchNhanViensByGeneral();
                    ResetColorButton();
                    HighlightButtonCurrentPage(button_ChangePage3);
                }
                else
                {
                    LoadData();
                    ResetColorButton();
                    HighlightButtonCurrentPage(button_ChangePage3);
                }
            }
        }

        private void button_ReturnFirstPage_Click(object sender, EventArgs e)
        {
            pageNumber = 1;
            if (textBox_SearchName.Text != null)
            {
                SearchNhanViensByGeneral();
                ResetColorButton();
                HighlightButtonCurrentPage(button_ChangePage1);
                SetDefaultButtonChangePageText();
            }
            else
            {
                LoadData();
                ResetColorButton();
                HighlightButtonCurrentPage(button_ChangePage1);
                SetDefaultButtonChangePageText();
            }
        }

        private void button_ReturnLastPage_Click(object sender, EventArgs e)
        {
            pageNumber = lastPageNumber;
            if (textBox_SearchName.Text != null)
            {
                SearchNhanViensByGeneral();
                if (pageNumber == 1)
                {
                    ResetColorButton();
                    HighlightButtonCurrentPage(button_ChangePage1);
                    return;
                }
                else if (pageNumber == 2)
                {
                    ResetColorButton();
                    HighlightButtonCurrentPage(button_ChangePage2);
                    return;
                }
                else
                {
                    ResetColorButton();
                    HighlightButtonCurrentPage(button_ChangePage3);
                    CreateOrderForButtonChangePageByLastPageNumber(lastPageNumber);
                }
            }
            else
            {
                LoadData();
                if (pageNumber == 1)
                {
                    ResetColorButton();
                    HighlightButtonCurrentPage(button_ChangePage1);
                    return;
                }
                else if (pageNumber == 2)
                {
                    ResetColorButton();
                    HighlightButtonCurrentPage(button_ChangePage2);
                    return;
                }
                else
                {
                    ResetColorButton();
                    HighlightButtonCurrentPage(button_ChangePage3);
                    CreateOrderForButtonChangePageByLastPageNumber(lastPageNumber);
                }
            }
        }

        private void dataGridView_PhieuMuonSach_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < dataGridView_PhieuMuonSach.Rows.Count - 1)
            {
                BindingDataSelected();
            }
        }

        private void button_SaveInsert_Click(object sender, EventArgs e)
        {
            CheckInputForXacNhanMuonSach(ref checkInputInsert);
            if (checkInputInsert == true)
            {
                MuonSach();
                UpdateSoLuongSachSauKhiMuon(Convert.ToInt32(textBox_IDSach.Text));
            }
        }

        private void button_ResetInsert_Click(object sender, EventArgs e)
        {
            ResetControl();
        }
        #endregion

        private void dataGridView_PhieuMuonSach_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int idSach = Convert.ToInt32(dataGridView_PhieuMuonSach.Rows[e.RowIndex].Cells["IDSach"].Value);
            XemThongTinSachChiTiet(idSach);
        }
        private void XemThongTinSachChiTiet(int idSach)
        {
            ffc_ThongTinSachChiTiet form = new ffc_ThongTinSachChiTiet(idSach);
            form.Show();
        }
    }
}
