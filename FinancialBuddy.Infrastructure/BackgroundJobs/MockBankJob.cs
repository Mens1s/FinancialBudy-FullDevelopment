using FinancialBuddy.Application.Interfaces.Repositories;
using FinancialBuddy.Domain.Entities;

namespace FinancialBuddy.Infrastructure.BackgroundJobs
{
    public class MockBankJob
    {
        private readonly IGenericRepository<Transaction> _transactionRepository;

        public MockBankJob(IGenericRepository<Transaction> transactionRepository)
        {
            _transactionRepository = transactionRepository;
        }

        public async Task SyncCreditCardDebts()
        {
            // mock bir bankadan çekilmiş gibi davranalım
            var mockDebts = new List<Transaction>
            {
                new Transaction
                {
                    Id = Guid.NewGuid(),
                    UserId = Guid.Parse("ca85fd33-c830-4bfc-d1a1-08dd9bb84f2d"), // ahmet.yigit.1@example.com
                    Category = "CreditCard",
                    Amount = 1500m,
                    Date = DateTime.UtcNow,
                    Description = "Mock credit card debt sync"
                }
            };

            foreach (var debt in mockDebts)
            {
                await _transactionRepository.AddAsync(debt);
                Console.WriteLine($"[MockBankJob] Synced debt for UserId: {debt.UserId}, Amount: {debt.Amount}");
            }

            await _transactionRepository.SaveChangesAsync();
        }
    }
}
