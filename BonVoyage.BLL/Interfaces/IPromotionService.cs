using BonVoyage.DAL.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BonVoyage.BLL.Interfaces
{
    public interface IPromotionService
    {
        Task<IEnumerable<Promotion>> GetAllPromotionsAsync();
        Task<Promotion> GetPromotionByIdAsync(int id);
        Task CreatePromotionAsync(Promotion promotion);
        Task UpdatePromotionAsync(Promotion promotion);
        Task DeletePromotionAsync(int id);
    }
}
