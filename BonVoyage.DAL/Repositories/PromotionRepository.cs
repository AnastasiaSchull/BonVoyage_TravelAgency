using BonVoyage.DAL.Entities;
using BonVoyage.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using BonVoyage.DAL.EF;


namespace BonVoyage.DAL.Repositories
{
    public class PromotionRepository : IRepository<Promotion>
    {
        private BonVoyageContext db;
        public PromotionRepository(BonVoyageContext context)
        {
            this.db = context;
        }
        public async Task<IQueryable<Promotion>> GetAll()
        {
            return db.Promotions;
        }

        public async Task<Promotion> Get(int id)
        {
            return await db.Promotions.FindAsync(id);
        }

        public async Task Create(Promotion promotion)
        {
            await db.Promotions.AddAsync(promotion);
        }

        public void Update(Promotion promotion)
        {
            db.Entry(promotion).State = EntityState.Modified;
        }

        public async Task Delete(int id)
        {
            Promotion? promotion = await db.Promotions.FindAsync(id);
            if (promotion != null)
                db.Promotions.Remove(promotion);
        }
    }
}
