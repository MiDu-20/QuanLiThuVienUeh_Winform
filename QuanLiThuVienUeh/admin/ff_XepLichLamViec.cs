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

namespace QuanLiThuVienUeh.admin
{
    public partial class ff_XepLichLamViec : Form
    {
        QLTVEntities database = new QLTVEntities();
        public ff_XepLichLamViec()
        {
            InitializeComponent();
            DisplayDays();
            UpdateDay();
        }
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
            btnRestart_Click(sender, e);
        }

        private void MoveUp_Click(object sender, EventArgs e)
        {
            if (month < 12)
            {
                dayFlowpanel.Controls.Clear();
                month++;
                Update();
            }
            btnRestart_Click(sender, e);

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
            btnRestart_Click(sender, e);

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
        private void btnRestart_Click(object sender, EventArgs e)
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
                            userCaControl.changeID(nhanVien.IDNhanVien.ToString());
                            userCaControl.changeHoTen(nhanVien.HoTen);
                            userCaControl.changePhoneNumber(nhanVien.SoDienThoai);
                        }
                        userCaControl.Click += userCaControl1_Click;
                        flowCaPanel.Controls.Add(userCaControl);
                    }
                }
            }
        }
        private void MessageBoxShow(string text, string caption)
        {
            MessageBox.Show(text, caption, MessageBoxButtons.OK, MessageBoxIcon.Information);

        }
        private void userCaControl1_Click(object sender, EventArgs e)
        {
            UserCaControl userCaControl = sender as UserCaControl;
            ffc_TaoCaLam form = new ffc_TaoCaLam(takeTimePicked(), userCaControl);
            if (userCaControl.BorderStyle != BorderStyle.FixedSingle)
            {
                form.ShowDialog();
                Form1_Load(sender, e);
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            //label1.Text = "Today: " + DateTime.Now.Day + " / " + DateTime.Now.Month + " / " + DateTime.Now.Year;
            takeTimePicked();
            btnRestart_Click(sender, e);
        }
        private void btnAdding_Click(object sender, EventArgs e)
        {
            if (DayPicked != 0)
            {
                int Count = 0;
                ffc_TaoCaLam form = new ffc_TaoCaLam(takeTimePicked());
                foreach (UserCaControl x in flowCaPanel.Controls)
                {
                    if (x is UserCaControl)
                    {
                        Count++;
                    }
                }
                if (Count == 2)
                {
                    MessageBoxShow("Đã tối đa ca một ngày!", "Thất bại");
                }
                else
                {
                    form.ShowDialog();
                    Form1_Load(sender, e);
                }
            }
            else
            {
                MessageBoxShow("Vui lòng chọn ngày!", "Thất bại");
            }
        }
        private void btnRemove_Click(object sender, EventArgs e)
        {
            UserCaControl userCaControl = null;
            foreach (UserCaControl x in flowCaPanel.Controls)
            {
                if (x.BorderStyle == BorderStyle.FixedSingle)
                {
                    userCaControl = x; break;
                }
            }
            if (userCaControl != null)
            {
                using (QLTVEntities db = new QLTVEntities())
                {
                    DateTime datenow = DateTime.Parse(takeTimePicked());
                    int STTCa = userCaControl.getSTTCa();
                    var CaLam = db.QuanLiCaLam.FirstOrDefault(p => p.Ngay == datenow
                        && p.STTCa == STTCa);
                    if (CaLam != null)
                    {
                        db.QuanLiCaLam.Remove(CaLam);
                        db.SaveChanges();
                        MessageBoxShow("Đã xóa ca vừa chọn", "Thành công");
                        btnRestart_Click(sender, e);
                    }
                    else
                    {
                        MessageBoxShow("Hãy lựa chọn 1 ca để xóa", "Thất bại");
                    }
                }
            }
        }

    }
}
