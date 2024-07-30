using BonVoyage.BLL.DTOs;
using BonVoyage.BLL.Interfaces;
using BonVoyage.DAL.Entities;
using BonVoyage.DAL.Interfaces;
using BonVoyage.BLL.Infrastructure;
using AutoMapper;

namespace BonVoyage.BLL.Services
{
	public class FAQService : IFAQService
	{
		IUnitOfWork Database { get; set; }

		public FAQService(IUnitOfWork uow)
		{
			Database = uow;
		}
		

		public async Task CreateFAQAsync(FAQDTO faqDTO)
		{
			var faq = new FAQ
			{
				FAQId = faqDTO.FAQId,
				Question = faqDTO.Question,
				Answer = faqDTO.Answer
			};
			await Database.FAQs.Create(faq);
			await Database.Save();
		}


		public async Task UpdateFAQAsync(FAQDTO faqDTO)
		{
			var faq = new FAQ
			{
				FAQId = faqDTO.FAQId,
				Question = faqDTO.Question,
				Answer = faqDTO.Answer
			};
			Database.FAQs.Update(faq);
			await Database.Save();
		}


		public async Task DeleteFAQAsync(int id)
		{
			await Database.FAQs.Delete(id);
			await Database.Save();
		}


		public async Task<FAQDTO> GetFAQByIdAsync(int id)
		{
			var faq = await Database.FAQs.Get(id);
			if (faq == null)
				throw new ValidationException("FAQ not found!", "");
			return new FAQDTO
			{
				FAQId = faq.FAQId,
				Question = faq.Question,
				Answer = faq.Answer
			};
		}

		// Automapper 		
		public async Task<IQueryable<FAQDTO>> GetAllFAQsAsync()
		{
			var config = new MapperConfiguration(cfg => cfg.CreateMap<FAQ, FAQDTO>());
			var mapper = new Mapper(config);
			return mapper.Map<IQueryable<FAQ>, IQueryable<FAQDTO>>(await Database.FAQs.GetAll());
		}
	}
}
