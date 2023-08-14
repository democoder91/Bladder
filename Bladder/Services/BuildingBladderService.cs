using Bladder.Constants;
using Bladder.Data;
using Bladder.Entities;
using Microsoft.EntityFrameworkCore;
using Polly;
using Volo.Abp.Domain.Repositories;

namespace Bladder.Services
{
    public class BuildingBladderService :   IBuildingBladderService
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
            return await repository.GetAsync(m =>m.Id == id);
        }

        public async Task<List<BuildingBladder>> GetAllAsync()
        {
            return await repository.GetListAsync();
        }

        public async Task CreateAsync(BuildingBladder bladder)
        {
            var newBladder = await repository.InsertAsync(bladder);            
        }

        public async Task UpdateAsync(BuildingBladder bladder)
        {
            var newBladder = await repository.UpdateAsync(bladder);
        }

        public async Task DeleteAsync(int id)
        {
            await repository.DeleteAsync(m => m.Id == id);
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
            return await dbContext.Bladders.Where(b => b.Status == BladderStatus.جاهز.ToString()).ToListAsync();
        }
        public async Task<List<BuildingBladder>> GetAllDismountableAsync()
        {
            return await dbContext.Bladders.Where(b => b.Status == BladderStatus.يعمل.ToString()).ToListAsync();
        }

        public async Task<List<BuildingBladder>> GetAllMaintainableAsync()
        {
            return await dbContext.Bladders.Where(b => b.Status == BladderStatus.تحت_الصيانة.ToString()).ToListAsync();
        }

        public async Task<List<BuildingBladder>> GetAllTestableAsync()
        {
            return await dbContext.Bladders.Where(b => b.Status == BladderStatus.في_الاختبار.ToString()).ToListAsync();
        }
    }
}
