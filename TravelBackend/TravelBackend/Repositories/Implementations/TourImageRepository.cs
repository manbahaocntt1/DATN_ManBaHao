using Microsoft.EntityFrameworkCore;
using TravelBackend.Data;
using TravelBackend.Entities;
using TravelBackend.Repositories;
using TravelBackend.Repositories.Implementations;
public class TourImageRepository : Repository<TourImage>, ITourImageRepository

{
    private readonly TravelDbContext _context;
    public TourImageRepository(TravelDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<IEnumerable<TourImage>> GetImagesByTourIdAsync(int tourId)
        => await _context.TourImages.Where(i => i.TourId == tourId).ToListAsync();

    public async Task<TourImage?> GetImageByIdAsync(int imageId)
        => await _context.TourImages.FindAsync(imageId);

    public async Task DeleteImagesByTourIdAsync(int tourId)
    {
        var images = _context.TourImages.Where(i => i.TourId == tourId);
        _context.TourImages.RemoveRange(images);
        await _context.SaveChangesAsync();
    }
}
