using AutoMapper;
using FinancialBuddy.Application.DTOs.Transfer;
using FinancialBuddy.Application.Interfaces.Repositories;
using FinancialBuddy.Domain.Entities;

namespace FinancialBuddy.Application.Interfaces.Services
{
    public class TransferService : ITransferService
    {
        private readonly IGenericRepository<Transfer> _transferRepository;
        private readonly IMapper _mapper;

        public TransferService(IGenericRepository<Transfer> transferRepository, IMapper mapper)
        {
            _transferRepository = transferRepository;
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
            var transfer = _mapper.Map<Transfer>(request);
            await _transferRepository.AddAsync(transfer);
            await _transferRepository.SaveChangesAsync();
            return _mapper.Map<TransferDto>(transfer);
        }

        public async Task UpdateTransferAsync(UpdateTransferRequest request)
        {
            var transfer = await _transferRepository.GetByIdAsync(request.Id);
            if (transfer != null)
            {
                transfer.FromAccount = request.FromAccount;
                transfer.ToAccount = request.ToAccount;
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
