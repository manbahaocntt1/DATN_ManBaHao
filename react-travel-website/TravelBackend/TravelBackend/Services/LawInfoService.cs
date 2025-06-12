using TravelBackend.Entities;
using TravelBackend.Repositories;

namespace TravelBackend.Services
{
    public class LawInfoService
    {
        private readonly ILawInfoRepository _lawInfoRepository;

        public LawInfoService(ILawInfoRepository lawInfoRepository)
        {
            _lawInfoRepository = lawInfoRepository;
        }

        public async Task<IEnumerable<LawInfo>> GetByCategoryAsync(string? category, string? lang = null)
            => await _lawInfoRepository.GetByCategoryAsync(category, lang);

        public async Task<LawInfo?> GetByIdAsync(int lawId)
            => await _lawInfoRepository.GetByIdAsync(lawId);
    }
}
