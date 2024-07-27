﻿using BonVoyage.BLL.DTOs;

namespace BonVoyage.BLL.Interfaces
{
    public interface IPromotionService
    {
        Task<IQueryable<PromotionDTO>> GetAllPromotionsAsync();
        Task<PromotionDTO> GetPromotionByIdAsync(int id);
        Task CreatePromotionAsync(PromotionDTO promotionDTO);
        Task UpdatePromotionAsync(PromotionDTO promotionDTO);
        Task DeletePromotionAsync(int id);
    }
}
