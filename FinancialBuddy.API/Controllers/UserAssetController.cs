using FinancialBuddy.Application.DTOs.Asset;
using FinancialBuddy.Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FinancialBuddy.API.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class UserAssetController : ControllerBase
    {
        private readonly IUserAssetService _userAssetService;

        public UserAssetController(IUserAssetService userAssetService)
        {
            _userAssetService = userAssetService;
        }

        [HttpGet("portfolio")]
        public async Task<IActionResult> GetPortfolio()
        {
            var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var portfolio = await _userAssetService.GetPortfolioAsync(userId);
            return Ok(portfolio);
        }

        [HttpPost("buy")]
        public async Task<IActionResult> Buy(BuyAssetRequest request)
        {
            request.UserId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            await _userAssetService.BuyAssetAsync(request);
            return Ok("Asset purchased successfully.");
        }

        [HttpPost("sell")]
        public async Task<IActionResult> Sell(SellAssetRequest request)
        {
            request.UserId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            await _userAssetService.SellAssetAsync(request);
            return Ok("Asset sold successfully.");
        }
    }
}
