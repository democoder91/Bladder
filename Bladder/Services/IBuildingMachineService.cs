using Bladder.Entities;

namespace Bladder.Services
{
    public interface IBuildingMachineService
    {
        Task CreateAsync(BuildingMachine machine);
        Task DeleteAsync(int id);
        Task<List<BuildingMachine>> GetAllAsync();
        Task<BuildingMachine> GetAsync(int id);
        Task UpdateAsync(BuildingMachine machine);
    }
}