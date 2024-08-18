using BonVoyage.DAL.Entities;


namespace BonVoyage.DAL.Interfaces
{
    public interface IUnitOfWork
    {
        IRepository<Booking> Bookings { get; }
        IRepository<CustomerPreference> CustomerPreferences { get; }
        IRepository <FAQ> FAQs { get; }
        IRepository<Flight> Flights { get; }
        IRepository<Hotel> Hotels { get; }
        IRepository<HotelPhoto> HotelPhotos { get; }
        IRepository<Promotion> Promotions { get; }
        IRepository<Review> Reviews { get; }
        IRepository<Tour> Tours { get; }
        IRepository<TourPhoto> TourPhotos { get; }

        //IRepository<User> Users { get; }
        IUserRepository Users { get; }
        IMessageRepository Messages { get; }
        Task Save();
    }
}
