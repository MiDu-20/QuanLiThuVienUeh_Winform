using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLiThuVienUeh.admin
{
    public partial class ff_GuiThongBaoStaff : Form
    {
        //Variables
        #region Variables
        int idNhanVien; //Biến chứa idNhanVien
        string chucvu; //Biến chứa chucvuNhanVien
        #endregion

        public ff_GuiThongBaoStaff(int id, string ChucVu)
        {
            InitializeComponent();
            this.idNhanVien = id;
            this.chucvu = ChucVu;
        }

        //Functions
        #region Functions
        private void HandleGuiNguoiDung() //Hàm xử lí khi gửi 1 người dùng
        {
            using (QLTVEntities db = new QLTVEntities())
            {
                ThongBao thongbao = new ThongBao();
                thongbao.TieuDe = textBox_TieuDe.Text;
                thongbao.IDNguoiGui = idNhanVien;
                if (chucvu == "Manager")
                    thongbao.NguoiGui = "Manager";
                else if (chucvu == "Staff")
                    thongbao.NguoiGui = "Staff";
                thongbao.IDNguoiNhan = (int)db.NguoiDung.Where(s => s.Email == textBox_NguoiNhan.Text).Select(s => s.IDNguoiDung).FirstOrDefault();
                thongbao.EmailNguoiNhan = textBox_NguoiNhan.Text;
                thongbao.ChucVuNguoiNhan = "Student";
                thongbao.NoiDung = textBox_NoiDung.Text;
                thongbao.NgayGui = DateTime.Now.Date;
                db.ThongBao.Add(thongbao);
                db.SaveChanges();
            }
        }

        private void HandleGuiNhanVien() //Hàm xử lí khi gửi 1 nhân viên
        {
            using (QLTVEntities db = new QLTVEntities())
            {
                ThongBao thongbao = new ThongBao();
                thongbao.TieuDe = textBox_TieuDe.Text;
                thongbao.IDNguoiGui = idNhanVien;
                if (chucvu == "Manager")
                {
                    thongbao.NguoiGui = "Manager";
                    thongbao.ChucVuNguoiNhan = "Manager";
                }
                else if (chucvu == "Staff")
                {
                    thongbao.NguoiGui = "Staff";
                    thongbao.ChucVuNguoiNhan = "Staff";
                }
                thongbao.IDNguoiNhan = (int)db.NhanVien.Where(s => s.Email == textBox_NguoiNhan.Text).Select(s => s.IDNhanVien).FirstOrDefault();
                thongbao.EmailNguoiNhan = textBox_NguoiNhan.Text;

                thongbao.NoiDung = textBox_NoiDung.Text;
                thongbao.NgayGui = DateTime.Now;
                db.ThongBao.Add(thongbao);
                db.SaveChanges();
            }
        }

        private void HandleGuiTatCaNguoiDung() //Hàm xử lí khi gửi tất cả người dùng
        {
            using (QLTVEntities db = new QLTVEntities())
            {
                var emailList = db.NguoiDung.OrderBy(s => s.IDNguoiDung).Select(s => s.Email).ToList();
                foreach (var email in emailList)
                {
                    ThongBao thongbao = new ThongBao();
                    thongbao.TieuDe = textBox_TieuDe.Text;
                    thongbao.IDNguoiGui = idNhanVien;
                    if (chucvu == "Manager")
                        thongbao.NguoiGui = "Manager";
                    else if (chucvu == "Staff")
                        thongbao.NguoiGui = "Staff";
                    thongbao.IDNguoiNhan = db.NguoiDung.Where(s => s.Email == email).Select(s => s.IDNguoiDung).FirstOrDefault();
                    thongbao.EmailNguoiNhan = email;
                    thongbao.ChucVuNguoiNhan = "Student";
                    thongbao.NoiDung = textBox_NoiDung.Text;
                    thongbao.NgayGui = DateTime.Now;
                    db.ThongBao.Add(thongbao);
                }
                db.SaveChanges();
            }
        }

        private void HandleGuiTatCaNhanVien() //Hàm xử lí khi gửi tất cả nhân viên
        {
            using (QLTVEntities db = new QLTVEntities())
            {
                var emailList = db.NhanVien.OrderBy(s => s.IDNhanVien).Select(s => s.Email).ToList();
                foreach (var email in emailList)
                {
                    ThongBao thongbao = new ThongBao();
                    thongbao.TieuDe = textBox_TieuDe.Text;
                    thongbao.IDNguoiGui = idNhanVien;
                    if (chucvu == "Manager")
                        thongbao.NguoiGui = "Manager";
                    else if (chucvu == "Staff")
                        thongbao.NguoiGui = "Staff";
                    thongbao.IDNguoiNhan = db.NhanVien.Where(s => s.Email == email).Select(s => s.IDNhanVien).FirstOrDefault();
                    thongbao.EmailNguoiNhan = email;
                    thongbao.ChucVuNguoiNhan = "Staff";
                    thongbao.NoiDung = textBox_NoiDung.Text;
                    thongbao.NgayGui = DateTime.Now;
                    db.ThongBao.Add(thongbao);
                }
                db.SaveChanges();
            }
        }
        #endregion

        //Events
        #region Events
        private void textBox_TieuDe_TextChanged(object sender, EventArgs e)
        {
            if (textBox_TieuDe.Text.Length > 0)
            {
                label_TieuDe.Text = "";
            }
            else label_TieuDe.Text = "Tiêu đề";
        }

        private void label_TieuDe_Click(object sender, EventArgs e)
        {
            label_TieuDe.Text = "";
            textBox_TieuDe.Focus();
        }

        private void textBox_TieuDe_Click(object sender, EventArgs e)
        {
            label_TieuDe.Text = "";
        }

        private void textBox_NguoiNhan_TextChanged(object sender, EventArgs e)
        {
            if (textBox_NguoiNhan.Text.Length > 0)
            {
                label_NguoiNhan.Text = "";
            }
            else label_NguoiNhan.Text = "Người nhận";
        }

        private void textBox_NguoiNhan_Click(object sender, EventArgs e)
        {
            label_NguoiNhan.Text = "";
        }

        private void label_NguoiNhan_Click(object sender, EventArgs e)
        {
            if (checkBox_GuiTatCaNguoiDung.Checked == false && checkBox_GuiTatCaNhanVien.Checked == false)
            {
                label_NguoiNhan.Text = "";
                textBox_NguoiNhan.Focus();
            }
        }

        private void textBox_NoiDung_TextChanged(object sender, EventArgs e)
        {
            if (textBox_NoiDung.Text.Length > 0)
            {
                label_NoiDung.Text = "";
            }
            else label_NoiDung.Text = "Nội dung";
        }

        private void textBox_NoiDung_Click(object sender, EventArgs e)
        {
            label_NoiDung.Text = "";
        }
        private void label_NoiDung_Click(object sender, EventArgs e)
        {
            label_NoiDung.Text = "";
            textBox_NoiDung.Focus();
        }
        private void checkBox_GuiTatCaNhanVien_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_GuiTatCaNhanVien.Checked == true || checkBox_GuiTatCaNguoiDung.Checked == true)
            {
                textBox_NguoiNhan.Text = null;
                textBox_NguoiNhan.Enabled = false;
                label_NguoiNhan.Text = "Prevent Editing While Send All...";
                label_NguoiNhan.ForeColor = Color.Silver;
            }

            else if (checkBox_GuiTatCaNguoiDung.Checked == false && checkBox_GuiTatCaNhanVien.Checked == false)
            {
                textBox_NguoiNhan.Enabled = true;
                textBox_NguoiNhan.Text = null;
                label_NguoiNhan.Text = "Người nhận";
                label_NguoiNhan.ForeColor = Color.Black;
            }
        }

        private void checkBox_GuiTatCaNguoiDung_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_GuiTatCaNhanVien.Checked == true || checkBox_GuiTatCaNguoiDung.Checked == true)
            {
                textBox_NguoiNhan.Text = null;
                textBox_NguoiNhan.Enabled = false;
                label_NguoiNhan.Text = "Prevent Editing While Send All...";
                label_NguoiNhan.ForeColor = Color.Silver;
            }

            else if (checkBox_GuiTatCaNguoiDung.Checked == false && checkBox_GuiTatCaNhanVien.Checked == false)
            {
                textBox_NguoiNhan.Enabled = true;
                textBox_NguoiNhan.Text = null;
                label_NguoiNhan.Text = "Người nhận";
                label_NguoiNhan.ForeColor = Color.Black;
            }
        }

        private void button_Gui_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("Kiểm tra kĩ các thông tin trước khi gửi! Bạn sẽ không thể hoàn tác sau khi gửi \nXác nhận gửi thông báo?","Thông báo",MessageBoxButtons.YesNo,MessageBoxIcon.Information);
            if (result == DialogResult.Yes)
            {
                if (textBox_NguoiNhan.Text != "")
                {
                    using (QLTVEntities db = new QLTVEntities())
                    {
                        if (db.NhanVien.Where(s => s.Email == textBox_NguoiNhan.Text).Any() == true)
                        {
                            HandleGuiNhanVien();
                            MessageBox.Show("Gửi thông báo thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else if (db.NguoiDung.Where(s => s.Email == textBox_NguoiNhan.Text).Any() == true)
                        {
                            HandleGuiNguoiDung();
                            MessageBox.Show("Gửi thông báo thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else MessageBox.Show("Không tìm thấy email người nhận", "Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
                    }
                }
                else if (textBox_NguoiNhan.Text == "")
                {
                    using (QLTVEntities db = new QLTVEntities())
                    {
                        if (checkBox_GuiTatCaNhanVien.Checked == true)
                        {
                            HandleGuiTatCaNhanVien();
                        }
                        if (checkBox_GuiTatCaNguoiDung.Checked == true)
                        {
                            HandleGuiTatCaNguoiDung();
                        }
                        MessageBox.Show("Gửi thông báo thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }

        private void pictureBox_Delete_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("Bạn muốn bỏ bản nháp này?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                textBox_TieuDe.Text = null;
                textBox_NguoiNhan.Text = null;
                textBox_NoiDung.Text = null;
                checkBox_GuiTatCaNhanVien.Checked = false;
                checkBox_GuiTatCaNguoiDung.Checked = false;
            }
        }
        #endregion
    }
}
