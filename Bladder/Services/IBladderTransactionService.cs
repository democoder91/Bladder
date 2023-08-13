using Bladder.Entities;

namespace Bladder.Services
{
    public interface IBladderTransactionService
    {
        Task CreateAsync(BladderTransaction transaction);
        Task DeleteAsync(int id);
        Task<List<BladderTransaction>> GetAllAsync();
        Task<BladderTransaction> GetAsync(int id);
        Task UpdateAsync(BladderTransaction transaction);
    }
}