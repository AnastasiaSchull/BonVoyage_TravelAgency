﻿using BonVoyage.BLL.DTOs;

namespace BonVoyage.BLL.Interfaces
{
    public interface ITourService
    {
        Task<IEnumerable<TourDTO>> GetAllToursAsync();
        Task<TourDTO> GetTourByIdAsync(int id);
        Task CreateTourAsync(TourDTO tourDTO);
        Task UpdateTourAsync(TourDTO tourDTO);
        Task DeleteTourAsync(int id);
    }
}
