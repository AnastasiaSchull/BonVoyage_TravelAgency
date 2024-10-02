using BonVoyage.BLL.DTOs;

namespace BonVoyage.BLL.Interfaces
{
    public interface ITourService
    {
        Task<IEnumerable<TourDTO>> GetAllToursAsync();
        Task<IEnumerable<TourDTO>> GetAllToursAsync(int pageNumber, int pageSize);
        Task<TourDTO> GetTourByIdAsync(int id);
        Task<TourDTO> CreateTourAsync(TourDTO tourDTO);
        Task UpdateTourAsync(TourDTO tourDTO);
        Task DeleteTourAsync(int id);
        Task<int> GetTotalToursCount();
    }
}
