using TravelBackend.Entities;

namespace TravelBackend.Repositories
{
    public interface IUserBehaviorRepository : IRepository<UserBehavior>
    {
        // Log a new user behavior (inherited from IRepository)

        // Get all behaviors for a specific user
        Task<IEnumerable<UserBehavior>> GetByUserIdAsync(int userId);

        // Get most common actions (for analytics)
        Task<IEnumerable<string>> GetMostCommonActionsAsync(int top = 10);

        // Get most searched keywords (for analytics)
        Task<IEnumerable<string>> GetMostSearchedKeywordsAsync(int top = 10);

        // Get favorite tours/places for a user
        Task<IEnumerable<int>> GetFavoriteTourIdsAsync(int userId, int top = 10);
        Task<IEnumerable<int>> GetFavoritePlaceIdsAsync(int userId, int top = 10);
    }
}
