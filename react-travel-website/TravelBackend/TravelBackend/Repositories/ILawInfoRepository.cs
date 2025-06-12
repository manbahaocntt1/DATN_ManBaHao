using TravelBackend.Entities;

namespace TravelBackend.Repositories
{
    public interface ILawInfoRepository : IRepository<LawInfo>
    {
        // Get all law info by category and/or language
        Task<IEnumerable<LawInfo>> GetByCategoryAsync(string? category, string? lang = null);

        
    }
}
