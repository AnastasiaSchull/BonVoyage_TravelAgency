using BonVoyage.DAL.Entities;
using BonVoyage.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using BonVoyage.DAL.EF;


namespace BonVoyage.DAL.Repositories
{
    public class BookingRepository: IRepository<Booking>
    {
        private BonVoyageContext db;
        public BookingRepository(BonVoyageContext context)
        {
            this.db = context;
        }

        public async Task<IQueryable<Booking>> GetAll()
        {
            return db.Bookings;
        }

        public async Task<Booking> Get(int id)
        {
            return await db.Bookings.FindAsync(id);            
        }        

        public async Task Create(Booking booking)
        {
            await db.Bookings.AddAsync(booking);
        }

        public void Update(Booking booking)
        {
            db.Entry(booking).State = EntityState.Modified;
        }

        public async Task Delete(int id)
        {
            Booking? booking = await db.Bookings.FindAsync(id);
            if (booking != null)
                db.Bookings.Remove(booking);
        }
    }
}
