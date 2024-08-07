using BonVoyage.BLL.DTOs;

namespace BonVoyage.BLL.Interfaces
{
    public interface ICustomerPreferenceService
    {
        Task<IEnumerable<CustomerPreferenceDTO>> GetAllPreferencesAsync();
        Task<CustomerPreferenceDTO> GetPreferenceByIdAsync(int id);
        Task CreatePreferenceAsync(CustomerPreferenceDTO preference);
        Task UpdatePreferenceAsync(CustomerPreferenceDTO preference);
        Task DeletePreferenceAsync(int id);
    }
}
