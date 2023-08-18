using Bladder.Entities;
using Volo.Abp.Domain.Repositories;

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

    public class BuildingMachineService : IBuildingMachineService
    {
        private readonly IRepository<BuildingMachine> repository;

        public BuildingMachineService(IRepository<BuildingMachine> repository)
        {
            this.repository = repository;
        }

        public async Task<BuildingMachine> GetAsync(int id)
        {
            return await repository.GetAsync(m => m.Id == id);
        }

        public async Task<List<BuildingMachine>> GetAllAsync()
        {
            return await repository.GetListAsync();
        }

        public async Task CreateAsync(BuildingMachine machine)
        {
            var newMachine = await repository.InsertAsync(machine);
        }

        public async Task UpdateAsync(BuildingMachine machine)
        {
            var newMachine = await repository.UpdateAsync(machine);
        }

        public async Task DeleteAsync(int id)
        {
            await repository.DeleteAsync(m => m.Id == id);
        }
    }
}
