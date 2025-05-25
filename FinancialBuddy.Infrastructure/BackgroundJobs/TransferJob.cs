using FinancialBuddy.Application.Interfaces.Repositories;
using FinancialBuddy.Domain.Entities;

namespace FinancialBuddy.Infrastructure.BackgroundJobs
{
    public class TransferJob
    {
        private readonly IGenericRepository<Transfer> _transferRepository;
        private readonly IGenericRepository<User> _userRepository;

        public TransferJob(IGenericRepository<Transfer> transferRepository, IGenericRepository<User> userRepository)
        {
            _transferRepository = transferRepository;
            _userRepository = userRepository;
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
