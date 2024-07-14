using BonVoyage.DAL.Entities;
using BonVoyage.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using BonVoyage.DAL.EF;


namespace BonVoyage.DAL.Repositories
{
    public class TourRepository : IRepository<Tour>
    {
        private BonVoyageContext db;
        public TourRepository(BonVoyageContext context)
        {
            this.db = context;
        }
        public async Task<IQueryable<Tour>> GetAll()
        {
            return db.Tours;
        }

        public async Task<Tour> Get(int id)
        {
            return await db.Tours.FindAsync(id);
        }

        public async Task Create(Tour tour)
        {
            await db.Tours.AddAsync(tour);
        }

        public void Update(Tour tour)
        {
            db.Entry(tour).State = EntityState.Modified;
        }

        public async Task Delete(int id)
        {
            Tour? tour = await db.Tours.FindAsync(id);
            if (tour != null)
                db.Tours.Remove(tour);
        }
    }
}
