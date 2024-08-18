﻿using BonVoyage.DAL.Entities;

namespace BonVoyage.DAL.Interfaces
{
    public interface IUserRepository
    {
        Task<IQueryable<User>> GetAll();
        Task<User> Get(int id);
        Task Create(User item);
        void Update(User item);
        Task Delete(int id);
        Task<User?> GetUserByUsernameAsync(string username);
        Task<User?> GetUserByConnectionIdAsync(string connectionId);
        Task<List<User>> GetActiveUsersAsync();
    }
}
