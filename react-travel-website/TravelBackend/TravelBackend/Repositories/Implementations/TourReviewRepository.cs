using Microsoft.EntityFrameworkCore;
using TravelBackend.Data;
using TravelBackend.Entities;
using TravelBackend.Models.DTOs;
using TravelBackend.Repositories;

namespace TravelBackend.Repositories.Implementations
{
    public class TourReviewRepository : Repository<TourReview>, ITourReviewRepository
    {
        private readonly TravelDbContext _context;
        public TourReviewRepository(TravelDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<TourReviewsDto>> GetReviewsByTourIdAsync(int tourId)
        {
            return await _context.TourReviews
                .Where(r => r.TourId == tourId ) // only show approved reviews!
                .Include(r => r.User)
                .Select(r => new TourReviewsDto
                {
                    ReviewId = r.ReviewId,
                    TourId = r.TourId,
                    UserId = r.UserId,
                    UserFullName = r.User.FullName,
                    UserAvatarUrl = r.User.AvatarUrl ?? "https://localhost:7084/images/avatars/default.jpg",
                    Rating = r.Rating,
                    Comment = r.Comment,
                    
                    CreatedAt = r.CreatedAt
                })
                .OrderByDescending(r => r.CreatedAt)
                .ToListAsync();
        }

        public async Task<IEnumerable<TourReview>> GetReviewsByUserIdAsync(int userId)
            => await _context.TourReviews.Where(r => r.UserId == userId).ToListAsync();

        public async Task<double> GetAverageRatingForTourAsync(int tourId)
            => await _context.TourReviews.Where(r => r.TourId == tourId).AverageAsync(r => (double?)r.Rating) ?? 0;

        public async Task ModerateReviewAsync(int reviewId, bool isApproved)
        {
            var review = await _context.TourReviews.FindAsync(reviewId);
            if (review != null)
            {
                review.IsApproved = isApproved;
                _context.TourReviews.Update(review);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<TourReview>> GetAllReviewsAsync(int? tourId = null, int? userId = null, int? minRating = null, int? maxRating = null, DateTime? fromDate = null, DateTime? toDate = null)
        {
            var query = _context.TourReviews.AsQueryable();
            if (tourId.HasValue)
                query = query.Where(r => r.TourId == tourId.Value);
            if (userId.HasValue)
                query = query.Where(r => r.UserId == userId.Value);
            if (minRating.HasValue)
                query = query.Where(r => r.Rating >= minRating.Value);
            if (maxRating.HasValue)
                query = query.Where(r => r.Rating <= maxRating.Value);
            if (fromDate.HasValue)
                query = query.Where(r => r.CreatedAt >= fromDate.Value);
            if (toDate.HasValue)
                query = query.Where(r => r.CreatedAt <= toDate.Value);
            return await query.ToListAsync();
        }
    }
}
