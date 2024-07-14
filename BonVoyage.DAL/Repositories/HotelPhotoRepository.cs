using BonVoyage.DAL.Entities;
using BonVoyage.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using BonVoyage.DAL.EF;


namespace BonVoyage.DAL.Repositories
{
    public class HotelPhotoRepository : IRepository<HotelPhoto>
    {
        private BonVoyageContext db;
        public HotelPhotoRepository(BonVoyageContext context)
        {
            this.db = context;
        }
        public async Task<IQueryable<HotelPhoto>> GetAll()
        {
            return db.HotelPhotos;
        }

        public async Task<HotelPhoto> Get(int id)
        {
            return await db.HotelPhotos.FindAsync(id);
        }

        public async Task Create(HotelPhoto hotelPhoto)
        {
            await db.HotelPhotos.AddAsync(hotelPhoto);
        }

        public void Update(HotelPhoto hotelPhoto)
        {
            db.Entry(hotelPhoto).State = EntityState.Modified;
        }

        public async Task Delete(int id)
        {
            HotelPhoto? hotelPhoto = await db.HotelPhotos.FindAsync(id);
            if (hotelPhoto != null)
                db.HotelPhotos.Remove(hotelPhoto);
        }
    }
}
