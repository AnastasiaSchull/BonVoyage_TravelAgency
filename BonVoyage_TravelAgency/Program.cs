using Microsoft.EntityFrameworkCore;
using BonVoyage.BLL.Interfaces;
using BonVoyage.BLL.Services;
using BonVoyage.BLL.Infrastructure;

namespace BonVoyage_TravelAgency
{
	public class Program
	{
		public static void Main(string[] args)
		{
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
			builder.Services.AddControllersWithViews();
            builder.Services.AddSession();

            var app = builder.Build();

			// Configure the HTTP request pipeline.
			if (!app.Environment.IsDevelopment())
			{
				app.UseExceptionHandler("/Home/Error");
				// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
				app.UseHsts();
			}

			app.UseHttpsRedirection();
			app.UseStaticFiles();
			app.UseSession();

			app.UseRouting();

			app.UseAuthorization();

			app.MapControllerRoute(
				name: "default",
				pattern: "{controller=Home}/{action=Index}/{id?}");

			app.Run();
		}
	}
}
