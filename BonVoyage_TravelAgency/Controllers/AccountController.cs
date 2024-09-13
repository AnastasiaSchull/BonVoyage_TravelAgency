using BonVoyage.DAL.EF;
using BonVoyage.DAL.Entities;
using BonVoyage_TravelAgency.Models;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using System.Text;
using BonVoyage.BLL.Interfaces;
using BonVoyage.BLL.DTOs;
using Microsoft.AspNetCore.Http;

namespace BonVoyage_TravelAgency.Controllers
{
    public class AccountController : BaseController
    {
        private readonly IUserService _userService;

        public AccountController(IUserService userService)
        {
            _userService = userService;
        }

        public IActionResult Index()
        {
            var users = _userService.GetAllUsersAsync();
            return View(users);
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterModel reg)
        {
            if (ModelState.IsValid)
            {
                var existingUsers = await _userService.GetAllUsersAsync();
                var existingUser = existingUsers.FirstOrDefault(u => u.Email == reg.Email);

                if (existingUser != null)
                {
                    ModelState.AddModelError("Email", "A user with this email already exists!");
                    return View(reg);
                }

                UserDTO user = new UserDTO();
                user.UserName = reg.UserName;
                user.UserSurname = reg.UserSurname;
                user.Email = reg.Email;
                user.Address = reg.Address;
                user.Country = reg.Country;

                byte[] saltbuf = new byte[16];

                RandomNumberGenerator randomNumberGenerator = RandomNumberGenerator.Create();
                randomNumberGenerator.GetBytes(saltbuf);

                StringBuilder sb = new StringBuilder(16);
                for (int i = 0; i < 16; i++)
                    sb.Append(string.Format("{0:X2}", saltbuf[i]));
                string salt = sb.ToString();

                byte[] password = Encoding.Unicode.GetBytes(salt + reg.Password);

                byte[] byteHash = SHA256.HashData(password);

                StringBuilder hash = new StringBuilder(byteHash.Length);
                for (int i = 0; i < byteHash.Length; i++)
                    hash.Append(string.Format("{0:X2}", byteHash[i]));

                user.Password = hash.ToString();
                user.Salt = salt;

                await _userService.CreateUserAsync(user);

                return RedirectToAction("Index", "Home");
            }

            return View(reg);
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginModel logon)
        {
            if (ModelState.IsValid)
            {
                var users = await _userService.GetAllUsersAsync();
                if (!users.Any())
                {
                    ModelState.AddModelError("", "Incorrect email or password!");
                    return View(logon);
                }

                var user = users.FirstOrDefault(u => u.Email == logon.Email);
                if (user == null)
                {
                    ModelState.AddModelError("", "Incorrect email or password!");
                    return View(logon);
                }

                string? salt = user.Salt;

                byte[] password = Encoding.Unicode.GetBytes(salt + logon.Password);

                byte[] byteHash = SHA256.HashData(password);

                StringBuilder hash = new StringBuilder(byteHash.Length);
                for (int i = 0; i < byteHash.Length; i++)
                    hash.Append(string.Format("{0:X2}", byteHash[i]));

                if (user.Password != hash.ToString())
                {
                    ModelState.AddModelError("", "Incorrect email or password!");
                    return View(logon);
                }

                HttpContext.Session.SetString("UserName", user.UserName);
                HttpContext.Session.SetInt32("UserId", user.UserId);

                return RedirectToAction("Index", "Home");
            }

            return View(logon);
        }

        public ActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }
    }
}
