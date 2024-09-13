using Microsoft.AspNetCore.Mvc;
using BonVoyage.BLL.DTOs;
using BonVoyage.BLL.Interfaces;
using BonVoyage.BLL.Infrastructure;
using Microsoft.AspNetCore.Mvc.Rendering;
using BonVoyage.DAL.Entities;

namespace BonVoyage_TravelAgency.Controllers
{
    public class CustomerPreferenceController : BaseController
    {
        private readonly ICustomerPreferenceService preferenceService;
        private readonly IUserService userService;

        public CustomerPreferenceController(ICustomerPreferenceService preferenceServ,
            IUserService userServ)
        {
            preferenceService = preferenceServ;
            userService = userServ;
        }

        // GET: Preferences
        public async Task<IActionResult> Index()
        {
            return View(await preferenceService.GetAllPreferencesAsync());
        }

        // GET: Preferences/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            try
            {
                if (id == null)
                {
                    return NotFound();
                }
                CustomerPreferenceDTO preference = await preferenceService.GetPreferenceByIdAsync((int)id);
                return View(preference);
            }
            catch (ValidationException ex)
            {
                return NotFound(ex.Message);
            }
        }

        //
        // GET: /Preferences/Create

        public async Task<IActionResult> Create()
        {
            ViewBag.ListUsers = new SelectList(await userService.GetAllUsersAsync(), "UserId", "Name");
            return View();
        }

        // POST: Preferences/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CustomerPreferenceDTO preference)
        {
            if (ModelState.IsValid)
            {
                await preferenceService.CreatePreferenceAsync(preference);
                return View("~/Views/CustomerPreference/Index.cshtml", await preferenceService.GetAllPreferencesAsync());
            }
            ViewBag.ListUsers = new SelectList(await userService.GetAllUsersAsync(), "UserId", "Name", preference.UserId);
            return View(preference);
        }

        // GET: Preferences/Edit
        public async Task<IActionResult> Edit(int? id)
        {
            try
            {
                if (id == null)
                {
                    return NotFound();
                }
                CustomerPreferenceDTO preference = await preferenceService.GetPreferenceByIdAsync((int)id);
                ViewBag.ListUsers = new SelectList(await userService.GetAllUsersAsync(), "UserId", "Name", preference.UserId);
                
                return View(preference);
            }
            catch (ValidationException ex)
            {
                return NotFound(ex.Message);
            }
        }

        // POST: Preferences/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CustomerPreferenceDTO preference)
        {
            if (ModelState.IsValid)
            {
                await preferenceService.UpdatePreferenceAsync(preference);

                ViewBag.ListUsers = new SelectList(await userService.GetAllUsersAsync(), "UserId", "Name", preference.UserId);
               
                return View("~/Views/CustomerPreference/Index.cshtml", await preferenceService.GetAllPreferencesAsync());
            }
            return View(preference);
        }

        // GET: Preferences/Delete/
        public async Task<IActionResult> Delete(int? id)
        {
            try
            {
                if (id == null)
                {
                    return NotFound();
                }
                CustomerPreferenceDTO preference = await preferenceService.GetPreferenceByIdAsync((int)id);

                return View(preference);
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

        // POST: Preferences/Delete/
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await preferenceService.DeletePreferenceAsync(id);

            return View("~/Views/CustomerPreference/Index.cshtml", await preferenceService.GetAllPreferencesAsync());
        }

    }
}