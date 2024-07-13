using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using BonVoyage.DAL.Entities;
using System.Numerics;

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


        public BonVoyageContext(DbContextOptions<BonVoyageContext> options)
                   : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
