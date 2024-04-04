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
    public partial class ff_Reward : Form
    {
        private static int IDNguoiDung;
        public ff_Reward(int ID)
        {
            InitializeComponent();
            IDNguoiDung = ID;
        }
        private void UpdateScore()
        {
            try
            {
                using (var db = new QLTVEntities())
                {
                    var DiemVaThuong = db.DiemVaThuong.Where(p => p.IDNguoiDung == IDNguoiDung).FirstOrDefault();
                    var UserTuido = db.TuiDo.Where(p => p.IDNguoiDung == IDNguoiDung).FirstOrDefault();
                    if (UserTuido == null)
                    {
                        var newUserTuido = new TuiDo()
                        {
                            IDNguoiDung = IDNguoiDung,
                            CountBookReward = 0,
                            CountWeekReward = 0
                        };
                        db.TuiDo.Add(newUserTuido);
                    }
                    else
                    {
                        labelCurrentCouponBook.Text = "Phiếu hiện tại\n" + UserTuido.CountBookReward.ToString();
                        labelCurrentCouponWeek.Text = "Phiếu hiện tại\n" + UserTuido.CountWeekReward.ToString();
                    }
                    if (DiemVaThuong == null)
                    {
                        DiemVaThuong newDiemVaThuong = new DiemVaThuong()
                        {
                            IDNguoiDung = IDNguoiDung,
                            Diem = 0,
                            Ngay = DateTime.Now.Date,
                            GameTetris = 0,
                            GameQuiz = 0,
                            GameMineSweeper = 0
                        };
                        db.DiemVaThuong.Add(newDiemVaThuong);
                    }
                    else
                    {
                        int? Diem = DiemVaThuong.Diem;
                        if (Diem is int diem)
                        {
                            labelCurrentScore.Text = "Điểm thưởng tích lũy hiện tại: " + diem;
                        }
                    }
                    db.SaveChanges();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Something went wrong", "Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }
        }
        private void DisplayPanel()
        {
            using (var db = new QLTVEntities())
            {
                var maphanthuongbook = db.MoreBookReward.FirstOrDefault();
                var maphanthuongweek = db.MoreWeekReward.FirstOrDefault();
                if (maphanthuongbook != null && maphanthuongweek != null)
                {
                    labelRequestScore.Text = "Số điểm cần\n" + maphanthuongbook.DiemCanDeDoi;
                    labelRequestScore2.Text = "Số điểm cần\n" + maphanthuongweek.DiemCanDeDoi;
                }
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            UpdateScore();
            DisplayPanel();
        }

        private void btnTrade1_Click(object sender, EventArgs e)
        {
            using (var db = new QLTVEntities())
            {
                var DiemVaThuong = db.DiemVaThuong.Where(p => p.IDNguoiDung == IDNguoiDung).FirstOrDefault();
                var maphanthuongbook = db.MoreBookReward.FirstOrDefault();
                var UserTuido = db.TuiDo.Where(p => p.IDNguoiDung == IDNguoiDung).FirstOrDefault();
                int DiemUser = DiemVaThuong.Diem ?? 0;
                if (DiemVaThuong != null && maphanthuongbook != null)
                {
                    if (DiemUser >= maphanthuongbook.DiemCanDeDoi)
                    {
                        DiemVaThuong.Diem = DiemVaThuong.Diem - maphanthuongbook.DiemCanDeDoi;
                        UserTuido.CountBookReward = UserTuido.CountBookReward + 1;
                    }
                    else
                    {
                        MessageBox.Show("Không đủ điểm đề đổi!", "Thất bại");
                    }
                }
                db.SaveChanges();
            }
            Form1_Load(sender, e);
        }

        private void btnTrade2_Click(object sender, EventArgs e)
        {
            using (var db = new QLTVEntities())
            {
                var DiemVaThuong = db.DiemVaThuong.Where(p => p.IDNguoiDung == IDNguoiDung).FirstOrDefault();
                var maphanthuongweek = db.MoreWeekReward.FirstOrDefault();
                var UserTuido = db.TuiDo.Where(p => p.IDNguoiDung == IDNguoiDung).FirstOrDefault();
                int DiemUser = DiemVaThuong.Diem ?? 0;
                if (DiemVaThuong != null && maphanthuongweek != null)
                {
                    if (DiemUser >= maphanthuongweek.DiemCanDeDoi)
                    {
                        DiemVaThuong.Diem = DiemVaThuong.Diem - maphanthuongweek.DiemCanDeDoi;
                        UserTuido.CountWeekReward = UserTuido.CountWeekReward + 1;
                    }
                    else
                    {
                        MessageBox.Show("Không đủ điểm đề đổi!", "Thất bại");
                    }
                }
                db.SaveChanges();
            }
            Form1_Load(sender, e);
        }
    }
}
