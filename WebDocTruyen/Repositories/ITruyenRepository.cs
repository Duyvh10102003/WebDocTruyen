using WebDocTruyen.Models;

namespace WebDocTruyen.Repository
{
    public interface ITruyenRepository
    {
        Task<IEnumerable<Truyen>> GetAllAsync();
        Task<Truyen> GetByIdAsync(int id);
        Task AddAsync(Truyen truyen);
        Task UpdateAsync(Truyen truyen);
        Task DeleteAsync(int id);
    }
}
