using BonVoyage.BLL.Interfaces;
using BonVoyage.DAL.Interfaces;
using BonVoyage.DAL.Entities;
using BonVoyage.BLL.Infrastructure;
using BonVoyage.BLL.DTOs;
using AutoMapper;

namespace BonVoyage.BLL.Services
{
    public class PromotionService: IPromotionService
    {
        IUnitOfWork Database { get; set; }

        public PromotionService(IUnitOfWork uow)
        {
            Database = uow;
        }
        public async Task CreatePromotionAsync(PromotionDTO promotionDTO)
        {
            var promotion = new Promotion
            {
                PromotionId = promotionDTO.PromotionId,
                Description = promotionDTO.Description,
                Discount = promotionDTO.Discount,
                StartDate = promotionDTO.StartDate,
                EndDate = promotionDTO.EndDate
            };
            await Database.Promotions.Create(promotion);
            await Database.Save();
        }
        public async Task UpdatePromotionAsync(PromotionDTO promotionDTO)
        {
            var promotion = new Promotion
            {
                PromotionId = promotionDTO.PromotionId,
                Description = promotionDTO.Description,
                Discount = promotionDTO.Discount,
                StartDate = promotionDTO.StartDate,
                EndDate = promotionDTO.EndDate
            };
            Database.Promotions.Update(promotion);
            await Database.Save();
        }
        public async Task DeletePromotionAsync(int id)
        {
            await Database.Promotions.Delete(id);
            await Database.Save();
        }
        public async Task<PromotionDTO> GetPromotionByIdAsync(int id)
        {
            var promotion = await Database.Promotions.Get(id);
            if (promotion   == null)
                throw new ValidationException("Wrong promotion!", "");
            return new PromotionDTO
            {
                PromotionId = promotion.PromotionId,
                Description = promotion.Description,
                Discount = promotion.Discount,
                StartDate = promotion.StartDate,
                EndDate = promotion.EndDate,
            };
        }
        // Automapper 
        public async Task<IQueryable<PromotionDTO>> GetAllPromotionsAsync()
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Promotion, PromotionDTO>()).CreateMapper();
            return mapper.Map<IQueryable<Promotion>, IQueryable<PromotionDTO>>(await Database.Promotions.GetAll());
        }
    }
}
