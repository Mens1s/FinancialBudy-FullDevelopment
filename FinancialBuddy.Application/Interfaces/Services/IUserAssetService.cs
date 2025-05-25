using FinancialBuddy.Application.DTOs.Asset;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialBuddy.Application.Interfaces.Services
{
    public interface IUserAssetService
    {
        Task<IEnumerable<UserAssetDto>> GetPortfolioAsync(Guid userId);
        Task BuyAssetAsync(BuyAssetRequest request);
        Task SellAssetAsync(SellAssetRequest request);
    }
}
