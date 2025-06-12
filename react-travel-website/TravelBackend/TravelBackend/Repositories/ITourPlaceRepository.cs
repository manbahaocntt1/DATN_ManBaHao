using TravelBackend.Entities;

namespace TravelBackend.Repositories
{
    public interface ITourPlaceRepository : IRepository<TourPlace>
    {
        // Get all places for a specific tour
        Task<IEnumerable<TourPlace>> GetByTourIdAsync(int tourId);

        // Get all tours for a specific place
        Task<IEnumerable<TourPlace>> GetByPlaceIdAsync(int placeId);
    }
}
