using WebDocTruyen.Models;

namespace WebDocTruyen.Repository
{
    public interface IChuongTruyenRepository
    {
        Task<IEnumerable<ChuongTruyen>> GetAllAsync();
        Task<ChuongTruyen> GetByIdAsync(int id);
        Task<IEnumerable<ChuongTruyen>> GetByTruyenIdAsync(int id);
        Task AddAsync(ChuongTruyen chuongTruyen);
        Task UpdateAsync(ChuongTruyen chuongTruyen);
        Task DeleteAsync(int id);
    }
}
