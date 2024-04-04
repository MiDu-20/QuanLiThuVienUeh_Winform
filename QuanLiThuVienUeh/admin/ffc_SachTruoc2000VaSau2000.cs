using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Guna.Charts.WinForms;

namespace QuanLiThuVienUeh.admin
{
    public partial class ffc_SachTruoc2000VaSau2000 : Form
    {
        private DataTable dataSet()
        {
            ffc_QuanLiSach quanLySach = new ffc_QuanLiSach();
            DataTable dataTable = new DataTable("MyTable");
            dataTable.Columns.Add("Tình trạng", typeof(string));
            dataTable.Columns.Add("Số lượng", typeof(double));
            int before2000Books = quanLySach.CountBooksBefore2000();
            int over2000Books = quanLySach.CountBooksAfterOrEqualTo2000();
            dataTable.Rows.Add("Số sách xuất bản trước 2000", before2000Books);
            dataTable.Rows.Add("Số sách xuất bản sau 2000", over2000Books);
            return dataTable;
        }
        public ffc_SachTruoc2000VaSau2000()
        {
            InitializeComponent();
            var creatorChart = new ffc_CreatorChart();
            creatorChart.ChartPie(chartThongKe, dataSet(), "Số sách xuất bản trước 2000 / Số sách xuất bản sau 2000");
        }
    }
}
