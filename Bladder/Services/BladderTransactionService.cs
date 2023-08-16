using Bladder.Constants;
using Bladder.Data;
using Bladder.Entities;
using Bladder.Entities.Transactions;
using Castle.Core.Smtp;
using Microsoft.EntityFrameworkCore;
using Polly;
using System.Transactions;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Emailing;

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

        public async Task<MountTransaction> GetLastMountTransactionAsync(int bladderId)
        {
            return await context.Set<MountTransaction>()
                .Where(mt => mt.BladderId == bladderId)
                .OrderByDescending(mt => mt.CreatedAt)
                .FirstOrDefaultAsync();
        }

        
    }
}
