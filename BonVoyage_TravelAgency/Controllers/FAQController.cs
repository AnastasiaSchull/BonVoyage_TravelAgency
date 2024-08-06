using BonVoyage.BLL.DTOs;
using BonVoyage.BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BonVoyage_TravelAgency.Controllers
{
    public class FAQController : BaseController
    {
        private readonly IFAQService _faqService;

        public FAQController(IFAQService faqService)
        {
            _faqService = faqService;
        }

        public async Task<IActionResult> Index()
        {
            var faqs = await _faqService.GetAllFAQsAsync();
            return View(faqs);
        }

        public async Task<IActionResult> Upsert(int? id)
        {
            FAQDTO faq = new FAQDTO();
            if (id.HasValue)
            {
                faq = await _faqService.GetFAQByIdAsync(id.Value);
                if (faq == null)
                {
                    return NotFound();
                }
            }
            return View(faq);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(FAQDTO faq)
        {
            if (ModelState.IsValid)
            {
                if (faq.FAQId == 0)
                {
                    await _faqService.CreateFAQAsync(faq);
                }
                else
                {
                    await _faqService.UpdateFAQAsync(faq);
                }
                return RedirectToAction(nameof(Index));
            }
            return View(faq);
        }


        [HttpPost]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var faq = await _faqService.GetFAQByIdAsync(id);
            if (faq == null)
            {
                return Json(new { success = false, message = "FAQ not found!" });
            }

            await _faqService.DeleteFAQAsync(id);
            return Json(new { success = true, message = "FAQ deleted successfully!" });
        }

    }
}