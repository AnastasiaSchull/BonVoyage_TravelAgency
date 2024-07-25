using BonVoyage.BLL.Interfaces;
using BonVoyage.DAL.Entities;
using BonVoyage.DAL.Interfaces;
using BonVoyage.DAL.Repositories;


namespace BonVoyage.BLL.Services
{
	public class CustomerPreferenceService : ICustomerPreferenceService
	{
		private readonly IUnitOfWork _unitOfWork;

		public CustomerPreferenceService(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}

		public async Task<IEnumerable<CustomerPreference>> GetAllPreferencesAsync()
		{
			return await _unitOfWork.CustomerPreferences.GetAll();
		}

		public async Task<CustomerPreference> GetPreferenceByIdAsync(int id)
		{
			return await _unitOfWork.CustomerPreferences.Get(id);
		}

		public async Task CreatePreferenceAsync(CustomerPreference preference)
		{
			await _unitOfWork.CustomerPreferences.Create(preference);
			await _unitOfWork.Save();
		}

		public async Task UpdatePreferenceAsync(CustomerPreference preference)
		{
			_unitOfWork.CustomerPreferences.Update(preference);
			await _unitOfWork.Save();
		}

		public async Task DeletePreferenceAsync(int id)
		{
			await _unitOfWork.CustomerPreferences.Delete(id);
			await _unitOfWork.Save();
		}

	}
}
