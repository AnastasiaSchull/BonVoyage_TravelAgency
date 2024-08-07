using BonVoyage.BLL.Interfaces;
using BonVoyage.DAL.Interfaces;
using BonVoyage.DAL.Entities;
using BonVoyage.BLL.Infrastructure;
using BonVoyage.BLL.DTOs;
using AutoMapper;

namespace BonVoyage.BLL.Services
{
	public class ReviewService: IReviewService
	{
		IUnitOfWork Database { get; set; }

		public ReviewService(IUnitOfWork uow)
		{
			Database = uow;
		}

		public async Task CreateReviewAsync(ReviewDTO reviewDTO)
		{
			var review = new Review
			{
				ReviewId = reviewDTO.ReviewId,
				UserId = reviewDTO.UserId,
				TourId = reviewDTO.TourId,
				HotelId = reviewDTO.HotelId,
				Text = reviewDTO.Text,
				Rating = reviewDTO.Rating
			};
			await Database.Reviews.Create(review);
			await Database.Save();
		}

		public async Task UpdateReviewAsync(ReviewDTO reviewDTO)
		{
			var review = new Review
			{
				ReviewId = reviewDTO.ReviewId,
				UserId = reviewDTO.UserId,
				TourId = reviewDTO.TourId,
				HotelId = reviewDTO.HotelId,
				Text = reviewDTO.Text,
				Rating = reviewDTO.Rating
			};
			Database.Reviews.Update(review);
			await Database.Save();
		}

		public async Task DeleteReviewAsync(int id)
		{
			await Database.Reviews.Delete(id);
			await Database.Save();
		}

		public async Task<ReviewDTO> GetReviewByIdAsync(int id)
		{
			var review = await Database.Reviews.Get(id);
			if (review == null)
				throw new ValidationException("Review not found!", "");
			return new ReviewDTO
			{
				ReviewId = review.ReviewId,
				UserId = review.UserId,
				TourId = review.TourId,
				HotelId = review.HotelId,
				Text = review.Text,
				Rating = review.Rating
			};
		}

		public async Task<IEnumerable<ReviewDTO>> GetAllReviewsAsync()
		{
			var config = new MapperConfiguration(cfg => cfg.CreateMap<Review, ReviewDTO>());
			var mapper = new Mapper(config);
			return mapper.Map<IQueryable<Review>, IEnumerable<ReviewDTO>>(await Database.Reviews.GetAll());
		}
	}
}
