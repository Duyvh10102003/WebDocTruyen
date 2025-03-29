using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebDocTruyen.DataAccess;
using WebDocTruyen.Models;
using WebDocTruyen.Repository;

namespace WebDocTruyen.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class ChuongTruyenController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IChuongTruyenRepository _chuongTruyenRepository;
        private readonly ITruyenRepository _truyenRepository;
        public ChuongTruyenController(IChuongTruyenRepository chuongTruyenRepository, ITruyenRepository truyenRepository, ApplicationDbContext context)
        {
            _context = context;
            _chuongTruyenRepository = chuongTruyenRepository;
            _truyenRepository = truyenRepository;
        }
        public async Task<IActionResult> Index(int id, int pageNumber = 1)
        {
            TempData["TruyenId"] = id;
            int pageSize = 5;
            IQueryable<ChuongTruyen> truyensQuery =  _context.ChuongTruyens.Include(p => p.Truyen).Where(p => p.TruyenId == id);
            var paginatedTruyens = await PaginatedList<ChuongTruyen>.CreateAsync(truyensQuery, pageNumber, pageSize);
            return View(paginatedTruyens);
            
            /*var chuongTruyens = await _chuongTruyenRepository.GetByTruyenIdAsync(id);
            return View(chuongTruyens);*/
        }
        public async Task<IActionResult> Details(int id)
        {
            var chuongTruyens = await _chuongTruyenRepository.GetByIdAsync(id);
            if (chuongTruyens == null)
            {
                return NotFound();
            }
            ViewBag.Id = chuongTruyens.TruyenId;
            return View(chuongTruyens);
        }
        public async Task<IActionResult> Create()
        {
            int truyenId = (int)TempData["TruyenId"];
            TempData["TruyenId"] = truyenId;
            ViewBag.truyenId = truyenId;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ChuongTruyen chuongTruyens)
        {
            int truyenId = (int)TempData["TruyenId"];
            if (ModelState.IsValid)
            {
                // Gán truyện ID cho chương truyện mới
                chuongTruyens.TruyenId = truyenId;

                // Gọi phương thức AddAsync từ repository để thêm chương truyện mới
                await _chuongTruyenRepository.AddAsync(chuongTruyens);

                // Chuyển hướng về action chi tiết của truyện sau khi thêm thành công
                return RedirectToAction("Index", new { id = chuongTruyens.TruyenId });
            }
            ViewBag.truyenId = truyenId;
            // Nếu ModelState không hợp lệ, hiển thị form với dữ liệu đã nhập
            return View(chuongTruyens);
        }
        public async Task<IActionResult> Edit(int id)
        {
            var chuongTruyens = await _chuongTruyenRepository.GetByIdAsync(id);
            if (chuongTruyens == null)
            {
                return NotFound();
            }
            TempData["TruyenId"] = chuongTruyens.TruyenId;
            ViewBag.Id = chuongTruyens.TruyenId;
            var truyen = await _truyenRepository.GetAllAsync();
            return View(chuongTruyens);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ChuongTruyen chuongTruyens)
        {
            int truyenId = (int)TempData["TruyenId"];
            if (id != chuongTruyens.ChuongTruyenId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var existingTruyen = await _chuongTruyenRepository.GetByIdAsync(id);
                // Cập nhật các thông tin khác của sản phẩm
                existingTruyen.TenChuong = chuongTruyens.TenChuong;
                existingTruyen.NoiDung = chuongTruyens.NoiDung;
              
                existingTruyen.DisplayOrder = chuongTruyens.DisplayOrder;
                existingTruyen.TruyenId = truyenId;

                await _chuongTruyenRepository.UpdateAsync(existingTruyen);

                return RedirectToAction("Index", new { id = truyenId });
            }
            var truyen = await _truyenRepository.GetAllAsync();
            ViewBag.truyenId = truyenId;
            return View(chuongTruyens);
        }
        public async Task<IActionResult> Delete(int id)
        {
            
            var chuongTruyens = await _chuongTruyenRepository.GetByIdAsync(id);
            if (chuongTruyens == null)
            {
                return NotFound();
            }
            TempData["TruyenId"] = chuongTruyens.TruyenId;
            ViewBag.Id = chuongTruyens.TruyenId;
            return View(chuongTruyens);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            
            await _chuongTruyenRepository.DeleteAsync(id);
            return RedirectToAction("Index", new { id = (int)TempData["TruyenId"]});
        }

        public async Task<IActionResult> PagingNoLibrary(int pageNumber)
        {
            int pageSize = 10;
            IQueryable<ChuongTruyen> productsQuery = _context.ChuongTruyens.Include(p => p.Truyen);
            var pagedProducts = await productsQuery.Skip((pageNumber - 1) * pageSize)

            .Take(pageSize)
            .ToListAsync();

            return View(pagedProducts);
        }
    }
}
