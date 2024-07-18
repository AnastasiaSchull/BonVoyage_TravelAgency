using BonVoyage.DAL.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BonVoyage.BLL.Interfaces
{
    public interface ICustomerPreferenceService
    {
        Task<IEnumerable<CustomerPreference>> GetAllPreferencesAsync();
        Task<CustomerPreference> GetPreferenceByIdAsync(int id);
        Task CreatePreferenceAsync(CustomerPreference preference);
        Task UpdatePreferenceAsync(CustomerPreference preference);
        Task DeletePreferenceAsync(int id);
    }
}
