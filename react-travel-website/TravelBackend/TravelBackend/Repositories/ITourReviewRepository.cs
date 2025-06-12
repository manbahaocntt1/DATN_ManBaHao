using TravelBackend.Entities;
using TravelBackend.Models.DTOs;

namespace TravelBackend.Repositories
{
    public interface ITourReviewRepository : IRepository<TourReview>
    {
        // Add a new review (inherited from IRepository)

        // Get all reviews for a specific tour
        Task<List<TourReviewsDto>> GetReviewsByTourIdAsync(int tourId);

        // Get all reviews by a specific user
        Task<IEnumerable<TourReview>> GetReviewsByUserIdAsync(int userId);

        // Get average rating for a tour
        Task<double> GetAverageRatingForTourAsync(int tourId);

        // (Optional) Admin: moderate (approve/remove) a review
        Task ModerateReviewAsync(int reviewId, bool isApproved);

        // (Optional) Get all reviews with filters (for admin)
        Task<IEnumerable<TourReview>> GetAllReviewsAsync(
            int? tourId = null,
            int? userId = null,
            int? minRating = null,
            int? maxRating = null,
            DateTime? fromDate = null,
            DateTime? toDate = null
        );
    }
}
