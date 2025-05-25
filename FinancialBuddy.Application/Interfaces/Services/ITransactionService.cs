using FinancialBuddy.Application.DTOs.Transaction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialBuddy.Application.Interfaces.Services
{
    public interface ITransactionService
    {
        Task<IEnumerable<TransactionDto>> GetAllTransactionsAsync(Guid userId);
        Task<TransactionDto> GetTransactionByIdAsync(Guid id);
        Task<TransactionDto> CreateTransactionAsync(CreateTransactionRequest request);
        Task UpdateTransactionAsync(UpdateTransactionRequest request);
        Task DeleteTransactionAsync(Guid id);
    }
}
