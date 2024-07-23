using Microsoft.AspNetCore.Mvc.Filters;
using System.Globalization;

namespace BonVoyage_TravelAgency.Filters
{
	// Access modifiers for each resource file are set to public.
	// The "Custom Tool" property value for resource files is "PublicResXFileCodeGenerator" - a resource generation tool.
	// Otherwise, the resource files will not be compiled and available.
	// Build Action - Embedded Resource
	// Custom Tool Namespace - Resources.


	// The CultureAttribute filter manages the selection of culture based on the saved value in cookies or sets English as the default language if cookies are missing or do not contain a suitable value.
	public class CultureAttribute : Attribute, IActionFilter
	{

		public void OnActionExecuted(ActionExecutedContext filterContext)
		{

		}

		// An action filter that triggers upon accessing controller actions and performs localization.
		public void OnActionExecuting(ActionExecutingContext filterContext)
		{
			string? cultureName = null;
			// Retrieving cookies from the context, which may contain the set culture.
			var cultureCookie = filterContext.HttpContext.Request.Cookies["lang"];
			if (cultureCookie != null)
				cultureName = cultureCookie;
			else
				cultureName = "en";

			// List of cultures
			List<string> cultures = new List<string>() { "en", "uk", "fr", "sk" };
			if (!cultures.Contains(cultureName))
			{
				cultureName = "en";
			}
			// CultureInfo.CreateSpecificCulture creates a CultureInfo object that represents a specific language and regional settings based on the given name.
			// CurrentCulture sets the language and regional settings for the current thread.
			Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(cultureName);
			// CurrentUICulture sets the current language and regional settings used by the resource manager to look for resources related to language and regional settings during runtime.
			Thread.CurrentThread.CurrentUICulture = CultureInfo.CreateSpecificCulture(cultureName);
			// After this, for localization purposes, the system will select the appropriate resource file.
		}
	}
	
}