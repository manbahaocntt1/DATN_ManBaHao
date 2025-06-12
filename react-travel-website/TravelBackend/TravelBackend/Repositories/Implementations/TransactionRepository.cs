using Microsoft.EntityFrameworkCore;
using TravelBackend.Data;
using TravelBackend.Entities; // if Transaction is here

namespace TravelBackend.Repositories.Implementations
{
    public class TransactionRepository : Repository<Transaction>, ITransactionRepository
    {
        private readonly new TravelDbContext _context;
        public TransactionRepository(TravelDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Transaction>> GetByBookingIdAsync(int bookingId)
            => await _context.Transactions.Where(t => t.BookingId == bookingId).ToListAsync();
        public async Task<Transaction?> GetByTransactionRefAsync(string transactionRef)
        {
            return await _context.Transactions
                .FirstOrDefaultAsync(t => t.TransactionRef == transactionRef);
        }

    }
}
