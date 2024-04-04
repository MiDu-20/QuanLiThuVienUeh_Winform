using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLiThuVienUeh.nguoidung
{
    public partial class ff_ThongBaoNguoiDung : Form
    {
        //Variable
        #region Variable
        int idNguoiDung; //Biến chứa idNguoiDung
        string email; //Bie
        bool menuExpand = false; //Biến hiển thị độ mở rộng của 1 control
        int pageNumber = 1; //Biến chứa trang hiện tại
        int numberRecord = 5; //Biến số trang hiển thị
        int totalRecord = 0; //Biến tổng số dòng trong bảng
        int lastPageNumber = 0; //Biến chứa trang cuối cùng
        bool isFilter = false; //Biến kiểm tra filter được mở hay đóng
        List<Button> buttonChangePageList = new List<Button>();
        #endregion

        public ff_ThongBaoNguoiDung(int idNguoiDung)
        {
            InitializeComponent();
            this.idNguoiDung = idNguoiDung;
            using (QLTVEntities db = new QLTVEntities())
            {
                email = db.NguoiDung.Find(idNguoiDung).Email;
            }
            LoadData();
        }

        //Functions
        #region Functions
        private void LoadData() //Hàm để hiển thị dữ liệu
        {
            using (QLTVEntities db = new QLTVEntities())
            {
                totalRecord = db.ThongBao.Where(s => s.EmailNguoiNhan == email).Count(); //Lấy ra tổng số dòng trong bảng
            }
            lastPageNumber = (int)Math.Ceiling((double)totalRecord / numberRecord); //Công thức tính trang cuối cùng trong bảng
            dataGridView_ThongBao.DataSource = LoadRecord(pageNumber, numberRecord); //Hiển thị lên dataGridView
            AdjustRowHeight(); //Customize lại height các dòng
            AdjustColumnWidth(); //Customize lại width các cột
            ChangeHeader(); //Thay đổi tiêu đề hiển thị trên dataGridView
            CustomizeColumnTieuDe(); //Customize lại cột Tiêu đề
        }

        List<object> LoadRecord(int page, int recordNum) //Hàm phân trang
        {
            List<object> result = new List<object>();
            using (QLTVEntities db = new QLTVEntities())
            {
                result = db.ThongBao.Where(e => e.EmailNguoiNhan == email && e.IDNguoiNhan == idNguoiDung)
                                    .GroupBy(e => new { e.TieuDe, e.IDNguoiGui, NgayGui = DbFunctions.TruncateTime(e.NgayGui) })
                                    .Select(group => group.FirstOrDefault())
                                    .OrderBy(e => e.IDThongBao)
                                    .Skip((page - 1) * recordNum)
                                    .Take(recordNum)
                                    .Select(e => new
                                    {
                                        e.TieuDe,
                                        e.NguoiGui,
                                        NgayGui = DbFunctions.TruncateTime(e.NgayGui)
                                    }).ToList<object>();
            }
            return result;
        }

        private void CustomizeColumnTieuDe() //Hàm customize cột Tiêu đề
        {
            if (dataGridView_ThongBao.Columns.Count > 0)
            {
                DataGridViewCellStyle style = new DataGridViewCellStyle();
                style.Font = new Font("Segoe UI", 14, FontStyle.Underline);
                style.ForeColor = Color.FromArgb(51, 122, 183);
                dataGridView_ThongBao.Columns["TieuDe"].DefaultCellStyle = style;
            }
        }

        public void AdjustRowHeight() //Hàm customize lại height các dòng
        {
            //Biến thể hiện height của các dòng sao cho bằng nhau
            int desiredHeight = dataGridView_ThongBao.Height / (dataGridView_ThongBao.Rows.Count + 1);
            if (dataGridView_ThongBao.Rows.Count > 0 && dataGridView_ThongBao.Rows.Count < 5)
            {
                foreach (DataGridViewRow row in dataGridView_ThongBao.Rows)
                {
                    row.Height = 100;
                }
            }
            else
            {
                // Thiết lập chiều cao cho mỗi dòng
                foreach (DataGridViewRow row in dataGridView_ThongBao.Rows)
                {
                    row.Height = desiredHeight;
                }
            }
        }

        private void AdjustColumnWidth() //Hàm customize width các cột
        {
            if (dataGridView_ThongBao.Columns.Count > 0)
            {
                dataGridView_ThongBao.Columns[0].Width = dataGridView_ThongBao.Width * 70 / 100;
                dataGridView_ThongBao.Columns[1].Width = dataGridView_ThongBao.Width * 15 / 100;
                dataGridView_ThongBao.Columns[2].Width = dataGridView_ThongBao.Width * 15 / 100;
            }
        }

        private void ChangeHeader() //Hàm thay đổi tiêu đề hiển thị trên dataGridView
        {
            if (dataGridView_ThongBao.Columns.Count > 0)
            {
                dataGridView_ThongBao.Columns[0].HeaderText = "Tiêu đề";
                dataGridView_ThongBao.Columns[1].HeaderText = "Người gửi";
                dataGridView_ThongBao.Columns[2].HeaderText = "Ngày gửi";
            }
        }

        private void AddButtonChangePageList() //Hàm thêm các button phân trang vào 1 list
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
        #endregion

        //Events
        #region Events
        private void ff_ThongBaoNguoiDung_Resize(object sender, EventArgs e)
        {
            AdjustColumnWidth();
            AdjustRowHeight();
        }

        private void dataGridView_ThongBao_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            // Kiểm tra xem dòng hiện tại có phải là dòng header không
            if (e.RowIndex < 0 || e.RowIndex >= dataGridView_ThongBao.Rows.Count)
                return;
            // Thiết lập mẫu dòng cho ô dữ liệu của cột đó
            dataGridView_ThongBao.Rows[e.RowIndex].Cells["TieuDe"].Style.BackColor = Color.White;
            dataGridView_ThongBao.Rows[e.RowIndex].Cells["TieuDe"].Style.ForeColor = Color.FromArgb(51, 122, 183);
            dataGridView_ThongBao.Rows[e.RowIndex].Cells["TieuDe"].Style.SelectionBackColor = Color.White;
            dataGridView_ThongBao.Rows[e.RowIndex].Cells["TieuDe"].Style.SelectionForeColor = Color.FromArgb(51, 122, 183);
        }

        private void dataGridView_ThongBao_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dataGridView_ThongBao.Columns["TieuDe"].Index && e.RowIndex >= 0)
            {
                DataGridViewCell cell = dataGridView_ThongBao.Rows[e.RowIndex].Cells[e.ColumnIndex];
                cell.Style.Font = new Font("Segoe UI", 14, FontStyle.Bold | FontStyle.Underline);
                cell.Style.ForeColor = Color.FromArgb(35, 82, 124); // Đổi màu chữ tùy chọn
            }
        }

        private void dataGridView_ThongBao_CellMouseLeave(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dataGridView_ThongBao.Columns["TieuDe"].Index && e.RowIndex >= 0)
            {
                DataGridViewCell cell = dataGridView_ThongBao.Rows[e.RowIndex].Cells[e.ColumnIndex];
                cell.Style.Font = new Font("Segoe UI", 14, FontStyle.Underline);
                cell.Style.ForeColor = Color.FromArgb(51, 122, 183); // Trở lại màu chữ mặc định
            }
        }

        private void button_ChangePage1_Click(object sender, EventArgs e)
        {
            pageNumber = Convert.ToInt32(button_ChangePage1.Text);

            if (button_ChangePage1.Text != "1")
            {
                LoadData();
                CreateOrderForButtonChangePageByPageNumber(pageNumber);
                ResetColorButton();
                HighlightButtonCurrentPage(button_ChangePage2);
            }
            else
            {
                LoadData();
                ResetColorButton();
                HighlightButtonCurrentPage(button_ChangePage1);
            }
        }

        private void button_ChangePage2_Click(object sender, EventArgs e)
        {
            if (lastPageNumber == 1) return;
            pageNumber = Convert.ToInt32(button_ChangePage2.Text);
            LoadData();
            ResetColorButton();
            HighlightButtonCurrentPage(sender);
        }

        private void button_ChangePage3_Click(object sender, EventArgs e)
        {
            if (lastPageNumber <= 2) return;
            pageNumber = Convert.ToInt32(button_ChangePage3.Text);

            if (lastPageNumber > pageNumber)
            {
                LoadData();
                CreateOrderForButtonChangePageByPageNumber(pageNumber);
                ResetColorButton();
                HighlightButtonCurrentPage(button_ChangePage2);
            }
            else
            {
                LoadData();
                ResetColorButton();
                HighlightButtonCurrentPage(button_ChangePage3);
            }
        }

        private void button_ReturnFirstPage_Click(object sender, EventArgs e)
        {
            pageNumber = 1;
            LoadData();
            ResetColorButton();
            HighlightButtonCurrentPage(button_ChangePage1);
            SetDefaultButtonChangePageText();
        }

        private void button_ReturnLastPage_Click(object sender, EventArgs e)
        {
            pageNumber = lastPageNumber;
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
        #endregion
    }
}
