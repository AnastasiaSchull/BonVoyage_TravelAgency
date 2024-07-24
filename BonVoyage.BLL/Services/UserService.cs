using BonVoyage.BLL.Interfaces;
using BonVoyage.DAL.Entities;
using BonVoyage.DAL.Interfaces;

namespace BonVoyage.BLL.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await _unitOfWork.Users.GetAll();
        }

        public async Task<User> GetUserByIdAsync(int id)
        {
            return await _unitOfWork.Users.Get(id);
        }

        public async Task CreateUserAsync(User user)
        {
            await _unitOfWork.Users.Create(user);
            await _unitOfWork.Save();
        }

        public async Task UpdateUserAsync(User user)
        {
            _unitOfWork.Users.Update(user);
            await _unitOfWork.Save();
        }

        public async Task DeleteUserAsync(int id)
        {
            await _unitOfWork.Users.Delete(id);
            await _unitOfWork.Save();
        }
    }
}
