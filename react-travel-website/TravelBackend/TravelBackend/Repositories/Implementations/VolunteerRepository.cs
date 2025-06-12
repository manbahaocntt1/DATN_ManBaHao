using Microsoft.EntityFrameworkCore;
using TravelBackend.Data;
using TravelBackend.Entities;
using TravelBackend.Repositories;

namespace TravelBackend.Repositories.Implementations
{
    public class VolunteerRepository : Repository<Volunteer>, IVolunteerRepository
    {
        private readonly TravelDbContext _context;
        public VolunteerRepository(TravelDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Volunteer>> GetApprovedVolunteersAsync()
            => await _context.Volunteers.Where(v => v.IsApproved).ToListAsync();

        public async Task<IEnumerable<Volunteer>> SearchVolunteersAsync(string? language = null, string? university = null)
        {
            var query = _context.Volunteers.AsQueryable();
            if (!string.IsNullOrEmpty(language))
                query = query.Where(v => v.Languages != null && v.Languages.Contains(language));
            if (!string.IsNullOrEmpty(university))
                query = query.Where(v => v.University == university);
            return await query.ToListAsync();
        }


        public async Task ApproveVolunteerAsync(int volunteerId)
        {
            var volunteer = await _context.Volunteers.FindAsync(volunteerId);
            if (volunteer != null)
            {
                volunteer.IsApproved = true;
                _context.Volunteers.Update(volunteer);
                await _context.SaveChangesAsync();
            }
        }
    }
}
