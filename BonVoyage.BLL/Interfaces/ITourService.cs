using BonVoyage.DAL.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BonVoyage.BLL.Interfaces
{
    public interface ITourService
    {
        Task<IEnumerable<Tour>> GetAllToursAsync();
        Task<Tour> GetTourByIdAsync(int id);
        Task CreateTourAsync(Tour tour);
        Task UpdateTourAsync(Tour tour);
        Task DeleteTourAsync(int id);
    }
}
