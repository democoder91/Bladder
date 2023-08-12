using Bladder.Data;
using Bladder.Entities;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories;

namespace Bladder.Services
{
    public class FindingService :   IFindingService
    {
        private readonly IRepository<Finding> repository;
        private readonly BladderDbContext dbContext;

        public FindingService(IRepository<Finding> repository, BladderDbContext dbContext)
        {
            this.repository = repository;
            this.dbContext = dbContext;
        }

        public async Task<Finding> GetAsync(int id)
        {
            return await repository.GetAsync(m =>m.Id == id);
        }

        public async Task<List<Finding>> GetAllAsync()
        {
            return await repository.GetListAsync();
        }

        public async Task CreateAsync(Finding finding)
        {
            var newBladder = await repository.InsertAsync(finding);            
        }

        public async Task UpdateAsync(Finding finding)
        {
            var newBladder = await repository.UpdateAsync(finding);
        }

        public async Task DeleteAsync(int id)
        {
            await repository.DeleteAsync(m => m.Id == id);
        }
        
    }
}
