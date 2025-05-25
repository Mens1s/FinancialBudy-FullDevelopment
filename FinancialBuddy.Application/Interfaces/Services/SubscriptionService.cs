using AutoMapper;
using FinancialBuddy.Application.DTOs.Subscription;
using FinancialBuddy.Application.Interfaces.Repositories;
using FinancialBuddy.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialBuddy.Application.Interfaces.Services
{
    public class SubscriptionService : ISubscriptionService
    {
        private readonly IGenericRepository<Subscription> _subscriptionRepository;
        private readonly IMapper _mapper;

        public SubscriptionService(IGenericRepository<Subscription> subscriptionRepository, IMapper mapper)
        {
            _subscriptionRepository = subscriptionRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<SubscriptionDto>> GetAllSubscriptionsAsync(Guid userId)
        {
            var subscriptions = await _subscriptionRepository.FindAsync(s => s.UserId == userId);
            return _mapper.Map<IEnumerable<SubscriptionDto>>(subscriptions);
        }

        public async Task<SubscriptionDto> GetSubscriptionByIdAsync(Guid id)
        {
            var subscription = await _subscriptionRepository.GetByIdAsync(id);
            return _mapper.Map<SubscriptionDto>(subscription);
        }

        public async Task<SubscriptionDto> CreateSubscriptionAsync(CreateSubscriptionRequest request)
        {
            var subscription = _mapper.Map<Subscription>(request);
            await _subscriptionRepository.AddAsync(subscription);
            await _subscriptionRepository.SaveChangesAsync();
            return _mapper.Map<SubscriptionDto>(subscription);
        }

        public async Task UpdateSubscriptionAsync(UpdateSubscriptionRequest request)
        {
            var subscription = await _subscriptionRepository.GetByIdAsync(request.Id);
            if (subscription != null)
            {
                subscription.ServiceName = request.ServiceName;
                subscription.Amount = request.Amount;
                subscription.NextPaymentDate = request.NextPaymentDate;
                subscription.Frequency = request.Frequency;

                _subscriptionRepository.Update(subscription);
                await _subscriptionRepository.SaveChangesAsync();
            }
        }

        public async Task DeleteSubscriptionAsync(Guid id)
        {
            var subscription = await _subscriptionRepository.GetByIdAsync(id);
            if (subscription != null)
            {
                _subscriptionRepository.Remove(subscription);
                await _subscriptionRepository.SaveChangesAsync();
            }
        }
    }
}
