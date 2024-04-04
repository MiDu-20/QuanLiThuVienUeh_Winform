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
    public partial class ff_History : Form
    {
        //Variables
        #region Variables
        int pageNumber = 1; //Biến thể hiện trang hiện tại
        int numberRecord = 10; //Biến thể hiện số dòng hiển thị
        int totalRecord = 0; //Biến chứa tổng số dòng trong bảng
        int lastPageNumber = 0; //Biến thể hiện trang cuối cùng trong bảng
        List<Button> buttonChangePageList = new List<Button>(); //List chứa các button phân trang
        public int idNguoiDung { get; private set; }
        #endregion

        public ff_History(int idNguoiDung)
        {
            InitializeComponent();
            this.idNguoiDung = idNguoiDung;
            LoadData();
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

        private void SearchMuonTrasByGeneral() //Hàm tìm kiếm nhân viên 1 cách tổng quát
        {
            using (QLTVEntities db = new QLTVEntities())
            {
                var result = db.MuonTraSach.Where(c => (c.IDMuonTra.ToString().Contains(textBox_SearchName.Text) && c.IDNguoiDung == idNguoiDung)
                                                     || (c.HoTen.Contains(textBox_SearchName.Text) && c.IDNguoiDung == idNguoiDung)
                                                     || (c.TenSach.Contains(textBox_SearchName.Text) && c.IDNguoiDung == idNguoiDung));
                if (result != null)
                {
                    totalRecord = result.Count();
                    lastPageNumber = (int)Math.Ceiling((double)totalRecord / numberRecord);
                    result = result.OrderBy(s => s.IDMuonTra)
                                 .Skip((pageNumber - 1) * numberRecord)
                                 .Take(numberRecord);
                    dataGridView_History.DataSource = result.Select(e => new
                    {
                        e.IDMuonTra,
                        e.TenSach,
                        e.IDSach,
                        e.NgayMuon,
                        e.NgayTraThucTe,
                        e.TinhTrang
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
                totalRecord = db.MuonTraSach.Count(); //Lấy ra tổng số dòng trong bảng
            }
            lastPageNumber = (int)Math.Ceiling((double)totalRecord / numberRecord); //Công thức tính trang cuối cùng trong bảng
            dataGridView_History.DataSource = LoadRecord(pageNumber, numberRecord); //Hiển thị lên dataGridView
            AdjustRowHeight(); //Customize lại height các dòng
            AdjustColumnWidth(); //Customize lại width các cột
            ChangeHeader(); //Thay đổi tiêu đề hiển thị trên dataGridView
        }

        public List<object> LoadRecord(int page, int recordNum)
        {
            List<object> result = new List<object>();
            using (QLTVEntities db = new QLTVEntities())
            {
                result = db.MuonTraSach.Where(e => e.IDNguoiDung == idNguoiDung)
                    .OrderBy(e => e.IDMuonTra)
                    .Skip((page - 1) * recordNum)
                    .Take(recordNum)
                    .Select(e => new
                    {
                        e.IDMuonTra,
                        e.TenSach,
                        e.IDSach,
                        e.NgayMuon,
                        e.NgayTraThucTe,
                        e.TinhTrang,
                    }).ToList<object>();
            }
            return result;
        }

        public void AdjustRowHeight() //Hàm customize lại height các dòng
        {
            //Biến thể hiện height của các dòng sao cho bằng nhau
            int desiredHeight = dataGridView_History.Height / (dataGridView_History.Rows.Count + 1);
            if (dataGridView_History    .Rows.Count > 0 && dataGridView_History.Rows.Count < 10)
            {
                foreach (DataGridViewRow row in dataGridView_History.Rows)
                {
                    row.Height = 50;
                }
            }
            else
            {
                // Thiết lập chiều cao cho mỗi dòng
                foreach (DataGridViewRow row in dataGridView_History.Rows)
                {
                    row.Height = desiredHeight;
                }
            }
        }

        private void AdjustColumnWidth() //Hàm customize lại width các dòng
        {
            if (dataGridView_History.Columns.Count > 0)
            {
                dataGridView_History.Columns[0].Width = dataGridView_History.Width * 15 / 100;
                dataGridView_History.Columns[1].Width = dataGridView_History.Width * 15 / 100;
                dataGridView_History.Columns[2].Width = dataGridView_History.Width * 15 / 100;
                dataGridView_History.Columns[3].Width = dataGridView_History.Width * 20 / 100;
                dataGridView_History.Columns[4].Width = dataGridView_History.Width * 20 / 100;
                dataGridView_History.Columns[5].Width = dataGridView_History.Width * 15 / 100;
            }
        }

        private void ChangeHeader() //Hàm thay đổi tiêu đề hiển thị trên dataGridView
        {
            if (dataGridView_History.Columns.Count > 0)
            {
                dataGridView_History.Columns[0].HeaderText = "ID Mượn trả";
                dataGridView_History.Columns[1].HeaderText = "Tên Sách";
                dataGridView_History.Columns[2].HeaderText = "ID Sách";
                dataGridView_History.Columns[3].HeaderText = "Ngày mượn";
                dataGridView_History.Columns[4].HeaderText = "Ngày trả";
                dataGridView_History.Columns[5].HeaderText = "Tình Trạng";
                //dataGridView_History.Columns[6].HeaderText = "Đánh giá";
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

        //Events
        #region Events
        private void FormHistory_Resize(object sender, EventArgs e)
        {
            AdjustRowHeight();
            AdjustColumnWidth();
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
                SearchMuonTrasByGeneral();
            }
            else
            {
                SetLabelText(label_SearchName, "Search by id, name..."); //Nếu text rỗng thì hiện lại label Search
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
                    SearchMuonTrasByGeneral();
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
                    SearchMuonTrasByGeneral();
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
                SearchMuonTrasByGeneral();
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
                    SearchMuonTrasByGeneral();
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
                    SearchMuonTrasByGeneral();
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
                SearchMuonTrasByGeneral();
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
                SearchMuonTrasByGeneral();
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
        #endregion

        #endregion

        private void dataGridView_History_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int idSach = Convert.ToInt32(dataGridView_History.Rows[e.RowIndex].Cells["IDSach"].Value);
            ffc_ThongTinSachChiTiet form = new ffc_ThongTinSachChiTiet(idSach, idNguoiDung, "History");
            form.Show();
        }
    }
}
