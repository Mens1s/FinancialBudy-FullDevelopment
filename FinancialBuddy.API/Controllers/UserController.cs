using AutoMapper;
using FinancialBuddy.API.Filters;
using FinancialBuddy.Application.DTOs.User;
using FinancialBuddy.Application.Interfaces.Services;
using FinancialBuddy.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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

        [HttpPut("{id}")]
        [AuthorizeOwner] // baya iyi oldu beğendim ben :D
        public async Task<IActionResult> Update(Guid id, UpdateUserRequest request)
        {
            if (id != request.Id)
                return BadRequest();

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
    }
}
