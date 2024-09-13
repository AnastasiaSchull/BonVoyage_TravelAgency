using Microsoft.AspNetCore.Mvc;
using BonVoyage.BLL.DTOs;
using BonVoyage.BLL.Interfaces;
using BonVoyage.BLL.Infrastructure;

namespace BonVoyage_TravelAgency.Controllers
{
    public class AdminController : BaseController
    {
        private readonly IUserService userService;

        public AdminController(IUserService serv)
        {
            userService = serv;
        }

        // GET: Users
        public async Task<IActionResult> Index()
        {
            return View(await userService.GetAllUsersAsync());
        }

        // GET: Users/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            try
            {
                if (id == null)
                {
                    return NotFound();
                }
                UserDTO user = await userService.GetUserByIdAsync((int)id);
                return View(user);
            }
            catch (ValidationException ex)
            {
                return NotFound(ex.Message);
            }
        }

        //
        // GET: /Users/Create

        public IActionResult Create()
        {
            return View();
        }

        // POST: Users/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(UserDTO user)
        {
            if (ModelState.IsValid)
            {
                await userService.CreateUserAsync(user);
                return View("~/Views/Users/Index.cshtml", await userService.GetAllUsersAsync());
            }
            return View(user);
        }

        // GET: Users/Edit
        public async Task<IActionResult> Edit(int? id)
        {
            try
            {
                if (id == null)
                {
                    return NotFound();
                }
                UserDTO user = await userService.GetUserByIdAsync((int)id);
                return View(user);
            }
            catch (ValidationException ex)
            {
                return NotFound(ex.Message);
            }
        }

        // POST: Users/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(UserDTO user)
        {
            if (ModelState.IsValid)
            {
                await userService.UpdateUserAsync(user);
                return View("~/Views/Users/Index.cshtml", await userService.GetAllUsersAsync());
            }
            return View(user);
        }

        // GET: Users/Delete/
        public async Task<IActionResult> Delete(int? id)
        {
            try
            {
                if (id == null)
                {
                    return NotFound();
                }
                UserDTO user = await userService.GetUserByIdAsync((int)id);
                return View(user);
            }
            catch (ValidationException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        // POST: Users/Delete/
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await userService.DeleteUserAsync(id);
            return View("~/Views/Users/Index.cshtml", await userService.GetAllUsersAsync());
        }

    }
}
