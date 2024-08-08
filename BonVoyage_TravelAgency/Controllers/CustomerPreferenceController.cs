using Microsoft.AspNetCore.Mvc;
using BonVoyage.BLL.DTOs;
using BonVoyage.BLL.Interfaces;
using BonVoyage.BLL.Infrastructure;

namespace BonVoyage_TravelAgency.Controllers
{
    public class CustomerPreferenceController : Controller
    {
        private readonly ICustomerPreferenceService customerPreferenceService;

        public CustomerPreferenceController(ICustomerPreferenceService serv)
        {
            customerPreferenceService = serv;
        }

        //GET: CustomerPreferences
        public async Task<IActionResult> Index()
        {
            return View(await customerPreferenceService.GetAllPreferencesAsync());
        }

        // GET: CustomerPreferences/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            try
            {
                if (id == null)
                {
                    return NotFound();
                }
                CustomerPreferenceDTO customerPreference = await customerPreferenceService.GetPreferenceByIdAsync((int)id);
                return View(customerPreference);
            }
            catch (ValidationException ex)
            {
                return NotFound(ex.Message);
            }
        }

        //
        // GET: /CustomerPreferences/Create

        public IActionResult Create()
        {
            return View();
        }

        // POST: CustomerPreferences/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CustomerPreferenceDTO customerPreference)
        {
            if (ModelState.IsValid)
            {
                await customerPreferenceService.CreatePreferenceAsync(customerPreference);
                return View("~/Views/CustomerPreferences/Index.cshtml", 
                    await customerPreferenceService.GetAllPreferencesAsync());
            }
            return View(customerPreference);
        }

        // GET: CustomerPreferences/Edit
        public async Task<IActionResult> Edit(int? id)
        {
            try
            {
                if (id == null)
                {
                    return NotFound();
                }
                CustomerPreferenceDTO customerPreference = await customerPreferenceService.GetPreferenceByIdAsync((int)id);
                return View(customerPreference);
            }
            catch (ValidationException ex)
            {
                return NotFound(ex.Message);
            }
        }

        // POST: CustomerPreferences/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CustomerPreferenceDTO customerPreference)
        {
            if (ModelState.IsValid)
            {
                await customerPreferenceService.UpdatePreferenceAsync(customerPreference);
                return View("~/Views/CustomerPreferences/Index.cshtml", await customerPreferenceService.GetAllPreferencesAsync());
            }
            return View(customerPreference);
        }

        // GET: CustomerPreferences/Delete/
        public async Task<IActionResult> Delete(int? id)
        {
            try
            {
                if (id == null)
                {
                    return NotFound();
                }
                CustomerPreferenceDTO customerPreference = await customerPreferenceService.GetPreferenceByIdAsync((int)id);
                return View(customerPreference);
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

        // POST: CustomerPreferences/Delete/
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await customerPreferenceService.DeletePreferenceAsync(id);
            return View("~/Views/CustomerPreferences/Index.cshtml", 
                await customerPreferenceService.GetAllPreferencesAsync());
        }

    }
}