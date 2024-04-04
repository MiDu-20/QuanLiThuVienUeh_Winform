using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLiThuVienUeh.nguoidung
{
    public partial class ff_Home : Form
    {
        private List<Sach> NewBooksList;
        private List<Sach> RequestBooksList;
        private List<Image> ImageList;
        private static int IDNguoiDung;
        private static int CurrentNewBookIndex = 0;
        private static int CurrentRequestBookIndex = 0;
        private static int lblTimerIndex = 0;

        public ff_Home(int ID)
        {
            InitializeComponent();
            NewBooksList = new List<Sach>();
            RequestBooksList = new List<Sach>();
            ImageList = new List<Image>();
            IDNguoiDung = ID;
            LoadDatabaseNewBooks();
            LoadDatabaseRequestBooks();
            LoadImageIntoList();
            panelThongTin.BackgroundImage = ImageList[ImageList.Count -1];
        }
        #region NewBooks
        private void LoadDatabaseNewBooks()
        {
            int Year = 2020;
            using (var db = new QLTVEntities())
            {
                var Books = db.Sach.Where(p => p.NamXuatBan.Value.Year > Year).ToList();
                if (Books != null)
                {
                    foreach (Sach sach in Books)
                    {
                        NewBooksList.Add(sach);
                    }
                    Random random = new Random();
                    NewBooksList = NewBooksList.OrderBy(p => random.Next()).ToList();
                }
            }
            if (NewBooksList.Count > 0)
            {
                ShowCurrentNewBook(NewBooksList.Count() / 2);
            }
        }
        private void ShowCurrentNewBook(int index)
        {
            Sach SelectedSach = NewBooksList[index];
            if (SelectedSach != null)
            {
                lblTuaDe.Text = SelectedSach.TenSach;
                lblTacGia.Text = SelectedSach.TacGia;
                lblNoiDung.Text = SelectedSach.GioiThieu;
                byte[] imageData = SelectedSach.Avatar;
                if (imageData == null) return;
                using (MemoryStream ms = new MemoryStream(imageData))
                {
                    Image image = Image.FromStream(ms);
                    pictureBook.Image = image;
                }
            }
        }
        private void btnRightNewBook_Click(object sender, EventArgs e)
        {
            if (CurrentNewBookIndex >= 0 && CurrentNewBookIndex < NewBooksList.Count - 1)
            {
                CurrentNewBookIndex++;
                ShowCurrentNewBook(CurrentNewBookIndex);
            }
        }

        private void btnLeftNewBook_Click(object sender, EventArgs e)
        {
            if (CurrentNewBookIndex > 0 && CurrentNewBookIndex < NewBooksList.Count)
            {
                CurrentNewBookIndex--;
                ShowCurrentNewBook(CurrentNewBookIndex);
            }
        }
        #endregion
        #region RequestBooks
        private void LoadDatabaseRequestBooks()
        {
            int Year = 2020;
            using (var db = new QLTVEntities())
            {
                var User = db.NguoiDung.Where(p => p.IDNguoiDung == IDNguoiDung).FirstOrDefault();
                if (User == null) return;
                string UserChuyenNganh = User.ChuyenNganh;
                var Books = db.Sach.Where(p => p.ChuyenNganh.Contains(UserChuyenNganh)).ToList();
                if (User != null && Books != null)
                {
                    foreach (Sach sach in Books)
                    {
                        RequestBooksList.Add(sach);
                    }
                    Random random = new Random();
                    RequestBooksList = RequestBooksList.OrderBy(p => random.Next()).ToList();
                }
            }
            if (RequestBooksList.Count > 0)
            {
                ShowCurrentRequestBook(RequestBooksList.Count / 2);
            }
        }
        private void ShowCurrentRequestBook(int index)
        {
            Sach SelectedSach = RequestBooksList[index];
            if (SelectedSach != null)
            {
                lblTuaDeRequest.Text = SelectedSach.TenSach;
                lblTacGiaRequest.Text = SelectedSach.TacGia;
                lblNoiDungRequest.Text = SelectedSach.GioiThieu;
                byte[] imageData = SelectedSach.Avatar;
                if (imageData == null) return;
                using (MemoryStream ms = new MemoryStream(imageData))
                {
                    Image image = Image.FromStream(ms);
                    pictureRequestBook.Image = image;
                }
            }
        }
        private void btnLeftRequestBook_Click(object sender, EventArgs e)
        {
            if (CurrentRequestBookIndex > 0 && CurrentRequestBookIndex < RequestBooksList.Count)
            {
                CurrentRequestBookIndex--;
                ShowCurrentRequestBook(CurrentRequestBookIndex++);
            }
        }
        private void btnRightRequestBook_Click(object sender, EventArgs e)
        {
            if (CurrentRequestBookIndex >= 0 && CurrentRequestBookIndex < RequestBooksList.Count - 1)
            {
                CurrentRequestBookIndex++;
                ShowCurrentRequestBook(CurrentRequestBookIndex++);
            }
        }
        #endregion
        #region HeaderImage
        private Image GetImageByName(string link)
        {
            Image Hinh = Properties.Resources.ResourceManager.GetObject(link) as Image;
            return Hinh;
        }
        private void LoadImageIntoList()
        {
            ImageList.Add(GetImageByName("QuangCao1"));
            ImageList.Add(GetImageByName("QuangCao2"));
            ImageList.Add(GetImageByName("QuangCao3"));
            //ImageList.Add(GetImageByName("QuangCao4"));
            //ImageList.Add(GetImageByName("QuangCao5"));
            //ImageList.Add(GetImageByName("QuangCao6"));
            //ImageList.Add(GetImageByName("QuangCao7"));
            //ImageList.Add(GetImageByName("QuangCao8"));
        }
        private void lblTimer_Tick(object sender, EventArgs e)
        {
            lblTimerIndex++;
            panelThongTin.BackgroundImage = ImageList[lblTimerIndex];
            if (lblTimerIndex == ImageList.Count - 1)
            {
                lblTimerIndex = -1;
            }
        }
        #endregion
    }
}
