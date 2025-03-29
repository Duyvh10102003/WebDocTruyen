using Microsoft.EntityFrameworkCore;
using WebDocTruyen.DataAccess;
using WebDocTruyen.Models;

namespace WebDocTruyen.Repository
{
    public class EFChuongTruyenRepository : IChuongTruyenRepository
    {
        private readonly ApplicationDbContext _context;
        public EFChuongTruyenRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<ChuongTruyen>> GetAllAsync()
        {
            return await _context.ChuongTruyens.Include(p => p.Truyen).ToListAsync();
        }

        public async Task<ChuongTruyen> GetByIdAsync(int id)
        {
            return await _context.ChuongTruyens.Include(p => p.Truyen).FirstOrDefaultAsync(p => p.ChuongTruyenId == id);
        }
        public async Task<IEnumerable<ChuongTruyen>> GetByTruyenIdAsync(int truyenId)
        {
            List<ChuongTruyen> chuongTruyens = await _context.ChuongTruyens.Include(p => p.Truyen).Where(p => p.TruyenId == truyenId).ToListAsync();
            return chuongTruyens;
        }
        public async Task AddAsync(ChuongTruyen chuongTruyen)
        {
            _context.ChuongTruyens.Add(chuongTruyen);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(ChuongTruyen chuongTruyen)
        {
            _context.ChuongTruyens.Update(chuongTruyen);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var chuongTruyen = await _context.ChuongTruyens.FindAsync(id);
            _context.ChuongTruyens.Remove(chuongTruyen);
            await _context.SaveChangesAsync();
        }
    }
}
