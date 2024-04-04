using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLiThuVienUeh.nguoidung.game.QuizGame
{
    public partial class QuizGame : Form
    {
        public List<CauHoi> CauHoiList;
        public CauHoi UsingCauHoi;
        public int CountCauHoi = 0, Score = 0;
        public int IDNguoiDung;
        public QuizGame(int ID)
        {
            InitializeComponent();
            DataAdding();
            PrintData(0);
            IDNguoiDung = ID;
        }
        private void DataAdding()
        {
            CauHoiList = new List<CauHoi>();
            using (var dbContext = new QLTVEntities())
            {
                // Truy vấn dữ liệu từ bảng CauHoi và chuyển đổi kết quả thành danh sách
                var cauHoiEntities = dbContext.CauHoi.ToList();
                // Duyệt qua từng đối tượng CauHoi và thêm vào danh sách CauHois
                foreach (var cauHoiEntity in cauHoiEntities)
                {
                    CauHoi cauHoi = new CauHoi
                    {
                        CauHoi1 = cauHoiEntity.CauHoi1,
                        CauTraLoi1 = cauHoiEntity.CauTraLoi1,
                        CauTraLoi2 = cauHoiEntity.CauTraLoi2,
                        CauTraLoi3 = cauHoiEntity.CauTraLoi3,
                        CauTraLoi4 = cauHoiEntity.CauTraLoi4,
                        HinhAnh = cauHoiEntity.HinhAnh,
                        DapAn = cauHoiEntity.DapAn
                    };
                    ShuffleAnswers(cauHoi);
                    CauHoiList.Add(cauHoi);
                }
            }
            var random = new Random();
            CauHoiList = CauHoiList.OrderBy(x => random.Next()).ToList();
        }
        public void ShuffleAnswers(CauHoi cauHoi)
        {
            var random = new Random();
            List<string> answers = new List<string>
            {
            cauHoi.CauTraLoi1,
            cauHoi.CauTraLoi2,
            cauHoi.CauTraLoi3,
            cauHoi.CauTraLoi4
            };
            answers = answers.OrderBy(x => random.Next()).ToList();
            cauHoi.CauTraLoi1 = answers[0];
            cauHoi.CauTraLoi2 = answers[1];
            cauHoi.CauTraLoi3 = answers[2];
            cauHoi.CauTraLoi4 = answers[3];
        }
        private void UpdateDatabase()
        {
            using (var db = new QLTVEntities())
            {
                DateTime dateTime = DateTime.Now.Date;
                var dbquiz = db.Quiz.FirstOrDefault(p => p.IDNguoiDung == IDNguoiDung && p.Ngay == dateTime);
                var dbDiemvaThuong = db.DiemVaThuong.FirstOrDefault(p => p.IDNguoiDung == IDNguoiDung);
                if (dbquiz != null)
                {
                    dbquiz.DiemQuiz = Score;
                    dbquiz.Limit = true;
                }
                if (dbDiemvaThuong != null)
                {
                    dbDiemvaThuong.GameQuiz = Score;
                    dbDiemvaThuong.Diem = dbDiemvaThuong.Diem + dbDiemvaThuong.GameQuiz;
                }
                db.SaveChanges();
            }
        }
        private void PrintData(int QuestionIndex)
        {
            if (QuestionIndex == 10)
            {
                MessageBox.Show("Kết thúc, điểm của bạn là: " + Score, "The End!");
                UpdateDatabase();
                this.Close();
            }
            labelQuestion.Text = ++CountCauHoi + ". " + CauHoiList[QuestionIndex].CauHoi1;
            buttonA.Text = CauHoiList[QuestionIndex].CauTraLoi1;
            buttonB.Text = CauHoiList[QuestionIndex].CauTraLoi2;
            buttonC.Text = CauHoiList[QuestionIndex].CauTraLoi3;
            buttonD.Text = CauHoiList[QuestionIndex].CauTraLoi4;
            InsertImage(CauHoiList[QuestionIndex].HinhAnh);
            UsingCauHoi = CauHoiList[QuestionIndex];
        }
        private void InsertImage(string link)
        {
            Image Hinh = Properties.Resources.ResourceManager.GetObject(link) as Image;
            pictureBoxQuestion.Image = Hinh;
        }
        private void ResetQuiz()
        {
            CountCauHoi = 0;
            Score = 0;
            PrintData(0);
        }
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void QuizGameForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            ResetQuiz();
        }

        private void btnSolve_Click(object sender, EventArgs e)
        {
            Button Answer = sender as Button;
            if (Answer.Text.Trim() == UsingCauHoi.DapAn.Trim())
            {
                Score += 10;
            }
            PrintData(CountCauHoi);
        }
    }
}
