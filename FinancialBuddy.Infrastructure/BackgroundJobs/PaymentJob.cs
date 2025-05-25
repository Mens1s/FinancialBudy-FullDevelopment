using FinancialBuddy.Application.Interfaces.Repositories;
using FinancialBuddy.Domain.Entities;


namespace FinancialBuddy.Infrastructure.BackgroundJobs
{
    public class PaymentJob
    {
        private readonly IGenericRepository<Subscription> _subscriptionRepository;

        public PaymentJob(IGenericRepository<Subscription> subscriptionRepository)
        {
            _subscriptionRepository = subscriptionRepository;
        }

        public async Task ProcessPayments()
        {
            var today = DateTime.UtcNow.Date;

            var dueSubscriptions = await _subscriptionRepository.FindAsync(
                s => s.NextPaymentDate.Date <= today);

            foreach (var sub in dueSubscriptions)
            {
                Console.WriteLine($"[PaymentJob] Processed payment for {sub.ServiceName}, UserId: {sub.UserId}");

                // Ödeme işlenince bir sonraki tarihi güncelle
                sub.NextPaymentDate = sub.Frequency == "Monthly"
                    ? sub.NextPaymentDate.AddMonths(1)
                    : sub.NextPaymentDate.AddYears(1);

                _subscriptionRepository.Update(sub);
            }

            await _subscriptionRepository.SaveChangesAsync();
        }
    }
}
