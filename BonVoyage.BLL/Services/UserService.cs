using BonVoyage.BLL.Interfaces;
using BonVoyage.DAL.Interfaces;
using BonVoyage.DAL.Entities;
using BonVoyage.BLL.Infrastructure;
using BonVoyage.BLL.DTOs;
using AutoMapper;

namespace BonVoyage.BLL.Services
{
    public class UserService : IUserService
    {
        IUnitOfWork Database { get; set; }

        public UserService(IUnitOfWork uow)
        {
            Database = uow;
        }

        public async Task CreateUserAsync(UserDTO userDTO)
        {
            var user = new User
            {
                UserId = userDTO.UserId,
                UserName = userDTO.UserName,
                UserSurname = userDTO.UserSurname,
                Email = userDTO.Email,
                Password = userDTO.Password,
                Salt = userDTO.Salt,
                Address = userDTO.Address,
                Country = userDTO.Country,
                Role = userDTO.Role
            };
            await Database.Users.Create(user);
            await Database.Save();
        }

        public async Task UpdateUserAsync(UserDTO userDTO)
        {
            var existingUser = await Database.Users.Get(userDTO.UserId);

            if (existingUser == null)
            {
                throw new ValidationException("User not found!", "");
            }

            existingUser.UserName = userDTO.UserName;
            existingUser.UserSurname = userDTO.UserSurname;
            existingUser.Email = userDTO.Email;
            existingUser.Address = userDTO.Address;
            existingUser.Country = userDTO.Country;
            existingUser.Role = userDTO.Role;

            await Database.Save();
        }

        public async Task DeleteUserAsync(int id)
        {
            await Database.Users.Delete(id);
            await Database.Save();
        }

        public async Task<UserDTO> GetUserByIdAsync(int id)
        {
            var user = await Database.Users.Get(id);
            if (user == null)
                throw new ValidationException("Wrong user!", "");
            return new UserDTO
            {
                UserId = user.UserId,
                UserName = user.UserName,
                UserSurname = user.UserSurname,
                Email = user.Email,
                Password = user.Password,
                Salt = user.Salt,
                Address = user.Address,
                Country = user.Country,
                Role = user.Role
            };
        }

        // Automapper 
        public async Task<IEnumerable<UserDTO>> GetAllUsersAsync()
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<User, UserDTO>()
            .ForMember("UserName", opt => opt.MapFrom(c => c.UserName)));
            var mapper = new Mapper(config);
            return mapper.Map<IQueryable<User>, IEnumerable<UserDTO>>(await Database.Users.GetAll());
        }
    }
}
