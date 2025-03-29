using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebDocTruyen.Models;
using WebDocTruyen.Repository;

namespace WebDocTruyen.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class TrangThaiController : Controller
    {
        private readonly ITrangThaiRepository _trangThaiRepository;
        public TrangThaiController(ITrangThaiRepository trangThaiRepository)
        {
            _trangThaiRepository = trangThaiRepository;
        }
        public async Task<IActionResult> Index()
        {
            var trangThais = await _trangThaiRepository.GetAllAsync();
            return View(trangThais);
        }
        public async Task<IActionResult> Details(int id)
        {
            var trangThais = await _trangThaiRepository.GetByIdAsync(id);
            if (trangThais == null)
            {
                return NotFound();
            }
            return View(trangThais);
        }
        public async Task<IActionResult> Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TrangThai trangThais)
        {
            if (ModelState.IsValid)
            {
                await _trangThaiRepository.AddAsync(trangThais);
                return RedirectToAction(nameof(Index));
            }
            return View(trangThais);
        }
        public async Task<IActionResult> Edit(int id)
        {
            var trangThais = await _trangThaiRepository.GetByIdAsync(id);
            if (trangThais == null)
            {
                return NotFound();
            }
            return View(trangThais);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, TrangThai trangThais)
        {
            if (id != trangThais.TrangThaiId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var existingTruyen = await _trangThaiRepository.GetByIdAsync(id);
                // Cập nhật các thông tin khác của sản phẩm
                existingTruyen.TrangThaiTruyen = trangThais.TrangThaiTruyen;

                await _trangThaiRepository.UpdateAsync(existingTruyen);

                return RedirectToAction(nameof(Index));
            }
            return View(trangThais);
        }
        public async Task<IActionResult> Delete(int id)
        {
            var trangThais = await _trangThaiRepository.GetByIdAsync(id);
            if (trangThais == null)
            {
                return NotFound();
            }
            return View(trangThais);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _trangThaiRepository.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
