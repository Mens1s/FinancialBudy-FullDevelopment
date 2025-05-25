using FinancialBuddy.Application.DTOs.Auth;
using FinancialBuddy.Application.DTOs.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialBuddy.Application.Interfaces.Services
{
    public interface IAuthService
    {
        Task<AuthResponse> LoginAsync(LoginRequest request);
        Task<AuthResponse> RegisterAsync(CreateUserRequest request);
    }
}
