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
    public partial class ff_Searching : Form
    {
        //Variable
        #region Variable
        int idNguoiDung; //Biến chứa idNguoiDung
        bool menuExpand = false; //Biến hiển thị độ mở rộng của 1 control
        int pageNumber = 1; //Biến thể hiện trang hiện tại
        int numberRecord = 10; //Biến thể hiện số dòng hiển thị
        int totalRecord = 0; //Biến chứa tổng số dòng trong bảng
        int lastPageNumber = 0; //Biến thể hiện trang cuối cùng trong bảng
        List<Button> buttonChangePageList = new List<Button>(); //List chứa các button phân trang
        bool isFilter = false; //Biến kiểm tra filter được mở hay đóng
        #endregion

        public ff_Searching(int idNguoiDung)
        {
            InitializeComponent();
            this.idNguoiDung = idNguoiDung;
            panel_Filter.Height = 0; //ban đầu panel filter = 0
            AddComboBoxTheLoai();
            LoadData();
        }

        //Function
        #region Function
        private void AddComboBoxTheLoai() //Thêm dữ liệu vào comboBox Thể loại
        {
            using (QLTVEntities db = new QLTVEntities())
            {
                var distinctTheLoai = db.Sach.Select(s => s.TheLoai).Distinct().ToList();

                // Xóa dữ liệu cũ trong ComboBox (nếu có)
                comboBox_SearchTheLoai.Items.Clear();

                // Thêm các giá trị distinct vào ComboBox
                foreach (var theLoai in distinctTheLoai)
                {
                    comboBox_SearchTheLoai.Items.Add(theLoai);
                }
            }
        }

        //Hàm để mở rộng và thu nhỏ các button con
        private void Sidebar_Transition(ref bool menuExpand, Panel panel, Timer timer)
        {
            if (menuExpand == false)
            {
                panel.Height += 5;
                if (panel.Height >= 83)
                {
                    StopTimer(timer);
                    menuExpand = true;
                }
                isFilter = true;
            }
            else
            {
                panel.Height -= 5;
                if (panel.Height <= 0)
                {
                    StopTimer(timer);
                    menuExpand = false;
                }
                isFilter = false;
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

        void LoadData() //Hàm hiển thị dữ liệu lên dataGridView
        {
            using (QLTVEntities db = new QLTVEntities())
            {
                totalRecord = db.Sach.Count(); //Lấy ra tổng số dòng trong bảng
            }
            lastPageNumber = (int)Math.Ceiling((double)totalRecord / numberRecord); //Công thức tính trang cuối cùng trong bảng
            dataGridView_TimKiemSach.DataSource = LoadRecord(pageNumber, numberRecord); //Hiển thị lên dataGridView
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
                         e.NhaXuatBan,
                         e.NamXuatBan
                     }).ToList<object>();
            }
            return result;
        }

        public void AdjustRowHeight() //Hàm customize lại height các dòng
        {
            //Biến thể hiện height của các dòng sao cho bằng nhau
            int desiredHeight = dataGridView_TimKiemSach.Height / (dataGridView_TimKiemSach.Rows.Count + 1);
            if (dataGridView_TimKiemSach.Rows.Count > 0 && dataGridView_TimKiemSach.Rows.Count < 10)
            {
                foreach (DataGridViewRow row in dataGridView_TimKiemSach.Rows)
                {
                    row.Height = 50;
                }
            }
            else
            {
                // Thiết lập chiều cao cho mỗi dòng
                foreach (DataGridViewRow row in dataGridView_TimKiemSach.Rows)
                {
                    row.Height = desiredHeight;
                }
            }
        }

        private void AdjustColumnWidth() //Hàm customize lại width các dòng
        {
            if (dataGridView_TimKiemSach.Columns.Count > 0)
            {
                dataGridView_TimKiemSach.Columns[0].Width = dataGridView_TimKiemSach.Width * 5 / 100;
                dataGridView_TimKiemSach.Columns[1].Width = dataGridView_TimKiemSach.Width * 25 / 100;
                dataGridView_TimKiemSach.Columns[2].Width = dataGridView_TimKiemSach.Width * 20 / 100;
                dataGridView_TimKiemSach.Columns[3].Width = dataGridView_TimKiemSach.Width * 15 / 100;
                dataGridView_TimKiemSach.Columns[4].Width = dataGridView_TimKiemSach.Width * 20 / 100;
                dataGridView_TimKiemSach.Columns[5].Width = dataGridView_TimKiemSach.Width * 15 / 100;
            }
        }

        private void ChangeHeader() //Hàm thay đổi tiêu đề hiển thị trên dataGridView
        {
            if (dataGridView_TimKiemSach.Columns.Count > 0)
            {
                dataGridView_TimKiemSach.Columns[0].HeaderText = "ID";
                dataGridView_TimKiemSach.Columns[1].HeaderText = "Tên sách";
                dataGridView_TimKiemSach.Columns[2].HeaderText = "Tác giả";
                dataGridView_TimKiemSach.Columns[3].HeaderText = "Thể loại";
                dataGridView_TimKiemSach.Columns[4].HeaderText = "Nhà xuất bản";
                dataGridView_TimKiemSach.Columns[5].HeaderText = "Năm xuất bản";
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

        private void SearchBooksByGeneral() //Hàm tìm kiếm sách 1 cách tổng quát
        {
            using (QLTVEntities db = new QLTVEntities())
            {
                var result = db.Sach.Where(c => c.TenSach.Contains(textBox_SearchName.Text)
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
                    dataGridView_TimKiemSach.DataSource = result.Select(s => new
                    {
                        s.IDSach,
                        s.TenSach,
                        s.TacGia,
                        s.TheLoai,
                        s.NhaXuatBan,
                        s.NamXuatBan
                    }).ToList();
                    AdjustRowHeight();
                    AdjustColumnWidth();
                    ChangeHeader();
                }
            }
        }

        private void SearchBooksByFilter() //Hàm tìm kiếm sách theo filter
        {
            using (QLTVEntities db = new QLTVEntities())
            {
                var query = db.Sach.AsQueryable();

                // Kiểm tra và thêm điều kiện tìm kiếm cho tên sách
                if (!string.IsNullOrWhiteSpace(textBox_SearchName.Text))
                {
                    query = query.Where(s => s.TenSach.Contains(textBox_SearchName.Text));
                }

                // Kiểm tra và thêm điều kiện tìm kiếm cho tác giả
                if (!string.IsNullOrWhiteSpace(textBox_SearchTacGia.Text))
                {
                    query = query.Where(s => s.TacGia.Contains(textBox_SearchTacGia.Text));
                }

                if (!string.IsNullOrWhiteSpace(textBox_SearchChuyenNganh.Text))
                {
                    query = query.Where(s => s.ChuyenNganh.Contains(textBox_SearchChuyenNganh.Text));
                }

                // Kiểm tra và thêm điều kiện tìm kiếm cho nhà xuất bản
                if (!string.IsNullOrWhiteSpace(textBox_SearchNhaXuatBan.Text))
                {
                    query = query.Where(s => s.NhaXuatBan.Contains(textBox_SearchNhaXuatBan.Text));
                }

                // Kiểm tra và thêm điều kiện tìm kiếm cho thể loại
                if (comboBox_SearchTheLoai.Text != null)
                {
                    var theloai = comboBox_SearchTheLoai.Text;
                    query = query.Where(s => s.TheLoai.Contains(theloai));
                }

                // Tính tổng số kết quả tìm kiếm
                totalRecord = query.Count();

                // Tính toán số trang và lưu vào biến lastPageNumber
                lastPageNumber = (int)Math.Ceiling((double)totalRecord / numberRecord);

                // Phân trang kết quả
                query = query.OrderBy(s => s.IDSach)
                             .Skip((pageNumber - 1) * numberRecord)
                             .Take(numberRecord);


                // Hiển thị kết quả trong DataGridView
                dataGridView_TimKiemSach.DataSource = query.Select(s => new
                {
                    s.IDSach,
                    s.TenSach,
                    s.TacGia,
                    s.TheLoai,
                    s.NhaXuatBan,
                    s.NamXuatBan
                }).ToList();
                AdjustRowHeight();
                AdjustColumnWidth();
                ChangeHeader();
            }
        }
        #endregion

        //Events
        #region Events
        private void ff_Booking_Resize(object sender, EventArgs e)
        {
            AdjustRowHeight();
            AdjustColumnWidth();
        }

        private void textBox_SearchName_TextChanged(object sender, EventArgs e)
        {
            if (isFilter == true)
            {
                SetLabelText(label_SearchName, "Search by book name...");
                if (textBox_SearchName.Text.Length != 0)
                {
                    ResetLabelTextToNull(label_SearchName);//Nếu text trong ô textBox được nhập thì xóa label Search 
                }
                else
                {
                    SetLabelText(label_SearchName, "Search by book name..."); //Nếu text rỗng thì hiện lại label Search
                }
            }
            else
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
                    SetLabelText(label_SearchName, "Search by name..."); //Nếu text rỗng thì hiện lại label Search
                    pageNumber = 1;
                    SetDefaultButtonChangePageText();
                    ResetColorButton();
                    HighlightButtonCurrentPage(button_ChangePage1);
                    LoadData();
                }
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

        private void textBox_SearchTacGia_TextChanged(object sender, EventArgs e)
        {
            if (textBox_SearchTacGia.Text.Length != 0)
            {
                ResetLabelTextToNull(label_SearchTacGia);//Nếu text trong ô textBox được nhập thì xóa label Search                
            }
            else
            {
                SetLabelText(label_SearchTacGia, "Search by author name..."); //Nếu text rỗng thì hiện lại label Search
                LoadData();

            }
        }

        private void textBox_SearchTacGia_Click(object sender, EventArgs e)
        {
            if (textBox_SearchTacGia.Text.Length == 0)
                ResetLabelTextToNull(label_SearchTacGia); //TextBox được click thì xóa label Search
        }

        private void label_SearchTacGia_Click(object sender, EventArgs e)
        {
            FocusTextBox(textBox_SearchTacGia); //Nếu click vào label Search thì chuyển Focus vào textBox
            ResetLabelTextToNull(label_SearchTacGia); //Xóa label Search
        }

        private void textBox_SearchChuyenNganh_TextChanged(object sender, EventArgs e)
        {
            if (textBox_SearchChuyenNganh.Text.Length != 0)
            {
                ResetLabelTextToNull(label_SearchChuyenNganh);//Nếu text trong ô textBox được nhập thì xóa label Search
            }
            else
            {
                SetLabelText(label_SearchChuyenNganh, "Search by major..."); //Nếu text rỗng thì hiện lại label Search
                LoadData();

            }
        }

        private void textBox_SearchChuyenNganh_Click(object sender, EventArgs e)
        {
            if (textBox_SearchChuyenNganh.Text.Length == 0)
                ResetLabelTextToNull(label_SearchChuyenNganh); //TextBox được click thì xóa label Search
        }

        private void label_SearchChuyenNganh_Click(object sender, EventArgs e)
        {
            FocusTextBox(textBox_SearchChuyenNganh); //Nếu click vào label Search thì chuyển Focus vào textBox
            ResetLabelTextToNull(label_SearchChuyenNganh); //Xóa label Search
        }

        private void textBox_SearchNhaXuatBan_TextChanged(object sender, EventArgs e)
        {
            if (textBox_SearchNhaXuatBan.Text.Length != 0)
            {
                ResetLabelTextToNull(label_SearchNhaXuatBan);//Nếu text trong ô textBox được nhập thì xóa label Search                
            }
            else
            {
                SetLabelText(label_SearchNhaXuatBan, "Search by publisher..."); //Nếu text rỗng thì hiện lại label Search
                LoadData();

            }
        }

        private void textBox_SearchNhaXuatBan_Click(object sender, EventArgs e)
        {
            if (textBox_SearchNhaXuatBan.Text.Length == 0)
                ResetLabelTextToNull(label_SearchNhaXuatBan); //TextBox được click thì xóa label Search
        }

        private void label_SearchNhaXuatBan_Click(object sender, EventArgs e)
        {
            FocusTextBox(textBox_SearchNhaXuatBan); //Nếu click vào label Search thì chuyển Focus vào textBox
            ResetLabelTextToNull(label_SearchNhaXuatBan); //Xóa label Search
        }

        private void pictureBox_Filter_Click(object sender, EventArgs e)
        {
            StartTimer(timer_FilterTransition);
            if (isFilter == true)
            {
                textBox_SearchName.Text = null;
                label_SearchName.Text = "Search by book, author, major...";
            }
            else
            {
                textBox_SearchName.Text = null;
                label_SearchName.Text = "Search by book name...";
            }
        }

        private void timer_FilterTransition_Tick(object sender, EventArgs e)
        {
            Sidebar_Transition(ref menuExpand, panel_Filter, timer_FilterTransition);
        }

        private void button_Search_Click(object sender, EventArgs e)
        {
            if (isFilter == true)
            {
                pageNumber = 1;
                SearchBooksByFilter();
                SetDefaultButtonChangePageText();
                ResetColorButton();
                HighlightButtonCurrentPage(button_ChangePage1);
            }
        }

        private void button_ChangePage1_Click(object sender, EventArgs e)
        {
            pageNumber = Convert.ToInt32(button_ChangePage1.Text);

            if (button_ChangePage1.Text != "1")
            {
                if (isFilter == true)
                {
                    SearchBooksByFilter();
                    CreateOrderForButtonChangePageByPageNumber(pageNumber);
                    ResetColorButton();
                    HighlightButtonCurrentPage(button_ChangePage2);
                }
                else
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
            }
            else
            {
                if (isFilter == true)
                {
                    SearchBooksByFilter();
                    ResetColorButton();
                    HighlightButtonCurrentPage(button_ChangePage1);
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
        }

        private void button_ChangePage2_Click(object sender, EventArgs e)
        {
            if (lastPageNumber == 1) return;
            pageNumber = Convert.ToInt32(button_ChangePage2.Text);
            if (isFilter == true)
            {
                SearchBooksByFilter();
                ResetColorButton();
                HighlightButtonCurrentPage(sender);
            }
            else
            {
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
        }

        private void button_ChangePage3_Click(object sender, EventArgs e)
        {
            if (lastPageNumber <= 2) return;
            pageNumber = Convert.ToInt32(button_ChangePage3.Text);
            if (lastPageNumber > pageNumber)
            {
                if (isFilter == true)
                {
                    SearchBooksByFilter();
                    CreateOrderForButtonChangePageByPageNumber(pageNumber);
                    ResetColorButton();
                    HighlightButtonCurrentPage(button_ChangePage2);
                }
                else
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
            }
            else
            {
                if (isFilter == true)
                {
                    SearchBooksByFilter();
                    ResetColorButton();
                    HighlightButtonCurrentPage(button_ChangePage3);
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
        }

        private void button_ReturnFirstPage_Click(object sender, EventArgs e)
        {
            pageNumber = 1;
            if (isFilter == true)
            {
                SearchBooksByFilter();
                ResetColorButton();
                HighlightButtonCurrentPage(button_ChangePage1);
                SetDefaultButtonChangePageText();
            }
            else
            {
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
        }

        private void button_ReturnLastPage_Click(object sender, EventArgs e)
        {
            pageNumber = lastPageNumber;
            if (isFilter == true)
            {
                SearchBooksByFilter();
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
        }

        private void dataGridView_TimKiemSach_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int idSach = Convert.ToInt32(dataGridView_TimKiemSach.Rows[e.RowIndex].Cells["IDSach"].Value);
            XemThongTinSachChiTiet(idSach, idNguoiDung);
        }

        private void XemThongTinSachChiTiet(int idSach, int idNguoiDung)
        {
            ffc_ThongTinSachChiTiet form = new ffc_ThongTinSachChiTiet(idSach,idNguoiDung);
            form.Show();
        }
        #endregion
    }
}
