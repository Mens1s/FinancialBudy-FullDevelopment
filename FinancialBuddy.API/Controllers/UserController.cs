using AutoMapper;
using Azure.Core;
using FinancialBuddy.API.Filters;
using FinancialBuddy.Application.DTOs.User;
using FinancialBuddy.Application.Interfaces.Services;
using FinancialBuddy.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FinancialBuddy.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize] // sadece giriş yapmış kullanıcılar erişsin..
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public UserController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        [HttpGet]
        [Authorize(Roles = "Admin")] // only Admin 
        public async Task<IActionResult> GetAll()
        {
            var users = await _userService.GetAllUsersAsync();
            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            if (user == null)
                return NotFound();

            return Ok(user);
        }

        [HttpPut]
        //[AuthorizeOwner] // baya iyi oldu beğendim ben :D
        public async Task<IActionResult> Update(UpdateUserRequest request)
        {
            request.Id = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            await _userService.UpdateUserAsync(request);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [AuthorizeOwner]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _userService.DeleteUserAsync(id);
            return NoContent();
        }

        [HttpGet("getDashboardInformation")]
        public async Task<IActionResult> GetById()
        {
            var guid = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var userData = await _userService.GetDashboardInfo(guid);
            if (userData == null)
                return NotFound();

            return Ok(userData);
        }
    }
}
