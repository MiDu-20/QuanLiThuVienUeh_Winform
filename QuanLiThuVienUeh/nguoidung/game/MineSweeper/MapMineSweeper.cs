using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLiThuVienUeh.nguoidung.game.MineSweeper
{
    internal class MapMineSweeper
    {
        public const int mapSize = 8;
        public const int cellSize = 50;
        public static int[,] map = new int[mapSize, mapSize];
        public static Button[,] buttons = new Button[mapSize, mapSize];
        public static Image SourceImage;
        private static bool isFirstStep;
        private static Point firstCoord;
        public static int NumberofBoms;
        public static Panel mainPanel;
        public static Label labelScore;
        public static int IDNguoiDung;
        public static int WinningScore = 50;
        public static Form thisform;
        private static void MapSizeSetting(Panel current)
        {
            current.Width = mapSize * cellSize + 20;
            current.Height = (mapSize + 1) * cellSize;
        }

        private static void SetUpMap()
        {
            for (int i = 0; i < mapSize; i++)
            {
                for (int j = 0; j < mapSize; j++)
                {
                    map[i, j] = 0;
                }
            }
        }
        public static void Saveving(Panel panel2, Label label, int ID)
        {
            mainPanel = panel2;
            labelScore = label;
            IDNguoiDung = ID;
        }
        public static void StartUp(Panel panel, Label label2, int ID, Form form)
        {
            isFirstStep = true;
            SourceImage = new Bitmap(Properties.Resources.tiles);
            IDNguoiDung = ID;
            thisform = form;
            MapSizeSetting(panel);
            SetUpMap();
            CreateButtons(panel);
        }
        private static void CreateButtons(Panel current)
        {
            for (int i = 0; i < mapSize; i++)
            {
                for (int j = 0; j < mapSize; j++)
                {
                    Button button = new Button();
                    button.Location = new Point(j * cellSize, i * cellSize + 25);
                    button.Size = new Size(cellSize, cellSize);
                    button.Image = FindNeededImage(0, 0);
                    button.MouseUp += new MouseEventHandler(MousePressed);
                    current.Controls.Add(button);
                    buttons[i, j] = button;
                }
            }
        }
        private static bool CheckWinGame()
        {
            int countEmptySlot = 0;
            for (int i = 0; i < mapSize; i++)
            {
                for (int j = 0; j < mapSize; j++)
                {
                    if (map[i, j] == -1)
                    {
                        continue;
                    }
                    else if (buttons[i, j].Enabled == false)
                    {
                        countEmptySlot++;
                    }
                }
            }
            if (countEmptySlot + NumberofBoms == 64)
            {
                ShowAllBombs(-5, -5);
                return true;
            }
            return false;

        }
        private static void MousePressed(object sender, MouseEventArgs e)
        {
            Button pressedButton = sender as Button;
            switch (e.Button.ToString())
            {
                case "Right":
                    RightButtonPressed(pressedButton);
                    break;
                case "Left":
                    LeftButtonPressed(pressedButton);
                    break;
            }
            if (CheckWinGame()) // Xử lý khi WinGame
            {
                UpdateDatabase();
                EndGameHandle();
            }
        }
        private static void EndGameHandle()
        {
            DialogResult result = MessageBox.Show("Bạn muốn chơi tiếp không?", "Kết Thúc", MessageBoxButtons.YesNo);

            if (result == DialogResult.Yes)
            {
                mainPanel.Controls.Clear();
                StartUp(mainPanel, labelScore, IDNguoiDung, thisform);
            }
            else
            {
                thisform.Close();
            }
        }
        private static void UpdateDatabase()
        {
            using (var db = new QLTVEntities())
            {
                DateTime dateTime = DateTime.Now.Date;
                var dbDiemVaThuong = db.DiemVaThuong.FirstOrDefault(p => p.IDNguoiDung == IDNguoiDung);
                var findminesweeper = db.MineSweeper.FirstOrDefault(p => p.IDNguoiDung == IDNguoiDung && p.Ngay == dateTime);
                if (findminesweeper != null)
                {
                    if (WinningScore + findminesweeper.DiemMineSweeper >= 100)
                    {
                        findminesweeper.DiemMineSweeper = 100;
                        findminesweeper.Limit = true;
                    }
                    else if (WinningScore + findminesweeper.DiemMineSweeper > findminesweeper.DiemMineSweeper)
                    {
                        findminesweeper.DiemMineSweeper = WinningScore + findminesweeper.DiemMineSweeper;
                    }
                }
                if (dbDiemVaThuong != null)
                {
                    if (dbDiemVaThuong.GameMineSweeper + findminesweeper.DiemMineSweeper <= 100)
                    {
                        dbDiemVaThuong.GameMineSweeper = dbDiemVaThuong.GameMineSweeper + findminesweeper.DiemMineSweeper;
                        dbDiemVaThuong.Diem = dbDiemVaThuong.Diem + findminesweeper.DiemMineSweeper;
                    }
                    else
                    {
                        dbDiemVaThuong.Diem = dbDiemVaThuong.Diem + (100 - dbDiemVaThuong.GameMineSweeper);
                        dbDiemVaThuong.GameMineSweeper = 100;
                    }
                }
                db.SaveChanges();
            }
        }

        private static void RightButtonPressed(Button pressedButton)
        {
            if (pressedButton.Tag == null || string.IsNullOrEmpty((string)pressedButton.Tag) || (string)pressedButton.Tag == "0, 0")
            {
                pressedButton.Tag = "0, 2";
                pressedButton.Image = FindNeededImage(0, 2);
            }
            else if ((string)pressedButton.Tag == "0, 2")
            {
                pressedButton.Image = FindNeededImage(2, 2);
                pressedButton.Tag = "2, 2";
            }
            else if ((string)pressedButton.Tag == "2, 2")
            {
                pressedButton.Image = FindNeededImage(0, 0);
                pressedButton.Tag = "0, 0";
            }
        }

        private static void LeftButtonPressed(Button pressedButton)
        {
            pressedButton.Enabled = false;
            int iButton = pressedButton.Location.Y / cellSize;
            int jButton = pressedButton.Location.X / cellSize;
            if (isFirstStep)
            {
                firstCoord = new Point(jButton, iButton);
                SeedMap();
                CountCellBomb();
                isFirstStep = false;
            }
            OpenCells(iButton, jButton);

            if (map[iButton, jButton] == -1)
            {
                ShowAllBombs(iButton, jButton);
                EndGameHandle();
            }
        }

        private static void ShowAllBombs(int iBomb, int jBomb)
        {
            for (int i = 0; i < mapSize; i++)
            {
                for (int j = 0; j < mapSize; j++)
                {
                    if (i == iBomb && j == jBomb)
                        continue;
                    if (map[i, j] == -1)
                    {
                        buttons[i, j].Image = FindNeededImage(3, 2);
                    }
                }
            }
        }

        public static Image FindNeededImage(int xPos, int yPos)
        {
            Image image = new Bitmap(cellSize, cellSize);
            Graphics g = Graphics.FromImage(image);
            g.DrawImage(SourceImage, new Rectangle(new Point(0, 0), new Size(cellSize, cellSize)), 0 + 32 * xPos, 0 + 32 * yPos, 33, 33, GraphicsUnit.Pixel);


            return image;
        }

        private static void SeedMap()
        {
            Random r = new Random();
            NumberofBoms = r.Next(7, 15);
            for (int i = 0; i < NumberofBoms; i++)
            {
                int posI = r.Next(0, mapSize - 1);
                int posJ = r.Next(0, mapSize - 1);

                while (map[posI, posJ] == -1 || (Math.Abs(posI - firstCoord.Y) <= 1 && Math.Abs(posJ - firstCoord.X) <= 1))
                {
                    posI = r.Next(0, mapSize - 1);
                    posJ = r.Next(0, mapSize - 1);
                }
                map[posI, posJ] = -1;
            }
        }

        private static void CountCellBomb()
        {
            for (int i = 0; i < mapSize; i++)
            {
                for (int j = 0; j < mapSize; j++)
                {
                    if (map[i, j] == -1)
                    {
                        for (int k = i - 1; k < i + 2; k++)
                        {
                            for (int l = j - 1; l < j + 2; l++)
                            {
                                if (!IsInBorder(k, l) || map[k, l] == -1)
                                    continue;
                                map[k, l] = map[k, l] + 1;
                            }
                        }
                    }
                }
            }
        }

        private static void OpenCell(int i, int j)
        {
            buttons[i, j].Enabled = false;
            switch (map[i, j])
            {
                case 1:
                    buttons[i, j].Image = FindNeededImage(1, 0);
                    break;
                case 2:
                    buttons[i, j].Image = FindNeededImage(2, 0);
                    break;
                case 3:
                    buttons[i, j].Image = FindNeededImage(3, 0);
                    break;
                case 4:
                    buttons[i, j].Image = FindNeededImage(4, 0);
                    break;
                case 5:
                    buttons[i, j].Image = FindNeededImage(0, 1);
                    break;
                case 6:
                    buttons[i, j].Image = FindNeededImage(1, 1);
                    break;
                case 7:
                    buttons[i, j].Image = FindNeededImage(2, 1);
                    break;
                case 8:
                    buttons[i, j].Image = FindNeededImage(3, 1);
                    break;
                case -1:
                    buttons[i, j].Image = FindNeededImage(1, 2);
                    break;
                case 0:
                    buttons[i, j].Image = FindNeededImage(0, 0);
                    break;
            }
        }

        private static void OpenCells(int i, int j)
        {
            OpenCell(i, j);

            if (map[i, j] > 0)
                return;

            for (int k = i - 1; k < i + 2; k++)
            {
                for (int l = j - 1; l < j + 2; l++)
                {
                    if (!IsInBorder(k, l))
                        continue;
                    if (!buttons[k, l].Enabled)
                        continue;
                    if (map[k, l] == 0)
                        OpenCells(k, l);
                    else if (map[k, l] > 0)
                        OpenCell(k, l);
                }
            }
        }

        private static bool IsInBorder(int i, int j)
        {
            if (i < 0 || j < 0 || j > mapSize - 1 || i > mapSize - 1)
            {
                return false;
            }
            return true;
        }
    }
}
