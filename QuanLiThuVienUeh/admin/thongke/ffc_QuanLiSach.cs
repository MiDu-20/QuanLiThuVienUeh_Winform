using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLiThuVienUeh.admin
{
    internal class ffc_QuanLiSach
    {
        private QLTVEntities _dbContext;

        public ffc_QuanLiSach()
        {
            _dbContext = new QLTVEntities(); // Khởi tạo DbContext của bạn
        }

        public int CountBorrowedBooks()
        {
            int borrowedBooksCount = _dbContext.MuonTraSach.Count(mts => mts.TinhTrang == "Đang mượn");
            return borrowedBooksCount;
        }

        public int CountAvailableBooks()
        {
            // Đếm số sách còn trong kho
            int? availableBooksCount = _dbContext.Sach.Sum(s => s.SoLuong);

            return availableBooksCount ?? 0;
        }
        public int CountOverdueBooks()
        {
            int overdueBooksCount = _dbContext.MuonTraSach.Count(mts => string.Equals(mts.TinhTrang, "Quá hạn"));

            return overdueBooksCount;
        }
        public Dictionary<string, int> GetBookCountsByGenre()
        {
            Dictionary<string, int> bookCountsByGenre = new Dictionary<string, int>();

            // Truy vấn cơ sở dữ liệu để lấy số sách của từng thể loại
            var booksByGenre = _dbContext.Sach
                .GroupBy(s => s.TheLoai)
                .Select(g => new { Genre = g.Key, Count = g.Count() })
                .ToList();
            foreach (var item in booksByGenre)
            {
                bookCountsByGenre.Add(item.Genre, item.Count);
            }

            return bookCountsByGenre;
        }
        public int CountBooksBefore2000()
        {
            // Truy vấn để đếm số lượng sách có năm xuất bản < 2000
            int count = _dbContext.Sach.Where(s => s.NamXuatBan.HasValue && s.NamXuatBan.Value.Year < 2000)
                                       .Sum(s => s.SoLuong).GetValueOrDefault();
            if (count == 0) return 0;
            return count;
        }

        public int CountBooksAfterOrEqualTo2000()
        {
            // Truy vấn để đếm số lượng sách có năm xuất bản >= 2000
            int count = _dbContext.Sach.Where(s => s.NamXuatBan.HasValue && s.NamXuatBan.Value.Year >= 2000)
                                       .Sum(s => s.SoLuong).GetValueOrDefault();
            if (count == 0) return 0;
            return count;
        }
    }
}
