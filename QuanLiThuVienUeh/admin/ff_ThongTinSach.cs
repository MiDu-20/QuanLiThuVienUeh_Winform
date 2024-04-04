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
    public partial class ff_ThongTinSach : Form
    {
        //Variables
        #region Variables
        bool menuExpand = false; //Biến hiển thị độ mở rộng của button con
        int pageNumber = 1; //Biến thể hiện trang hiện tại
        int numberRecord = 10; //Biến thể hiện số dòng hiển thị
        int totalRecord = 0; //Biến chứa tổng số dòng trong bảng
        int lastPageNumber = 0; //Biến thể hiện trang cuối cùng trong bảng
        List<Button> buttonChangePageList = new List<Button>(); //List chứa các button phân trang
        int doubleClickCount = 0; //Biến đếm số lần double click
        #endregion

        public ff_ThongTinSach()
        {
            InitializeComponent();
            LoadData();
            panel_ChinhSuaSach.Height = 0;
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

        private void SearchBooksByGeneral() //Hàm tìm kiếm nhân viên 1 cách tổng quát
        {
            using (QLTVEntities db = new QLTVEntities())
            {
                var result = db.Sach.Where(c => c.IDSach.ToString().Contains(textBox_SearchName.Text)
                                                     || c.TenSach.Contains(textBox_SearchName.Text)
                                                     || c.TacGia.Contains(textBox_SearchName.Text)
                                                     || c.TheLoai.Contains(textBox_SearchName.Text)
                                                     || c.ChuyenNganh.Contains(textBox_SearchName.Text)
                                                     || c.NhaXuatBan.Contains(textBox_SearchName.Text));
                if (result != null)
                {
                    // Tính toán lại số trang khi có kết quả mới
                    totalRecord = result.Count();
                    lastPageNumber = (int)Math.Ceiling((double)totalRecord / numberRecord);

                    // Hiển thị trang đầu tiên
                    result = result.OrderBy(s => s.IDSach)
                                 .Skip((pageNumber - 1) * numberRecord)
                                 .Take(numberRecord);

                    // Hiển thị kết quả trong DataGridView
                    dataGridView_ChinhSuaSach.DataSource = result.Select(s => new
                    {
                        s.IDSach,
                        s.TenSach,
                        s.TacGia,
                        s.TheLoai,
                        s.SoLuong,
                        s.NhaXuatBan,
                        s.NamXuatBan
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
                totalRecord = db.Sach.Count(); //Lấy ra tổng số dòng trong bảng
            }
            lastPageNumber = (int)Math.Ceiling((double)totalRecord / numberRecord); //Công thức tính trang cuối cùng trong bảng
            dataGridView_ChinhSuaSach.DataSource = LoadRecord(pageNumber, numberRecord); //Hiển thị lên dataGridView
            AdjustRowHeight(); //Customize lại height các dòng
            AdjustColumnWidth(); //Customize lại width các cột
            ChangeHeader(); //Thay đổi tiêu đề hiển thị trên dataGridView

        }

        List<object> LoadRecord(int page, int recordNum) //Hàm phân trang 
        {
            List<object> result = new List<object>();
            using (QLTVEntities db = new QLTVEntities())
            {
                result = db.Sach.OrderBy(e => e.IDSach)
                    .Skip((page - 1) * recordNum)
                    .Take(recordNum)
                    .Select(e => new
                    {
                        e.IDSach,
                        e.TenSach,
                        e.TacGia,
                        e.TheLoai,
                        e.SoLuong,
                        e.NhaXuatBan,
                        e.NamXuatBan
                    }).ToList<object>();
            }
            return result;
        }

        public void AdjustRowHeight() //Hàm customize lại height các dòng
        {
            //Biến thể hiện height của các dòng sao cho bằng nhau
            int desiredHeight = dataGridView_ChinhSuaSach.Height / (dataGridView_ChinhSuaSach.Rows.Count + 1);
            if (dataGridView_ChinhSuaSach.Rows.Count > 0 && dataGridView_ChinhSuaSach.Rows.Count < 5)
            {
                foreach (DataGridViewRow row in dataGridView_ChinhSuaSach.Rows)
                {
                    row.Height = 60;
                }
            }
            else
            {
                // Thiết lập chiều cao cho mỗi dòng
                foreach (DataGridViewRow row in dataGridView_ChinhSuaSach.Rows)
                {
                    row.Height = desiredHeight;
                }
            }
        }

        private void AdjustColumnWidth() //Hàm customize lại width các dòng
        {
            if (dataGridView_ChinhSuaSach.Columns.Count > 0)
            {
                dataGridView_ChinhSuaSach.Columns[0].Width = dataGridView_ChinhSuaSach.Width * 5 / 100;
                dataGridView_ChinhSuaSach.Columns[1].Width = dataGridView_ChinhSuaSach.Width * 20 / 100;
                dataGridView_ChinhSuaSach.Columns[2].Width = dataGridView_ChinhSuaSach.Width * 20 / 100;
                dataGridView_ChinhSuaSach.Columns[3].Width = dataGridView_ChinhSuaSach.Width * 10 / 100;
                dataGridView_ChinhSuaSach.Columns[4].Width = dataGridView_ChinhSuaSach.Width * 10 / 100;
                dataGridView_ChinhSuaSach.Columns[5].Width = dataGridView_ChinhSuaSach.Width * 20 / 100;
                dataGridView_ChinhSuaSach.Columns[6].Width = dataGridView_ChinhSuaSach.Width * 15 / 100;
            }
        }

        private void ChangeHeader() //Hàm thay đổi tiêu đề hiển thị trên dataGridView
        {
            if (dataGridView_ChinhSuaSach.Columns.Count > 0)
            {
                dataGridView_ChinhSuaSach.Columns[0].HeaderText = "ID";
                dataGridView_ChinhSuaSach.Columns[1].HeaderText = "Tên sách";
                dataGridView_ChinhSuaSach.Columns[2].HeaderText = "Tác giả";
                dataGridView_ChinhSuaSach.Columns[3].HeaderText = "Thể loại";
                dataGridView_ChinhSuaSach.Columns[4].HeaderText = "Số lượng";
                dataGridView_ChinhSuaSach.Columns[5].HeaderText = "Nhà xuất bản";
                dataGridView_ChinhSuaSach.Columns[6].HeaderText = "Năm xuất bản";
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

        //Hàm để mở rộng và thu nhỏ các button con
        private void Sidebar_Transition(ref bool menuExpand, Panel panel, Timer timer)
        {
            if (menuExpand == false)
            {
                panel.Height += 5;
                if (panel.Height >= 195)
                {
                    StopTimer(timer);
                    menuExpand = true;
                }
            }
            else
            {
                panel.Height -= 5;
                if (panel.Height <= 0)
                {
                    StopTimer(timer);
                    menuExpand = false;
                }
            }
        }

        private void StartTimer(Timer timer) //Hàm để Start Timer
        {
            timer.Start();
        }

        private void StopTimer(Timer timer) //Hàm để Stop Timer
        {
            timer.Stop();
        }

        private void BindingDataSelected() //Hàm biding dữ liệu có trong dataGridView
        {
            int selectedID = Convert.ToInt32(dataGridView_ChinhSuaSach.SelectedCells[0].OwningRow.Cells["IDSach"].Value.ToString());
            using (QLTVEntities db = new QLTVEntities())
            {
                Sach sach = db.Sach.Find(selectedID);
                textBox_IDSachUpdateInput.Text = sach.IDSach.ToString();
                textBox_TenSachUpdateInput.Text = sach.TenSach;
                textBox_TacGiaUpdateInput.Text = sach.TacGia;
                textBox_SoLuongUpdateInput.Text = sach.SoLuong.ToString();
                textBox_TheLoaiUpdateInput.Text = sach.TheLoai;
                textBox_NhaXuatBanUpdateInput.Text = sach.NhaXuatBan;
                dateTimePicker_NamXuatBanUpdateInput.Value = (DateTime)sach.NamXuatBan;
            }
        }

        private void UpdateSach() //Hàm update lại sách
        {
            int selectedID = Convert.ToInt32(dataGridView_ChinhSuaSach.SelectedCells[0].OwningRow.Cells["IDSach"].Value.ToString());
            using (QLTVEntities db = new QLTVEntities())
            {
                Sach sach = db.Sach.Find(selectedID);
                List<string> updateFields = new List<string>();

                if (textBox_TenSachUpdateInput.Text != sach.TenSach)
                    updateFields.Add("Tên sách");
                if (textBox_TacGiaUpdateInput.Text != sach.TacGia)
                    updateFields.Add("Tác giả");
                if (textBox_TheLoaiUpdateInput.Text != sach.TheLoai)
                    updateFields.Add("Thể loại");
                if (textBox_SoLuongUpdateInput.Text != sach.SoLuong.ToString())
                    updateFields.Add("Số lượng");
                if (textBox_NhaXuatBanUpdateInput.Text != sach.NhaXuatBan)
                    updateFields.Add("Nhà xuất bản");
                if (dateTimePicker_NamXuatBanUpdateInput.Value != (DateTime)sach.NamXuatBan)
                    updateFields.Add("Năm xuất bản");

                string updateFieldMessage = "Bạn muốn thay đổi \n";
                foreach (string field in updateFields)
                {
                    updateFieldMessage += field + "\n";
                }
                updateFieldMessage += $"của sách {sach.TenSach}";

                DialogResult result = MessageBox.Show(updateFieldMessage, "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    sach.TenSach = textBox_TenSachUpdateInput.Text;
                    sach.TacGia = textBox_TacGiaUpdateInput.Text;
                    sach.TheLoai = textBox_TheLoaiUpdateInput.Text;
                    sach.SoLuong = Convert.ToInt32(textBox_SoLuongUpdateInput.Text);
                    sach.NhaXuatBan = textBox_NhaXuatBanUpdateInput.Text;
                    sach.NamXuatBan = dateTimePicker_NamXuatBanUpdateInput.Value;
                    db.SaveChanges();
                    MessageBox.Show("Chỉnh sửa thông tin sách thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadData();
                }
                else
                {
                    MessageBox.Show("Chỉnh sửa thông tin sách thất bại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }
        #endregion

        //Events
        #region Events
        private void timer_ChinhSuaSachTransition_Tick(object sender, EventArgs e)
        {
            Sidebar_Transition(ref menuExpand, panel_ChinhSuaSach, timer_ChinhSuaSachTransition);
        }

        private void Form_ChinhSuaSach_Resize(object sender, EventArgs e)
        {
            AdjustRowHeight();
            AdjustColumnWidth();
        }
        private void dataGridView_ChinhSuaSach_Resize(object sender, EventArgs e)
        {
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
                SearchBooksByGeneral();
            }
            else
            {
                SetLabelText(label_SearchName, "Search by name, author, major..."); //Nếu text rỗng thì hiện lại label Search
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
                    SearchBooksByGeneral();
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
                    SearchBooksByGeneral();
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
                SearchBooksByGeneral();
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
                    SearchBooksByGeneral();
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
                    SearchBooksByGeneral();
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
                SearchBooksByGeneral();
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
                SearchBooksByGeneral();
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

        private void dataGridView_ChinhSuaSach_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            menuExpand = false;
            StartTimer(timer_ChinhSuaSachTransition);
            if (panel_ChinhSuaSach.Height == 195) StopTimer(timer_ChinhSuaSachTransition);
            doubleClickCount++;
            if (doubleClickCount == 1)
            {
                numberRecord = 5;
                if (textBox_SearchName.Text != null)
                {
                    SearchBooksByGeneral();
                }
                else
                {
                    LoadData();
                }
            }
        }
        private void pictureBox_Exit_Click(object sender, EventArgs e)
        {
            menuExpand = true;
            StartTimer(timer_ChinhSuaSachTransition);
            numberRecord = 10;
            LoadData();
            doubleClickCount = 0;
        }

        private void dataGridView_ChinhSuaSach_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (doubleClickCount > 0)
                BindingDataSelected();
        }

        private void button_SaveUpdate_Click(object sender, EventArgs e)
        {
            UpdateSach();
        }
        private void button_ResetUpdate_Click(object sender, EventArgs e)
        {
            BindingDataSelected();
        }

        private void button_InsertDelete_Click(object sender, EventArgs e)
        {
            ffc_ThemXoaSach form = new ffc_ThemXoaSach();
            form.Show();
        }
        #endregion
    }
}
