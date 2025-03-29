using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using WebDocTruyen.DataAccess;
using WebDocTruyen.Models;
using WebDocTruyen.ViewModel;

namespace WebDocTruyen.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index(int pageNumber = 1)
        {
            var danhSachTheLoai = await _context.TheLoais.ToListAsync();
            var danhSachTacGia = await _context.TacGias.ToListAsync();
            var danhSachTruyen = await _context.Truyens.OrderByDescending(p => p.TruyenId).ToListAsync();
            var danhSachTruyenHoanThanh = await _context.Truyens
                .Include(p => p.TrangThai)
                .Where(p => p.TrangThai.TrangThaiTruyen == "Hoàn Thành")
                .ToListAsync();

            int pageSize = 16;
            IQueryable<Truyen> truyensQuery = _context.Truyens.OrderByDescending(p=>p.TruyenId)
                .Include(p => p.TheLoai)
                .Include(p => p.TacGia)
                .Include(p => p.TrangThai);

            var paginatedTruyens = await PaginatedList<Truyen>.CreateAsync(truyensQuery, pageNumber, pageSize);

            var viewModel = new HomeViewModel
            {
                DanhSachTheLoai = danhSachTheLoai,
                DanhSachTacGia = danhSachTacGia,
                DanhSachtruyen = danhSachTruyen,
                DanhSachTruyenHoanThanh = danhSachTruyenHoanThanh,
                DSTruyen = paginatedTruyens,
            };

            return View(viewModel);
        }


        public IActionResult LoadTheLoai(int id)
        {
            var danhSachTheLoai = _context.TheLoais.ToList();
            var danhSachTacGia = _context.TacGias.ToList();
            var tl = _context.Truyens.OrderByDescending(p => p.TruyenId).Include(p => p.TheLoai).Where(p => p.TheLoaiId == id).ToList();
            var viewModel = new HomeViewModel
            {
                DanhSachTheLoai = danhSachTheLoai,
                DanhSachTacGia = danhSachTacGia,
                TimKiemTheLoai = tl,
            };

            return View(viewModel);
        }
        public IActionResult LoadTacGia(int id)
        {
            var danhSachTheLoai = _context.TheLoais.ToList();
            var danhSachTacGia = _context.TacGias.ToList();
            var tg = _context.Truyens.OrderByDescending(p => p.TruyenId).Include(p => p.TacGia).Where(p => p.TacGiaId == id).ToList();
            var viewModel = new HomeViewModel
            {
                DanhSachTheLoai = danhSachTheLoai,
                DanhSachTacGia = danhSachTacGia,
                TimKiemTacGia = tg,
            };

            return View(viewModel);
        }
        public async Task<IActionResult> _MenuPartial()
        {
            return PartialView();
        }

        [Authorize(Roles = "Admin,User")]
        public async Task<IActionResult> DisplayTruyen(int id,int pageNumber = 1)
        {
            var danhSachTheLoai = await _context.TheLoais.ToListAsync();
            var danhSachTacGia = await _context.TacGias.ToListAsync();
            var danhSachTruyen = await _context.Truyens.OrderByDescending(p => p.TruyenId).Take(10).ToListAsync();
            var chitiettruyen = await _context.Truyens.Include(p => p.TheLoai).Include(p => p.TacGia).Include(p => p.TrangThai).Where(p => p.TruyenId == id).ToListAsync();
            int pageSize = 10;
            IQueryable<ChuongTruyen> truyensQuery = _context.ChuongTruyens.Include(p => p.Truyen).Where(p => p.TruyenId == id);
            var paginatedTruyens = await PaginatedList<ChuongTruyen>.CreateAsync(truyensQuery, pageNumber, pageSize);
            var viewModel = new HomeViewModel
            {
                DanhSachTheLoai = danhSachTheLoai,
                DanhSachTacGia = danhSachTacGia,
                TruyenHot = danhSachTruyen,
                ProTruyen = chitiettruyen,
                DanhSachChuongTruyen = paginatedTruyens,
            };
            return View(viewModel);
        }
        [Authorize(Roles = "Admin,User")]

        public IActionResult DocTruyen(int id,int truyenId)
        {
            var danhSachTheLoai = _context.TheLoais.ToList();
            var danhSachTacGia = _context.TacGias.ToList();
            var chiTietChuongTruyen = _context.ChuongTruyens.Include(p => p.Truyen).Where(p => p.ChuongTruyenId == id ).ToList();
            var danhsachChuongTruyen = _context.ChuongTruyens.Include(p => p.Truyen).Where(p => p.TruyenId == truyenId).ToList();
            var viewModel = new HomeViewModel
            {
                DanhSachTheLoai = danhSachTheLoai,
                DanhSachTacGia = danhSachTacGia,
                ChiTietChuongTruyen = chiTietChuongTruyen,
                DSChuongTruyen = danhsachChuongTruyen,
            };

            return View(viewModel);
        }

        public List<string> SearchSuggestions(string query)
        {
            return _context.Truyens
            .Where(p => p.TenTruyen.StartsWith(query))
            .Select(p => p.TenTruyen)
            .ToList();
        }
        [HttpGet]
        public async Task<IActionResult> SearchTruyens(string query, int pageNumber = 1)
        {
            IQueryable<Truyen> productsQuery = _context.Truyens.Include(p => p.TheLoai).Where(p => p.TenTruyen.Contains(query));

            var paginatedProducts = await PaginatedList<Truyen>.CreateAsync(productsQuery, pageNumber, 20);
            var danhSachTheLoai = await _context.TheLoais.ToListAsync();
            var danhSachTacGia = await _context.TacGias.ToListAsync();
            var danhSachTruyen = await _context.Truyens.OrderByDescending(p => p.TruyenId).ToListAsync();
            var danhSachTruyenHoanThanh = await _context.Truyens
                .Include(p => p.TrangThai)
                .Where(p => p.TrangThai.TrangThaiTruyen == "Hoàn Thành")
                .ToListAsync();
            IQueryable<Truyen> truyensQuery = _context.Truyens.OrderByDescending(p => p.TruyenId)
                .Include(p => p.TheLoai)
                .Include(p => p.TacGia)
                .Include(p => p.TrangThai);
            var viewModel = new HomeViewModel
            {
                DanhSachTheLoai = danhSachTheLoai,
                DanhSachTacGia = danhSachTacGia,
                DanhSachtruyen = danhSachTruyen,
                DanhSachTruyenHoanThanh = danhSachTruyenHoanThanh,
                DSTruyen = paginatedProducts,
            };

            return View("Index",viewModel);
        }
        public IActionResult ChuongTiepTheo(int truyenId, int order)
        {
            order++;
            var chuongTiepTheo = _context.ChuongTruyens.Include(p => p.Truyen).FirstOrDefault(p => p.Truyen.TruyenId == truyenId && p.DisplayOrder == order);
            var minDisplayOrder = _context.ChuongTruyens
              .Where(p => p.Truyen.TruyenId == truyenId)
              .Min(p => p.DisplayOrder);
            if (chuongTiepTheo == null)
            {
                chuongTiepTheo = _context.ChuongTruyens.Include(p => p.Truyen).FirstOrDefault(p => p.Truyen.TruyenId == truyenId && p.DisplayOrder == minDisplayOrder);
            }
            return RedirectToAction("DocTruyen", new { id = chuongTiepTheo.ChuongTruyenId ,truyenId=chuongTiepTheo.TruyenId});

        }
        public IActionResult ChuongTruoc(int truyenId, int order)
        {

            order--;
            var chuongTruoc = _context.ChuongTruyens.Include(p => p.Truyen).FirstOrDefault(p => p.Truyen.TruyenId == truyenId && p.DisplayOrder == order);
            var minDisplayOrder = _context.ChuongTruyens
            .Where(p => p.Truyen.TruyenId == truyenId)
            .Min(p => p.DisplayOrder);
            if (chuongTruoc == null)
            {
                chuongTruoc = _context.ChuongTruyens.Include(p => p.Truyen).FirstOrDefault(p => p.Truyen.TruyenId == truyenId && p.DisplayOrder == minDisplayOrder);
            }
            return RedirectToAction("DocTruyen", new { id = chuongTruoc.ChuongTruyenId ,truyenId = chuongTruoc.TruyenId });

        }


    }
}
