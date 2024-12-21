namespace QuanLiThuVienUeh
{
    partial class Form_NhanVien
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_NhanVien));
            this.panel_Sidebar = new System.Windows.Forms.Panel();
            this.button_Logout = new System.Windows.Forms.Button();
            this.panel_ChildQuanLiDocGia = new System.Windows.Forms.Panel();
            this.button_ChinhSuaDocGia = new System.Windows.Forms.Button();
            this.button_ThongTinDocGia = new System.Windows.Forms.Button();
            this.button_QuanLiDocGia = new System.Windows.Forms.Button();
            this.panel_ChildQuanLiSach = new System.Windows.Forms.Panel();
            this.button_MuonTraSach = new System.Windows.Forms.Button();
            this.button_ThongTinSach = new System.Windows.Forms.Button();
            this.button_QuanLiSach = new System.Windows.Forms.Button();
            this.panel_ChildTaiKhoan = new System.Windows.Forms.Panel();
            this.button_DoiMatKhau = new System.Windows.Forms.Button();
            this.button_Thongtin = new System.Windows.Forms.Button();
            this.button_TaiKhoan = new System.Windows.Forms.Button();
            this.panel_LogoUeh = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pictureBox_MenuIcon = new System.Windows.Forms.PictureBox();
            this.panel_Search = new System.Windows.Forms.Panel();
            this.pictureBox_SearchIcon = new System.Windows.Forms.PictureBox();
            this.label_Search = new System.Windows.Forms.Label();
            this.textBox_Search = new System.Windows.Forms.TextBox();
            this.panel_Info = new System.Windows.Forms.Panel();
            this.label_CurrentFunction = new System.Windows.Forms.Label();
            this.label_CurrentPage = new System.Windows.Forms.Label();
            this.label_DecorLine = new System.Windows.Forms.Label();
            this.label_Home_CurrentFunction = new System.Windows.Forms.Label();
            this.panel_ChildForm = new System.Windows.Forms.Panel();
            this.timer_TaiKhoanTransition = new System.Windows.Forms.Timer(this.components);
            this.timer_QuanLiSachTransition = new System.Windows.Forms.Timer(this.components);
            this.timer_QuanLiDocGiaTransition = new System.Windows.Forms.Timer(this.components);
            this.panel_Sidebar.SuspendLayout();
            this.panel_ChildQuanLiDocGia.SuspendLayout();
            this.panel_ChildQuanLiSach.SuspendLayout();
            this.panel_ChildTaiKhoan.SuspendLayout();
            this.panel_LogoUeh.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_MenuIcon)).BeginInit();
            this.panel_Search.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_SearchIcon)).BeginInit();
            this.panel_Info.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel_Sidebar
            // 
            this.panel_Sidebar.Controls.Add(this.button_Logout);
            this.panel_Sidebar.Controls.Add(this.panel_ChildQuanLiDocGia);
            this.panel_Sidebar.Controls.Add(this.button_QuanLiDocGia);
            this.panel_Sidebar.Controls.Add(this.panel_ChildQuanLiSach);
            this.panel_Sidebar.Controls.Add(this.button_QuanLiSach);
            this.panel_Sidebar.Controls.Add(this.panel_ChildTaiKhoan);
            this.panel_Sidebar.Controls.Add(this.button_TaiKhoan);
            this.panel_Sidebar.Controls.Add(this.panel_LogoUeh);
            this.panel_Sidebar.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel_Sidebar.Location = new System.Drawing.Point(0, 0);
            this.panel_Sidebar.Margin = new System.Windows.Forms.Padding(4);
            this.panel_Sidebar.Name = "panel_Sidebar";
            this.panel_Sidebar.Size = new System.Drawing.Size(289, 1014);
            this.panel_Sidebar.TabIndex = 0;
            // 
            // button_Logout
            // 
            this.button_Logout.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button_Logout.FlatAppearance.BorderSize = 0;
            this.button_Logout.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_Logout.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_Logout.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(95)))), ((int)(((byte)(105)))));
            this.button_Logout.Image = ((System.Drawing.Image)(resources.GetObject("button_Logout.Image")));
            this.button_Logout.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button_Logout.Location = new System.Drawing.Point(35, 960);
            this.button_Logout.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button_Logout.Name = "button_Logout";
            this.button_Logout.Size = new System.Drawing.Size(223, 48);
            this.button_Logout.TabIndex = 16;
            this.button_Logout.Text = "         Log Out";
            this.button_Logout.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button_Logout.UseVisualStyleBackColor = true;
            this.button_Logout.Click += new System.EventHandler(this.button_Logout_Click);
            // 
            // panel_ChildQuanLiDocGia
            // 
            this.panel_ChildQuanLiDocGia.Controls.Add(this.button_ChinhSuaDocGia);
            this.panel_ChildQuanLiDocGia.Controls.Add(this.button_ThongTinDocGia);
            this.panel_ChildQuanLiDocGia.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel_ChildQuanLiDocGia.Location = new System.Drawing.Point(0, 555);
            this.panel_ChildQuanLiDocGia.Margin = new System.Windows.Forms.Padding(4);
            this.panel_ChildQuanLiDocGia.Name = "panel_ChildQuanLiDocGia";
            this.panel_ChildQuanLiDocGia.Size = new System.Drawing.Size(289, 123);
            this.panel_ChildQuanLiDocGia.TabIndex = 11;
            // 
            // button_ChinhSuaDocGia
            // 
            this.button_ChinhSuaDocGia.BackColor = System.Drawing.Color.White;
            this.button_ChinhSuaDocGia.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button_ChinhSuaDocGia.Dock = System.Windows.Forms.DockStyle.Top;
            this.button_ChinhSuaDocGia.FlatAppearance.BorderSize = 0;
            this.button_ChinhSuaDocGia.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_ChinhSuaDocGia.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_ChinhSuaDocGia.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(111)))), ((int)(((byte)(51)))));
            this.button_ChinhSuaDocGia.Location = new System.Drawing.Point(0, 62);
            this.button_ChinhSuaDocGia.Margin = new System.Windows.Forms.Padding(4);
            this.button_ChinhSuaDocGia.Name = "button_ChinhSuaDocGia";
            this.button_ChinhSuaDocGia.Size = new System.Drawing.Size(289, 62);
            this.button_ChinhSuaDocGia.TabIndex = 15;
            this.button_ChinhSuaDocGia.Text = "           > Chỉnh sửa độc giả";
            this.button_ChinhSuaDocGia.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button_ChinhSuaDocGia.UseVisualStyleBackColor = false;
            this.button_ChinhSuaDocGia.Click += new System.EventHandler(this.button_ChinhSuaDocGia_Click);
            // 
            // button_ThongTinDocGia
            // 
            this.button_ThongTinDocGia.BackColor = System.Drawing.Color.White;
            this.button_ThongTinDocGia.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button_ThongTinDocGia.Dock = System.Windows.Forms.DockStyle.Top;
            this.button_ThongTinDocGia.FlatAppearance.BorderSize = 0;
            this.button_ThongTinDocGia.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_ThongTinDocGia.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_ThongTinDocGia.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(111)))), ((int)(((byte)(51)))));
            this.button_ThongTinDocGia.Location = new System.Drawing.Point(0, 0);
            this.button_ThongTinDocGia.Margin = new System.Windows.Forms.Padding(4);
            this.button_ThongTinDocGia.Name = "button_ThongTinDocGia";
            this.button_ThongTinDocGia.Size = new System.Drawing.Size(289, 62);
            this.button_ThongTinDocGia.TabIndex = 14;
            this.button_ThongTinDocGia.Text = "           > Thông tin độc giả";
            this.button_ThongTinDocGia.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button_ThongTinDocGia.UseVisualStyleBackColor = false;
            this.button_ThongTinDocGia.Click += new System.EventHandler(this.button_ThongTinDocGia_Click);
            // 
            // button_QuanLiDocGia
            // 
            this.button_QuanLiDocGia.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button_QuanLiDocGia.Dock = System.Windows.Forms.DockStyle.Top;
            this.button_QuanLiDocGia.FlatAppearance.BorderSize = 0;
            this.button_QuanLiDocGia.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_QuanLiDocGia.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_QuanLiDocGia.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(95)))), ((int)(((byte)(105)))));
            this.button_QuanLiDocGia.Image = ((System.Drawing.Image)(resources.GetObject("button_QuanLiDocGia.Image")));
            this.button_QuanLiDocGia.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button_QuanLiDocGia.Location = new System.Drawing.Point(0, 493);
            this.button_QuanLiDocGia.Margin = new System.Windows.Forms.Padding(4);
            this.button_QuanLiDocGia.Name = "button_QuanLiDocGia";
            this.button_QuanLiDocGia.Padding = new System.Windows.Forms.Padding(15, 0, 0, 0);
            this.button_QuanLiDocGia.Size = new System.Drawing.Size(289, 62);
            this.button_QuanLiDocGia.TabIndex = 10;
            this.button_QuanLiDocGia.Text = "        Quản lí độc giả";
            this.button_QuanLiDocGia.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button_QuanLiDocGia.UseVisualStyleBackColor = true;
            this.button_QuanLiDocGia.Click += new System.EventHandler(this.button_QuanLiDocGia_Click);
            // 
            // panel_ChildQuanLiSach
            // 
            this.panel_ChildQuanLiSach.Controls.Add(this.button_MuonTraSach);
            this.panel_ChildQuanLiSach.Controls.Add(this.button_ThongTinSach);
            this.panel_ChildQuanLiSach.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel_ChildQuanLiSach.Location = new System.Drawing.Point(0, 370);
            this.panel_ChildQuanLiSach.Margin = new System.Windows.Forms.Padding(4);
            this.panel_ChildQuanLiSach.Name = "panel_ChildQuanLiSach";
            this.panel_ChildQuanLiSach.Size = new System.Drawing.Size(289, 123);
            this.panel_ChildQuanLiSach.TabIndex = 7;
            // 
            // button_MuonTraSach
            // 
            this.button_MuonTraSach.BackColor = System.Drawing.Color.White;
            this.button_MuonTraSach.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button_MuonTraSach.Dock = System.Windows.Forms.DockStyle.Top;
            this.button_MuonTraSach.FlatAppearance.BorderSize = 0;
            this.button_MuonTraSach.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_MuonTraSach.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_MuonTraSach.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(111)))), ((int)(((byte)(51)))));
            this.button_MuonTraSach.Location = new System.Drawing.Point(0, 62);
            this.button_MuonTraSach.Margin = new System.Windows.Forms.Padding(4);
            this.button_MuonTraSach.Name = "button_MuonTraSach";
            this.button_MuonTraSach.Size = new System.Drawing.Size(289, 62);
            this.button_MuonTraSach.TabIndex = 1;
            this.button_MuonTraSach.Text = "           > Mượn - trả sách";
            this.button_MuonTraSach.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button_MuonTraSach.UseVisualStyleBackColor = false;
            this.button_MuonTraSach.Click += new System.EventHandler(this.button_MuonTraSach_Click);
            // 
            // button_ThongTinSach
            // 
            this.button_ThongTinSach.BackColor = System.Drawing.Color.White;
            this.button_ThongTinSach.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button_ThongTinSach.Dock = System.Windows.Forms.DockStyle.Top;
            this.button_ThongTinSach.FlatAppearance.BorderSize = 0;
            this.button_ThongTinSach.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_ThongTinSach.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_ThongTinSach.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(111)))), ((int)(((byte)(51)))));
            this.button_ThongTinSach.Location = new System.Drawing.Point(0, 0);
            this.button_ThongTinSach.Margin = new System.Windows.Forms.Padding(4);
            this.button_ThongTinSach.Name = "button_ThongTinSach";
            this.button_ThongTinSach.Size = new System.Drawing.Size(289, 62);
            this.button_ThongTinSach.TabIndex = 0;
            this.button_ThongTinSach.Text = "           > Thông tin sách";
            this.button_ThongTinSach.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button_ThongTinSach.UseVisualStyleBackColor = false;
            this.button_ThongTinSach.Click += new System.EventHandler(this.button_ThongTinSach_Click);
            // 
            // button_QuanLiSach
            // 
            this.button_QuanLiSach.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button_QuanLiSach.Dock = System.Windows.Forms.DockStyle.Top;
            this.button_QuanLiSach.FlatAppearance.BorderSize = 0;
            this.button_QuanLiSach.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_QuanLiSach.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_QuanLiSach.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(95)))), ((int)(((byte)(105)))));
            this.button_QuanLiSach.Image = ((System.Drawing.Image)(resources.GetObject("button_QuanLiSach.Image")));
            this.button_QuanLiSach.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button_QuanLiSach.Location = new System.Drawing.Point(0, 308);
            this.button_QuanLiSach.Margin = new System.Windows.Forms.Padding(4);
            this.button_QuanLiSach.Name = "button_QuanLiSach";
            this.button_QuanLiSach.Padding = new System.Windows.Forms.Padding(15, 0, 0, 0);
            this.button_QuanLiSach.Size = new System.Drawing.Size(289, 62);
            this.button_QuanLiSach.TabIndex = 8;
            this.button_QuanLiSach.Text = "        Quản lí sách";
            this.button_QuanLiSach.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button_QuanLiSach.UseVisualStyleBackColor = true;
            this.button_QuanLiSach.Click += new System.EventHandler(this.button_QuanLiSach_Click);
            // 
            // panel_ChildTaiKhoan
            // 
            this.panel_ChildTaiKhoan.Controls.Add(this.button_DoiMatKhau);
            this.panel_ChildTaiKhoan.Controls.Add(this.button_Thongtin);
            this.panel_ChildTaiKhoan.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel_ChildTaiKhoan.Location = new System.Drawing.Point(0, 185);
            this.panel_ChildTaiKhoan.Margin = new System.Windows.Forms.Padding(4);
            this.panel_ChildTaiKhoan.Name = "panel_ChildTaiKhoan";
            this.panel_ChildTaiKhoan.Size = new System.Drawing.Size(289, 123);
            this.panel_ChildTaiKhoan.TabIndex = 6;
            // 
            // button_DoiMatKhau
            // 
            this.button_DoiMatKhau.BackColor = System.Drawing.Color.White;
            this.button_DoiMatKhau.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button_DoiMatKhau.Dock = System.Windows.Forms.DockStyle.Top;
            this.button_DoiMatKhau.FlatAppearance.BorderSize = 0;
            this.button_DoiMatKhau.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_DoiMatKhau.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_DoiMatKhau.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(111)))), ((int)(((byte)(51)))));
            this.button_DoiMatKhau.Location = new System.Drawing.Point(0, 62);
            this.button_DoiMatKhau.Margin = new System.Windows.Forms.Padding(4);
            this.button_DoiMatKhau.Name = "button_DoiMatKhau";
            this.button_DoiMatKhau.Size = new System.Drawing.Size(289, 62);
            this.button_DoiMatKhau.TabIndex = 1;
            this.button_DoiMatKhau.Text = "           > Đổi mật khẩu";
            this.button_DoiMatKhau.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button_DoiMatKhau.UseVisualStyleBackColor = false;
            this.button_DoiMatKhau.Click += new System.EventHandler(this.button_ChinhSuaTaiKhoan_Click);
            // 
            // button_Thongtin
            // 
            this.button_Thongtin.BackColor = System.Drawing.Color.White;
            this.button_Thongtin.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button_Thongtin.Dock = System.Windows.Forms.DockStyle.Top;
            this.button_Thongtin.FlatAppearance.BorderSize = 0;
            this.button_Thongtin.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_Thongtin.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_Thongtin.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(111)))), ((int)(((byte)(51)))));
            this.button_Thongtin.Location = new System.Drawing.Point(0, 0);
            this.button_Thongtin.Margin = new System.Windows.Forms.Padding(4);
            this.button_Thongtin.Name = "button_Thongtin";
            this.button_Thongtin.Size = new System.Drawing.Size(289, 62);
            this.button_Thongtin.TabIndex = 0;
            this.button_Thongtin.Text = "           > Thông tin cá nhân";
            this.button_Thongtin.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button_Thongtin.UseVisualStyleBackColor = false;
            this.button_Thongtin.Click += new System.EventHandler(this.button_Thongtin_Click);
            // 
            // button_TaiKhoan
            // 
            this.button_TaiKhoan.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button_TaiKhoan.Dock = System.Windows.Forms.DockStyle.Top;
            this.button_TaiKhoan.FlatAppearance.BorderSize = 0;
            this.button_TaiKhoan.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_TaiKhoan.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_TaiKhoan.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(95)))), ((int)(((byte)(105)))));
            this.button_TaiKhoan.Image = ((System.Drawing.Image)(resources.GetObject("button_TaiKhoan.Image")));
            this.button_TaiKhoan.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button_TaiKhoan.Location = new System.Drawing.Point(0, 123);
            this.button_TaiKhoan.Margin = new System.Windows.Forms.Padding(4);
            this.button_TaiKhoan.Name = "button_TaiKhoan";
            this.button_TaiKhoan.Padding = new System.Windows.Forms.Padding(15, 0, 0, 0);
            this.button_TaiKhoan.Size = new System.Drawing.Size(289, 62);
            this.button_TaiKhoan.TabIndex = 5;
            this.button_TaiKhoan.Text = "        Tài khoản";
            this.button_TaiKhoan.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button_TaiKhoan.UseVisualStyleBackColor = true;
            this.button_TaiKhoan.Click += new System.EventHandler(this.button_TaiKhoan_Click);
            // 
            // panel_LogoUeh
            // 
            this.panel_LogoUeh.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(95)))), ((int)(((byte)(105)))));
            this.panel_LogoUeh.Controls.Add(this.pictureBox1);
            this.panel_LogoUeh.Controls.Add(this.pictureBox_MenuIcon);
            this.panel_LogoUeh.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel_LogoUeh.Location = new System.Drawing.Point(0, 0);
            this.panel_LogoUeh.Margin = new System.Windows.Forms.Padding(4);
            this.panel_LogoUeh.Name = "panel_LogoUeh";
            this.panel_LogoUeh.Size = new System.Drawing.Size(289, 123);
            this.panel_LogoUeh.TabIndex = 1;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(37, 27);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(4);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(117, 73);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 12;
            this.pictureBox1.TabStop = false;
            // 
            // pictureBox_MenuIcon
            // 
            this.pictureBox_MenuIcon.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.pictureBox_MenuIcon.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(95)))), ((int)(((byte)(105)))));
            this.pictureBox_MenuIcon.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureBox_MenuIcon.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox_MenuIcon.Image")));
            this.pictureBox_MenuIcon.Location = new System.Drawing.Point(225, 39);
            this.pictureBox_MenuIcon.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pictureBox_MenuIcon.Name = "pictureBox_MenuIcon";
            this.pictureBox_MenuIcon.Size = new System.Drawing.Size(55, 55);
            this.pictureBox_MenuIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox_MenuIcon.TabIndex = 11;
            this.pictureBox_MenuIcon.TabStop = false;
            // 
            // panel_Search
            // 
            this.panel_Search.Controls.Add(this.pictureBox_SearchIcon);
            this.panel_Search.Controls.Add(this.label_Search);
            this.panel_Search.Controls.Add(this.textBox_Search);
            this.panel_Search.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel_Search.Location = new System.Drawing.Point(289, 0);
            this.panel_Search.Margin = new System.Windows.Forms.Padding(4);
            this.panel_Search.Name = "panel_Search";
            this.panel_Search.Size = new System.Drawing.Size(1635, 62);
            this.panel_Search.TabIndex = 1;
            // 
            // pictureBox_SearchIcon
            // 
            this.pictureBox_SearchIcon.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox_SearchIcon.Image")));
            this.pictureBox_SearchIcon.Location = new System.Drawing.Point(36, 14);
            this.pictureBox_SearchIcon.Margin = new System.Windows.Forms.Padding(4);
            this.pictureBox_SearchIcon.Name = "pictureBox_SearchIcon";
            this.pictureBox_SearchIcon.Size = new System.Drawing.Size(40, 39);
            this.pictureBox_SearchIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox_SearchIcon.TabIndex = 2;
            this.pictureBox_SearchIcon.TabStop = false;
            // 
            // label_Search
            // 
            this.label_Search.AutoSize = true;
            this.label_Search.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label_Search.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_Search.ForeColor = System.Drawing.Color.Gray;
            this.label_Search.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label_Search.Location = new System.Drawing.Point(83, 21);
            this.label_Search.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label_Search.Name = "label_Search";
            this.label_Search.Size = new System.Drawing.Size(75, 25);
            this.label_Search.TabIndex = 1;
            this.label_Search.Text = "Search";
            this.label_Search.Click += new System.EventHandler(this.labelSearch_Click);
            // 
            // textBox_Search
            // 
            this.textBox_Search.BackColor = System.Drawing.SystemColors.Window;
            this.textBox_Search.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox_Search.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox_Search.Location = new System.Drawing.Point(80, 18);
            this.textBox_Search.Margin = new System.Windows.Forms.Padding(4);
            this.textBox_Search.Name = "textBox_Search";
            this.textBox_Search.Size = new System.Drawing.Size(451, 32);
            this.textBox_Search.TabIndex = 0;
            this.textBox_Search.Click += new System.EventHandler(this.textBox_Search_Click);
            this.textBox_Search.TextChanged += new System.EventHandler(this.textBox_Search_TextChanged);
            // 
            // panel_Info
            // 
            this.panel_Info.Controls.Add(this.label_CurrentFunction);
            this.panel_Info.Controls.Add(this.label_CurrentPage);
            this.panel_Info.Controls.Add(this.label_DecorLine);
            this.panel_Info.Controls.Add(this.label_Home_CurrentFunction);
            this.panel_Info.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel_Info.Location = new System.Drawing.Point(289, 62);
            this.panel_Info.Margin = new System.Windows.Forms.Padding(4);
            this.panel_Info.Name = "panel_Info";
            this.panel_Info.Size = new System.Drawing.Size(1635, 73);
            this.panel_Info.TabIndex = 2;
            // 
            // label_CurrentFunction
            // 
            this.label_CurrentFunction.AutoSize = true;
            this.label_CurrentFunction.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label_CurrentFunction.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_CurrentFunction.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(111)))), ((int)(((byte)(51)))));
            this.label_CurrentFunction.Location = new System.Drawing.Point(100, 36);
            this.label_CurrentFunction.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label_CurrentFunction.Name = "label_CurrentFunction";
            this.label_CurrentFunction.Size = new System.Drawing.Size(0, 28);
            this.label_CurrentFunction.TabIndex = 4;
            this.label_CurrentFunction.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label_CurrentPage
            // 
            this.label_CurrentPage.AutoSize = true;
            this.label_CurrentPage.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_CurrentPage.Location = new System.Drawing.Point(24, 0);
            this.label_CurrentPage.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label_CurrentPage.Name = "label_CurrentPage";
            this.label_CurrentPage.Size = new System.Drawing.Size(129, 32);
            this.label_CurrentPage.TabIndex = 0;
            this.label_CurrentPage.Text = "Main Page";
            // 
            // label_DecorLine
            // 
            this.label_DecorLine.AutoSize = true;
            this.label_DecorLine.Font = new System.Drawing.Font("Gill Sans Ultra Bold Condensed", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_DecorLine.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(111)))), ((int)(((byte)(51)))));
            this.label_DecorLine.Location = new System.Drawing.Point(24, 2);
            this.label_DecorLine.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label_DecorLine.Name = "label_DecorLine";
            this.label_DecorLine.Size = new System.Drawing.Size(42, 36);
            this.label_DecorLine.TabIndex = 2;
            this.label_DecorLine.Text = "___";
            // 
            // label_Home_CurrentFunction
            // 
            this.label_Home_CurrentFunction.AutoSize = true;
            this.label_Home_CurrentFunction.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_Home_CurrentFunction.Location = new System.Drawing.Point(25, 36);
            this.label_Home_CurrentFunction.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label_Home_CurrentFunction.Name = "label_Home_CurrentFunction";
            this.label_Home_CurrentFunction.Size = new System.Drawing.Size(70, 28);
            this.label_Home_CurrentFunction.TabIndex = 1;
            this.label_Home_CurrentFunction.Text = "Home ";
            // 
            // panel_ChildForm
            // 
            this.panel_ChildForm.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_ChildForm.Location = new System.Drawing.Point(289, 135);
            this.panel_ChildForm.Margin = new System.Windows.Forms.Padding(4);
            this.panel_ChildForm.Name = "panel_ChildForm";
            this.panel_ChildForm.Size = new System.Drawing.Size(1635, 879);
            this.panel_ChildForm.TabIndex = 3;
            // 
            // timer_TaiKhoanTransition
            // 
            this.timer_TaiKhoanTransition.Interval = 10;
            this.timer_TaiKhoanTransition.Tick += new System.EventHandler(this.timer_TaiKhoanTransition_Tick);
            // 
            // timer_QuanLiSachTransition
            // 
            this.timer_QuanLiSachTransition.Interval = 10;
            this.timer_QuanLiSachTransition.Tick += new System.EventHandler(this.timer_QuanLiSachTransition_Tick);
            // 
            // timer_QuanLiDocGiaTransition
            // 
            this.timer_QuanLiDocGiaTransition.Interval = 10;
            this.timer_QuanLiDocGiaTransition.Tick += new System.EventHandler(this.timer_QuanLiDocGiaTransition_Tick);
            // 
            // Form_NhanVien
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1924, 1014);
            this.Controls.Add(this.panel_ChildForm);
            this.Controls.Add(this.panel_Info);
            this.Controls.Add(this.panel_Search);
            this.Controls.Add(this.panel_Sidebar);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MinimumSize = new System.Drawing.Size(1194, 728);
            this.Name = "Form_NhanVien";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form_NhanVien";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.panel_Sidebar.ResumeLayout(false);
            this.panel_ChildQuanLiDocGia.ResumeLayout(false);
            this.panel_ChildQuanLiSach.ResumeLayout(false);
            this.panel_ChildTaiKhoan.ResumeLayout(false);
            this.panel_LogoUeh.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_MenuIcon)).EndInit();
            this.panel_Search.ResumeLayout(false);
            this.panel_Search.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_SearchIcon)).EndInit();
            this.panel_Info.ResumeLayout(false);
            this.panel_Info.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel_Sidebar;
        private System.Windows.Forms.Panel panel_LogoUeh;
        private System.Windows.Forms.Panel panel_ChildTaiKhoan;
        private System.Windows.Forms.Button button_DoiMatKhau;
        private System.Windows.Forms.Button button_Thongtin;
        private System.Windows.Forms.Button button_TaiKhoan;
        private System.Windows.Forms.Panel panel_Search;
        private System.Windows.Forms.TextBox textBox_Search;
        private System.Windows.Forms.Label label_Search;
        private System.Windows.Forms.Panel panel_Info;
        private System.Windows.Forms.Label label_Home_CurrentFunction;
        private System.Windows.Forms.Label label_CurrentPage;
        private System.Windows.Forms.Panel panel_ChildForm;
        private System.Windows.Forms.Label label_DecorLine;
        private System.Windows.Forms.Button button_QuanLiSach;
        private System.Windows.Forms.Timer timer_TaiKhoanTransition;
        private System.Windows.Forms.Button button_QuanLiDocGia;
        private System.Windows.Forms.PictureBox pictureBox_MenuIcon;
        private System.Windows.Forms.PictureBox pictureBox_SearchIcon;
        private System.Windows.Forms.Timer timer_QuanLiSachTransition;
        private System.Windows.Forms.Panel panel_ChildQuanLiSach;
        private System.Windows.Forms.Button button_MuonTraSach;
        private System.Windows.Forms.Button button_ThongTinSach;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label_CurrentFunction;
        private System.Windows.Forms.Panel panel_ChildQuanLiDocGia;
        private System.Windows.Forms.Button button_ThongTinDocGia;
        private System.Windows.Forms.Button button_ChinhSuaDocGia;
        private System.Windows.Forms.Timer timer_QuanLiDocGiaTransition;
        private System.Windows.Forms.Button button_Logout;
    }
}