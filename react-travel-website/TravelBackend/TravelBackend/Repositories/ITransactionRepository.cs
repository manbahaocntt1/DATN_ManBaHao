using TravelBackend.Entities;

namespace TravelBackend.Repositories
{
    public interface ITransactionRepository : IRepository<Transaction>
    {
        // Only methods specific to Transaction, not already covered by IRepository<T>
        Task<IEnumerable<Transaction>> GetByBookingIdAsync(int bookingId);
        // Add more if needed, e.g., search by status, user, etc.
        Task<Transaction?> GetByTransactionRefAsync(string transactionRef);
    }
}
