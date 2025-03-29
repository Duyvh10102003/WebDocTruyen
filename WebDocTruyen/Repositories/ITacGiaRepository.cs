using WebDocTruyen.Models;

namespace WebDocTruyen.Repository
{
    public interface ITacGiaRepository
    {
        Task<IEnumerable<TacGia>> GetAllAsync();
        Task<TacGia> GetByIdAsync(int id);
        Task AddAsync(TacGia tacGia);
        Task UpdateAsync(TacGia tacGia);
        Task DeleteAsync(int id);
    }
}
