using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebDocTruyen.DataAccess;
using WebDocTruyen.Models;
using WebDocTruyen.Repository;

namespace WebDocTruyen.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class TruyenController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ITruyenRepository _truyenRepository;
        private readonly ITheLoaiRepository _theLoaiRepository;
        private readonly ITacGiaRepository _tacGiaRepository;
        private readonly ITrangThaiRepository _trangThaiRepository;
        
        public TruyenController(ITruyenRepository truyenRepository, ITheLoaiRepository theLoaiRepository, ITacGiaRepository tacGiaRepository, ITrangThaiRepository trangThaiRepository, ApplicationDbContext context)
        {
            _context = context;
            _truyenRepository = truyenRepository;
            _theLoaiRepository = theLoaiRepository;
            _tacGiaRepository = tacGiaRepository;
            _trangThaiRepository = trangThaiRepository;
        }
        public async Task<IActionResult> Index(int pageNumber = 1)
        {
            int pageSize = 10;
            IQueryable<Truyen> truyensQuery = _context.Truyens.Include(p => p.TheLoai).Include(p => p.TacGia).Include(p => p.TrangThai);
            var paginatedTruyens = await PaginatedList<Truyen>.CreateAsync(truyensQuery, pageNumber, pageSize);
            return View(paginatedTruyens);
            /*var truyens = await _truyenRepository.GetAllAsync();
            return View(truyens);*/
        }
        public async Task<IActionResult> Details(int id)
        {
            var product = await _truyenRepository.GetByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }
        public async Task<IActionResult> Create()
        {
            var theLoais = await _theLoaiRepository.GetAllAsync();
            var tacgias = await _tacGiaRepository.GetAllAsync();
            var trangthais = await _trangThaiRepository.GetAllAsync();
            ViewBag.TheLoais = new SelectList(theLoais, "TheLoaiId", "TenTheLoai");
            ViewBag.TacGias = new SelectList(tacgias, "TacGiaId", "TenTacGia");
            ViewBag.TrangThais = new SelectList(trangthais, "TrangThaiId", "TrangThaiTruyen");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Truyen truyen, IFormFile anh)
        {
            if (ModelState.IsValid)
            {
                if (anh != null)
                {
                    truyen.Anh = await SaveImage(anh);
                }
                await _truyenRepository.AddAsync(truyen);
                return RedirectToAction(nameof(Index));
            }
            // Nếu ModelState không hợp lệ, hiển thị form với dữ liệu đã nhập
            var theLoais = await _theLoaiRepository.GetAllAsync();
            var tacgias = await _tacGiaRepository.GetAllAsync();
            var trangthais = await _trangThaiRepository.GetAllAsync();
            ViewBag.TheLoais = new SelectList(theLoais, "TheLoaiId", "TenTheLoai");
            ViewBag.TacGias = new SelectList(tacgias, "TacGiaId", "TenTacGia");
            ViewBag.TrangThais = new SelectList(trangthais, "TrangThaiId", "TrangThaiTruyen");
            return View(truyen);
        }
        public async Task<IActionResult> Edit(int id)
        {
            var truyen = await _truyenRepository.GetByIdAsync(id);
            if (truyen == null)
            {
                return NotFound();
            }
            var theLoais = await _theLoaiRepository.GetAllAsync();
            var tacgias = await _tacGiaRepository.GetAllAsync();
            var trangthais = await _trangThaiRepository.GetAllAsync();
            ViewBag.TheLoais = new SelectList(theLoais, "TheLoaiId", "TenTheLoai");
            ViewBag.TacGias = new SelectList(tacgias, "TacGiaId", "TenTacGia");
            ViewBag.TrangThais = new SelectList(trangthais, "TrangThaiId", "TrangThaiTruyen");
            return View(truyen);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Truyen truyen,IFormFile anh)
        {
            ModelState.Remove("Anh"); 
            // Loại bỏ xác thực ModelState cho ImageUrl
            if (id != truyen.TruyenId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var existingTruyen = await _truyenRepository.GetByIdAsync(id); 
                // Giả định có phương thức GetByIdAsync                                                     
                // Giữ nguyên thông tin hình ảnh nếu không có hình mới được tải lên            
                if (anh == null)
                {
                    truyen.Anh = existingTruyen.Anh;
                }
                else
                {
                    // Lưu hình ảnh mới
                    truyen.Anh = await SaveImage(anh);
                }
                // Cập nhật các thông tin khác của sản phẩm
                existingTruyen.TenTruyen = truyen.TenTruyen;
                existingTruyen.NoiDung = truyen.NoiDung;
              
                existingTruyen.TheLoaiId = truyen.TheLoaiId;
                existingTruyen.TacGiaId = truyen.TacGiaId;
                existingTruyen.TrangThaiId = truyen.TrangThaiId;
                existingTruyen.Anh = truyen.Anh;

                await _truyenRepository.UpdateAsync(existingTruyen);

                return RedirectToAction(nameof(Index));
            }
            var theLoais = await _theLoaiRepository.GetAllAsync();
            var tacgias = await _tacGiaRepository.GetAllAsync();
            var trangthais = await _trangThaiRepository.GetAllAsync();
            ViewBag.TheLoais = new SelectList(theLoais, "TheLoaiId", "TenTheLoai");
            ViewBag.TacGias = new SelectList(tacgias, "TacGiaId", "TenTacGia");
            ViewBag.TrangThais = new SelectList(trangthais, "TrangThaiId", "TrangThaiTruyen");
            return View(truyen);
        }
        public async Task<IActionResult> Delete(int id)
        {
            var truyen = await _truyenRepository.GetByIdAsync(id);
            if (truyen == null)
            {
                return NotFound();
            }
            return View(truyen);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _truyenRepository.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
        private async Task<string> SaveImage(IFormFile image)
        {
            var savePath = Path.Combine("wwwroot/frontend/assets/images", image.FileName); // Thay   đổi đường dẫn theo cấu hình của bạn
            using (var fileStream = new FileStream(savePath, FileMode.Create))
            {
                await image.CopyToAsync(fileStream);
            }
            return "/frontend/assets/images/" + image.FileName; // Trả về đường dẫn tương đối
        }

        [HttpGet]
        public async Task<IActionResult> SearchTruyens(string query, int pageNumber = 1)
        {
            IQueryable<Truyen> productsQuery = _context.Truyens.Include(p => p.TheLoai).Where(p => p.TenTruyen.Contains(query));

            var paginatedProducts = await PaginatedList<Truyen>.CreateAsync(productsQuery, pageNumber, 2);
            return PartialView(paginatedProducts);
        }

        public async Task<IActionResult> PagingNoLibrary(int pageNumber)
        {
            int pageSize = 10;
            IQueryable<Truyen> productsQuery = _context.Truyens.Include(p => p.TheLoai).Include(p => p.TacGia).Include(p => p.TrangThai);
            var pagedProducts = await productsQuery.Skip((pageNumber - 1) * pageSize)

            .Take(pageSize)
            .ToListAsync();

            return View(pagedProducts);
        }
        public List<string> SearchSuggestions(string query)
        {
            return _context.Truyens
            .Where(p => p.TenTruyen.StartsWith(query))
            .Select(p => p.TenTruyen)
            .ToList();
        }
    }
}
