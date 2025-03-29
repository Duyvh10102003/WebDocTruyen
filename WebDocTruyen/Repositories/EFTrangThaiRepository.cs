using Microsoft.EntityFrameworkCore;
using WebDocTruyen.DataAccess;
using WebDocTruyen.Models;

namespace WebDocTruyen.Repository
{
    public class EFTrangThaiRepository : ITrangThaiRepository
    {
        private readonly ApplicationDbContext _context;

        public EFTrangThaiRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TrangThai>> GetAllAsync()
        {
            return await _context.TrangThais.Include(p => p.Truyens).ToListAsync();
        }

        public async Task<TrangThai> GetByIdAsync(int id)
        {
            return await _context.TrangThais.Include(p => p.Truyens).FirstOrDefaultAsync(p => p.TrangThaiId== id);
        }
        public async Task AddAsync(TrangThai trangThai)
        {
            _context.TrangThais.Add(trangThai);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(TrangThai trangThai)
        {
            _context.TrangThais.Update(trangThai);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var trangThai = await _context.TrangThais.Include(c => c.Truyens).ThenInclude(b => b.ChuongTruyens).FirstOrDefaultAsync(c => c.TrangThaiId == id);
            if (trangThai == null)
            {
                throw new Exception("Không có trang thái cần tìm.");
            }

            // Xóa tất cả các cuốn sách thuộc chủ đề này
            foreach (var truyen in trangThai.Truyens)
            {
                foreach (var chuong in truyen.ChuongTruyens)
                {
                    _context.ChuongTruyens.Remove(chuong);
                }
                _context.Truyens.Remove(truyen);
            }

            // Xóa chủ đề
            _context.TrangThais.Remove(trangThai);

            await _context.SaveChangesAsync();
        }
    }
}
