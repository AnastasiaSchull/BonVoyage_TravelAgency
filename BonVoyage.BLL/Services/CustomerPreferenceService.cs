using BonVoyage.BLL.DTOs;
using BonVoyage.BLL.Interfaces;
using BonVoyage.DAL.Entities;
using BonVoyage.DAL.Interfaces;
using BonVoyage.BLL.Infrastructure;
using AutoMapper;

namespace BonVoyage.BLL.Services
{
	public class CustomerPreferenceService : ICustomerPreferenceService
	{
		IUnitOfWork Database { get; set; }

		public CustomerPreferenceService(IUnitOfWork uow)
		{
			Database = uow;
		}

		public async Task<CustomerPreferenceDTO> GetPreferenceByIdAsync(int id)
		{
			var preference = await Database.CustomerPreferences.Get(id);
			if (preference == null)
				throw new ValidationException("Preference not found!", "");
			return new CustomerPreferenceDTO
			{
				CustomerPreferenceId = preference.CustomerPreferenceId,
				UserId = preference.UserId,
				PreferencesDetails = preference.PreferencesDetails
			};
		}

		public async Task CreatePreferenceAsync(CustomerPreferenceDTO preferenceDTO)
		{
			var preference = new CustomerPreference
			{
				CustomerPreferenceId = preferenceDTO.CustomerPreferenceId,
				UserId = preferenceDTO.UserId,
				PreferencesDetails = preferenceDTO.PreferencesDetails
			};
			await Database.CustomerPreferences.Create(preference);
			await Database.Save();
		}

		public async Task UpdatePreferenceAsync(CustomerPreferenceDTO preferenceDTO)
		{
			var preference = new CustomerPreference
			{
				CustomerPreferenceId = preferenceDTO.CustomerPreferenceId,
				UserId = preferenceDTO.UserId,
				PreferencesDetails = preferenceDTO.PreferencesDetails
			};
			Database.CustomerPreferences.Update(preference);
			await Database.Save();
		}

		public async Task DeletePreferenceAsync(int id)
		{
			await Database.CustomerPreferences.Delete(id);
			await Database.Save();
		}

		// Automapper 
		public async Task<IQueryable<CustomerPreferenceDTO>> GetAllPreferencesAsync()
		{
			var config = new MapperConfiguration(cfg => cfg.CreateMap<CustomerPreference, CustomerPreferenceDTO>());
			var mapper = new Mapper(config);
			return mapper.Map<IQueryable<CustomerPreference>, IQueryable<CustomerPreferenceDTO>>(await Database.CustomerPreferences.GetAll());
		}
	}
}
