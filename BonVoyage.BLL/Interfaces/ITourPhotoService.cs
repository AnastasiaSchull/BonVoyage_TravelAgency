using BonVoyage.DAL.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BonVoyage.BLL.Interfaces
{
    public interface ITourPhotoService
    {
        Task<IEnumerable<TourPhoto>> GetAllTourPhotosAsync();
        Task<TourPhoto> GetTourPhotoByIdAsync(int id);
        Task CreateTourPhotoAsync(TourPhoto tourPhoto);
        Task UpdateTourPhotoAsync(TourPhoto tourPhoto);
        Task DeleteTourPhotoAsync(int id);
    }
}
