using BonVoyage.BLL.DTOs;

namespace BonVoyage.BLL.Interfaces
{
    public interface IReviewService
	{
        Task<IQueryable<ReviewDTO>> GetAllReviewsAsync();
        Task<ReviewDTO> GetReviewByIdAsync(int id);
        Task CreateReviewAsync(ReviewDTO review);
        Task UpdateReviewAsync(ReviewDTO review);
        Task DeleteReviewAsync(int id);
    }
}
