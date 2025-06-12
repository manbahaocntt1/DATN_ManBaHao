using TravelBackend.Entities;
using TravelBackend.Repositories;

namespace TravelBackend.Services
{
    public class VolunteerService
    {
        private readonly IVolunteerRepository _volunteerRepository;

        public VolunteerService(IVolunteerRepository volunteerRepository)
        {
            _volunteerRepository = volunteerRepository;
        }
        public async Task<IEnumerable<Volunteer>> GetAllVolunteersAsync()
        {
            return await _volunteerRepository.GetAllAsync();
        }

        public async Task<IEnumerable<Volunteer>> GetApprovedVolunteersAsync()
            => await _volunteerRepository.GetApprovedVolunteersAsync();

        public async Task<IEnumerable<Volunteer>> SearchVolunteersAsync(string? language = null, string? university = null)
            => await _volunteerRepository.SearchVolunteersAsync(language, university);

        public async Task ApproveVolunteerAsync(int volunteerId)
            => await _volunteerRepository.ApproveVolunteerAsync(volunteerId);
    }
}
