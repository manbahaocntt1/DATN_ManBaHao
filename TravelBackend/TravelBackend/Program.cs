using Microsoft.EntityFrameworkCore;
using TravelBackend.Data;
using TravelBackend.Repositories.Implementations;
using TravelBackend.Repositories;
using TravelBackend.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.WithOrigins("http://localhost:3000")  // your React app
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var connectionString = builder.Configuration.GetConnectionString("TravelProjectDb");
builder.Services.AddDbContext<TravelDbContext>(options => options.UseSqlServer(connectionString), ServiceLifetime.Scoped);

builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ITourRepository, TourRepository>();
builder.Services.AddScoped<ITourBookingRepository, TourBookingRepository>();
builder.Services.AddScoped<ITourReviewRepository, TourReviewRepository>();
builder.Services.AddScoped<IUserBehaviorRepository, UserBehaviorRepository>();
builder.Services.AddScoped<IVolunteerRequestRepository, VolunteerRequestRepository>();
builder.Services.AddScoped<ILawInfoRepository, LawInfoRepository>();
builder.Services.AddScoped<IJobRepository, JobRepository>();
builder.Services.AddScoped<IApplicationRepository, ApplicationRepository>();
builder.Services.AddScoped<ITourImageRepository, TourImageRepository>();
builder.Services.AddScoped<ITourAvailabilityRepository, TourAvailabilityRepository>();
builder.Services.AddScoped<ITourPlaceRepository, TourPlaceRepository>();
builder.Services.AddScoped<IPlaceRepository, PlaceRepository>();
builder.Services.AddScoped<IVolunteerRepository, VolunteerRepository>();


//services register
builder.Services.AddScoped<ApplicationService>();
builder.Services.AddScoped<JobService>();
builder.Services.AddScoped<LawInfoService>();
builder.Services.AddScoped<PlaceService>();
builder.Services.AddScoped<TourAvailabilityService>();
builder.Services.AddScoped<TourBookingService>();
builder.Services.AddScoped<TourImageService>();
builder.Services.AddScoped<TourPlaceService>();
builder.Services.AddScoped<TourReviewService>();
builder.Services.AddScoped<TourService>();
builder.Services.AddScoped<UserBehaviorService>();
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<VolunteerRequestService>();
builder.Services.AddScoped<VolunteerService>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors();              // <-- ADD THIS BEFORE UseAuthorization

app.UseAuthorization();

app.MapControllers();

app.UseStaticFiles(); // This enables serving wwwroot files


app.Run();
