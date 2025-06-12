using TravelBackend.Entities;

namespace TravelBackend.Repositories
{
    public interface ITourImageRepository : IRepository<TourImage>
    {
        // Get all images for a specific tour
        Task<IEnumerable<TourImage>> GetImagesByTourIdAsync(int tourId);

        // (Optional) Get a single image by its ID
        Task<TourImage?> GetImageByIdAsync(int imageId);

        // (Optional) Delete all images for a specific tour
        Task DeleteImagesByTourIdAsync(int tourId);
    }
}
