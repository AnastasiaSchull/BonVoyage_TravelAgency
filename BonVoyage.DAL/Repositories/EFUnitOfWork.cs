using BonVoyage.DAL.Interfaces;
using BonVoyage.DAL.EF;
using BonVoyage.DAL.Entities;


namespace BonVoyage.DAL.Repositories
{
    public class EFUnitOfWork : IUnitOfWork
    {
        private BonVoyageContext db;
        private BookingRepository bookingRepository;
        private CustomerPreferenceRepository customerPreferenceRepository;
        private FAQRepository faqRepository;
        private FlightRepository flightRepository;
        private HotelRepository hotelRepository;
        private HotelPhotoRepository hotelPhotoRepository;
        private PromotionRepository promotionRepository;
        private ReviewRepository reviewRepository;
        private TourRepository tourRepository;
        private TourPhotoRepository tourPhotoRepository;
        private UserRepository userRepository;


        public EFUnitOfWork(BonVoyageContext context)
        {
            db = context;
        }

        public IRepository<Booking> Bookings
        {
            get
            {
                if (bookingRepository == null)
                    bookingRepository = new BookingRepository(db);
                return bookingRepository;
            }
        }
        public IRepository<CustomerPreference> CustomerPreferences
        {
            get
            {
                if (customerPreferenceRepository == null)
                    customerPreferenceRepository = new CustomerPreferenceRepository(db);
                return customerPreferenceRepository;
            }
        }

        public IRepository<FAQ> FAQs
        {
            get
            {
                if (faqRepository == null)
                    faqRepository = new FAQRepository(db);
                return faqRepository;
            }
        }
        public IRepository<Flight> Flights
        {
            get
            {
                if (flightRepository == null)
                    flightRepository = new FlightRepository(db);
                return flightRepository;
            }
        }
        public IRepository<Hotel> Hotels
        {
            get
            {
                if (hotelRepository == null)
                    hotelRepository = new HotelRepository(db);
                return hotelRepository;
            }
        }
        public IRepository<HotelPhoto> HotelPhotos
        {
            get
            {
                if (hotelPhotoRepository == null)
                    hotelPhotoRepository = new HotelPhotoRepository(db);
                return hotelPhotoRepository;
            }
        }
        public IRepository<Promotion> Promotions
        {
            get
            {
                if (promotionRepository == null)
                    promotionRepository = new PromotionRepository(db);
                return promotionRepository;
            }
        }
        public IRepository<Review> Reviews
        {
            get
            {
                if (reviewRepository == null)
                    reviewRepository = new ReviewRepository(db);
                return reviewRepository;
            }
        }
        public IRepository<Tour> Tours
        {
            get
            {
                if (tourRepository == null)
                    tourRepository = new TourRepository(db);
                return tourRepository;
            }
        }
        public IRepository<TourPhoto> TourPhotos
        {
            get
            {
                if (tourPhotoRepository == null)
                    tourPhotoRepository = new TourPhotoRepository(db);
                return tourPhotoRepository;
            }
        }
        public IRepository<User> Users
        {
            get
            {
                if (userRepository == null)
                   userRepository = new UserRepository(db);
                return userRepository;
            }
        }

        public async Task Save()
        {
            await db.SaveChangesAsync();
        }

    }
}
