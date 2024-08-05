using Microsoft.EntityFrameworkCore;
using BonVoyage.BLL.Interfaces;
using BonVoyage.BLL.Services;
using BonVoyage.BLL.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

string? connection = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddBonVoyageContext(connection);
builder.Services.AddUnitOfWorkService();
builder.Services.AddTransient<IBookingService, BookingService>();
builder.Services.AddTransient<ICustomerPreferenceService, CustomerPreferenceService>();
builder.Services.AddTransient<IHotelPhotoService, HotelPhotoService>();
builder.Services.AddTransient<IHotelService, HotelService>();
builder.Services.AddTransient<IFAQService, FAQService>();
builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<IReviewService, ReviewService>();
builder.Services.AddTransient<ITourPhotoService, TourPhotoService>();
builder.Services.AddTransient<ITourService, TourService>();
builder.Services.AddTransient<IPromotionService, PromotionService>();
builder.Services.AddTransient<IFlightService, FlightService>();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure CORS
app.UseCors(builder => builder.WithOrigins("https://localhost:7079")
                            .AllowAnyHeader()
                            .AllowAnyMethod());

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
