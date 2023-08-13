using Bladder.Entities;

namespace Bladder.Services
{
    public interface IBuildingBladderService
    {
        Task<bool> IsBladderCodeUnique(string bladderCode, int? currentBladderId = null);
        Task CreateAsync(BuildingBladder bladder);
        Task DeleteAsync(int id);
        Task<List<BuildingBladder>> GetAllAsync();
        Task<BuildingBladder> GetAsync(int id);
        Task UpdateAsync(BuildingBladder bladder);
        Task<List<BuildingBladder>> GetAllMountableAsync();
        Task<List<BuildingBladder>> GetAllDismountableAsync();
        Task<List<BuildingBladder>> GetAllMaintainableAsync();
    }
}