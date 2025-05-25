using FinancialBuddy.Application.Interfaces.Repositories;
using FinancialBuddy.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialBuddy.Infrastructure.BackgroundJobs
{
    public class SubscriptionAutoPaymentJob
    {
        private readonly IGenericRepository<Subscription> _subscriptionRepository;
        private readonly IGenericRepository<User> _userRepository;

        public SubscriptionAutoPaymentJob(IGenericRepository<Subscription> subscriptionRepository, IGenericRepository<User> userRepository)
        {
            _subscriptionRepository = subscriptionRepository;
            _userRepository = userRepository;
        }

        public async Task ProcessAutoPayments()
        {

            var today = DateTime.UtcNow.Date;

            var activeSubs = await _subscriptionRepository.FindAsync(s =>
                s.IsAutoPayment && s.NextPaymentDate.Date <= today);

            foreach (var sub in activeSubs)
            {
                var user = await _userRepository.GetByIdAsync(sub.UserId);
                if (user != null && user.Balance >= sub.Amount)
                {
                    var roundUpAmount = 0m;
                    var rounded = Math.Ceiling(sub.Amount / 100) * 100;

                    if (user.IsRoundUpEnabled && user.Balance >= rounded)   
                    { 
                        roundUpAmount = rounded - sub.Amount;
                        user.SavingBalance += roundUpAmount;
                        Console.WriteLine($"[AutoPaymentJob] Rounded Payment Processed {sub.ServiceName} for {user.Email}, Amount: {roundUpAmount}");
                    }

                    user.Balance -= sub.Amount + roundUpAmount;
                    sub.NextPaymentDate = sub.Frequency == "Monthly"
                        ? sub.NextPaymentDate.AddMonths(1)
                        : sub.NextPaymentDate.AddYears(1);

                    _userRepository.Update(user);
                    _subscriptionRepository.Update(sub);

                    Console.WriteLine($"[AutoPaymentJob] Processed {sub.ServiceName} for {user.Email}, Amount: {sub.Amount}");
                }
                else
                {
                    Console.WriteLine($"[AutoPaymentJob] Failed: Insufficient balance or user not found for {sub.ServiceName}");
                    // rabbitmq eklenerek aslında mesaj her türlü kullanıcıya login yapıtğında ulaştırılabilir.
                }
            }
            await _userRepository.SaveChangesAsync();
            await _subscriptionRepository.SaveChangesAsync();
        }
    }
}
