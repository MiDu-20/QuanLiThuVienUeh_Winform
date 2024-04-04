namespace QuanLiThuVienUeh.nguoidung.game.Tetris
{
    partial class TetrisGame
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TetrisGame));
            this.GameTimer = new System.Windows.Forms.Timer(this.components);
            this.lblScore = new System.Windows.Forms.Label();
            this.lblLines = new System.Windows.Forms.Label();
            this.CheckGameStatus = new System.Windows.Forms.Timer(this.components);
            this.CheckMovingTimer = new System.Windows.Forms.Timer(this.components);
            this.toolStripGame = new System.Windows.Forms.ToolStrip();
            this.toolStripButtonExit = new System.Windows.Forms.ToolStripButton();
            this.toolStripGame.SuspendLayout();
            this.SuspendLayout();
            // 
            // GameTimer
            // 
            this.GameTimer.Interval = 400;
            this.GameTimer.Tick += new System.EventHandler(this.Update);
            // 
            // lblScore
            // 
            this.lblScore.AutoSize = true;
            this.lblScore.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblScore.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(116)))), ((int)(((byte)(75)))));
            this.lblScore.Location = new System.Drawing.Point(291, 189);
            this.lblScore.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblScore.Name = "lblScore";
            this.lblScore.Size = new System.Drawing.Size(71, 29);
            this.lblScore.TabIndex = 0;
            this.lblScore.Text = "label";
            // 
            // lblLines
            // 
            this.lblLines.AutoSize = true;
            this.lblLines.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLines.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(116)))), ((int)(((byte)(75)))));
            this.lblLines.Location = new System.Drawing.Point(291, 256);
            this.lblLines.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblLines.Name = "lblLines";
            this.lblLines.Size = new System.Drawing.Size(85, 29);
            this.lblLines.TabIndex = 1;
            this.lblLines.Text = "labelScore";
            // 
            // CheckGameStatus
            // 
            this.CheckGameStatus.Enabled = true;
            this.CheckGameStatus.Interval = 10000;
            this.CheckGameStatus.Tick += new System.EventHandler(this.CheckGameStatus_Tick);
            // 
            // CheckMovingTimer
            // 
            this.CheckMovingTimer.Enabled = true;
            this.CheckMovingTimer.Interval = 90;
            this.CheckMovingTimer.Tick += new System.EventHandler(this.CheckMovingTimer_Tick);
            // 
            // toolStripGame
            // 
            this.toolStripGame.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStripGame.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButtonExit});
            this.toolStripGame.Location = new System.Drawing.Point(0, 0);
            this.toolStripGame.Name = "toolStripGame";
            this.toolStripGame.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.toolStripGame.Size = new System.Drawing.Size(450, 25);
            this.toolStripGame.TabIndex = 2;
            this.toolStripGame.Text = "toolStrip";
            // 
            // toolStripButtonExit
            // 
            this.toolStripButtonExit.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonExit.Image = Properties.Resources.x_24850_1280;
            this.toolStripButtonExit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonExit.Name = "toolStripButtonExit";
            this.toolStripButtonExit.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonExit.Text = "Exit";
            this.toolStripButtonExit.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // TetrisGame
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(95)))), ((int)(((byte)(103)))));
            this.ClientSize = new System.Drawing.Size(450, 569);
            this.Controls.Add(this.toolStripGame);
            this.Controls.Add(this.lblLines);
            this.Controls.Add(this.lblScore);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.MaximumSize = new System.Drawing.Size(450, 569);
            this.MinimumSize = new System.Drawing.Size(450, 569);
            this.Name = "TetrisGame";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TetrisGame";
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.PainingUpdate);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Tetris_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Tetris_KeyUp);
            this.toolStripGame.ResumeLayout(false);
            this.toolStripGame.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
            this.SuspendLayout();
            // 
            // TetrisGame
            // 
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Name = "TetrisGame";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.TetrisGame_FormClosing);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Timer GameTimer;
        private System.Windows.Forms.Label lblScore;
        private System.Windows.Forms.Label lblLines;
        private System.Windows.Forms.Timer CheckGameStatus;
        private System.Windows.Forms.Timer CheckMovingTimer;
        private System.Windows.Forms.ToolStrip toolStripGame;
        private System.Windows.Forms.ToolStripButton toolStripButtonExit;
    }
}