using BonVoyage.DAL.EF;
using BonVoyage.DAL.Entities;
using BonVoyage_TravelAgency.Models;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using System.Text;

namespace BonVoyage_TravelAgency.Controllers
{
    public class AccountController : Controller
    {
        private readonly BonVoyageContext _context;

        public AccountController(BonVoyageContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var users = _context.Users.ToList();
            return View(users);
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(RegisterModel reg)
        {
            if (ModelState.IsValid)
            {
                var existingUser = _context.Users.FirstOrDefault(u => u.Email == reg.Email);

                if (existingUser != null)
                {
                    ModelState.AddModelError("Email", "A user with this login already exists!");
                    return View(reg);
                }

                User user = new User();
                user.UserName = reg.UserName;
                user.UserSurname = reg.UserSurname;
                user.Email = reg.Email;

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

                _context.Users.Add(user);
                _context.SaveChanges();

                return RedirectToAction("Index", "Home");
            }

            return View(reg);
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginModel logon)
        {
            if (ModelState.IsValid)
            {
                if (_context.Users.ToList().Count == 0)
                {
                    ModelState.AddModelError("", "Incorrect email or password!");
                    return View(logon);
                }
                var users = _context.Users.Where(a => a.Email == logon.Email);
                if (users.ToList().Count == 0)
                {
                    ModelState.AddModelError("", "Incorrect email or password!");
                    return View(logon);
                }
                var user = users.First();
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

                return RedirectToAction("Index", "Home");
            }

            return View(logon);
        }
    }
}
