using Microsoft.EntityFrameworkCore;
using WebDocTruyen.DataAccess;
using WebDocTruyen.Models;
using WebDocTruyen.Repository;

namespace WebDocTruyen.Repository
{
    public class EFTruyenRepository : ITruyenRepository
    {
        private readonly ApplicationDbContext _context;
        public EFTruyenRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Truyen>> GetAllAsync()
        {
            return await _context.Truyens.Include(p => p.TheLoai).Include(p => p.TacGia).Include(p => p.TrangThai).ToListAsync();
        }
        public async Task<Truyen> GetByIdAsync(int id)
        {
            return await _context.Truyens.Include(p => p.TheLoai).Include(p => p.TacGia).Include(p => p.TrangThai).FirstOrDefaultAsync(p => p.TruyenId== id);
        }
        public async Task AddAsync(Truyen truyen)
        {
            _context.Truyens.Add(truyen);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateAsync(Truyen truyen)
        {
            _context.Truyens.Update(truyen);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteAsync(int id)
        {
            var truyen = await _context.Truyens.Include(c => c.ChuongTruyens).FirstOrDefaultAsync(c => c.TruyenId== id);
            if (truyen == null)
            {
                throw new Exception("Không có truyện cần tìm.");
            }

            foreach (var chuongTruyen in truyen.ChuongTruyens)
            {
                _context.ChuongTruyens.Remove(chuongTruyen);
            }

            // Xóa chủ đề
            _context.Truyens.Remove(truyen);

            await _context.SaveChangesAsync();
        }
    }
}
