using AutoMapper;
using TravelBackend.Entities;
using TravelBackend.Models.DTOs;
using TravelBackend.Repositories;

namespace TravelBackend.Services
{
    public class TransactionService 
    {
        private readonly ITransactionRepository _transactionRepo;
        private readonly IMapper _mapper; // Use AutoMapper for DTO conversion

        public TransactionService(ITransactionRepository transactionRepo, IMapper mapper)
        {
            _transactionRepo = transactionRepo;
            _mapper = mapper;
        }

        public async Task<IEnumerable<TransactionDto>> GetTransactionsByBookingIdAsync(int bookingId)
        {
            var list = await _transactionRepo.GetByBookingIdAsync(bookingId);
            return _mapper.Map<IEnumerable<TransactionDto>>(list);
        }

        public async Task<TransactionDto?> GetByIdAsync(int transactionId)
        {
            var entity = await _transactionRepo.GetByIdAsync(transactionId);
            return entity == null ? null : _mapper.Map<TransactionDto>(entity);
        }

        public async Task<TransactionDto> AddTransactionAsync(CreateTransactionDto dto)
        {
            var entity = _mapper.Map<Transaction>(dto);
            await _transactionRepo.AddAsync(entity);
            await _transactionRepo.SaveChangesAsync();
            return _mapper.Map<TransactionDto>(entity);
        }
    }

}
