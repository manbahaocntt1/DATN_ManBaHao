using TravelBackend.Entities;
using TravelBackend.Repositories;

public interface IPlaceRepository : IRepository<Place>
{
    // Search places by keyword and/or category
    Task<IEnumerable<Place>> SearchPlacesAsync(string? keyword = null, string? category = null, string? langCode = null);

    // Get all places filtered by language
    Task<IEnumerable<Place>> GetAllAsync(string? langCode);

    // Get place by id and language
    Task<Place?> GetByIdAsync(int id, string? langCode);
    // **NEW**
    Task<IEnumerable<Place>> GetPlacesByTourIdAsync(int tourId, string? langCode = null);
}
