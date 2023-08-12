using Bladder.Entities;

namespace Bladder.Services
{
    public interface IFindingService
    {
        Task CreateAsync(Finding finding);
        Task DeleteAsync(int id);
        Task<List<Finding>> GetAllAsync();
        Task<Finding> GetAsync(int id);
        Task UpdateAsync(Finding finding);
    }
}