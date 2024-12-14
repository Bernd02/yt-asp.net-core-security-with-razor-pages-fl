using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApp_Security.UnderTheHood.Pages
{
	[Authorize(Constants.Policies.POLICY_MUST_BELONG_TO_HR_DEPARTMENT)]
	public class HumanResourceModel : PageModel
	{
		public void OnGet()
		{
		}
	}
}
