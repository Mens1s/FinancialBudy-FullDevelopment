using FinancialBuddy.Application.DTOs.User;
using FinancialBuddy.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialBuddy.Application.Interfaces.Services
{
    public interface IUserService
    {
        Task<IEnumerable<UserDto>> GetAllUsersAsync();
        Task<UserDto> GetUserByIdAsync(Guid id);
        Task<UserDto> CreateUserAsync(CreateUserRequest request);
        Task UpdateUserAsync(UpdateUserRequest request);
        Task DeleteUserAsync(Guid id);
        Task<UserDashboardGeneralInfoDto> GetDashboardInfo(Guid id);
    }
}
