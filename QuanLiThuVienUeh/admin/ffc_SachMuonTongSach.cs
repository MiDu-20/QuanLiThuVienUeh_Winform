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
    public partial class ffc_SachMuonTongSach : Form
    {
        private DataTable dataSet()
        {
            ffc_QuanLiSach quanLySach = new ffc_QuanLiSach();
            DataTable dataTable = new DataTable("MyTable");
            dataTable.Columns.Add("Tình trạng", typeof(string));
            dataTable.Columns.Add("Số lượng", typeof(double));
            int borrowedBooksCount = quanLySach.CountBorrowedBooks();
            int availableBooksCount = quanLySach.CountAvailableBooks();
            dataTable.Rows.Add("Số sách đang mượn", borrowedBooksCount);
            dataTable.Rows.Add("Tổng số sách", availableBooksCount);
            return dataTable;
        }
        public ffc_SachMuonTongSach()
        {
            InitializeComponent();
            var creatorChart = new ffc_CreatorChart();
            creatorChart.ChartPie(chartThongKe, dataSet(), "Số sách mượn / Tổng số sách");
        }
    }
}
