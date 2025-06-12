using TravelBackend.Entities;
using TravelBackend.Repositories;

namespace TravelBackend.Services
{
    public class UserBehaviorService
    {
        private readonly IUserBehaviorRepository _behaviorRepository;

        public UserBehaviorService(IUserBehaviorRepository behaviorRepository)
        {
            _behaviorRepository = behaviorRepository;
        }

        public async Task<IEnumerable<UserBehavior>> GetByUserIdAsync(int userId)
            => await _behaviorRepository.GetByUserIdAsync(userId);

        public async Task<IEnumerable<string>> GetMostCommonActionsAsync(int top = 10)
            => await _behaviorRepository.GetMostCommonActionsAsync(top);

        public async Task<IEnumerable<string>> GetMostSearchedKeywordsAsync(int top = 10)
            => await _behaviorRepository.GetMostSearchedKeywordsAsync(top);

        public async Task<IEnumerable<int>> GetFavoriteTourIdsAsync(int userId, int top = 10)
            => await _behaviorRepository.GetFavoriteTourIdsAsync(userId, top);

        public async Task<IEnumerable<int>> GetFavoritePlaceIdsAsync(int userId, int top = 10)
            => await _behaviorRepository.GetFavoritePlaceIdsAsync(userId, top);
    }
}
