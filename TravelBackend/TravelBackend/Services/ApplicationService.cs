using TravelBackend.Entities;
using TravelBackend.Repositories;

namespace TravelBackend.Services
{
    public class ApplicationService
    {
        private readonly IApplicationRepository _applicationRepository;

        public ApplicationService(IApplicationRepository applicationRepository)
        {
            _applicationRepository = applicationRepository;
        }

        public async Task<IEnumerable<Appli>> GetApplicationsByUserIdAsync(int userId)
            => await _applicationRepository.GetApplicationsByUserIdAsync(userId);

        public async Task<IEnumerable<Appli>> GetApplicationsByJobIdAsync(int jobId)
            => await _applicationRepository.GetApplicationsByJobIdAsync(jobId);

        public async Task UpdateApplicationStatusAsync(int applicationId, string status)
            => await _applicationRepository.UpdateApplicationStatusAsync(applicationId, status);
    }
}
