using BonVoyage.BLL.Interfaces;
using BonVoyage.DAL.Entities;
using BonVoyage.DAL.Interfaces;

namespace BonVoyage.BLL.Services
{
	public class FAQService : IFAQService
	{
		private readonly IUnitOfWork _unitOfWork;

		public FAQService(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}

		public async Task<IEnumerable<FAQ>> GetAllFAQsAsync()
		{
			return await _unitOfWork.FAQs.GetAll();
		}

		public async Task<FAQ> GetFAQByIdAsync(int id)
		{
			return await _unitOfWork.FAQs.Get(id);
		}

		public async Task CreateFAQAsync(FAQ faq)
		{
			await _unitOfWork.FAQs.Create(faq);
			await _unitOfWork.Save();
		}

		public async Task UpdateFAQAsync(FAQ faq)
		{
			_unitOfWork.FAQs.Update(faq);
			await _unitOfWork.Save();
		}

		public async Task DeleteFAQAsync(int id)
		{
			await _unitOfWork.FAQs.Delete(id);
			await _unitOfWork.Save();
		}
	}
}
