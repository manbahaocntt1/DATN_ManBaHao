using TravelBackend.Entities;
using TravelBackend.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;
using TravelBackend.Models.DTOs;

namespace TravelBackend.Services
{
    public class TourService
    {
        private readonly ITourRepository _tourRepository;

        public TourService(ITourRepository tourRepository)
        {
            _tourRepository = tourRepository;
        }

        public async Task<IEnumerable<Tour>> SearchToursAsync(
            string? keyword,
            string? location,
            string? type,
            decimal? minPrice,
            decimal? maxPrice,
            int? durationDays,
            string? lang = null) // Added lang
        {
            return await _tourRepository.SearchToursAsync(keyword, location, type, minPrice, maxPrice, durationDays, lang);
        }

        public async Task<Tour?> GetTourDetailsAsync(int tourId)
        {
            return await _tourRepository.GetTourDetailsAsync(tourId);
        }

        public async Task<IEnumerable<Tour>> GetPersonalizedToursAsync(int userId, int maxResults = 10, string? lang = null) // Added lang
        {
            return await _tourRepository.GetPersonalizedToursAsync(userId, maxResults, lang);
        }

        public async Task<TourStatsDto> GetTourStatsAsync(int tourId)
        {
            return await _tourRepository.GetTourStatsAsync(tourId);
        }

        public async Task<IEnumerable<Tour>> GetAllToursAsync(bool? isActive = null, string? lang = null) // Added lang
        {
            return await _tourRepository.GetAllToursAsync(isActive, lang);
        }

        public async Task ActivateTourAsync(int tourId)
        {
            await _tourRepository.ActivateTourAsync(tourId);
        }

        public async Task DeactivateTourAsync(int tourId)
        {
            await _tourRepository.DeactivateTourAsync(tourId);
        }

        // In Service Layer
        public async Task<PagedResult<Tour>> GetAllToursPagedAsync(bool? isActive, string? lang, int page, int pageSize)
        {
            return await _tourRepository.GetAllToursPagedAsync(isActive, lang, page, pageSize);
        }

        public async Task<PagedResult<Tour>> SearchToursPagedAsync(
    string? keyword, string? location, string? type, decimal? minPrice, decimal? maxPrice, int? durationDays, string? langCode, int page, int pageSize)
        {
            return await _tourRepository.SearchToursPagedAsync(keyword, location, type, minPrice, maxPrice, durationDays, langCode, page, pageSize);
        }




    }
}
