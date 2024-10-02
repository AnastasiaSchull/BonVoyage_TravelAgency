using BonVoyage.DAL.Entities;
using BonVoyage.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using BonVoyage.DAL.EF;


namespace BonVoyage.DAL.Repositories
{
    public class TourPhotoRepository : IRepository<TourPhoto>
    {
        private BonVoyageContext db;
        public TourPhotoRepository(BonVoyageContext context)
        {
            this.db = context;
        }
        public async Task<IQueryable<TourPhoto>> GetAll()
        {
            return db.TourPhotos;
        }

        public async Task<TourPhoto> Get(int tourId)
        {
            Console.WriteLine($"Fetching photo with ID: {tourId}");
            var tourPhoto = await db.TourPhotos.FirstOrDefaultAsync(tp => tp.TourId == tourId);
            if (tourPhoto == null)
            {
                Console.WriteLine("Photo not found");
            }
            else
            {
                Console.WriteLine($"Photo found: {tourPhoto.PhotoUrl}");
            }
            return tourPhoto;
        }

        public async Task Create(TourPhoto tourPhoto)
        {
            await db.TourPhotos.AddAsync(tourPhoto);
        }

        public void Update(TourPhoto tourPhoto)
        {
            db.Entry(tourPhoto).State = EntityState.Modified;
        }

        public async Task Delete(int id)
        {
            TourPhoto? tourPhoto = await db.TourPhotos.FindAsync(id);
            if (tourPhoto != null)
                db.TourPhotos.Remove(tourPhoto);
        }
    }
}
