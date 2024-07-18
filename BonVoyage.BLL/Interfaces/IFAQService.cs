using BonVoyage.DAL.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BonVoyage.BLL.Interfaces
{
    public interface IFAQService
    {
        Task<IEnumerable<FAQ>> GetAllFAQsAsync();
        Task<FAQ> GetFAQByIdAsync(int id);
        Task CreateFAQAsync(FAQ faq);
        Task UpdateFAQAsync(FAQ faq);
        Task DeleteFAQAsync(int id);
    }
}
