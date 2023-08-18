using Bladder.Constants;
using Bladder.Data;
using Bladder.Entities;
using Bladder.Entities.Transactions;
using Bladder.Localization;
using Castle.Core.Smtp;
using Hangfire;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Polly;
using System.Transactions;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Emailing;

namespace Bladder.Services
{
    public interface IBladderTransactionService
    {
        Task CreateAsync(BladderTransaction transaction);
        Task DeleteAsync(int id);
        Task<List<BladderTransaction>> GetAllAsync();
        Task<BladderTransaction> GetAsync(int id);
        Task<MountTransaction> GetLastMountTransactionAsync(int bladderId);
        Task UpdateAsync(BladderTransaction transaction);
    }

    public class BladderTransactionService : IBladderTransactionService
    {
        private readonly BladderDbContext context;
        private readonly IRepository<BladderTransaction> repository;
        private readonly IBackgroundJobClient backgroundJobClient;

        public BladderTransactionService(BladderDbContext context, IRepository<BladderTransaction> repository, IBackgroundJobClient backgroundJobClient)
        {
            this.context = context;
            this.repository = repository;
            this.backgroundJobClient = backgroundJobClient;
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
            backgroundJobClient.Enqueue(() => Console.WriteLine($"transaction of type {transaction.TransactionType} Transaction has been created"));
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
