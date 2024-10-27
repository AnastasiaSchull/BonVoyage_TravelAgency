using BonVoyage.DAL.Entities;
using BonVoyage.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using BonVoyage.DAL.EF;


namespace BonVoyage.DAL.Repositories
{
    public class BookingHotelRepository: IRepository<BookingHotel>
    {
        private BonVoyageContext db;
        public BookingHotelRepository(BonVoyageContext context)
        {
            this.db = context;
        }

        public async Task<IQueryable<BookingHotel>> GetAll()
        {
            return db.BookingsHotels;
        }

        public async Task<BookingHotel> Get(int id)
        {
            return await db.BookingsHotels.FindAsync(id);            
        }        

        public async Task Create(BookingHotel bookingHotel)
        {
            await db.BookingsHotels.AddAsync(bookingHotel);
        }

        public void Update(BookingHotel bookingHotel)
        {
            db.Entry(bookingHotel).State = EntityState.Modified;
        }

        public async Task Delete(int id)
        {
            BookingHotel? bookingHotel = await db.BookingsHotels.FindAsync(id);
            if (bookingHotel != null)
                db.BookingsHotels.Remove(bookingHotel);
        }
    }
}
