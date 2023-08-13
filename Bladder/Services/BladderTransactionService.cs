using Bladder.Constants;
using Bladder.Data;
using Bladder.Entities;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories;

namespace Bladder.Services
{
    public class BladderTransactionService :   IBladderTransactionService
    {
        private readonly BladderDbContext context;
        private readonly IRepository<BladderTransaction> repository;

        public BladderTransactionService(BladderDbContext context, IRepository<BladderTransaction> repository)
        {
            this.context = context;
            this.repository = repository;
        }

        public async Task<BladderTransaction> GetAsync(int id)
        {
            return await context.BladderTransactions.Include(bt => bt.Bladder).FirstOrDefaultAsync(bt => bt.Id == id);
        }

        public async Task<List<BladderTransaction>> GetAllAsync()
        {
            return await context.BladderTransactions.Include(bt => bt.Bladder).ToListAsync();
        }

        public async Task CreateAsync(BladderTransaction transaction)
        {
             await repository.InsertAsync(transaction);            
        }

        public async Task UpdateAsync(BladderTransaction transaction)
        {
            var newBladder = await repository.UpdateAsync(transaction);
        }

        public async Task DeleteAsync(int id)
        {
            await repository.DeleteAsync(m => m.Id == id);
        }
        

    }
}
