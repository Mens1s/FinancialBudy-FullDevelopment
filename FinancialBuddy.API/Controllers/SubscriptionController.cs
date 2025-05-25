using FinancialBuddy.Application.DTOs.Subscription;
using FinancialBuddy.Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FinancialBuddy.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class SubscriptionController : ControllerBase
    {
        private readonly ISubscriptionService _subscriptionService;

        public SubscriptionController(ISubscriptionService subscriptionService)
        {
            _subscriptionService = subscriptionService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var subscriptions = await _subscriptionService.GetAllSubscriptionsAsync(userId);
            return Ok(subscriptions);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var subscription = await _subscriptionService.GetSubscriptionByIdAsync(id);
            if (subscription == null)
                return NotFound();

            return Ok(subscription);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateSubscriptionRequest request)
        {
            var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            request.UserId = userId;
            var created = await _subscriptionService.CreateSubscriptionAsync(request);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, UpdateSubscriptionRequest request)
        {
            if (id != request.Id)
                return BadRequest();

            await _subscriptionService.UpdateSubscriptionAsync(request);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _subscriptionService.DeleteSubscriptionAsync(id);
            return NoContent();
        }
    }
}
