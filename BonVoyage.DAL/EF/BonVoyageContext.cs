using Microsoft.EntityFrameworkCore;
using BonVoyage.DAL.Entities;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace BonVoyage.DAL.EF
{
    public class BonVoyageContext : DbContext
    {
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<CustomerPreference> CustomerPreferences { get; set; }
        public DbSet<FAQ> FAQs { get; set; }
        public DbSet<Flight> Flights { get; set; }
        public DbSet<Hotel> Hotels { get; set; }
        public DbSet<HotelPhoto> HotelPhotos { get; set; }
        public DbSet<Promotion> Promotions { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Tour> Tours { get; set; }
        public DbSet<TourPhoto> TourPhotos { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Message> Messages { get; set; }

        public BonVoyageContext(DbContextOptions<BonVoyageContext> options)
                   : base(options)
        {
            //Database.EnsureCreated();
        }
        // Класс необходим исключительно для миграций
        public class SampleContextFactory : IDesignTimeDbContextFactory<BonVoyageContext>
        {
            public BonVoyageContext CreateDbContext(string[] args)
            {
                var optionsBuilder = new DbContextOptionsBuilder<BonVoyageContext>();

                // получаем конфигурацию из файла appsettings.json
                ConfigurationBuilder builder = new ConfigurationBuilder();
                builder.SetBasePath(Directory.GetCurrentDirectory());
                builder.AddJsonFile("appsettings.json");
                IConfigurationRoot config = builder.Build();

                // получаем строку подключения из файла appsettings.json
                string connectionString = config.GetConnectionString("DefaultConnection");
                optionsBuilder.UseSqlServer(connectionString);
                return new BonVoyageContext(optionsBuilder.Options);
            }
        }

		 /* OnModelCreating method is necessary for configuring the mapping of decimal properties using Fluent API.
		 By default, SQL Server uses decimal(18,0) for decimal properties, which truncates the fractional part.
		 Adding this configuration explicitly sets the precision and scale to ensure correct storage of decimal values,
		 preventing loss of precision and eliminating warnings from Entity Framework Core.*/
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Flight>()
				.Property(f => f.Price)
				.HasColumnType("decimal(10,2)");

			modelBuilder.Entity<Hotel>()
				.Property(h => h.PricePerNight)
				.HasColumnType("decimal(10,2)");

			modelBuilder.Entity<Promotion>()
				.Property(p => p.Discount)
				.HasColumnType("decimal(10,2)");

			modelBuilder.Entity<Tour>()
				.Property(t => t.Price)
				.HasColumnType("decimal(10,2)");

			base.OnModelCreating(modelBuilder);
		}
	}
}
