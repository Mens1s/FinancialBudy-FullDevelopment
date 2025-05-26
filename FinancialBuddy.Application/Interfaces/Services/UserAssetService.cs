using AutoMapper;
using FinancialBuddy.Application.DTOs.Asset;
using FinancialBuddy.Application.DTOs.Transaction;
using FinancialBuddy.Application.Interfaces.Repositories;
using FinancialBuddy.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialBuddy.Application.Interfaces.Services
{
    public class UserAssetService : IUserAssetService
    {
        private readonly IGenericRepository<UserAsset> _userAssetRepository;
        private readonly IGenericRepository<User> _userRepository;
        private readonly IGenericRepository<ValueAsset> _valueAssetRepository;
        private readonly ITransactionService _transactionService;
        private readonly IMapper _mapper;

        public UserAssetService(
            IGenericRepository<UserAsset> userAssetRepository,
            IGenericRepository<User> userRepository,
            IGenericRepository<ValueAsset> valueAssetRepository,
            ITransactionService transactionService,
            IMapper mapper)
        {
            _userAssetRepository = userAssetRepository;
            _userRepository = userRepository;
            _valueAssetRepository = valueAssetRepository;
            _mapper = mapper;
            _transactionService = transactionService;
        }

        public async Task<IEnumerable<UserAssetDto>> GetPortfolioAsync(Guid userId)
        {
            var assets = await _userAssetRepository.FindAsync(ua => ua.UserId == userId);
            return assets.Select(a => new UserAssetDto
            {
                Id = a.Id,
                AssetId = a.AssetId,
                AssetName = a.Asset.Name,
                Quantity = a.Quantity,
                AveragePrice = a.AveragePrice
            });
        }

        public async Task BuyAssetAsync(BuyAssetRequest request)
        {
            var user = await _userRepository.GetByIdAsync(request.UserId);
            var asset = await _valueAssetRepository.GetByIdAsync(request.AssetId);
            var userAsset = (await _userAssetRepository.FindAsync(ua =>
                ua.UserId == request.UserId && ua.AssetId == request.AssetId)).FirstOrDefault();

            var totalCost = request.Quantity * asset.CurrentPrice;

            if (user.Balance < totalCost)
                throw new Exception("Insufficient balance.");

            if (userAsset == null)
            {
                userAsset = new UserAsset
                {
                    Id = Guid.NewGuid(),
                    UserId = user.Id,
                    AssetId = asset.Id,
                    Quantity = request.Quantity,
                    AveragePrice = asset.CurrentPrice
                };
                await _userAssetRepository.AddAsync(userAsset);
            }
            else
            {
                var totalValue = userAsset.Quantity * userAsset.AveragePrice + totalCost;
                var newQuantity = userAsset.Quantity + request.Quantity;
                userAsset.AveragePrice = totalValue / newQuantity;
                userAsset.Quantity = newQuantity;

                _userAssetRepository.Update(userAsset);
            }

            user.Balance -= totalCost;
            _userRepository.Update(user);

            _transactionService.CreateTransactionAsync(new CreateTransactionRequest
            {
                UserId = user.Id,
                Amount = totalCost,
                Description = $"Bought {request.Quantity} of {asset.Name}",
                Category = "Asset Purchase",
                Date = DateTime.UtcNow
            }).GetAwaiter().GetResult();

            // bi tek ortama çekmek lazım bunları iki iki yazıyoruz unutuluyor debuglada zor çıkıyo TODO:
            await _userRepository.SaveChangesAsync();
            await _userAssetRepository.SaveChangesAsync();
        }

        public async Task SellAssetAsync(SellAssetRequest request)
        {
            var user = await _userRepository.GetByIdAsync(request.UserId);
            var asset = await _valueAssetRepository.GetByIdAsync(request.AssetId);
            var userAsset = (await _userAssetRepository.FindAsync(ua =>
                ua.UserId == request.UserId && ua.AssetId == request.AssetId)).FirstOrDefault();

            if (userAsset == null || userAsset.Quantity < request.Quantity)
                throw new Exception("Insufficient asset quantity.");

            var totalSale = request.Quantity * asset.CurrentPrice;

            userAsset.Quantity -= request.Quantity;
            if (userAsset.Quantity == 0)
                _userAssetRepository.Remove(userAsset);
            else
                _userAssetRepository.Update(userAsset);

            user.Balance += totalSale;
            _userRepository.Update(user);

            _transactionService.CreateTransactionAsync(new CreateTransactionRequest
            {
                UserId = user.Id,
                Amount = totalSale,
                Description = $"Sell {request.Quantity} of {asset.Name}",
                Category = "Asset Purchase",
                Date = DateTime.UtcNow
            }).GetAwaiter().GetResult();

            await _userRepository.SaveChangesAsync();
            await _userAssetRepository.SaveChangesAsync();
        }
    }
}
