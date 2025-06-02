using Microsoft.EntityFrameworkCore;
using TravelBackend.Data;
using TravelBackend.Entities;
using TravelBackend.Repositories;

namespace TravelBackend.Repositories.Implementations
{
    public class TourPlaceRepository : Repository<TourPlace>, ITourPlaceRepository
    {
        private readonly TravelDbContext _context;
        public TourPlaceRepository(TravelDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TourPlace>> GetByTourIdAsync(int tourId)
            => await _context.TourPlaces.Where(tp => tp.TourId == tourId).ToListAsync();

        public async Task<IEnumerable<TourPlace>> GetByPlaceIdAsync(int placeId)
            => await _context.TourPlaces.Where(tp => tp.PlaceId == placeId).ToListAsync();
    }
}
