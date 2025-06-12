using TravelBackend.Entities;
using TravelBackend.Models.DTOs;
using TravelBackend.Repositories;

namespace TravelBackend.Services
{
    public class TourReviewService
    {
        private readonly ITourReviewRepository _reviewRepository;

        public TourReviewService(ITourReviewRepository reviewRepository)
        {
            _reviewRepository = reviewRepository;
        }
        public async Task AddReviewAsync(TourReview review)
        {
            await _reviewRepository.AddAsync(review);
            await _reviewRepository.SaveChangesAsync();
        }

        public async Task<IEnumerable<TourReviewsDto>> GetReviewsByTourIdAsync(int tourId)
            => await _reviewRepository.GetReviewsByTourIdAsync(tourId);

        public async Task<IEnumerable<TourReview>> GetReviewsByUserIdAsync(int userId)
            => await _reviewRepository.GetReviewsByUserIdAsync(userId);

        public async Task<double> GetAverageRatingForTourAsync(int tourId)
            => await _reviewRepository.GetAverageRatingForTourAsync(tourId);

        public async Task<IEnumerable<TourReview>> GetAllReviewsAsync(int? tourId = null, int? userId = null, int? minRating = null, int? maxRating = null, DateTime? fromDate = null, DateTime? toDate = null)
            => await _reviewRepository.GetAllReviewsAsync(tourId, userId, minRating, maxRating, fromDate, toDate);

        public async Task ModerateReviewAsync(int reviewId, bool isApproved)
    => await _reviewRepository.ModerateReviewAsync(reviewId, isApproved);

    }
}
