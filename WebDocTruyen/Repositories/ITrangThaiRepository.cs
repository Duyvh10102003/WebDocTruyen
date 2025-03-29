using WebDocTruyen.Models;

namespace WebDocTruyen.Repository
{
    public interface ITrangThaiRepository
    {
        Task<IEnumerable<TrangThai>> GetAllAsync();
        Task<TrangThai> GetByIdAsync(int id);
        Task AddAsync(TrangThai TrangThai);
        Task UpdateAsync(TrangThai TrangThai);
        Task DeleteAsync(int id);
    }
}
