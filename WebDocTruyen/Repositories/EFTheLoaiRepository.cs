using Microsoft.EntityFrameworkCore;
using WebDocTruyen.DataAccess;
using WebDocTruyen.Models;
using WebDocTruyen.Repository;

namespace WebDocTruyen.Repository
{
    public class EFTheLoaiRepository : ITheLoaiRepository
    {
        private readonly ApplicationDbContext _context;

        public EFTheLoaiRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TheLoai>> GetAllAsync()
        {
            return await _context.TheLoais.Include(p => p.Truyens).ToListAsync();
        }

        public async Task<TheLoai> GetByIdAsync(int id)
        {
            return await _context.TheLoais.Include(p => p.Truyens).FirstOrDefaultAsync(p => p.TheLoaiId == id);
        }
        public async Task AddAsync(TheLoai theLoai)
        {
            _context.TheLoais.Add(theLoai);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(TheLoai theLoai)
        {
            _context.TheLoais.Update(theLoai);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var theLoai = await _context.TheLoais.Include(c => c.Truyens).ThenInclude(b => b.ChuongTruyens).FirstOrDefaultAsync(c => c.TheLoaiId == id);
            if (theLoai == null)
            {
                throw new Exception("Không có thể loại cần tìm.");
            }

            // Xóa tất cả các cuốn sách thuộc chủ đề này
            foreach (var truyen in theLoai.Truyens)
            {
                foreach (var chuong in truyen.ChuongTruyens)
                {
                    _context.ChuongTruyens.Remove(chuong);
                }
                _context.Truyens.Remove(truyen);
            }

            // Xóa chủ đề
            _context.TheLoais.Remove(theLoai);

            await _context.SaveChangesAsync();
        }
    }
}
