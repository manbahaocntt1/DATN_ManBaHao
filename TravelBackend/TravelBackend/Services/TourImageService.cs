using TravelBackend.Entities;
using TravelBackend.Repositories;
public class TourImageService
{
    private readonly ITourImageRepository _imageRepository;
    public TourImageService(ITourImageRepository imageRepository)
    {
        _imageRepository = imageRepository;
    }

    public async Task<IEnumerable<TourImage>> GetImagesByTourIdAsync(int tourId)
        => await _imageRepository.GetImagesByTourIdAsync(tourId);

    public async Task<TourImage?> GetImageByIdAsync(int imageId)
        => await _imageRepository.GetImageByIdAsync(imageId);

    public async Task DeleteImagesByTourIdAsync(int tourId)
        => await _imageRepository.DeleteImagesByTourIdAsync(tourId);
}
