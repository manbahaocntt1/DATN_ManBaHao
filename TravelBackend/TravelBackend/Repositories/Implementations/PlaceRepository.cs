using Microsoft.EntityFrameworkCore;
using TravelBackend.Data;
using TravelBackend.Entities;
using TravelBackend.Repositories.Implementations;

public class PlaceRepository : Repository<Place>, IPlaceRepository
{
    private readonly TravelDbContext _context;
    public PlaceRepository(TravelDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Place>> SearchPlacesAsync(string? keyword = null, string? category = null, string? langCode = null)
    {
        var query = _context.Places.AsQueryable();
        if (!string.IsNullOrEmpty(keyword))
            query = query.Where(p => p.Name.Contains(keyword) || (p.Description != null && p.Description.Contains(keyword)));
        if (!string.IsNullOrEmpty(category))
            query = query.Where(p => p.Category == category);
        if (!string.IsNullOrEmpty(langCode))
            query = query.Where(p => p.LangCode == langCode);
        return await query.ToListAsync();
    }

    public async Task<IEnumerable<Place>> GetAllAsync(string? langCode)
    {
        var query = _context.Places.AsQueryable();
        if (!string.IsNullOrEmpty(langCode))
            query = query.Where(p => p.LangCode == langCode);
        return await query.ToListAsync();
    }

    public async Task<Place?> GetByIdAsync(int id, string? langCode)
    {
        var query = _context.Places.AsQueryable();
        query = query.Where(p => p.PlaceId == id);
        if (!string.IsNullOrEmpty(langCode))
            query = query.Where(p => p.LangCode == langCode);
        return await query.FirstOrDefaultAsync();
    }
    // **NEW**
    public async Task<IEnumerable<Place>> GetPlacesByTourIdAsync(int tourId, string? langCode = null)
    {
        var query = _context.TourPlaces.Include(tp => tp.Place).Where(tp => tp.TourId == tourId);

        if (!string.IsNullOrEmpty(langCode))
            query = query.Where(tp => tp.Place.LangCode == langCode);

        return await query.Select(tp => tp.Place).ToListAsync();
    }
}
