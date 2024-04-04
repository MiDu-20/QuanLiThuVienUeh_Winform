using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UserCa;

namespace QuanLiThuVienUeh.admin
{
    public partial class ffc_TaoCaLam : Form
    {
        string DateNow;
        DateTime TodayDate;
        List<string> dates;
        QLTVEntities database;
        UserCaControl userCaControl;
        public ffc_TaoCaLam(string Date) //Gọi form dạng tạo mới
        {
            InitializeComponent();
            DateNow = Date;
            buttonChinhSua.Enabled = false;
            TodayDate = DateTime.ParseExact(Date, "yyyy-MM-dd", CultureInfo.InvariantCulture);
            database = new QLTVEntities();
        }
        public ffc_TaoCaLam(string Date, UserCaControl user) //Gọi form dạng chỉnh sửa
        {
            InitializeComponent();
            ToggleCbPanel();
            userCaControl = user;
            DateNow = Date;
            txtSTTCa.Text = userCaControl.getSTTCa().ToString();
            txtMaNhanVien.Text = StringHandleID(userCaControl.getID()).ToString();
            numericHour.Value = userCaControl.getStartTime().Hours;
            numericMinute.Value = userCaControl.getStartTime().Minutes;
            numericHourEnd.Value = userCaControl.getEndTime().Hours;
            numericMinuteEnd.Value = userCaControl.getEndTime().Minutes;
            buttonDangKy.Enabled = false;
            database = new QLTVEntities();
            TodayDate = DateTime.ParseExact(Date, "yyyy-MM-dd", CultureInfo.InvariantCulture);
            DataGridViewLoad();
        }
        public void DataGridViewLoad()
        {
            int STTCa = Convert.ToInt32(txtSTTCa.Text);
            var Dangkys = database.DangKyCaLam.Where(dk => dk.NgayDuocDangKy == TodayDate && dk.STTCaYeuCau == STTCa).ToList();
            dtgvFormDangKy.DataSource = Dangkys;
            foreach (DataGridViewColumn column in dtgvFormDangKy.Columns)
            {
                column.Visible = false;
            }

            // Hiển thị các cột cần thiết
            dtgvFormDangKy.Columns["NgayDangKy"].Visible = true;
            dtgvFormDangKy.Columns["NgayDangKy"].HeaderText = "Ngày Đăng Ký";
            dtgvFormDangKy.Columns["IDNhanVien"].Visible = true;
            dtgvFormDangKy.Columns["IDNhanVien"].HeaderText = "ID Nhân Viên";
            dtgvFormDangKy.ReadOnly = true;
        }
        private string StringHandleID(string input)
        {
            string[] parts = input.Split(' ');
            if (parts.Length == 2)
            {
                string numberPart = parts[1].Trim();
                string number = numberPart;
                return number;
            }
            return "";
        }
        //private string StringHandlePhoneNumberHoTen (string input)
        //{
        //    string[] parts = input.Split(':');
        //    if (parts.Length == 2)
        //    {
        //        string numberPart = parts[1].Trim();
        //        string number = numberPart;
        //        return number;
        //    }
        //    return "";
        //}
        private void MessageBoxShow(string text, string caption)
        {
            MessageBox.Show(text, caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        private void FormDangKy_Load(object sender, EventArgs e)
        {
            LoadCheckBox();
            txtNgayDangKy.Text = DateNow;
            using (var dbContext = new QLTVEntities())
            {
                var caLams = dbContext.QuanLiCaLam.Where(q => q.Ngay == TodayDate).ToList();
                if (caLams.Count == 0 && string.IsNullOrEmpty(txtSTTCa.Text))
                {
                    txtSTTCa.Text = "1";
                }
                else if (string.IsNullOrEmpty(txtSTTCa.Text))
                {
                    int maxSTTCa = Convert.ToInt32(caLams.Max(q => q.STTCa));
                    txtSTTCa.Text = (maxSTTCa + 1).ToString();
                }
                int STTCa = int.Parse(txtSTTCa.Text);
                var quanLyCaLam = database.QuanLiCaLam.FirstOrDefault(q => q.STTCa == STTCa
                && q.Ngay == TodayDate);
                if (quanLyCaLam != null)
                {
                    if (quanLyCaLam.IDNhanVien != null)
                    {
                        txtMaNhanVien.Text = quanLyCaLam.IDNhanVien.ToString();
                    }
                }
            }
        }
        private void ToggleCbPanel()
        {
            foreach (CheckBox x in cbPanel.Controls)
            {
                x.Enabled = false;
            }
        }
        private void LoadCheckBox()
        {
            DayOfWeek dayOfWeek = TodayDate.DayOfWeek;
            Tuple<string, int> Thu = new Tuple<string, int>("", 0);
            switch ((int)dayOfWeek + 1)
            {
                case 1:
                    Thu = new Tuple<string, int>("Sun", 8);
                    break;
                case 2:
                    Thu = new Tuple<string, int>("Mon", 2);
                    break;
                case 3:
                    Thu = new Tuple<string, int>("Tue", 3);
                    break;
                case 4:
                    Thu = new Tuple<string, int>("Wed", 4);
                    break;
                case 5:
                    Thu = new Tuple<string, int>("Thu", 5);
                    break;
                case 6:
                    Thu = new Tuple<string, int>("Fri", 6);
                    break;
                case 7:
                    Thu = new Tuple<string, int>("Sat", 7);
                    break;
            }
            foreach (CheckBox x in cbPanel.Controls.OfType<CheckBox>())
            {
                if (x.Name.Contains(Thu.Item1))
                {
                    x.Checked = true;
                    x.Enabled = false;
                }
                else if (int.Parse(x.Tag.ToString()) < Thu.Item2)
                {
                    x.Enabled = false;
                }
            }
            foreach (CheckBox x in cbPanel.Controls.OfType<CheckBox>())
            {
                string TagDate = "";
                if (int.Parse(x.Tag.ToString()) > Thu.Item2)
                {
                    int thisday = (Convert.ToInt32(TodayDate.Day) +
                    (Convert.ToInt32(x.Tag.ToString()) - Thu.Item2));
                    string month = "";
                    string day = "";
                    if (TodayDate.Month < 10)
                    {
                        month = "0" + TodayDate.Month.ToString();
                    }
                    else
                    {
                        month = TodayDate.Month.ToString();
                    }
                    if (thisday < 10)
                    {
                        day = "0" + thisday.ToString();
                    }
                    else
                    {
                        day = thisday.ToString();
                    }
                    if (thisday <= DateTime.DaysInMonth(TodayDate.Year, TodayDate.Month))
                    {
                        TagDate = TodayDate.Year + "-" + month + "-" + day;
                        x.Tag = TagDate;
                    }
                }
            }
        }

        private void buttonChinhSua_Click(object sender, EventArgs e)
        {
            int sttCa = int.Parse(txtSTTCa.Text);
            TimeSpan GioBatDau = new TimeSpan(Convert.ToInt32(numericHour.Value), Convert.ToInt32(numericMinute.Value), 0);
            TimeSpan GioKetThuc = new TimeSpan(Convert.ToInt32(numericHourEnd.Value), Convert.ToInt32(numericMinuteEnd.Value), 0);
            var quanLyCaLam = database.QuanLiCaLam.FirstOrDefault(q => q.STTCa == sttCa
            && q.Ngay == TodayDate);
            if (!string.IsNullOrEmpty(txtMaNhanVien.Text))
            {
                if (quanLyCaLam != null)
                {
                    quanLyCaLam.IDNhanVien = Convert.ToInt32(txtMaNhanVien.Text);
                    quanLyCaLam.GioBatDau = GioBatDau;
                    quanLyCaLam.GioKetThuc = GioKetThuc;
                    database.SaveChanges();
                    MessageBoxShow("Đã cập nhật MaNhanVien thành công", "Thành công");
                }
                else
                {
                    MessageBoxShow("Không tìm thấy dữ liệu cần cập nhật!", "Thất bại");
                }
            }
            else
            {
                MessageBoxShow("Mã nhân viên không được để trống", "Thất bại");
            }
        }
        private void AddCaLam()
        {
            int CountCaLam = 1;
            TimeSpan GioBatDau = new TimeSpan(Convert.ToInt32(numericHour.Value), Convert.ToInt32(numericMinute.Value), 0);
            TimeSpan GioKetThuc = new TimeSpan(Convert.ToInt32(numericHourEnd.Value), Convert.ToInt32(numericMinuteEnd.Value), 0);
            int existingCaLamCount = database.QuanLiCaLam.Count(q => q.Ngay == TodayDate);
            int newSTTCa = existingCaLamCount + 1;
            var newCaLam = new QuanLiCaLam
            {
                Ngay = TodayDate,
                STTCa = newSTTCa,
                GioBatDau = GioBatDau,
                GioKetThuc = GioKetThuc
            };
            database.QuanLiCaLam.Add(newCaLam);
            for (int i = 0; i < dates.Count; i++)
            {
                DateTime TryParseDate = DateTime.ParseExact(dates[i], "yyyy-MM-dd", CultureInfo.InvariantCulture);
                var CaLam = new QuanLiCaLam
                {
                    Ngay = TryParseDate,
                    STTCa = newSTTCa,
                    GioBatDau = GioBatDau,
                    GioKetThuc = GioKetThuc
                };
                database.QuanLiCaLam.Add(CaLam);
                CountCaLam++;
            }
            MessageBoxShow("Đã thêm thành công " + CountCaLam + " Ca làm", "Thành công");
            database.SaveChanges();
            this.Close();
        }
        private void buttonDangKy_Click(object sender, EventArgs e)
        {
            CheckBoxCheck();
            AddCaLam();
        }
        private void numericHour_ValueChanged(object sender, EventArgs e)
        {
            numericHourEnd.Minimum = Convert.ToInt32(numericHour.Value) + 3;
            numericHourEnd.Value = Convert.ToInt32(numericHour.Value) + 3;
        }
        private void CheckBoxCheck()
        {
            dates = new List<string>();
            foreach (CheckBox x in cbPanel.Controls)
            {
                if (x.Checked && x.Tag.ToString().Length > 1)
                {
                    dates.Add(x.Tag.ToString());
                }
            }
        }
        private void cbMon_Click(object sender, EventArgs e)
        {
            //dates = new List<string>();
            //foreach (CheckBox x in cbPanel.Controls.OfType<CheckBox>())
            //{
            //    if (x.Checked && x.Tag.ToString().Length > 1)
            //    {
            //        dates.Add(x.Tag.ToString());
            //    }
            //}
            //label6.Text = "";
            //foreach (string x in dates)
            //{
            //    label6.Text += x.ToString() + " ";
            //}
        }

        private void dtgvFormDangKy_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dtgvFormDangKy_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dtgvFormDangKy.Rows[e.RowIndex];
                object maNhanVien = row.Cells["IDNhanVien"].Value;
                if (maNhanVien != null)
                {
                    txtMaNhanVien.Text = maNhanVien.ToString();
                }
            }
        }
    }
}
