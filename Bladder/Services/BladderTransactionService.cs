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
        private readonly IEmailService emailSender;

        public BladderTransactionService(BladderDbContext context, IRepository<BladderTransaction> repository, IEmailService emailSender)
        {
            this.context = context;
            this.repository = repository;
            this.emailSender = emailSender;
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
            await emailSender.SendEmailAsync( "demo.composer2@gmail.com" , $"{transaction.TransactionType} created!", $"<h1>{transaction.TransactionType} created!</h1>");

            
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
