using BonVoyage.DAL.Entities;
using BonVoyage.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using BonVoyage.DAL.EF;


namespace BonVoyage.DAL.Repositories
{
    public class ReviewRepository : IRepository<Review>
    {
        private BonVoyageContext db;
        public ReviewRepository(BonVoyageContext context)
        {
            this.db = context;
        }
        public async Task<IQueryable<Review>> GetAll()
        {
            return db.Reviews;
        }

        public async Task<Review> Get(int id)
        {
            return await db.Reviews.FindAsync(id);
        }

        public async Task Create(Review review)
        {
            await db.Reviews.AddAsync(review);
        }

        public void Update(Review review)
        {
            db.Entry(review).State = EntityState.Modified;
        }

        public async Task Delete(int id)
        {
            Review? review = await db.Reviews.FindAsync(id);
            if (review != null)
                db.Reviews.Remove(review);
        }
    }
}
