using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebDocTruyen.Models;
using WebDocTruyen.Repository;

namespace WebDocTruyen.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class TheLoaiController : Controller
    {
        private readonly ITheLoaiRepository _theLoaiRepository;
        public TheLoaiController(ITheLoaiRepository theLoaiRepository)
        {
            _theLoaiRepository = theLoaiRepository;
        }
        public async Task<IActionResult> Index()
        {
            var theLoai = await _theLoaiRepository.GetAllAsync();
            return View(theLoai);
        }
        public async Task<IActionResult> Details(int id)
        {
            var theLoai = await _theLoaiRepository.GetByIdAsync(id);
            if (theLoai == null)
            {
                return NotFound();
            }
            return View(theLoai);
        }
        public async Task<IActionResult> Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TheLoai theLoai)
        {
            if (ModelState.IsValid)
            {
                await _theLoaiRepository.AddAsync(theLoai);
                return RedirectToAction(nameof(Index));
            }
            return View(theLoai);
        }
        public async Task<IActionResult> Edit(int id)
        {
            var theLoai = await _theLoaiRepository.GetByIdAsync(id);
            if (theLoai == null)
            {
                return NotFound();
            }
            return View(theLoai);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, TheLoai theLoai)
        {
            if (id != theLoai.TheLoaiId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var existingTruyen = await _theLoaiRepository.GetByIdAsync(id);
                // Cập nhật các thông tin khác của sản phẩm
                existingTruyen.TenTheLoai = theLoai.TenTheLoai;

                await _theLoaiRepository.UpdateAsync(existingTruyen);

                return RedirectToAction(nameof(Index));
            }
            return View(theLoai);
        }
        public async Task<IActionResult> Delete(int id)
        {
            var theLoai = await _theLoaiRepository.GetByIdAsync(id);
            if (theLoai == null)
            {
                return NotFound();
            }
            return View(theLoai);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _theLoaiRepository.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
