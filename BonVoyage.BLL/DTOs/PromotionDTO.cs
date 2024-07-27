using System;

namespace BonVoyage.BLL.DTOs
{
    public class PromotionDTO
    {
        public int PromotionId { get; set; }
        public string? Description { get; set; }
        public decimal Discount { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
