using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Management;
using System.Windows.Forms;

namespace QuanLiThuVienUeh.nguoidung
{
    public partial class ff_Booking : Form
    {
        int idNguoiDung;
        List<PhieuMuonSach> listPhieuMuonSach;
        List<Panel> listPanel;
        List<Label> listTenSach;
        List<Label> listIDSach;
        List<Label> listCoupon;
        List<PictureBox> listAvatar;
        int currentRecord;

        public ff_Booking(int idNguoiDung)
        {
            InitializeComponent();
            this.idNguoiDung = idNguoiDung;
            panel_Pms2.Visible = false;
            panel_Pms3.Visible = false;
            listPhieuMuonSach = new List<PhieuMuonSach>();
            listPanel = new List<Panel>();
            listTenSach = new List<Label>();
            listIDSach = new List<Label>();
            listCoupon = new List<Label>();
            listAvatar = new List<PictureBox>();
            AddListPanel();
            AddListTenSach();
            AddListIDSach();
            AddListCoupon();
            AddListAvatar();
            currentRecord = 0;
            LoadData(currentRecord);
        }

        private void AddListPanel()
        {
            listPanel.Add(panel_Pms1);
            listPanel.Add(panel_Pms2);
            listPanel.Add(panel_Pms3);
        }

        private void AddListTenSach()
        {
            listTenSach.Add(label_TenSachPms1);
            listTenSach.Add(label_TenSachPms2);
            listTenSach.Add(label_TenSachPms3);
        }

        private void AddListIDSach()
        {
            listIDSach.Add(label_IDSachInfoPms1);
            listIDSach.Add(label_IDSachInfoPms2);
            listIDSach.Add(label_IDSachInfoPms3);
        }

        private void AddListCoupon()
        {
            listCoupon.Add(label_CouponInfoPms1);
            listCoupon.Add(label_CouponInfoPms2);
            listCoupon.Add(label_CouponInfoPms3);
        }

        private void AddListAvatar()
        {
            listAvatar.Add(pictureBox_AvatarPms1);
            listAvatar.Add(pictureBox_AvatarPms2);
            listAvatar.Add(pictureBox_AvatarPms3);
        }

        private void LoadData(int startRecord)
        {
            using (QLTVEntities db = new QLTVEntities())
            {
                var pms = db.PhieuMuonSach.Where(p => p.IDNguoiDung == idNguoiDung).ToList();
                for (int i = 0; i < 3; i++)
                {
                    if (startRecord + i < pms.Count)
                    {
                        listPhieuMuonSach.Add(pms[startRecord + i]); // Thêm dòng dữ liệu vào danh sách
                        listPanel[i].Visible = true;
                        listTenSach[i].Text = pms[startRecord + i].TenSach;
                        listIDSach[i].Text = pms[startRecord + i].IDSach.ToString();
                        if (pms[startRecord + i].Coupon == true)
                            listCoupon[i].Text = "+7 Day Borrow";
                        else
                            listCoupon[i].Text = "";

                        int? idSach = pms[startRecord + i].IDSach;
                        Sach sach = db.Sach.Where(s => s.IDSach == idSach).FirstOrDefault();
                        if (sach != null && sach.Avatar != null)
                        {
                            MemoryStream avatarSteam = new MemoryStream(sach.Avatar.ToArray());
                            Image avt = Image.FromStream(avatarSteam);
                            if (avt != null)
                                listAvatar[i].Image = avt;
                        }
                    }
                    else
                    {
                        // Nếu không còn dữ liệu để hiển thị, ẩn các panel không cần thiết
                        listPanel[i].Visible = false;
                    }
                }
            }
        }

        // Sự kiện khi click vào nút Next
        private void button_Next_Click(object sender, EventArgs e)
        {
            if (currentRecord + 3 > listPhieuMuonSach.Count) return;
            else
            {
                currentRecord += 3; // Tăng vị trí bắt đầu tải dữ liệu cho lần kế tiếp
                LoadData(currentRecord); // Tải dữ liệu cho 3 dòng kế tiếp
            }
        }

        // Sự kiện khi click vào nút Previous
        private void button_Previous_Click(object sender, EventArgs e)
        {
            if (currentRecord - 3 < 0) return;
            else
            {
                currentRecord -= 3; // Giảm vị trí bắt đầu tải dữ liệu cho lần trước đó
                if (currentRecord < 0)
                    currentRecord = 0; // Đảm bảo không bị vượt quá giới hạn dữ liệu
                LoadData(currentRecord); // Tải dữ liệu cho 3 dòng trước đó
            }
        }
    }
}
