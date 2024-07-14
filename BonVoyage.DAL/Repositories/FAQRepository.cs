using BonVoyage.DAL.Entities;
using BonVoyage.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using BonVoyage.DAL.EF;


namespace BonVoyage.DAL.Repositories
{
    public class FAQRepository : IRepository<FAQ>
    {
        private BonVoyageContext db;
        public FAQRepository(BonVoyageContext context)
        {
            this.db = context;
        }
        public async Task<IQueryable<FAQ>> GetAll()
        {
            return db.FAQs;
        }

        public async Task<FAQ> Get(int id)
        {
            return await db.FAQs.FindAsync(id);
        }

        public async Task Create(FAQ faq)
        {
            await db.FAQs.AddAsync(faq);
        }

        public void Update(FAQ faq)
        {
            db.Entry(faq).State = EntityState.Modified;
        }

        public async Task Delete(int id)
        {
            FAQ? faq = await db.FAQs.FindAsync(id);
            if (faq != null)
                db.FAQs.Remove(faq);
        }
    }
}
