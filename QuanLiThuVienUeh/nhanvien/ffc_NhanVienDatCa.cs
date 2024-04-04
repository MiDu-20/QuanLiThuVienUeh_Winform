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
    public partial class ffc_NhanVienDatCa : Form
    {
        int idNhanVien;
        public ffc_NhanVienDatCa(int idNhanVien)
        {
            InitializeComponent();
            this.idNhanVien = idNhanVien;
        }
        private void Form_NhanVienDatCa_Load(object sender, EventArgs e)
        {
            DisplayDays();
            UpdateDay();
            LoadTableLayOutPanel();
            LoadingCaLam(sender, e);

        }

        #region TableLayOutPanel
        private void LoadingCaLam(object sender, EventArgs e)
        {
            if (DayPicked != 0)
            {
                DateTime dateTimenow = DateTime.Parse(takeTimePicked());
                using (var db = new QLTVEntities())
                {
                    var caLams = db.QuanLiCaLam.Where(q => q.Ngay == dateTimenow).ToList();
                    flowCaPanel.Controls.Clear();
                    foreach (var caLam in caLams)
                    {
                        UserCaControl userCaControl = new UserCaControl();
                        userCaControl.Width = flowCaPanel.Width - 5;
                        userCaControl.changeButtonCaNum("Ca" + caLam.STTCa);
                        userCaControl.changeTimeNow((TimeSpan)caLam.GioBatDau, (TimeSpan)caLam.GioKetThuc);
                        userCaControl.changeID(caLam.IDNhanVien.ToString());
                        var nhanVien = db.NhanVien.FirstOrDefault(nv => nv.IDNhanVien == caLam.IDNhanVien);
                        if (nhanVien != null)
                        {
                            userCaControl.changeHoTen(nhanVien.HoTen);
                            userCaControl.changePhoneNumber(nhanVien.SoDienThoai);
                        }
                        userCaControl.Click += userCaControl1_Click;
                        flowCaPanel.Controls.Add(userCaControl);
                    }
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
            DateTime date = DateTime.ParseExact(takeTimePicked(), "yyyy-MM-dd", CultureInfo.InvariantCulture);
            DateTime ngayDauTienTrongTuan = date.AddDays(-(int)date.DayOfWeek);
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
        #endregion
        #region UserCalendar
        public static int month, year, DayPicked, DayNum;
        public static List<int> Days;
        private void WireAllControls(Control cont)
        {
            foreach (Control ctl in cont.Controls)
            {
                ctl.Click += ctl_Click;
                if (ctl.HasChildren)
                {
                    WireAllControls(ctl);
                }
            }
        }
        private void ClearCadetSlot()
        {
            foreach (Control x in dayFlowpanel.Controls)
            {
                if (x is UserDay)
                {
                    if (x.BackColor == Color.CadetBlue)
                    {
                        x.BackColor = Color.White;
                    }
                }
            }
        }
        private void ctl_Click(object sender, EventArgs e)
        {
            this.InvokeOnClick(dayFlowpanel, EventArgs.Empty);

        }
        private void MoveDown_Click(object sender, EventArgs e)
        {
            if (month > 1)
            {
                dayFlowpanel.Controls.Clear();
                month--;
                Update();
            }
        }

        private void MoveUp_Click(object sender, EventArgs e)
        {
            if (month < 12)
            {
                dayFlowpanel.Controls.Clear();
                month++;
                Update();
            }
        }
        private void UpdateDay()
        {
            foreach (Control x in dayFlowpanel.Controls)
            {
                if (x is UserDay x1)
                {
                    if (x1.BackColor == Color.CadetBlue)
                    {
                        DayPicked = Convert.ToInt32(x1.labelText());
                    }
                }
            }
        }
        private void DisplayDays()
        {
            DateTime now = DateTime.Now;
            month = now.Month;
            year = now.Year;
            Update();
        }
        public void Update()
        {
            string monthname = DateTimeFormatInfo.CurrentInfo.GetMonthName(month);
            labelYearMonth.Text = monthname + " " + year;
            DateTime startofthemonth = new DateTime(year, month, 1);
            int days = DateTime.DaysInMonth(year, month);
            int dayoftheweek = Convert.ToInt32(startofthemonth.DayOfWeek.ToString("d")) + 1;
            for (int i = 1; i < dayoftheweek; i++)
            {
                UserBlank Blank = new UserBlank();
                dayFlowpanel.Controls.Add(Blank);
            }
            for (int i = 1; i <= days; i++)
            {
                UserDay DayControl = new UserDay();
                DayControl.days(i);
                if (i == DateTime.Now.Day)
                {
                    DayControl.BackColor = Color.CadetBlue;
                    DayPicked = DateTime.Now.Day;
                    Days = new List<int>();
                    Days.Add(DayPicked);
                }
                DayControl.Click += DayControl_Click;
                dayFlowpanel.Controls.Add(DayControl);
            }
            WireAllControls(dayFlowpanel);
        }
        private void DayControl_Click(object sender, EventArgs e)
        {
            UserDay pressedbutton = sender as UserDay;
            if (pressedbutton.BackColor == Color.White)
            {
                ClearCadetSlot();
                pressedbutton.BackColor = Color.CadetBlue;
            }
            UpdateDay();
            LoadingCaLam(sender, e);
            LoadTableLayOutPanel();

        }
        public string takeTimePicked() //Lấy ra ngày/tháng/năm đang chọn (màu xanh)
        {
            string DateNow = "";
            string MonthNow = "";
            string DayNow = "";
            if (month >= 10)
            {
                MonthNow = month.ToString();
            }
            else
            {
                MonthNow = "0" + month.ToString();
            }
            if (DayPicked < 10)
            {
                DayNow = "0" + DayPicked.ToString();
            }
            else
            {
                DayNow = DayPicked.ToString();
            }
            if (DayPicked != 0)
            {
                DateNow = year + "-" + MonthNow + "-" + DayNow;
            }
            return DateNow;
        }

        #endregion
        private void userCaControl1_Click(object sender, EventArgs e)
        {
            UserCaControl userCaControl = sender as UserCaControl;
            ff_LichLamViec form = new ff_LichLamViec(idNhanVien,takeTimePicked(), userCaControl);
            DateTime date = DateTime.ParseExact(takeTimePicked(), "yyyy-MM-dd", CultureInfo.InvariantCulture);
            if (date >= DateTime.Today)
            {
                if (userCaControl.BorderStyle != BorderStyle.FixedSingle)
                {
                    form.ShowDialog();
                }
            }
        }
    }
}
