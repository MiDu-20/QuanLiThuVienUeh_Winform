using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Guna.Charts.WinForms;

namespace QuanLiThuVienUeh.admin
{
    internal class ffc_CreatorChart
    {
        public bool checkEmpty(DataTable dataTable)
        {
            return dataTable.Rows.Count > 0;
        }

        public void ChartPie(GunaChart chart, DataTable data, string nameChart)
        {
            if (checkEmpty(data))
            {
                chart.Datasets.Clear();
                chart.Legend.Position = LegendPosition.Right;
                chart.Legend.Display = true;
                chart.XAxes.Display = false;
                chart.YAxes.Display = false;
                chart.Title.Text = nameChart;
                var dataset = new GunaPieDataset();
                for (int i = 0; i < data.Rows.Count; i++)
                {
                    dataset.DataPoints.Add(
                    Convert.ToString(data.Rows[i][0]),
                    Convert.ToDouble(data.Rows[i][1])
                    );
                }
                chart.Datasets.Add(dataset);
            }
            else
                MessageBox.Show("Chưa có dữ liệu.", "Thất bại");
        }
        public void ChartBar(GunaChart chart, DataTable data, string nameChart)
        {
            if (checkEmpty(data))
            {
                chart.Datasets.Clear();
                chart.Legend.Display = false;
                chart.YAxes.GridLines.Display = true;
                chart.XAxes.Display = true;
                chart.YAxes.Display = true;
                chart.Title.Text = nameChart;

                var dataset = new GunaBarDataset();
                for (int i = 0; i < data.Rows.Count; i++)
                {
                    dataset.DataPoints.Add(
                    Convert.ToString(data.Rows[i][0]),
                    Convert.ToDouble(data.Rows[i][1])
                    );
                }
                chart.Datasets.Add(dataset);
            }
            else
                MessageBox.Show("Chưa có dữ liệu.", "Thất bại");
        }
        public void ChartHorizontalBar(GunaChart chart, DataTable data, string nameChart)
        {
            if (checkEmpty(data))
            {
                chart.Datasets.Clear();
                //Chart configuration 
                chart.Legend.Display = false;
                chart.XAxes.Display = true;
                chart.YAxes.Display = true;
                chart.Title.Text = nameChart;

                var dataset = new GunaHorizontalBarDataset();
                dataset.Label = "Số lượng";
                for (int i = 0; i < data.Rows.Count; i++)
                {
                    dataset.DataPoints.Add(
                      Convert.ToString(data.Rows[i][0]),
                      Convert.ToDouble(data.Rows[i][1])
                      );
                }
                chart.Datasets.Add(dataset);
            }
            else
                MessageBox.Show("Chưa có dữ liệu.", "Thất bại");
        }
        public void ChartPolar(GunaChart chart, DataTable data, string nameChart)
        {
            if (checkEmpty(data))
            {
                //Chart configuration  
                chart.Legend.Position = LegendPosition.Right;
                chart.XAxes.Display = false;
                chart.YAxes.Display = false;
                chart.Legend.Display = true;

                chart.Datasets.Clear();
                var dataset = new GunaPolarAreaDataset();
                for (int i = 0; i < data.Rows.Count; i++)
                {
                    dataset.DataPoints.Add(
                      Convert.ToString(data.Rows[i][0]),
                      Convert.ToDouble(data.Rows[i][1])
                      );
                }
                chart.Datasets.Add(dataset);
            }
            else
                MessageBox.Show("Chưa có dữ liệu.", "Thất bại");
        }
    }
}
