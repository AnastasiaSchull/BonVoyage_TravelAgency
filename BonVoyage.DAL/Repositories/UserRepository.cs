﻿using BonVoyage.DAL.Entities;
using BonVoyage.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using BonVoyage.DAL.EF;


namespace BonVoyage.DAL.Repositories
{
    public class UserRepository : IUserRepository//IRepository<User>
    {
        private BonVoyageContext db;
        public UserRepository(BonVoyageContext context)
        {
            this.db = context;
        }
        public async Task<IQueryable<User>> GetAll()
        {
            return db.Users;
        }

        public async Task<User> Get(int id)
        {
            return await db.Users.FindAsync(id);
        }

        public async Task Create(User user)
        {
            await db.Users.AddAsync(user);
        }

        public void Update(User user)
        {
            db.Entry(user).State = EntityState.Modified;
        }      

        public async Task Delete(int id)
        {
            User? user = await db.Users.FindAsync(id);
            if (user != null)
                db.Users.Remove(user);
        }

        // методы для поиска по UserName и ConnectionId
        public async Task<User?> GetUserByUsernameAsync(string username)
        {
            return await db.Users.FirstOrDefaultAsync(u => u.UserName == username);
        }

        public async Task<User?> GetUserByConnectionIdAsync(string connectionId)
        {
            return await db.Users.FirstOrDefaultAsync(u => u.ConnectionId == connectionId);
        }

        public async Task<List<User>> GetActiveUsersAsync()
        {
            return await db.Users.Where(u => u.IsActive).ToListAsync();
        }
    }
}
