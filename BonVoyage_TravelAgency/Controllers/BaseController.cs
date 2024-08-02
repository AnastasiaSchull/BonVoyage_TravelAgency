using Microsoft.AspNetCore.Mvc;
using BonVoyage_TravelAgency.Filters;
using Microsoft.Extensions.Localization;

namespace BonVoyage_TravelAgency.Controllers
{
    // BaseController applies the CultureAttribute filter (by using the [Culture] attribute), inherits from Controller. All other controllers will inherit from BaseController

    [Culture]
    public class BaseController : Controller
    {
        public ActionResult ChangeCulture(string lang)
        {
            string? returnUrl = HttpContext.Session.GetString("path") ?? "/Home/Index";

            List<string> cultures = new List<string>() { "en", "uk", "fr", "sk" };
            if (!cultures.Contains(lang))
            {
                lang = "en"; // default language
            }

            CookieOptions option = new CookieOptions
            {
                Expires = DateTime.Now.AddDays(10)
            };
            Response.Cookies.Append("lang", lang, option);
            return Redirect(returnUrl);
        }
    }
}
