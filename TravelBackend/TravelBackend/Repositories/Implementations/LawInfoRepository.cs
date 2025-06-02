using Microsoft.EntityFrameworkCore;
using TravelBackend.Data;
using TravelBackend.Entities;
using TravelBackend.Repositories;

namespace TravelBackend.Repositories.Implementations
{
    public class LawInfoRepository : Repository<LawInfo>, ILawInfoRepository
    {
        private readonly TravelDbContext _context;
        public LawInfoRepository(TravelDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<LawInfo>> GetByCategoryAsync(string? category, string? lang = null)
        {
            var query = _context.LawInfo.AsQueryable();
            if (!string.IsNullOrEmpty(category))
                query = query.Where(l => l.Category == category);
            if (!string.IsNullOrEmpty(lang))
                query = query.Where(l => l.LangCode == lang);
            return await query.ToListAsync();
        }


    }
}
