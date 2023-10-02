using Bladder.Constants;
using Bladder.Data;
using Bladder.Entities;
using Microsoft.EntityFrameworkCore;
using Polly;
using Volo.Abp.Domain.Repositories;

namespace Bladder.Services
{
    public interface IBuildingBladderService
    {
        Task CreateAsync(BuildingBladder bladder);
        Task<bool> DeleteAsync(int id);
        Task<List<BuildingBladder>> GetAllAsync();
        Task<List<BuildingBladder>> GetAllDismountableAsync();
        Task<List<BuildingBladder>> GetAllMaintainableAsync();
        Task<List<BuildingBladder>> GetAllMountableAsync();
        Task<List<BuildingBladder>> GetAllTestableAsync();
        Task<BuildingBladder> GetAsync(int id);
        Task<bool> IsBladderCodeUnique(string bladderCode, int? currentBladderId = null);
        Task UpdateAsync(BuildingBladder bladder);
    }

    public class BuildingBladderService : IBuildingBladderService
    {
        private readonly IRepository<BuildingBladder> repository;
        private readonly BladderDbContext dbContext;

        public BuildingBladderService(IRepository<BuildingBladder> repository, BladderDbContext dbContext)
        {
            this.repository = repository;
            this.dbContext = dbContext;
        }

        public async Task<BuildingBladder> GetAsync(int id)
        {
            return await repository.GetAsync(m => m.Id == id);
        }

        public async Task<List<BuildingBladder>> GetAllAsync()
        {
            return await dbContext.Bladders.Include(b =>b.BladderSize).ToListAsync();
        }

        public async Task CreateAsync(BuildingBladder bladder)
        {
            var newBladder = await repository.InsertAsync(bladder);
        }

        public async Task UpdateAsync(BuildingBladder bladder)
        {
            var newBladder = await repository.UpdateAsync(bladder);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var machines = new List<BuildingMachine>();
            //var bladder = await dbContext.Bladders.FindAsync(id);
            machines = await dbContext.Machines.Where(m => m.LeftBladderId == id || m.RightBladderId == id).ToListAsync();

            if (machines.Count == 0)
            {
                await repository.DeleteAsync(m => m.Id == id);
                return true;
            }

            return false;
        }
        public async Task<bool> IsBladderCodeUnique(string bladderCode, int? currentBladderId = null)
        {
            var query = dbContext.Bladders.Where(b => b.BladderCode == bladderCode);

            if (currentBladderId.HasValue)
            {
                query = query.Where(b => b.Id != currentBladderId.Value);
            }

            return !await query.AnyAsync();
        }

        public async Task<List<BuildingBladder>> GetAllMountableAsync()
        {
            return await dbContext.Bladders.Where(b => b.Status == BladderStatus.Ready.ToString()).ToListAsync();
        }
        public async Task<List<BuildingBladder>> GetAllDismountableAsync()
        {
            return await dbContext.Bladders.Where(b => b.Status == BladderStatus.Mounted.ToString()).ToListAsync();
        }

        public async Task<List<BuildingBladder>> GetAllMaintainableAsync()
        {
            return await dbContext.Bladders.Where(b => b.Status == BladderStatus.Maintenance.ToString()).ToListAsync();
        }

        public async Task<List<BuildingBladder>> GetAllTestableAsync()
        {
            return await dbContext.Bladders.Where(b => b.Status == BladderStatus.Testing.ToString()).ToListAsync();
        }
    }
}
