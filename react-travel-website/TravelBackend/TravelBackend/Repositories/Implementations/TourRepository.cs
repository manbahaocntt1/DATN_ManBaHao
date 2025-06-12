using Microsoft.EntityFrameworkCore;
using TravelBackend.Data;
using TravelBackend.Entities;
using TravelBackend.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TravelBackend.Models.DTOs;

namespace TravelBackend.Repositories.Implementations
{
    public class TourRepository : Repository<Tour>, ITourRepository
    {
        private readonly TravelDbContext _context;

        public TourRepository(TravelDbContext context) : base(context)
        {
            _context = context;
        }

        // Search tours with filters
        public async Task<IEnumerable<Tour>> SearchToursAsync(
            string? keyword = null,
            string? location = null,
            string? type = null,
            decimal? minPrice = null,
            decimal? maxPrice = null,
            int? durationDays = null,
            string? lang = null) // Added lang
        {
            var query = _context.Tours.AsQueryable();

            if (!string.IsNullOrEmpty(keyword))
                query = query.Where(t => t.Title.Contains(keyword) || (t.Description != null && t.Description.Contains(keyword)));
            if (!string.IsNullOrEmpty(location))
                query = query.Where(t => t.Location == location);
            if (!string.IsNullOrEmpty(type))
                query = query.Where(t => t.Type == type);
            if (minPrice.HasValue)
                query = query.Where(t => t.PriceAdult >= minPrice.Value || t.PriceChild >= minPrice.Value);
            if (maxPrice.HasValue)
                query = query.Where(t => t.PriceAdult <= maxPrice.Value || t.PriceChild <= maxPrice.Value);
            if (durationDays.HasValue)
                query = query.Where(t => t.DurationDays == durationDays.Value);
            if (!string.IsNullOrEmpty(lang))
                query = query.Where(t => t.LangCode == lang); // Language filter

            return await query.ToListAsync();
        }

        // Get full tour details
        public async Task<Tour?> GetTourDetailsAsync(int tourId)
        {
            return await _context.Tours
                .Include(t => t.Images)
                .Include(t => t.Reviews)
                .Include(t => t.Availabilities)
                .Include(t => t.Bookings)
                .Include(t => t.TourPlaces)
                    .ThenInclude(tp => tp.Place)
                .FirstOrDefaultAsync(t => t.TourId == tourId);
        }

        // Personalized tour suggestions (simple version: by user's preferred location or type)
        public async Task<IEnumerable<Tour>> GetPersonalizedToursAsync(int userId, int maxResults = 10, string? lang = null) // Added lang
        {
            var user = await _context.Users.FindAsync(userId);
            if (user == null)
                return new List<Tour>();

            var query = _context.Tours.AsQueryable();

            if (!string.IsNullOrEmpty(user.Nationality))
                query = query.Where(t => t.Location == user.Nationality);

            if (!string.IsNullOrEmpty(lang))
                query = query.Where(t => t.LangCode == lang); // Language filter

            // You can add more personalization logic here (e.g., based on user behavior)

            return await query.Take(maxResults).ToListAsync();
        }

        // Admin: get statistics for a tour
        public async Task<TourStatsDto> GetTourStatsAsync(int tourId)
        {
            var tour = await _context.Tours
                .Include(t => t.Bookings)
                .Include(t => t.Reviews)
                .FirstOrDefaultAsync(t => t.TourId == tourId);

            if (tour == null)
                return new TourStatsDto();

            return new TourStatsDto
            {
                TourId = tour.TourId,
                TotalBookings = tour.Bookings.Count,
                AverageRating = tour.Reviews.Any() ? tour.Reviews.Average(r => r.Rating) : 0,
                TotalViews = 0 // Implement view tracking if needed
            };
        }

        // Admin: get all tours, optionally filter by active status
        public async Task<IEnumerable<Tour>> GetAllToursAsync(bool? isActive = null, string? lang = null) // Added lang
        {
            var query = _context.Tours.AsQueryable();
            if (isActive.HasValue)
                query = query.Where(t => t.IsActive == isActive.Value);
            if (!string.IsNullOrEmpty(lang))
                query = query.Where(t => t.LangCode == lang); // Language filter
            return await query.ToListAsync();
        }

