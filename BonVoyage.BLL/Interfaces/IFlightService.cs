using BonVoyage.BLL.DTOs;

namespace BonVoyage.BLL.Interfaces
{
    public interface IFlightService
    {
        Task<IEnumerable<FlightDTO>> GetAllFlightsAsync();
        Task<FlightDTO> GetFlightByIdAsync(int id);
        Task CreateFlightAsync(FlightDTO flightDTO);
        Task UpdateFlightAsync(FlightDTO flightDTO);
        Task DeleteFlightAsync(int id);
    }
}
