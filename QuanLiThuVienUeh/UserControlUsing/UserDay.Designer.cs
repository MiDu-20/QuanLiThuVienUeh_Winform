namespace QuanLiThuVienUeh.UserControlUsing
{
    partial class UserDay
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.labelDay = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // labelDay
            // 
            this.labelDay.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelDay.Location = new System.Drawing.Point(0, 0);
            this.labelDay.Name = "labelDay";
            this.labelDay.Size = new System.Drawing.Size(69, 67);
            this.labelDay.TabIndex = 0;
            this.labelDay.Text = "5";
            this.labelDay.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // UserDay
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.labelDay);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "UserDay";
            this.Size = new System.Drawing.Size(69, 67);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label labelDay;
    }
}
