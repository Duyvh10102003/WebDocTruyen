using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebDocTruyen.Models;
using WebDocTruyen.Repository;

namespace WebDocTruyen.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class TacGiaController : Controller
    {
        private readonly ITacGiaRepository _tacGiaRepository;
        public TacGiaController(ITacGiaRepository tacGiaRepository)
        {
            _tacGiaRepository = tacGiaRepository;
        }
        public async Task<IActionResult> Index()
        {
            var tacGias = await _tacGiaRepository.GetAllAsync();
            return View(tacGias);
        }
        public async Task<IActionResult> Details(int id)
        {
            var tacGias = await _tacGiaRepository.GetByIdAsync(id);
            if (tacGias == null)
            {
                return NotFound();
            }
            return View(tacGias);
        }
        public async Task<IActionResult> Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TacGia tacGias)
        {
            if (ModelState.IsValid)
            {
                await _tacGiaRepository.AddAsync(tacGias);
                return RedirectToAction(nameof(Index));
            }
            return View(tacGias);
        }
        public async Task<IActionResult> Edit(int id)
        {
            var tacGias = await _tacGiaRepository.GetByIdAsync(id);
            if (tacGias == null)
            {
                return NotFound();
            }
            return View(tacGias);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, TacGia tacGias)
        {
            if (id != tacGias.TacGiaId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var existingTruyen = await _tacGiaRepository.GetByIdAsync(id);
                // Cập nhật các thông tin khác của sản phẩm
                existingTruyen.TenTacGia = tacGias.TenTacGia;

                await _tacGiaRepository.UpdateAsync(existingTruyen);

                return RedirectToAction(nameof(Index));
            }
            return View(tacGias);
        }
        public async Task<IActionResult> Delete(int id)
        {
            var tacGias = await _tacGiaRepository.GetByIdAsync(id);
            if (tacGias == null)
            {
                return NotFound();
            }
            return View(tacGias);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _tacGiaRepository.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
