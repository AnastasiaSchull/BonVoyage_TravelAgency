﻿using BonVoyage.DAL.EF;
using BonVoyage.DAL.Entities;
using BonVoyage_TravelAgency.Models;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using System.Text;
using BonVoyage.BLL.Interfaces;
using BonVoyage.BLL.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using BonVoyage.BLL.Services;
using BonVoyage.BLL.Infrastructure;

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
                // проверка для пользователя "admin"
                if ((logon.Email == "admin" && logon.Password == "admin") ||
                    (logon.Email == "Admin" && logon.Password == "admin"))
                {
                    HttpContext.Session.SetString("UserName", "Admin"); // сохраняем роль "Admin" в сессии
                    Console.WriteLine("Logged in as Admin"); 
                    return RedirectToAction("Index", "Home");
                }

               
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

        public async Task<IActionResult> Profile(int? id)
        {
            if (id == null)
            {
                var sessionId = HttpContext.Session.GetInt32("UserId");
                if (sessionId == null)
                {
                    return RedirectToAction("Login");
                }
                id = sessionId;
            }

            try
            {
                if (id == null)
                {
                    return NotFound();
                }
                UserDTO user = await _userService.GetUserByIdAsync((int)id);
                return View(user);
            }
            catch (ValidationException ex)
            {
                return NotFound(ex.Message);
            }
        }

        private string HashPassword(string password, string salt)
        {
            // Об'єднуємо сіль і пароль
            byte[] passwordBytes = Encoding.Unicode.GetBytes(salt + password);

            // Хешуємо об'єднаний пароль
            byte[] byteHash = SHA256.HashData(passwordBytes);

            // Перетворюємо хеш в шістнадцятковий рядок
            StringBuilder hash = new StringBuilder(byteHash.Length * 2);
            for (int i = 0; i < byteHash.Length; i++)
                hash.AppendFormat("{0:X2}", byteHash[i]);

            return hash.ToString();
        }

        [HttpPost]
        public async Task<IActionResult> Profile(UserDTO model, string oldPassword, string newPassword)
        {
            if (model.UserId == 0)
            {
                ModelState.AddModelError("", "User ID is required.");
                return View(model);
            }

            var existingUser = await _userService.GetUserByIdAsync(model.UserId);
            if (existingUser == null)
            {
                return NotFound();
            }

            if (model.UserName == null ||
            model.UserSurname == null ||
            model.Address == null ||
            model.Country == null)
            {
                ModelState.AddModelError("FieldError", "Please provide at least one field to update.");
                return View(model);
            }

            if (!string.IsNullOrEmpty(oldPassword) && !string.IsNullOrEmpty(newPassword))
            {
                string hashedOldPassword = HashPassword(oldPassword, existingUser.Salt);

                if (existingUser.Password != hashedOldPassword)
                {
                    ModelState.AddModelError("PasswordError", "The old password is incorrect.");
                    return View(model);
                }

                if (oldPassword == newPassword)
                {
                    ModelState.AddModelError("PasswordError", "The new password cannot be the same as the old password.");
                    return View(model);
                }

                model.Password = HashPassword(newPassword, existingUser.Salt);

                existingUser.Password = model.Password;
            }

             await _userService.UpdateUserAsync(model);

             TempData["SuccessMessage"] = "Your profile has been successfully updated.";
             return RedirectToAction("Profile", new { id = model.UserId });
        }
    }
}
