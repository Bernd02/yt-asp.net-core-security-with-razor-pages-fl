using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApp_Security.UnderTheHood.Pages
{
	[Authorize(Policy = Constants.Policies.HR_MANAGER_ONLY)]
	public class HrManagerModel : PageModel
	{
		public void OnGet()
		{
		}
	}
}
