using TravelBackend.Entities;
using TravelBackend.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TravelBackend.Services
{
    public class JobService
    {
        private readonly IJobRepository _jobRepository;

        public JobService(IJobRepository jobRepository)
        {
            _jobRepository = jobRepository;
        }

        public async Task<IEnumerable<Job>> GetAllJobsAsync()
            => await _jobRepository.GetAllAsync();

        public async Task<IEnumerable<Job>> GetActiveJobsAsync(string? langCode = null)
            => await _jobRepository.GetActiveJobsAsync(langCode);

        public async Task<IEnumerable<Job>> GetJobsByUserIdAsync(int userId, string? langCode = null)
            => await _jobRepository.GetJobsByUserIdAsync(userId, langCode);

        public async Task<IEnumerable<Job>> SearchJobsAsync(string keyword, string? langCode = null)
            => await _jobRepository.SearchJobsAsync(keyword, langCode);

        public async Task UpdateJobStatusAsync(int jobId, bool isActive)
            => await _jobRepository.UpdateJobStatusAsync(jobId, isActive);
    }
}
