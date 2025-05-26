using AutoMapper;
using FinancialBuddy.Application.DTOs.User;
using FinancialBuddy.Application.Interfaces.Repositories;
using FinancialBuddy.Domain.Entities;

namespace FinancialBuddy.Application.Interfaces.Services
{
    public class UserService : IUserService
    {
        private readonly IGenericRepository<User> _userRepository;
        private readonly IGenericRepository<UserAsset> _userAssetRepository;
        private readonly IGenericRepository<ValueAsset> _valueAssetRepository;
        private readonly ITransactionService _transactionService;
        private readonly IGenericRepository<Subscription> _subscriptionRepository;

        private readonly IMapper _mapper;

        public UserService(IGenericRepository<User> userRepository,
             IGenericRepository<UserAsset> userAssetRepository,
             IGenericRepository<ValueAsset> valueAssetRepository,
             ITransactionService transactionService,
             IGenericRepository<Subscription> subscriptionRepository,
            IMapper mapper)
        {
            _userRepository = userRepository;
            _userAssetRepository = userAssetRepository;
            _valueAssetRepository = valueAssetRepository;
            _transactionService = transactionService;
            _subscriptionRepository = subscriptionRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<UserDto>> GetAllUsersAsync()
        {
            var users = await _userRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<UserDto>>(users);
        }

        public async Task<UserDto> GetUserByIdAsync(Guid id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            return _mapper.Map<UserDto>(user);
        }

        public async Task<UserDto> CreateUserAsync(CreateUserRequest request)
        {
            var user = _mapper.Map<User>(request);
            await _userRepository.AddAsync(user);
            await _userRepository.SaveChangesAsync();
            return _mapper.Map<UserDto>(user);
        }

        public async Task UpdateUserAsync(UpdateUserRequest request)
        {
            var user = await _userRepository.GetByIdAsync(request.Id);

            if (user == null)
                throw new Exception("User not found.");

            if (!string.IsNullOrEmpty(request.Password))
                user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);

            _mapper.Map(request, user);

            await _userRepository.SaveChangesAsync();
        }

        public async Task DeleteUserAsync(Guid id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user != null)
            {
                _userRepository.Remove(user);
                await _userRepository.SaveChangesAsync();
            }
        }

        public async Task<UserDashboardGeneralInfoDto> GetDashboardInfo(Guid id)
        {
            var response = new UserDashboardGeneralInfoDto();
            var user = await _userRepository.GetByIdAsync(id);

            if (user == null)
                throw new Exception("User not found.");

            // otomatik ödeme sayısı
            var activeSubscriptions = await _subscriptionRepository.FindAsync(s => s.UserId == id && s.IsAutoPayment);
            response.autoPaymentCount = activeSubscriptions.Count();

            // yatirim tutari
            var userAssets = await _userAssetRepository.FindAsync(ua => ua.UserId == id);
            response.investmentAmount = userAssets.Sum(ua => ua.Quantity * ua.AveragePrice);

            // küsüratlı bakiye kazanci
            response.roundedBalance = user.SavingBalance;

            // hesap bakiyesi
            response.accountBalance = user.Balance;
            return response;
        }
    }
}
