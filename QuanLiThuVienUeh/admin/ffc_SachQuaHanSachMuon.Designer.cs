namespace QuanLiThuVienUeh.admin
{
    partial class ffc_SachQuaHanSachMuon
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
            Guna.Charts.WinForms.ChartFont chartFont1 = new Guna.Charts.WinForms.ChartFont();
            Guna.Charts.WinForms.ChartFont chartFont2 = new Guna.Charts.WinForms.ChartFont();
            Guna.Charts.WinForms.ChartFont chartFont3 = new Guna.Charts.WinForms.ChartFont();
            Guna.Charts.WinForms.ChartFont chartFont4 = new Guna.Charts.WinForms.ChartFont();
            Guna.Charts.WinForms.Grid grid1 = new Guna.Charts.WinForms.Grid();
            Guna.Charts.WinForms.Tick tick1 = new Guna.Charts.WinForms.Tick();
            Guna.Charts.WinForms.ChartFont chartFont5 = new Guna.Charts.WinForms.ChartFont();
            Guna.Charts.WinForms.Grid grid2 = new Guna.Charts.WinForms.Grid();
            Guna.Charts.WinForms.Tick tick2 = new Guna.Charts.WinForms.Tick();
            Guna.Charts.WinForms.ChartFont chartFont6 = new Guna.Charts.WinForms.ChartFont();
            Guna.Charts.WinForms.Grid grid3 = new Guna.Charts.WinForms.Grid();
            Guna.Charts.WinForms.PointLabel pointLabel1 = new Guna.Charts.WinForms.PointLabel();
            Guna.Charts.WinForms.ChartFont chartFont7 = new Guna.Charts.WinForms.ChartFont();
            Guna.Charts.WinForms.Tick tick3 = new Guna.Charts.WinForms.Tick();
            Guna.Charts.WinForms.ChartFont chartFont8 = new Guna.Charts.WinForms.ChartFont();
            this.chartThongKe = new Guna.Charts.WinForms.GunaChart();
            this.SuspendLayout();
            // 
            // chartThongKe
            // 
            this.chartThongKe.BackColor = System.Drawing.Color.White;
            this.chartThongKe.Dock = System.Windows.Forms.DockStyle.Fill;
            chartFont1.FontName = "Segoe UI Semibold";
            chartFont1.Size = 20;
            this.chartThongKe.Legend.LabelFont = chartFont1;
            this.chartThongKe.Legend.LabelForeColor = System.Drawing.Color.Black;
            this.chartThongKe.Location = new System.Drawing.Point(0, 0);
            this.chartThongKe.Name = "chartThongKe";
            this.chartThongKe.PaletteCustomColors.FillColors.AddRange(new System.Drawing.Color[] {
            System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(111)))), ((int)(((byte)(51))))),
            System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(95)))), ((int)(((byte)(105)))))});
            this.chartThongKe.Size = new System.Drawing.Size(1234, 499);
            this.chartThongKe.TabIndex = 1;
            chartFont2.FontName = "Segoe UI";
            chartFont2.Size = 15;
            chartFont2.Style = Guna.Charts.WinForms.ChartFontStyle.Bold;
            this.chartThongKe.Title.Font = chartFont2;
            this.chartThongKe.Title.ForeColor = System.Drawing.Color.Black;
            chartFont3.FontName = "Segoe UI";
            chartFont3.Size = 15;
            this.chartThongKe.Tooltips.BodyFont = chartFont3;
            chartFont4.FontName = "Arial";
            chartFont4.Size = 9;
            chartFont4.Style = Guna.Charts.WinForms.ChartFontStyle.Bold;
            this.chartThongKe.Tooltips.TitleFont = chartFont4;
            this.chartThongKe.XAxes.GridLines = grid1;
            chartFont5.FontName = "Arial";
            tick1.Font = chartFont5;
            this.chartThongKe.XAxes.Ticks = tick1;
            this.chartThongKe.YAxes.GridLines = grid2;
            chartFont6.FontName = "Arial";
            tick2.Font = chartFont6;
            this.chartThongKe.YAxes.Ticks = tick2;
            this.chartThongKe.ZAxes.GridLines = grid3;
            chartFont7.FontName = "Arial";
            pointLabel1.Font = chartFont7;
            this.chartThongKe.ZAxes.PointLabels = pointLabel1;
            chartFont8.FontName = "Arial";
            tick3.Font = chartFont8;
            this.chartThongKe.ZAxes.Ticks = tick3;
            // 
            // TongSoSachQuaHanSoSachMuon
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1234, 499);
            this.Controls.Add(this.chartThongKe);
            this.Name = "TongSoSachQuaHanSoSachMuon";
            this.Text = "TongSoSachQuaHanSoSachMuon";
            this.ResumeLayout(false);

        }

        #endregion

        private Guna.Charts.WinForms.GunaChart chartThongKe;
    }
}