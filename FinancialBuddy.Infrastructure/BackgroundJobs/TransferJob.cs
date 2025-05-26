using FinancialBuddy.Application.Interfaces.Repositories;
using FinancialBuddy.Application.Interfaces.Services;
using FinancialBuddy.Domain.Entities;

namespace FinancialBuddy.Infrastructure.BackgroundJobs
{
    public class TransferJob
    {
        private readonly IGenericRepository<Transfer> _transferRepository;
        private readonly IGenericRepository<User> _userRepository;
        private readonly ITransactionService _transactionService;

        public TransferJob(IGenericRepository<Transfer> transferRepository, IGenericRepository<User> userRepository,
            ITransactionService transactionService)
        {
            _transferRepository = transferRepository;
            _userRepository = userRepository;
            _transactionService = transactionService;
        }

        public async Task ProcessScheduledTransfers()
        {
            var pendingTransfers = await _transferRepository.FindAsync(t => !t.IsCompleted && !t.IsFast);

            foreach (var transfer in pendingTransfers)
            {
                var sender = await _userRepository.GetByIdAsync(transfer.UserId);
                var receiver = await _userRepository.GetByIdAsync(transfer.ReceiverUserId);

                if (sender != null && receiver != null && sender.Balance >= transfer.Amount)
                {
                    sender.Balance -= transfer.Amount;
                    receiver.Balance += transfer.Amount;
                    transfer.IsCompleted = true;

                    _userRepository.Update(sender);
                    _userRepository.Update(receiver);
                    _transferRepository.Update(transfer);

                    await _transactionService.CreateTransactionAsync(new Application.DTOs.Transaction.CreateTransactionRequest
                    {
                        UserId = sender.Id,
                        Category = "Transfer",
                        Amount = transfer.Amount,
                        Description = $"Scheduled transfer to {receiver.Email}",
                        Date = DateTime.UtcNow
                    });

                    await _transactionService.CreateTransactionAsync(new Application.DTOs.Transaction.CreateTransactionRequest
                    {
                        UserId = receiver.Id,
                        Category = "Transfer",
                        Amount = transfer.Amount,
                        Description = $"Received scheduled transfer from {sender.Email}",
                        Date = DateTime.UtcNow
                    });


                    Console.WriteLine($"[TransferJob] Processed scheduled transfer {transfer.Id} from {sender.Email} to {receiver.Email}");
                }
                else
                {
                    Console.WriteLine($"[TransferJob] Failed scheduled transfer {transfer.Id} (insufficient balance or user not found)");
                }
            }

            await _transferRepository.SaveChangesAsync();
        }
    }
}
