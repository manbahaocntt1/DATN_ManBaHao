using Microsoft.EntityFrameworkCore;
using TravelBackend.Data;
using TravelBackend.Entities;
using TravelBackend.Repositories;

namespace TravelBackend.Repositories.Implementations
{
    public class UserBehaviorRepository : Repository<UserBehavior>, IUserBehaviorRepository
    {
        private readonly TravelDbContext _context;
        public UserBehaviorRepository(TravelDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<UserBehavior>> GetByUserIdAsync(int userId)
            => await _context.UserBehavior.Where(b => b.UserId == userId).ToListAsync();

        public async Task<IEnumerable<string>> GetMostCommonActionsAsync(int top = 10)
            => await _context.UserBehavior
                .GroupBy(b => b.ActionType)
                .OrderByDescending(g => g.Count())
                .Take(top)
                .Select(g => g.Key)
                .ToListAsync();

        public async Task<IEnumerable<string>> GetMostSearchedKeywordsAsync(int top = 10)
     => await _context.UserBehavior
         .Where(b => b.ActionType == "search" && b.ActionDetail != null)
         .GroupBy(b => b.ActionDetail)
         .OrderByDescending(g => g.Count())
         .Take(top)
         .Select(g => g.Key!)
         .ToListAsync();


        public async Task<IEnumerable<int>> GetFavoriteTourIdsAsync(int userId, int top = 10)
     => await _context.UserBehavior
         .Where(b => b.UserId == userId && b.ActionType == "view_tour" && b.ActionDetail != null)
         .GroupBy(b => b.ActionDetail)
         .OrderByDescending(g => g.Count())
         .Take(top)
         .Select(g => int.Parse(g.Key!))
         .ToListAsync();


        public async Task<IEnumerable<int>> GetFavoritePlaceIdsAsync(int userId, int top = 10)
     => await _context.UserBehavior
         .Where(b => b.UserId == userId && b.ActionType == "view_place" && b.ActionDetail != null)
         .GroupBy(b => b.ActionDetail)
         .OrderByDescending(g => g.Count())
         .Take(top)
         .Select(g => int.Parse(g.Key!))
         .ToListAsync();

    }
}
