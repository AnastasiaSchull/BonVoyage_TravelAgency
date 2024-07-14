using BonVoyage.DAL.Entities;
using BonVoyage.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using BonVoyage.DAL.EF;


namespace BonVoyage.DAL.Repositories
{
    public class CustomerPreferenceRepository : IRepository<CustomerPreference>
    {
        private BonVoyageContext db;
        public CustomerPreferenceRepository(BonVoyageContext context)
        {
            this.db = context;
        }
        public async Task<IQueryable<CustomerPreference>> GetAll()
        {
            return db.CustomerPreferences;
        }

        public async Task<CustomerPreference> Get(int id)
        {
            return await db.CustomerPreferences.FindAsync(id);
        }

        public async Task Create(CustomerPreference customerPreference)
        {
            await db.CustomerPreferences.AddAsync(customerPreference);
        }

        public void Update(CustomerPreference customerPreference)
        {
            db.Entry(customerPreference).State = EntityState.Modified;
        }

        public async Task Delete(int id)
        {
            CustomerPreference? customerPreference = await db.CustomerPreferences.FindAsync(id);
            if (customerPreference != null)
                db.CustomerPreferences.Remove(customerPreference);
        }
    }
}
