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
    public partial class ffc_ChinhSuaDocGia : Form
    {
        //Variables
        #region Variables
        bool menuExpand = false; //Biến hiển thị độ mở rộng của button con
        int pageNumber = 1; //Biến thể hiện trang hiện tại
        int numberRecord = 10; //Biến thể hiện số dòng hiển thị
        int totalRecord = 0; //Biến chứa tổng số dòng trong bảng
        int lastPageNumber = 0; //Biến thể hiện trang cuối cùng trong bảng
        List<Button> buttonChangePageList = new List<Button>(); //List chứa các button phân trang
        bool isChinhSuaThongTin = false;
        #endregion

        public ffc_ChinhSuaDocGia()
        {
            InitializeComponent();
            AddDataSourceComboBox();
            LoadData();
            panel_ChinhSuaTaiKhoan.Height = 0;
        }

        //Search function
        #region Search function

        #endregion

        //Functions
        #region Functions
        private void AddDataSourceComboBox()
        {
            string[] GioiTinhs = { "Nam", "Nữ" };
            comboBox_GioiTinhUpdateInput.DataSource = GioiTinhs;
            comboBox_GioiTinhUpdateInput.SelectedIndex = -1;
        }

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
                var result = db.NguoiDung.Where(c => c.IDNguoiDung.ToString().Contains(textBox_SearchName.Text)
                                                     || c.HoTen.Contains(textBox_SearchName.Text)
                                                     || c.Lop.Contains(textBox_SearchName.Text)
                                                     || c.ChuyenNganh.Contains(textBox_SearchName.Text)
                                                     || c.Email.Contains(textBox_SearchName.Text)
                                                     || c.SoDienThoai.Contains(textBox_SearchName.Text));
                if (result != null)
                {
                    totalRecord = result.Count();
                    lastPageNumber = (int)Math.Ceiling((double)totalRecord / numberRecord);
                    result = result.OrderBy(s => s.IDNguoiDung)
                                 .Skip((pageNumber - 1) * numberRecord)
                                 .Take(numberRecord);
                    dataGridView_ChinhSuaTaiKhoan.DataSource = result.Select(s => new
                    {
                        s.IDNguoiDung,
                        s.HoTen,
                        s.GioiTinh,
                        s.NgaySinh,
                        s.Lop,
                        s.ChuyenNganh,
                        s.Email,
                        s.SoDienThoai,
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
                totalRecord = db.NguoiDung.Count(); //Lấy ra tổng số dòng trong bảng
            }
            lastPageNumber = (int)Math.Ceiling((double)totalRecord / numberRecord); //Công thức tính trang cuối cùng trong bảng
            dataGridView_ChinhSuaTaiKhoan.DataSource = LoadRecord(pageNumber, numberRecord); //Hiển thị lên dataGridView
            AdjustRowHeight(); //Customize lại height các dòng
            AdjustColumnWidth(); //Customize lại width các cột
            ChangeHeader(); //Thay đổi tiêu đề hiển thị trên dataGridView

        }

        List<object> LoadRecord(int page, int recordNum) //Hàm phân trang 
        {
            List<object> result = new List<object>();
            using (QLTVEntities db = new QLTVEntities())
            {
                result = db.NguoiDung.OrderBy(e => e.IDNguoiDung)
                    .Skip((page - 1) * recordNum)
                    .Take(recordNum)
                    .Select(s => new
                    {
                        s.IDNguoiDung,
                        s.HoTen,
                        s.GioiTinh,
                        s.NgaySinh,
                        s.Lop,
                        s.ChuyenNganh,
                        s.Email,
                        s.SoDienThoai,
                    }).ToList<object>();
            }
            return result;
        }

        public void AdjustRowHeight() //Hàm customize lại height các dòng
        {
            //Biến thể hiện height của các dòng sao cho bằng nhau
            int desiredHeight = dataGridView_ChinhSuaTaiKhoan.Height / (dataGridView_ChinhSuaTaiKhoan.Rows.Count + 1);
            if (dataGridView_ChinhSuaTaiKhoan.Rows.Count > 0 && dataGridView_ChinhSuaTaiKhoan.Rows.Count < 10)
            {
                foreach (DataGridViewRow row in dataGridView_ChinhSuaTaiKhoan.Rows)
                {
                    row.Height = 50;
                }
            }
            else
            {
                // Thiết lập chiều cao cho mỗi dòng
                foreach (DataGridViewRow row in dataGridView_ChinhSuaTaiKhoan.Rows)
                {
                    row.Height = desiredHeight;
                }
            }
        }

        private void AdjustColumnWidth() //Hàm customize lại width các dòng
        {
            if (dataGridView_ChinhSuaTaiKhoan.Columns.Count > 0)
            {
                dataGridView_ChinhSuaTaiKhoan.Columns[0].Width = dataGridView_ChinhSuaTaiKhoan.Width * 10 / 100;
                dataGridView_ChinhSuaTaiKhoan.Columns[1].Width = dataGridView_ChinhSuaTaiKhoan.Width * 20 / 100;
                dataGridView_ChinhSuaTaiKhoan.Columns[2].Width = dataGridView_ChinhSuaTaiKhoan.Width * 10 / 100;
                dataGridView_ChinhSuaTaiKhoan.Columns[3].Width = dataGridView_ChinhSuaTaiKhoan.Width * 10 / 100;
                dataGridView_ChinhSuaTaiKhoan.Columns[4].Width = dataGridView_ChinhSuaTaiKhoan.Width * 10 / 100;
                dataGridView_ChinhSuaTaiKhoan.Columns[5].Width = dataGridView_ChinhSuaTaiKhoan.Width * 15 / 100;
                dataGridView_ChinhSuaTaiKhoan.Columns[6].Width = dataGridView_ChinhSuaTaiKhoan.Width * 15 / 100;
                dataGridView_ChinhSuaTaiKhoan.Columns[7].Width = dataGridView_ChinhSuaTaiKhoan.Width * 10 / 100;
            }
        }

        private void ChangeHeader() //Hàm thay đổi tiêu đề hiển thị trên dataGridView
        {
            if (dataGridView_ChinhSuaTaiKhoan.Columns.Count > 0)
            {
                dataGridView_ChinhSuaTaiKhoan.Columns[0].HeaderText = "ID";
                dataGridView_ChinhSuaTaiKhoan.Columns[1].HeaderText = "Tên";
                dataGridView_ChinhSuaTaiKhoan.Columns[2].HeaderText = "GT";
                dataGridView_ChinhSuaTaiKhoan.Columns[3].HeaderText = "Ngày sinh";
                dataGridView_ChinhSuaTaiKhoan.Columns[4].HeaderText = "Lớp";
                dataGridView_ChinhSuaTaiKhoan.Columns[5].HeaderText = "Chuyên ngành";
                dataGridView_ChinhSuaTaiKhoan.Columns[6].HeaderText = "Email";
                dataGridView_ChinhSuaTaiKhoan.Columns[7].HeaderText = "SĐT";
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

        private void Sidebar_Transition(ref bool menuExpand, Panel panel, Timer timer) //Func để mở rộng và thu nhỏ các button con
        {
            if (menuExpand == false)
            {
                panel.Height += 5;
                if (panel.Height >= 195)
                {
                    timer.Stop();
                    menuExpand = true;
                }
                AdjustRowHeight();
            }
            else
            {
                panel.Height -= 5;
                if (panel.Height <= 0)
                {
                    timer.Stop();
                    menuExpand = false;
                }
                AdjustRowHeight();
            }
        }

        private void StartTimer(Timer timer) //Func để Start Timer
        {
            timer.Start();
        }

        private void StopTimer(Timer timer)
        {
            timer.Stop();
        }

        private void BindingDataSelected()
        {
            int selectedID = Convert.ToInt32(dataGridView_ChinhSuaTaiKhoan.SelectedCells[0].OwningRow.Cells["IDNguoiDung"].Value.ToString());
            using (QLTVEntities db = new QLTVEntities())
            {
                NguoiDung tk = db.NguoiDung.Find(selectedID);
                textBox_IDUpdateInput.Text = tk.IDNguoiDung.ToString();
                textBox_HoVaTenUpdateInput.Text = tk.HoTen;
                comboBox_GioiTinhUpdateInput.Text = tk.GioiTinh;
                dateTimePicker_NgaySinhInput.Value = (DateTime)tk.NgaySinh;
                textBox_Lop.Text = tk.Lop;
                textBox_ChuyenNganh.Text = tk.ChuyenNganh;
                textBox_SoDienThoaiUpdateInput.Text = tk.SoDienThoai;
            }
        }
        #endregion

        //Events
        #region Events
        private void Form_ChinhSuaTaiKhoan_Resize(object sender, EventArgs e)
        {
            AdjustRowHeight();
            AdjustColumnWidth();
        }

        private void timer_ChinhSuaTaiKhoanTransition_Tick(object sender, EventArgs e)
        {
            Sidebar_Transition(ref menuExpand, panel_ChinhSuaTaiKhoan, timer_ChinhSuaTaiKhoanTransition);
        }

        private void dateTimePicker_NgaySinhInput_ValueChanged(object sender, EventArgs e)
        {
            if (dateTimePicker_NgaySinhInput.Value > DateTime.Now)
            {
                MessageBox.Show("Ngày sinh không được lớn hơn ngày hiện tại", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                dateTimePicker_NgaySinhInput.Value = DateTime.Now;
            }
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

        private void dataGridView_ChinhSuaTaiKhoan_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < dataGridView_ChinhSuaTaiKhoan.Rows.Count - 1)
            {
                menuExpand = false;
                StartTimer(timer_ChinhSuaTaiKhoanTransition); //Start Timer của CSTK
                if (panel_ChinhSuaTaiKhoan.Height == 195) StopTimer(timer_ChinhSuaTaiKhoanTransition);
                numberRecord = 5;
                if (textBox_SearchName.Text != null)
                {
                    SearchNhanViensByGeneral();
                }
                else
                {
                    LoadData();
                }
            }
        }

        private void dataGridView_ChinhSuaTaiKhoan_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            BindingDataSelected();
        }

        private void button_SaveUpdate_Click(object sender, EventArgs e)
        {
            int selectedID = Convert.ToInt32(dataGridView_ChinhSuaTaiKhoan.SelectedCells[0].OwningRow.Cells["IDNguoiDung"].Value.ToString());
            using (QLTVEntities db = new QLTVEntities())
            {
                NguoiDung tk = db.NguoiDung.Find(selectedID);
                List<string> updateFields = new List<string>();

                if (textBox_HoVaTenUpdateInput.Text != tk.HoTen)
                    updateFields.Add("Họ và tên");
                if (comboBox_GioiTinhUpdateInput.Text != tk.GioiTinh)
                    updateFields.Add("Giới tính");
                if (dateTimePicker_NgaySinhInput.Value != tk.NgaySinh)
                    updateFields.Add("Ngày sinh");
                if (textBox_Lop.Text != tk.Lop)
                    updateFields.Add("Lớp");
                if (textBox_ChuyenNganh.Text != tk.ChuyenNganh)
                    updateFields.Add("Chuyên ngành");
                if (textBox_SoDienThoaiUpdateInput.Text != tk.SoDienThoai)
                    updateFields.Add("Số điện thoại");

                string updateFieldMessage = "Bạn muốn chỉnh sửa thông tin \n";
                foreach (string field in updateFields)
                {
                    updateFieldMessage += field + "\n";
                }
                updateFieldMessage += $"của độc giả {tk.HoTen}";

                DialogResult result = MessageBox.Show(updateFieldMessage, "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    tk.HoTen = textBox_HoVaTenUpdateInput.Text;
                    tk.GioiTinh = comboBox_GioiTinhUpdateInput.Text;
                    tk.NgaySinh = dateTimePicker_NgaySinhInput.Value;
                    tk.Lop = textBox_Lop.Text;
                    tk.ChuyenNganh = textBox_ChuyenNganh.Text;
                    tk.SoDienThoai = textBox_SoDienThoaiUpdateInput.Text;
                    db.SaveChanges();
                    MessageBox.Show("Chỉnh sửa thông tin độc giả thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadData();
                }
                else
                {
                    MessageBox.Show("Chỉnh sửa thông tin độc giả thất bại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }
        private void button_ResetUpdate_Click(object sender, EventArgs e)
        {
            BindingDataSelected();
        }

        private void pictureBox_Exit_Click(object sender, EventArgs e)
        {
            menuExpand = true;
            StartTimer(timer_ChinhSuaTaiKhoanTransition); //Start Timer của CSTK
            numberRecord = 10;
            LoadData();
        }
        #endregion
    }
}
