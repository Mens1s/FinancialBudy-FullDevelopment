using FinancialBuddy.Application.DTOs.Transfer;

namespace FinancialBuddy.Application.Interfaces.Services
{
    public interface ITransferService
    {
        Task<IEnumerable<TransferDto>> GetAllTransfersAsync(Guid userId);
        Task<TransferDto> GetTransferByIdAsync(Guid id);
        Task<TransferDto> CreateTransferAsync(CreateTransferRequest request);
        Task UpdateTransferAsync(UpdateTransferRequest request);
        Task DeleteTransferAsync(Guid id);
    }
}
