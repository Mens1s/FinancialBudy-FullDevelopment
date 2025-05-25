using FinancialBuddy.Application.DTOs.Subscription;

namespace FinancialBuddy.Application.Interfaces.Services
{
    public interface ISubscriptionService
    {
        Task<IEnumerable<SubscriptionDto>> GetAllSubscriptionsAsync(Guid userId);
        Task<SubscriptionDto> GetSubscriptionByIdAsync(Guid id);
        Task<SubscriptionDto> CreateSubscriptionAsync(CreateSubscriptionRequest request);
        Task UpdateSubscriptionAsync(UpdateSubscriptionRequest request);
        Task DeleteSubscriptionAsync(Guid id);
    }
}
