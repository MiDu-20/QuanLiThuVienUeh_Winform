using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLiThuVienUeh.nguoidung.game.Tetris
{
    public partial class TetrisGame : Form
    {
        //Variable        
        //Method
        private static int IDNguoiDung;
        private static int CurrentGameInterval;
        public TetrisGame(int ID)
        {
            InitializeComponent();
            Init();
            IDNguoiDung = ID;
        }
        private void UpdateDatabase()
        {
            using (var db = new QLTVEntities())
            {
                DateTime dateTime = DateTime.Now.Date;
                var dbDiemVaThuong = db.DiemVaThuong.FirstOrDefault(p => p.IDNguoiDung == IDNguoiDung);
                var findTetris = db.Tetris.FirstOrDefault(p => p.IDNguoiDung == IDNguoiDung && p.Ngay == dateTime);
                if (findTetris != null)
                {
                    if (MapController.score + findTetris.DiemTetris >= 100)
                    {
                        findTetris.DiemTetris = 100;
                        findTetris.Limit = true;
                    }
                    else if (MapController.score + findTetris.DiemTetris > findTetris.DiemTetris)
                    {
                        findTetris.DiemTetris = MapController.score + findTetris.DiemTetris;
                    }
                }
                if (dbDiemVaThuong != null)
                {
                    if (dbDiemVaThuong.GameTetris + findTetris.DiemTetris <= 100)
                    {
                        dbDiemVaThuong.GameTetris = dbDiemVaThuong.GameTetris + findTetris.DiemTetris;
                        dbDiemVaThuong.Diem = dbDiemVaThuong.Diem + findTetris.DiemTetris;
                    }
                    else
                    {
                        dbDiemVaThuong.Diem = dbDiemVaThuong.Diem + (100 - dbDiemVaThuong.GameTetris);
                        dbDiemVaThuong.GameTetris = 100;
                    }
                }
                db.SaveChanges();
            }
        }

        public void Init()
        {
            this.Text = "Тetris: ";
            MapController.size = 25;
            MapController.score = 0;
            MapController.linesRemoved = 0;
            MapController.currentShape = new Shape(3, 0);
            MapController.ResetMap();
            MapController.ClearMap();
            lblScore.Text = "Score: " + MapController.score;
            lblLines.Text = "Lines: " + MapController.linesRemoved;
            GameTimer.Interval = 400;
            CurrentGameInterval = GameTimer.Interval;
            GameTimer.Start();
            CheckMovingTimer.Start();
            Invalidate();
        }
        private void Update(object sender, EventArgs e)
        {
            MapController.ResetMap();
            if (!MapController.CheckCollideVertical())
            {
                MapController.currentShape.MoveDown();
            }
            else
            {
                MapController.ImageMerge();
                MapController.MapPointing(lblScore, lblLines);
                if (MapController.CheckGameOver())
                {
                    GameTimer.Stop();
                    CheckMovingTimer.Stop();
                    DialogResult result = MessageBox.Show("Diểm của bạn là: " + MapController.score + "\nBạn muốn chơi tiếp không?", "Kết thúc", MessageBoxButtons.YesNo);
                    if (result == DialogResult.Yes)
                    {
                        ResetKey();
                        MapController.ResetMap();
                        MapController.ClearMap();
                        UpdateDatabase();
                        Init();
                    }
                    else
                    {
                        UpdateDatabase();
                        this.Close();
                    }
                }
                MapController.currentShape.ResetShape(3, 0);

            }

            MapController.ImageMerge();
            Invalidate();

        }

        private void PainingUpdate(object sender, PaintEventArgs e)
        {
            MapController.DrawGrid(e.Graphics);
            MapController.DrawMap(e.Graphics);
            MapController.ShowNextShape(e.Graphics);
        }
        private void CheckRotate()
        {
            if (MapController.MovingA)
            {
                if (!MapController.IsIntersects())
                {
                    MapController.ResetMap();
                    MapController.currentShape.RotateShape();
                    MapController.ImageMerge();
                    Invalidate();
                }
            }
        }
        private void CheckKeyCase()
        {
            if (MapController.MovingRight)
            {
                if (!MapController.CheckCollideHorizon(1))
                {
                    MapController.ResetMap();
                    MapController.currentShape.MoveRight();
                    MapController.ImageMerge();
                    Invalidate();
                }
            }
            if (MapController.MovingLeft)
            {
                if (!MapController.CheckCollideHorizon(-1))
                {
                    MapController.ResetMap();
                    MapController.currentShape.MoveLeft();
                    MapController.ImageMerge();
                    Invalidate();
                }
            }
            if (MapController.MovingSpace)
            {
                GameTimer.Interval = 30;
            }
        }
        private void ResetKey()
        {
            MapController.MovingA = false;
            MapController.MovingSpace = false;
            MapController.MovingRight = false;
            MapController.MovingLeft = false;

        }
        private void Tetris_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.A:
                    MapController.MovingA = true;
                    break;
                case Keys.Space:
                    MapController.MovingSpace = true;
                    break;
                case Keys.Right:
                    MapController.MovingRight = true;
                    break;
                case Keys.Left:
                    MapController.MovingLeft = true;
                    break;
                default:
                    break;
            }
        }

        private void Tetris_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.A:
                    MapController.MovingA = false;
                    break;
                case Keys.Space:
                    GameTimer.Interval = CurrentGameInterval;
                    MapController.MovingSpace = false;
                    break;
                case Keys.Right:
                    MapController.MovingRight = false;
                    break;
                case Keys.Left:
                    MapController.MovingLeft = false;
                    break;
                default:
                    break;
            }
        }

        private void CheckGameStatus_Tick(object sender, EventArgs e)
        {
            if (GameTimer.Interval > 100)
            {
                CurrentGameInterval -= 50;
                GameTimer.Interval -= 50;
            }
        }

        private void CheckMovingTimer_Tick(object sender, EventArgs e)
        {
            CheckKeyCase();
            CheckRotate();
        }
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void TetrisGame_FormClosing(object sender, FormClosingEventArgs e)
        {
            ResetKey();
            MapController.ResetMap();
            MapController.ClearMap();
            GameTimer.Stop();
            CheckMovingTimer.Stop();
        }
    }
}
