using QuanLiThuVienUeh.nguoidung.game.Tetris;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLiThuVienUeh.nguoidung.game.MineSweeper
{
    public partial class MineSweeper : Form
    {
        private Panel mainPanel;
        public static int IDNguoiDung;
        public MineSweeper(int ID)
        {
            InitializeComponent();
            IDNguoiDung = ID;
            InitializeGame();
        }
        private void InitializeGame()
        {
            mainPanel = new Panel();
            mainPanel.Dock = DockStyle.Fill;
            Controls.Add(mainPanel);

            MapMineSweeper.Saveving(mainPanel, label1, IDNguoiDung);
            MapMineSweeper.StartUp(mainPanel, label1, IDNguoiDung, this);
        }
        private void toolStripButtonExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
