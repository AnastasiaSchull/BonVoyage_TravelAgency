using BonVoyage.DAL.Entities;
using BonVoyage.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using BonVoyage.DAL.EF;


namespace BonVoyage.DAL.Repositories
{
    public class HotelRepository : IRepository<Hotel>
    {
        private BonVoyageContext db;
        public HotelRepository(BonVoyageContext context)
        {
            this.db = context;
        }
        public async Task<IQueryable<Hotel>> GetAll()
        {
            return db.Hotels;
        }

        public async Task<Hotel> Get(int id)
        {
            return await db.Hotels.FindAsync(id);
        }

        public async Task Create(Hotel hotel)
        {
            await db.Hotels.AddAsync(hotel);
        }

        public void Update(Hotel hotel)
        {
            db.Entry(hotel).State = EntityState.Modified;
        }

        public async Task Delete(int id)
        {
            Hotel? hotel = await db.Hotels.FindAsync(id);
            if (hotel != null)
                db.Hotels.Remove(hotel);
        }
    }
}
