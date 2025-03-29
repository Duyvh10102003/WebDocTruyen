using Microsoft.EntityFrameworkCore;
using WebDocTruyen.DataAccess;
using WebDocTruyen.Models;

namespace WebDocTruyen.Repository
{
    public class EFTacGiaRepository : ITacGiaRepository
    {
        private readonly ApplicationDbContext _context;
        public EFTacGiaRepository( ApplicationDbContext context )
        {
            _context = context;
        }
        public async Task<IEnumerable<TacGia>> GetAllAsync()
        {
            return await _context.TacGias.Include(p => p.Truyens).ToListAsync();
        }

        public async Task<TacGia> GetByIdAsync(int id)
        {
            return await _context.TacGias.Include(p => p.Truyens).FirstOrDefaultAsync(p => p.TacGiaId == id);
        }
        public async Task AddAsync(TacGia tacGia)
        {
            _context.TacGias.Add(tacGia);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(TacGia tacGia)
        {
            _context.TacGias.Update(tacGia);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var tacGia = await _context.TacGias.Include(c => c.Truyens).ThenInclude(b => b.ChuongTruyens).FirstOrDefaultAsync(c => c.TacGiaId == id);
            if (tacGia == null)
            {
                throw new Exception("Không có tác giả cần tìm.");
            }

            foreach (var truyen in tacGia.Truyens)
            {
                foreach (var chuong in truyen.ChuongTruyens)
                {
                    _context.ChuongTruyens.Remove(chuong);
                }
                _context.Truyens.Remove(truyen);
            }
            _context.TacGias.Remove(tacGia);

            await _context.SaveChangesAsync();
        }

    }
}
