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
    public partial class ffc_TheLoaiSach : Form
    {
        private DataTable dataSet()
        {
            ffc_QuanLiSach quanLySach = new ffc_QuanLiSach();
            DataTable dataTable = new DataTable("MyTable");
            dataTable.Columns.Add("Tình trạng", typeof(string));
            dataTable.Columns.Add("Số lượng", typeof(double));
            Dictionary<string, int> TheLoaiSach = quanLySach.GetBookCountsByGenre();
            foreach (var x in TheLoaiSach)
            {
                dataTable.Rows.Add(x.Key, x.Value);
            }

            return dataTable;
        }
        public ffc_TheLoaiSach()
        {
            InitializeComponent();
            var creatorChart = new ffc_CreatorChart();
            creatorChart.ChartHorizontalBar(chartThongKe, dataSet(), "Tổng số lượng sách theo từng thể loại");
        }
    }
}
