using Bladder.Entities;
using Volo.Abp.Domain.Repositories;

namespace Bladder.Services
{
    public interface IBladderSizeService
    {
        Task CreateAsync(BladderSize size);
        Task DeleteAsync(int id);
        Task<List<BladderSize>> GetAllAsync();
        Task<BladderSize> GetAsync(int id);
        Task UpdateAsync(BladderSize size);
    }

    public class BladderSizeService : IBladderSizeService
    {
        private readonly IRepository<BladderSize> repository;

        public BladderSizeService(IRepository<BladderSize> repository)
        {
            this.repository = repository;
        }

        public async Task<BladderSize> GetAsync(int id)
        {
            return await repository.GetAsync(m => m.Id == id);
        }

        public async Task<List<BladderSize>> GetAllAsync()
        {
            return await repository.GetListAsync(true);
        }

        public async Task CreateAsync(BladderSize size)
        {
            var newMachine = await repository.InsertAsync(size);
        }

        public async Task UpdateAsync(BladderSize size)
        {
            var newMachine = await repository.UpdateAsync(size);
        }

        public async Task DeleteAsync(int id)
        {
            await repository.DeleteAsync(m => m.Id == id);

        }


    }
}
