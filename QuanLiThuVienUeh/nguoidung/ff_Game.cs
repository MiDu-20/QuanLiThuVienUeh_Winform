using QuanLiThuVienUeh.nguoidung.game.QuizGame;
using QuanLiThuVienUeh.nguoidung.game.Tetris;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLiThuVienUeh.nguoidung
{
    public partial class ff_Game : Form
    {
        private static int IDNguoiDung;
        private string lblScore = "Today Score: ";
        public ff_Game(int ID)
        {
            InitializeComponent();
            IDNguoiDung = ID;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            SolveDatabase();
            UpdateScore();
        }
        private void LoadImageAndColor(string imgname, PictureBox Box, Label UsingLabel, Label ScoreLabel, int x, int y, int z)
        {
            Image Hinh = Properties.Resources.ResourceManager.GetObject(imgname) as Image;
            Box.Image = Hinh;
            UsingLabel.ForeColor = Color.FromArgb(x, y, z);
            ScoreLabel.ForeColor = Color.FromArgb(x, y, z);
        }
        private void TetrisPictureBox_MouseEnter(object sender, EventArgs e)
        {
            LoadImageAndColor("tetrisgif", TetrisPictureBox, labelTetris, labelTetrisscore, 242, 111, 51);
        }

        private void TetrisPictureBox_MouseLeave(object sender, EventArgs e)
        {
            LoadImageAndColor("tetris", TetrisPictureBox, labelTetris, labelTetrisscore, 0, 0, 0);
        }

        private void QuizUEHPictureBox_MouseEnter(object sender, EventArgs e)
        {
            LoadImageAndColor("uehgif", QuizUEHPictureBox, labelUEHQuiz, labelQuizscore, 242, 111, 51);
        }
        private void QuizUEHPictureBox_MouseLeave(object sender, EventArgs e)
        {
            LoadImageAndColor("ueh", QuizUEHPictureBox, labelUEHQuiz, labelQuizscore, 0, 0, 0);
        }

        private void MinesweeperPictureBox_MouseEnter(object sender, EventArgs e)
        {
            LoadImageAndColor("Minesweepergif", MinesweeperPictureBox, labelMinesweeper, labelMinesweeperscore, 242, 111, 51);

        }

        private void MinesweeperPictureBox_MouseLeave(object sender, EventArgs e)
        {
            LoadImageAndColor("Minesweeper", MinesweeperPictureBox, labelMinesweeper, labelMinesweeperscore, 0, 0, 0);

        }

        private void TetrisPictureBox_Click(object sender, EventArgs e)
        {
            TetrisGame Form = new TetrisGame(IDNguoiDung);
            Form.ShowDialog();
            Form.Close();
            Form1_Load(sender, e);
        }

        private void QuizUEHPictureBox_Click(object sender, EventArgs e)
        {
            QuizGame Form = new QuizGame(IDNguoiDung);
            Form.ShowDialog();
            Form.Close();
            Form1_Load(sender, e);
        }

        private void MinesweeperPictureBox_Click(object sender, EventArgs e)
        {
            game.MineSweeper.MineSweeper Form = new game.MineSweeper.MineSweeper(IDNguoiDung);
            Form.ShowDialog();
            Form.Close();
            Form1_Load(sender, e);
        }
        private void UpdateScore()
        {
            using (var db = new QLTVEntities())
            {
                DateTime dateTime = DateTime.Now.Date;
                var dbDiemvaThuong = db.DiemVaThuong.FirstOrDefault(p => p.IDNguoiDung == IDNguoiDung);
                var dbTetris = db.Tetris.FirstOrDefault(p => p.IDNguoiDung == IDNguoiDung && p.Ngay == dateTime);
                var dbQuiz = db.Quiz.FirstOrDefault(p => p.IDNguoiDung == IDNguoiDung && p.Ngay == dateTime);
                var dbMinesweeper = db.MineSweeper.FirstOrDefault(p => p.IDNguoiDung == IDNguoiDung && p.Ngay == dateTime);
                if (dbTetris != null && dbQuiz != null && dbMinesweeper != null)
                {
                    labelMinesweeperscore.Text = lblScore + dbDiemvaThuong.GameMineSweeper.ToString() + "/100";
                    labelQuizscore.Text = lblScore + dbDiemvaThuong.GameQuiz.ToString() + "/100"; ;
                    labelTetrisscore.Text = lblScore + dbDiemvaThuong.GameTetris.ToString() + "/100";
                }
                labelnowScore.Text = "Score: " + dbDiemvaThuong.Diem.ToString();
            }
        }
        private void SolveDatabase()
        {
            try
            {
                using (var db = new QLTVEntities())
                {
                    DateTime dateTime = DateTime.Now.Date;
                    var dbDiemvaThuong = db.DiemVaThuong.Where(p => p.IDNguoiDung == IDNguoiDung).FirstOrDefault();
                    var dbTetris = db.Tetris.Where(p => p.IDNguoiDung == IDNguoiDung && p.Ngay == dateTime).FirstOrDefault();
                    var dbQuiz = db.Quiz.Where(p => p.IDNguoiDung == IDNguoiDung && p.Ngay == dateTime).FirstOrDefault();
                    var dbMinesweeper = db.MineSweeper.Where(p => p.IDNguoiDung == IDNguoiDung && p.Ngay == dateTime).FirstOrDefault();
                    if (dbDiemvaThuong == null)
                    {
                        DiemVaThuong newDiemvaThuong = new DiemVaThuong
                        {
                            IDNguoiDung = IDNguoiDung,
                            Diem = 0,
                            Ngay = DateTime.Now.Date,
                            GameTetris = 0,
                            GameQuiz = 0,
                            GameMineSweeper = 0
                        };
                        db.DiemVaThuong.Add(newDiemvaThuong);
                    }
                    else
                    {
                        if (dbDiemvaThuong.Ngay != DateTime.Now.Date)
                        {
                            dbDiemvaThuong.Ngay = DateTime.Now.Date;
                            dbDiemvaThuong.GameTetris = 0;
                            dbDiemvaThuong.GameQuiz = 0;
                            dbDiemvaThuong.GameMineSweeper = 0;
                        }
                    }
                    db.SaveChanges();
                    if (dbTetris == null && dbQuiz == null && dbMinesweeper == null)
                    {
                        var newTetris = new Tetris
                        {
                            IDNguoiDung = IDNguoiDung,
                            Ngay = DateTime.Now.Date,
                            DiemTetris = 0,
                            Limit = false
                        };
                        var newQuiz = new Quiz
                        {
                            IDNguoiDung = IDNguoiDung,
                            Ngay = DateTime.Now.Date,
                            DiemQuiz = 0,
                            Limit = false
                        };
                        var newMinesweeper = new MineSweeper
                        {
                            IDNguoiDung = IDNguoiDung,
                            Ngay = DateTime.Now.Date,
                            DiemMineSweeper = 0,
                            Limit = false
                        };
                        db.Tetris.Add(newTetris);
                        db.Quiz.Add(newQuiz);
                        db.MineSweeper.Add(newMinesweeper);
                        db.SaveChanges();
                    }
                    else
                    {
                        if (dbTetris.Limit == true)
                        {
                            //Xử lý khi full điểm 1 ngày
                        }
                        if (dbQuiz.Limit == true)
                        {

                        }
                        if (dbQuiz.Limit == true)
                        {
                            QuizUEHPictureBox.Enabled = false;
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
        private void btnRestart_Click(object sender, EventArgs e)
        {
            UpdateScore();

        }
    }
}
