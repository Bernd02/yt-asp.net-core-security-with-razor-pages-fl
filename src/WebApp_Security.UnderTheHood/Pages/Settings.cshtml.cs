using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApp_Security.UnderTheHood.Pages
{
	[Authorize(Policy = Constants.Policies.ADMIN_ONLY)]
	public class SettingsModel : PageModel
	{
		public void OnGet()
		{
		}
	}
}
