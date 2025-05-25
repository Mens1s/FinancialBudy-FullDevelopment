using AutoMapper;
using FinancialBuddy.Application.DTOs.Auth;
using FinancialBuddy.Application.DTOs.User;
using FinancialBuddy.Application.Interfaces.Repositories;
using FinancialBuddy.Domain.Entities;
using Microsoft.Extensions.Configuration;
using BCrypt.Net;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;

namespace FinancialBuddy.Application.Interfaces.Services
{
    public class AuthService : IAuthService
    {
        private readonly IGenericRepository<User> _userRepository;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        public AuthService(IGenericRepository<User> userRepository, IMapper mapper, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _configuration = configuration;
        }
        public async Task<AuthResponse> LoginAsync(LoginRequest request)
        {
            var user = (await _userRepository.FindAsync(u => u.Email == request.Email)).FirstOrDefault();

            if (user == null || !BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
                throw new Exception("Invalid xredentials");

            var token = GenerateJwtToken(user);
            return new AuthResponse
            {
                Token = token,
                Email = user.Email,
                FullName = user.FullName
            };
        }

        public async Task<AuthResponse> RegisterAsync(CreateUserRequest request)
        {
            var userExists = (await _userRepository.FindAsync(u => u.Email == request.Email)).Any();

            if (userExists)
                throw new Exception("User already exists");

            var user = _mapper.Map<User>(request);
            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);
            user.Role = request.Role;

            await _userRepository.AddAsync(user);
            await _userRepository.SaveChangesAsync();

            var token = GenerateJwtToken(user);
            return new AuthResponse
            {
                Token = token,
                Email = user.Email,
                FullName = user.FullName
            };
        }

        private string GenerateJwtToken(User user) 
        {
            Console.WriteLine(_configuration["Jwt:Key"]);
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role)
            };

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddHours(3),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