        // Admin: activate/deactivate a tour
        public async Task ActivateTourAsync(int tourId)
        {
            var tour = await _context.Tours.FindAsync(tourId);
            if (tour != null)
            {
                tour.IsActive = true;
                _context.Tours.Update(tour);
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeactivateTourAsync(int tourId)
        {
            var tour = await _context.Tours.FindAsync(tourId);
            if (tour != null)
            {
                tour.IsActive = false;
                _context.Tours.Update(tour);
                await _context.SaveChangesAsync();
            }
        }
        public async Task<PagedResult<Tour>> GetAllToursPagedAsync(bool? isActive, string? lang, int page, int pageSize)
        {
            var query = _context.Tours.Include(t => t.Images).AsQueryable();
            if (isActive.HasValue)
                query = query.Where(t => t.IsActive == isActive.Value);
            if (!string.IsNullOrEmpty(lang))
                query = query.Where(t => t.LangCode == lang);

            var totalCount = await query.CountAsync();
            var totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);
            var tours = await query
                .OrderByDescending(t => t.CreatedAt)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PagedResult<Tour>
            {
                Items = tours,
                TotalCount = totalCount,
                TotalPages = totalPages,
                CurrentPage = page
            };
        }


        public async Task<PagedResult<Tour>> SearchToursPagedAsync(
    string? keyword, string? location, string? type, decimal? minPrice, decimal? maxPrice, int? durationDays, string? lang, int page, int pageSize)
        {
            var query = _context.Tours.Include(t => t.Images).AsQueryable();

            if (!string.IsNullOrEmpty(keyword))
                query = query.Where(t => t.Title.Contains(keyword) || (t.Description != null && t.Description.Contains(keyword)));
            if (!string.IsNullOrEmpty(location))
                query = query.Where(t => t.Location == location);
            if (!string.IsNullOrEmpty(type))
                query = query.Where(t => t.Type == type);
            if (minPrice.HasValue)
                query = query.Where(t => t.PriceAdult >= minPrice.Value || t.PriceChild >= minPrice.Value);
            if (maxPrice.HasValue)
                query = query.Where(t => t.PriceAdult <= maxPrice.Value || t.PriceChild <= maxPrice.Value);
            if (durationDays.HasValue)
                query = query.Where(t => t.DurationDays == durationDays.Value);
            if (!string.IsNullOrEmpty(lang))
                query = query.Where(t => t.LangCode == lang);

            var totalCount = await query.CountAsync();
            var totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);
            var tours = await query
                .OrderByDescending(t => t.CreatedAt)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PagedResult<Tour>
            {
                Items = tours,
                TotalCount = totalCount,
                TotalPages = totalPages,
                CurrentPage = page
            };
        }
        public async Task<List<HomeTourDto>> GetNewestToursAsync(int count)
        {
            return await _context.Tours
                .Include(t => t.Images)
                .OrderByDescending(t => t.CreatedAt)
                .Take(count)
                .Select(t => new HomeTourDto
                {
                    TourId = t.TourId,
                    Title = t.Title,
                    Description = t.Description,
                    Location = t.Location,
                    Type = t.Type,
                    PriceAdult = t.PriceAdult,
                    PriceChild = t.PriceChild,
                    DurationDays = t.DurationDays,
                    CreatedAt = t.CreatedAt,
                    ImageUrl = t.Images.FirstOrDefault().ImageUrl, // null if no image
                    AverageRating = t.Reviews.Any() ? t.Reviews.Average(r => r.Rating) : 0,
                    TotalReviews = t.Reviews.Count(r => r.IsApproved)
                })
                .ToListAsync();
        }

        public async Task<List<HomeTourDto>> GetTopRatingToursAsync(int count)
        {
            // Only tours with at least 1 approved review, ordered by average rating
            return await _context.Tours
                .Include(t => t.Images)
                .Where(t => t.Reviews.Any())
                .Select(t => new HomeTourDto
                {
                    TourId = t.TourId,
                    Title = t.Title,
                    Description = t.Description,
                    Location = t.Location,
                    Type = t.Type,
                    PriceAdult = t.PriceAdult,
                    PriceChild = t.PriceChild,
                    DurationDays = t.DurationDays,
                    CreatedAt = t.CreatedAt,
                    ImageUrl = t.Images.FirstOrDefault().ImageUrl,
                    AverageRating = t.Reviews.Average(r => r.Rating),
                    TotalReviews = t.Reviews.Count(r => r.IsApproved)
                })
                .OrderByDescending(t => t.AverageRating)
                .ThenByDescending(t => t.TotalReviews) // Optional: secondary sort by review count
                .Take(count)
                .ToListAsync();
        }



    }
}
