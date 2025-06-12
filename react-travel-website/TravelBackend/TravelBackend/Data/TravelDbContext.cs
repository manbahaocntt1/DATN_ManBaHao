using Microsoft.EntityFrameworkCore;
using TravelBackend.Entities;
using static System.Net.Mime.MediaTypeNames;

namespace TravelBackend.Data
{
    public class TravelDbContext : DbContext
    {
        public TravelDbContext(DbContextOptions<TravelDbContext> options)
            : base(options)
        {
        }

        public DbSet<Users> Users { get; set; }
        public DbSet<Tour> Tours { get; set; }
        public DbSet<TourImage> TourImages { get; set; }
        public DbSet<TourReview> TourReviews { get; set; }
        public DbSet<TourAvailability> TourAvailability { get; set; }
        public DbSet<UserBehavior> UserBehavior { get; set; }
        public DbSet<TourBooking> TourBookings { get; set; }
        public DbSet<VolunteerRequest> VolunteerRequests { get; set; }
        public DbSet<LawInfo> LawInfo { get; set; }
        public DbSet<Place> Places { get; set; }
        public DbSet<Volunteer> Volunteers { get; set; }
        public DbSet<Job> Jobs { get; set; }
        public DbSet<Appli> Applications { get; set; }
        public DbSet<TourPlace> TourPlaces { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<VolunteerProfile> VolunteerProfiles { get; set; }
        
        public DbSet<VolunteerRequestAssignment> VolunteerRequestAssignments { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Users
            modelBuilder.Entity<Users>(entity =>
            {
                entity.HasKey(e => e.UserId);
                entity.HasIndex(e => e.Email).IsUnique();
                entity.Property(e => e.UserRole)
                      .HasConversion<string>();
            });

            // Tours
            modelBuilder.Entity<Tour>(entity =>
            {
                entity.HasKey(e => e.TourId);
                entity.HasOne(e => e.Creator)
                      .WithMany(u => u.CreatedTours)
                      .HasForeignKey(e => e.CreatedBy)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            // TourImages
            modelBuilder.Entity<TourImage>(entity =>
            {
                entity.HasKey(e => e.ImageId);
                entity.HasOne(e => e.Tour)
                      .WithMany(t => t.Images)
                      .HasForeignKey(e => e.TourId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            // TourReviews
            modelBuilder.Entity<TourReview>(entity =>
            {
                entity.HasKey(e => e.ReviewId);
                entity.HasOne(e => e.Tour)
                      .WithMany(t => t.Reviews)
                      .HasForeignKey(e => e.TourId)
                      .OnDelete(DeleteBehavior.Cascade);
                entity.HasOne(e => e.User)
                      .WithMany(u => u.Reviews)
                      .HasForeignKey(e => e.UserId)
                      .OnDelete(DeleteBehavior.NoAction);
            });

            // TourAvailability
            modelBuilder.Entity<TourAvailability>(entity =>
            {
                entity.HasKey(e => e.AvailabilityId);
                entity.HasOne(e => e.Tour)
                      .WithMany(t => t.Availabilities)
                      .HasForeignKey(e => e.TourId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            // UserBehavior
            modelBuilder.Entity<UserBehavior>(entity =>
            {
                entity.HasKey(e => e.BehaviorId);
                entity.HasOne(e => e.User)
                      .WithMany(u => u.Behaviors)
                      .HasForeignKey(e => e.UserId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            // TourBookings
            modelBuilder.Entity<TourBooking>(entity =>
            {
                entity.HasKey(e => e.BookingId);
                entity.HasOne(e => e.User)
                      .WithMany(u => u.Bookings)
                      .HasForeignKey(e => e.UserId)
                      .OnDelete(DeleteBehavior.NoAction);
                entity.HasOne(e => e.Tour)
                      .WithMany(t => t.Bookings)
                      .HasForeignKey(e => e.TourId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            // VolunteerRequests
            modelBuilder.Entity<VolunteerRequest>(entity =>
            {
                entity.HasKey(e => e.RequestId);
                entity.HasOne(e => e.User)
                      .WithMany(u => u.VolunteerRequests)
                      .HasForeignKey(e => e.UserId)
                      .OnDelete(DeleteBehavior.Cascade);
            });


            // LawInfo
            modelBuilder.Entity<LawInfo>(entity =>
            {
                entity.HasKey(e => e.LawId);
                entity.HasOne(e => e.AddedByUser)
                      .WithMany(u => u.AddedLaws)
                      .HasForeignKey(e => e.AddedBy)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            // Places
            modelBuilder.Entity<Place>(entity =>
            {
                entity.HasKey(e => e.PlaceId);
            });

            // Volunteers
            modelBuilder.Entity<Volunteer>(entity =>
            {
                entity.HasKey(e => e.VolunteerId);
            });

            // Jobs
            modelBuilder.Entity<Job>(entity =>
            {
                entity.HasKey(e => e.JobId);
                entity.HasOne(e => e.Poster)
                      .WithMany(u => u.PostedJobs)
                      .HasForeignKey(e => e.PostedBy)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            // Applications
            modelBuilder.Entity<Appli>(entity =>
            {
                entity.HasKey(e => e.ApplicationId);
                entity.HasOne(e => e.User)
                      .WithMany(u => u.Applications)
                      .HasForeignKey(e => e.UserId)
                      .OnDelete(DeleteBehavior.NoAction);
                entity.HasOne(e => e.Job)
                      .WithMany(j => j.Applications)
                      .HasForeignKey(e => e.JobId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            // TourPlaces (many-to-many)
            modelBuilder.Entity<TourPlace>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasOne(e => e.Tour)
                      .WithMany(t => t.TourPlaces)
                      .HasForeignKey(e => e.TourId)
                      .OnDelete(DeleteBehavior.Cascade);
                entity.HasOne(e => e.Place)
                      .WithMany(p => p.TourPlaces)
                      .HasForeignKey(e => e.PlaceId)
                      .OnDelete(DeleteBehavior.Cascade);
            });
            // 1:1 VolunteerProfile <-> Users
            modelBuilder.Entity<VolunteerProfile>(entity =>
            {
                entity.HasKey(e => e.UserId);
                entity.HasOne(e => e.User)
                      .WithOne(u => u.VolunteerProfile)
                      .HasForeignKey<VolunteerProfile>(e => e.UserId)
                      .OnDelete(DeleteBehavior.Cascade); // Delete profile if user is deleted
            });
            // VolunteerRequestAssignments
            modelBuilder.Entity<VolunteerRequestAssignment>(entity =>
            {
                entity.HasKey(e => e.AssignmentId);
                entity.HasOne(e => e.Request)
                      .WithMany(r => r.Assignments)
                      .HasForeignKey(e => e.RequestId)
                      .OnDelete(DeleteBehavior.Cascade); // If request is deleted, assignments go too

                entity.HasOne(e => e.VolunteerUser)
                      .WithMany(u => u.VolunteerAssignments)
                      .HasForeignKey(e => e.VolunteerUserId)
                      .OnDelete(DeleteBehavior.NoAction); // Prevent multiple cascade path
            });
        }
    }
}