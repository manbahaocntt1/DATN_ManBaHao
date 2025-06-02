using TravelBackend.Entities;
using TravelBackend.Repositories;

namespace TravelBackend.Services
{
    public class VolunteerRequestService
    {
        private readonly IVolunteerRequestRepository _requestRepository;

        public VolunteerRequestService(IVolunteerRequestRepository requestRepository)
        {
            _requestRepository = requestRepository;
        }

        public async Task<IEnumerable<VolunteerRequest>> GetRequestsByUserIdAsync(int userId)
            => await _requestRepository.GetRequestsByUserIdAsync(userId);

        public async Task<IEnumerable<VolunteerRequest>> GetPendingRequestsAsync()
            => await _requestRepository.GetPendingRequestsAsync();

        public async Task UpdateRequestStatusAsync(int requestId, string status)
            => await _requestRepository.UpdateRequestStatusAsync(requestId, status);
    }
}
