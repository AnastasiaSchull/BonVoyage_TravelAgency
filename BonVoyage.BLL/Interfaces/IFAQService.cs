﻿using BonVoyage.BLL.DTOs;

namespace BonVoyage.BLL.Interfaces
{
    public interface IFAQService
	{
        Task<IQueryable<FAQDTO>> GetAllFAQsAsync();
        Task<FAQDTO> GetFAQByIdAsync(int id);
        Task CreateFAQAsync(FAQDTO faq);
        Task UpdateFAQAsync(FAQDTO faq);
        Task DeleteFAQAsync(int id);
    }
}
