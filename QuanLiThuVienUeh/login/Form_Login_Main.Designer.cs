namespace QuanLiThuVienUeh
{
    partial class Form_Login_Main
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_Login_Main));
            this.panel_UehLogo = new System.Windows.Forms.Panel();
            this.panel_LoginOption = new System.Windows.Forms.Panel();
            this.panel_UehStaff = new System.Windows.Forms.Panel();
            this.label_UehStaff = new System.Windows.Forms.Label();
            this.panel_UehStudent = new System.Windows.Forms.Panel();
            this.label_UehStudent = new System.Windows.Forms.Label();
            this.pictureBox_UehLogin = new System.Windows.Forms.PictureBox();
            this.pictureBox_UehLogo = new System.Windows.Forms.PictureBox();
            this.button_Login2 = new System.Windows.Forms.Button();
            this.button_Login1 = new System.Windows.Forms.Button();
            this.panel_UehLogo.SuspendLayout();
            this.panel_LoginOption.SuspendLayout();
            this.panel_UehStaff.SuspendLayout();
            this.panel_UehStudent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_UehLogin)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_UehLogo)).BeginInit();
            this.SuspendLayout();
            // 
            // panel_UehLogo
            // 
            this.panel_UehLogo.Controls.Add(this.pictureBox_UehLogo);
            this.panel_UehLogo.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel_UehLogo.Location = new System.Drawing.Point(0, 0);
            this.panel_UehLogo.Margin = new System.Windows.Forms.Padding(4);
            this.panel_UehLogo.Name = "panel_UehLogo";
            this.panel_UehLogo.Size = new System.Drawing.Size(1179, 244);
            this.panel_UehLogo.TabIndex = 2;
            // 
            // panel_LoginOption
            // 
            this.panel_LoginOption.Controls.Add(this.panel_UehStaff);
            this.panel_LoginOption.Controls.Add(this.panel_UehStudent);
            this.panel_LoginOption.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_LoginOption.Location = new System.Drawing.Point(0, 0);
            this.panel_LoginOption.Margin = new System.Windows.Forms.Padding(4);
            this.panel_LoginOption.Name = "panel_LoginOption";
            this.panel_LoginOption.Size = new System.Drawing.Size(1179, 690);
            this.panel_LoginOption.TabIndex = 3;
            // 
            // panel_UehStaff
            // 
            this.panel_UehStaff.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.panel_UehStaff.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panel_UehStaff.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.panel_UehStaff.Controls.Add(this.button_Login2);
            this.panel_UehStaff.Controls.Add(this.label_UehStaff);
            this.panel_UehStaff.Cursor = System.Windows.Forms.Cursors.Hand;
            this.panel_UehStaff.Location = new System.Drawing.Point(788, 384);
            this.panel_UehStaff.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel_UehStaff.Name = "panel_UehStaff";
            this.panel_UehStaff.Size = new System.Drawing.Size(373, 53);
            this.panel_UehStaff.TabIndex = 4;
            this.panel_UehStaff.Click += new System.EventHandler(this.label_UehStaff_Click);
            // 
            // label_UehStaff
            // 
            this.label_UehStaff.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label_UehStaff.AutoSize = true;
            this.label_UehStaff.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_UehStaff.Location = new System.Drawing.Point(20, 14);
            this.label_UehStaff.Name = "label_UehStaff";
            this.label_UehStaff.Size = new System.Drawing.Size(104, 28);
            this.label_UehStaff.TabIndex = 2;
            this.label_UehStaff.Text = "UEH Staff";
            this.label_UehStaff.Click += new System.EventHandler(this.label_UehStaff_Click);
            // 
            // panel_UehStudent
            // 
            this.panel_UehStudent.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.panel_UehStudent.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panel_UehStudent.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.panel_UehStudent.Controls.Add(this.button_Login1);
            this.panel_UehStudent.Controls.Add(this.label_UehStudent);
            this.panel_UehStudent.Cursor = System.Windows.Forms.Cursors.Hand;
            this.panel_UehStudent.Location = new System.Drawing.Point(788, 267);
            this.panel_UehStudent.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel_UehStudent.Name = "panel_UehStudent";
            this.panel_UehStudent.Size = new System.Drawing.Size(373, 53);
            this.panel_UehStudent.TabIndex = 3;
            this.panel_UehStudent.Click += new System.EventHandler(this.label_UehStudent_Click);
            // 
            // label_UehStudent
            // 
            this.label_UehStudent.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label_UehStudent.AutoSize = true;
            this.label_UehStudent.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_UehStudent.Location = new System.Drawing.Point(20, 14);
            this.label_UehStudent.Name = "label_UehStudent";
            this.label_UehStudent.Size = new System.Drawing.Size(132, 28);
            this.label_UehStudent.TabIndex = 2;
            this.label_UehStudent.Text = "UEH Student";
            this.label_UehStudent.Click += new System.EventHandler(this.label_UehStudent_Click);
            // 
            // pictureBox_UehLogin
            // 
            this.pictureBox_UehLogin.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.pictureBox_UehLogin.Image = global::QuanLiThuVienUeh.Properties.Resources.Co_so_N_1536x863;
            this.pictureBox_UehLogin.Location = new System.Drawing.Point(0, 0);
            this.pictureBox_UehLogin.Margin = new System.Windows.Forms.Padding(4);
            this.pictureBox_UehLogin.Name = "pictureBox_UehLogin";
            this.pictureBox_UehLogin.Size = new System.Drawing.Size(767, 690);
            this.pictureBox_UehLogin.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox_UehLogin.TabIndex = 0;
            this.pictureBox_UehLogin.TabStop = false;
            // 
            // pictureBox_UehLogo
            // 
            this.pictureBox_UehLogo.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.pictureBox_UehLogo.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox_UehLogo.Image")));
            this.pictureBox_UehLogo.Location = new System.Drawing.Point(907, 73);
            this.pictureBox_UehLogo.Margin = new System.Windows.Forms.Padding(4);
            this.pictureBox_UehLogo.Name = "pictureBox_UehLogo";
            this.pictureBox_UehLogo.Size = new System.Drawing.Size(153, 105);
            this.pictureBox_UehLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox_UehLogo.TabIndex = 1;
            this.pictureBox_UehLogo.TabStop = false;
            // 
            // button_Login2
            // 
            this.button_Login2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.button_Login2.Image = ((System.Drawing.Image)(resources.GetObject("button_Login2.Image")));
            this.button_Login2.Location = new System.Drawing.Point(311, -1);
            this.button_Login2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button_Login2.Name = "button_Login2";
            this.button_Login2.Size = new System.Drawing.Size(63, 55);
            this.button_Login2.TabIndex = 3;
            this.button_Login2.UseVisualStyleBackColor = true;
            this.button_Login2.Click += new System.EventHandler(this.label_UehStaff_Click);
            // 
            // button_Login1
            // 
            this.button_Login1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.button_Login1.Image = ((System.Drawing.Image)(resources.GetObject("button_Login1.Image")));
            this.button_Login1.Location = new System.Drawing.Point(311, -1);
            this.button_Login1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button_Login1.Name = "button_Login1";
            this.button_Login1.Size = new System.Drawing.Size(63, 55);
            this.button_Login1.TabIndex = 3;
            this.button_Login1.UseVisualStyleBackColor = true;
            this.button_Login1.Click += new System.EventHandler(this.label_UehStudent_Click);
            // 
            // Form_Login_Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1179, 690);
            this.Controls.Add(this.pictureBox_UehLogin);
            this.Controls.Add(this.panel_UehLogo);
            this.Controls.Add(this.panel_LoginOption);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.MinimumSize = new System.Drawing.Size(1194, 728);
            this.Name = "Form_Login_Main";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Login_Main";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form_Login_Main_FormClosed);
            this.panel_UehLogo.ResumeLayout(false);
            this.panel_LoginOption.ResumeLayout(false);
            this.panel_UehStaff.ResumeLayout(false);
            this.panel_UehStaff.PerformLayout();
            this.panel_UehStudent.ResumeLayout(false);
            this.panel_UehStudent.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_UehLogin)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_UehLogo)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox_UehLogin;
        private System.Windows.Forms.PictureBox pictureBox_UehLogo;
        private System.Windows.Forms.Panel panel_UehLogo;
        private System.Windows.Forms.Panel panel_LoginOption;
        private System.Windows.Forms.Panel panel_UehStudent;
        private System.Windows.Forms.Label label_UehStudent;
        private System.Windows.Forms.Button button_Login1;
        private System.Windows.Forms.Panel panel_UehStaff;
        private System.Windows.Forms.Button button_Login2;
        private System.Windows.Forms.Label label_UehStaff;
    }
}

