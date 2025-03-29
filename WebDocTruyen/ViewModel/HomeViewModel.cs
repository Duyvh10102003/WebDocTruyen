using WebDocTruyen.Models;

namespace WebDocTruyen.ViewModel
{
    public class HomeViewModel
    {
        public List<TheLoai> DanhSachTheLoai { get; set; }
        public List<TacGia> DanhSachTacGia { get; set; }
        public List<Truyen> DanhSachtruyen { get; set; }
        public List<Truyen> DanhSachTruyenHoanThanh { get; set; }
        public List<Truyen> TimKiemTheLoai { get; set; }

        public List<Truyen> TimKiemTacGia { get; set; }

        public List<Truyen> ProTruyen { get; set; }
        public PaginatedList<ChuongTruyen> DanhSachChuongTruyen { get; set; }
        public List<ChuongTruyen> DSChuongTruyen { get; set; }
        public List<ChuongTruyen> ChiTietChuongTruyen { get; set; }
        public PaginatedList<Truyen> DSTruyen { get; set; }

        public PaginatedList<Truyen> TimKiem {  get; set; }

        public List<Truyen> TruyenHot { get; set; }


    }
}
