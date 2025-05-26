using AutoMapper;
using FinancialBuddy.Application.DTOs.Transaction;
using FinancialBuddy.Application.DTOs.Transfer;
using FinancialBuddy.Application.Interfaces.Repositories;
using FinancialBuddy.Domain.Entities;

namespace FinancialBuddy.Application.Interfaces.Services
{
    public class TransferService : ITransferService
    {
        private readonly IGenericRepository<Transfer> _transferRepository;
        private readonly IGenericRepository<User> _userRepository;
        private readonly ITransactionService _transactionService;
        private readonly IMapper _mapper;

        public TransferService(IGenericRepository<Transfer> transferRepository, IGenericRepository<User> userRepository,
              ITransactionService transactionService, IMapper mapper)
        {
            _transferRepository = transferRepository;
            _userRepository = userRepository;
            _transactionService = transactionService;
            _mapper = mapper;
        }

        public async Task<IEnumerable<TransferDto>> GetAllTransfersAsync(Guid userId)
        {
            var transfers = await _transferRepository.FindAsync(t => t.UserId == userId);
            return _mapper.Map<IEnumerable<TransferDto>>(transfers);
        }

        public async Task<TransferDto> GetTransferByIdAsync(Guid id)
        {
            var transfer = await _transferRepository.GetByIdAsync(id);
            return _mapper.Map<TransferDto>(transfer);
        }        
        public async Task<TransferDto> CreateTransferAsync(CreateTransferRequest request)
        {
            var sender = await _userRepository.GetByIdAsync(request.UserId);
            var receiver = await _userRepository.GetByIdAsync(request.ReceiverUserId);

            if (sender == null || receiver == null)
                throw new Exception("Sender or receiver not found.");

            if (request.Amount <= 0)
                throw new Exception("Invalid amount.");

            if (sender.Balance < request.Amount)
                throw new Exception("Insufficient balance.");

            if(request.IsFast && sender.Balance < (request.Amount * 1.02m))
                throw new Exception("Insufficient balance. Fast Operations takes commision.");

            var transfer = _mapper.Map<Transfer>(request);

            if (request.IsFast)
            {
                sender.Balance -= request.Amount * 1.02m;
                receiver.Balance += request.Amount;
                transfer.IsCompleted = true;
            }
            else
            {
                transfer.IsCompleted = false;
            }

            await _transferRepository.AddAsync(transfer);
            _userRepository.Update(sender);
            _userRepository.Update(receiver);

            _transactionService.CreateTransactionAsync(new CreateTransactionRequest
            {
                UserId = sender.Id,
                Amount = request.Amount * (request.IsFast ? 1.02m : 1),
                Description = $"Transfer to {receiver.Email}",
                Category = "Transfer",
                Date = DateTime.UtcNow
            }).GetAwaiter().GetResult();

            _transactionService.CreateTransactionAsync(new CreateTransactionRequest
            {
                UserId = receiver.Id,
                Amount = request.Amount,
                Description = $"Received transfer from {sender.Email}",
                Category = "Transfer",
                Date = DateTime.UtcNow
            }).GetAwaiter().GetResult();

            await _transferRepository.SaveChangesAsync();

            return _mapper.Map<TransferDto>(transfer);
        }

        public async Task UpdateTransferAsync(UpdateTransferRequest request)
        {
            var transfer = await _transferRepository.GetByIdAsync(request.Id);
            if (transfer != null)
            {
                transfer.Amount = request.Amount;
                transfer.Date = request.Date;
                transfer.Description = request.Description;

                _transferRepository.Update(transfer);
                await _transferRepository.SaveChangesAsync();
            }
        }

        public async Task DeleteTransferAsync(Guid id)
        {
            var transfer = await _transferRepository.GetByIdAsync(id);
            if (transfer != null)
            {
                _transferRepository.Remove(transfer);
                await _transferRepository.SaveChangesAsync();
            }
        }
    }
}
