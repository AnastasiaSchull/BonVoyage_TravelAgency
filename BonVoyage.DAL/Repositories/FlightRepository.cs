using BonVoyage.DAL.Entities;
using BonVoyage.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using BonVoyage.DAL.EF;


namespace BonVoyage.DAL.Repositories
{
    public class FlightRepository : IRepository<Flight>
    {
        private BonVoyageContext db;
        public FlightRepository(BonVoyageContext context)
        {
            this.db = context;
        }
        public async Task<IQueryable<Flight>> GetAll()
        {
            return db.Flights;
        }

        public async Task<Flight> Get(int id)
        {
            return await db.Flights.FindAsync(id);
        }

        public async Task Create(Flight flight)
        {
            await db.Flights.AddAsync(flight);
        }

        public void Update(Flight flight)
        {
            db.Entry(flight).State = EntityState.Modified;
        }

        public async Task Delete(int id)
        {
            Flight? flight = await db.Flights.FindAsync(id);
            if (flight != null)
                db.Flights.Remove(flight);
        }
    }
}
