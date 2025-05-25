using AutoMapper;
using FinancialBuddy.Application.DTOs.Transaction;
using FinancialBuddy.Application.Interfaces.Repositories;
using FinancialBuddy.Domain.Entities;

namespace FinancialBuddy.Application.Interfaces.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly IGenericRepository<Transaction> _transactionRepository;
        private readonly IMapper _mapper;

        public TransactionService(IGenericRepository<Transaction> transactionRepository, IMapper mapper)
        {
            _transactionRepository = transactionRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<TransactionDto>> GetAllTransactionsAsync(Guid userId)
        {
            var transactions = await _transactionRepository.FindAsync(t => t.UserId == userId);
            return _mapper.Map<IEnumerable<TransactionDto>>(transactions);
        }

        public async Task<TransactionDto> GetTransactionByIdAsync(Guid id)
        {
            var transaction = await _transactionRepository.GetByIdAsync(id);
            return _mapper.Map<TransactionDto>(transaction);
        }

        public async Task<TransactionDto> CreateTransactionAsync(CreateTransactionRequest request)
        {
            var transaction = _mapper.Map<Transaction>(request);
            await _transactionRepository.AddAsync(transaction);
            await _transactionRepository.SaveChangesAsync();
            return _mapper.Map<TransactionDto>(transaction);
        }

        public async Task UpdateTransactionAsync(UpdateTransactionRequest request)
        {
            var transaction = await _transactionRepository.GetByIdAsync(request.Id);
            if (transaction != null)
            {
                transaction.Category = request.Category;
                transaction.Amount = request.Amount;
                transaction.Date = request.Date;
                transaction.Description = request.Description;

                _transactionRepository.Update(transaction);
                await _transactionRepository.SaveChangesAsync();
            }
        }

        public async Task DeleteTransactionAsync(Guid id)
        {
            var transaction = await _transactionRepository.GetByIdAsync(id);
            if (transaction != null)
            {
                _transactionRepository.Remove(transaction);
                await _transactionRepository.SaveChangesAsync();
            }
        }
    }
}
