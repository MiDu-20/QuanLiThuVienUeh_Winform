using QuanLiThuVienUeh.UserControlUsing;
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

namespace QuanLiThuVienUeh.nhanvien
{
    public partial class ff_LichLamViec : Form
    {
        DateTime DangKyDate, TodayDate;
        static int ThisSTTCa;
        List<string> dates;
        QLTVEntities database;
        int idNhanVien;
        public ff_LichLamViec(int idNhanVien,string Date, UserCaControl userCaControl)
        {
            InitializeComponent();
            this.idNhanVien = idNhanVien;
            DangKyDate = DateTime.ParseExact(Date, "yyyy-MM-dd", CultureInfo.InvariantCulture);
            TodayDate = DateTime.Now.Date;
            txtSTTCa.Text = userCaControl.getSTTCa().ToString();
            ThisSTTCa = userCaControl.getSTTCa();
            database = new QLTVEntities();
            LoadTableLayOutPanel();
            LoadCheckBox();
        }
        private void AddCaLam()
        {
            int soNhanVien1 = database.DangKyCaLam.Where(s => s.IDNhanVien == idNhanVien && s.NgayDuocDangKy == DangKyDate).Count();
            int CountDangKyCaLam = 0;
            if (soNhanVien1 == 0)
            {
                var newThongTinCaLam = new DangKyCaLam
                {
                    NgayDangKy = TodayDate,
                    NgayDuocDangKy = DangKyDate,
                    STTCaYeuCau = ThisSTTCa,
                    IDNhanVien = idNhanVien
                };
                CountDangKyCaLam++;
                database.DangKyCaLam.Add(newThongTinCaLam);
            }
            for (int i = 0; i < dates.Count; i++)
            {
                DateTime TryParseDate = DateTime.ParseExact(dates[i], "yyyy-MM-dd", CultureInfo.InvariantCulture).Date;
                int soNhanVien2 = database.DangKyCaLam.Where(s => s.IDNhanVien == idNhanVien && s.NgayDuocDangKy == TryParseDate).Count();
                if (soNhanVien2 == 0)
                {
                    var newThongTinCaLam1 = new DangKyCaLam
                    {
                        NgayDangKy = TodayDate,
                        NgayDuocDangKy = TryParseDate,
                        STTCaYeuCau = ThisSTTCa,
                        IDNhanVien = idNhanVien,
                    };
                    database.DangKyCaLam.Add(newThongTinCaLam1);
                    CountDangKyCaLam++;
                }
            }
            database.SaveChanges();
            if (CountDangKyCaLam == 0)
            {
                MessageBoxShow("Bạn đã đăng kí các ca làm này rồi!", "Thông báo");
            }
            else
            {
                MessageBoxShow("Đã đăng ký thành công " + CountDangKyCaLam + " Ca làm", "Thành công");
                this.Close();
            }
        }
        private void MessageBoxShow(string text, string caption)
        {
            MessageBox.Show(text, caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
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
        public void ClearDayReview()
        {
            for (int i = 1; i < tableLayoutDayPanel.RowCount; i++)
            {
                // Lặp qua từng ô trong hàng
                for (int j = 0; j < tableLayoutDayPanel.ColumnCount; j++)
                {
                    Control control = tableLayoutDayPanel.GetControlFromPosition(j, i);
                    if (control != null)
                    {
                        tableLayoutDayPanel.Controls.Remove(control);
                        control.Dispose();
                    }
                }
            }
        }
        public void LoadTableLayOutPanel()
        {
            ClearDayReview();
            for (int i = 1; i < tableLayoutDayPanel.RowCount; i++)
            {
                for (int j = 0; j < tableLayoutDayPanel.ColumnCount; j++)
                {
                    DayReview panel = new DayReview();
                    panel.BorderStyle = BorderStyle.FixedSingle;
                    if (i == 1)
                    {
                        panel.BackColor = Color.FromArgb(40, 95, 103);
                        panel.GetLabel().ForeColor = Color.White;
                    }
                    else
                    {
                        panel.BackColor = Color.FromArgb(226, 116, 73);
                        panel.GetLabel().ForeColor = Color.Black;
                    }
                    panel.Dock = DockStyle.Fill;
                    panel.ChangeTime(TableLayoutPanelHandle(j, i));
                    tableLayoutDayPanel.Controls.Add(panel, j, i); // Thêm panel vào TableLayoutPanel
                    tableLayoutDayPanel.SetRow(panel, i);
                    tableLayoutDayPanel.SetColumn(panel, j);
                }
            }
        }
        public string TableLayoutPanelHandle(int Index, int STTca)
        {
            DateTime ngayDauTienTrongTuan = DangKyDate.AddDays(-(int)DangKyDate.DayOfWeek);
            DateTime usingdate = ngayDauTienTrongTuan.Date.AddDays(Index);

            using (var dbContext = new QLTVEntities())
            {
                var caLam = dbContext.QuanLiCaLam.FirstOrDefault(q => q.Ngay == usingdate && q.STTCa == STTca);
                if (caLam != null)
                {
                    TimeSpan gioBatDau = (TimeSpan)caLam.GioBatDau;
                    TimeSpan gioKetThuc = (TimeSpan)caLam.GioKetThuc;

                    return changeTimeNow(gioBatDau, gioKetThuc);
                }
                else
                {
                    return "Trống";
                }
            }

        }
        public string changeTimeNow(TimeSpan GioBatDau, TimeSpan GioKetThuc)
        {
            string text = "";
            string text2 = "";
            text = ((GioBatDau.Minutes >= 10) ? GioBatDau.Minutes.ToString() : ("0" + GioBatDau.Minutes));
            text2 = ((GioKetThuc.Minutes >= 10) ? GioKetThuc.Minutes.ToString() : ("0" + GioKetThuc.Minutes));
            return GioBatDau.Hours + ":" + text + "\n-\n" + GioKetThuc.Hours + ":" + text2;
        }

        private void buttonDangKy_Click(object sender, EventArgs e)
        {
            CheckBoxCheck();
            AddCaLam();
        }

        private void LoadCheckBox()
        {
            DayOfWeek dayOfWeek = DangKyDate.DayOfWeek;
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
                    int thisday = (Convert.ToInt32(DangKyDate.Day) +
                    (Convert.ToInt32(x.Tag.ToString()) - Thu.Item2));
                    string month = "";
                    string day = "";
                    if (DangKyDate.Month < 10)
                    {
                        month = "0" + DangKyDate.Month.ToString();
                    }
                    else
                    {
                        month = DangKyDate.Month.ToString();
                    }
                    if (thisday < 10)
                    {
                        day = "0" + thisday.ToString();
                    }
                    else
                    {
                        day = thisday.ToString();
                    }
                    if (thisday <= DateTime.DaysInMonth(DangKyDate.Year, DangKyDate.Month))
                    {
                        TagDate = DangKyDate.Year + "-" + month + "-" + day;
                        x.Tag = TagDate;
                    }
                }
            }
        }
    }
}
