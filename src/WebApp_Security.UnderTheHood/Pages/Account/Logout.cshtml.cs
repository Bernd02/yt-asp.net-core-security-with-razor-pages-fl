using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApp_Security.UnderTheHood.Pages.Account
{
	public class LogoutModel : PageModel
	{
		public const string ROUTE = "/Account/Logout";

		//public void OnGet()
		//{
		//}

		public async Task<IActionResult> OnPostAsync()
		{
			await this.HttpContext.SignOutAsync(Constants.AuthTypes.AUTH_TYPE);
			return RedirectToPage(IndexModel.ROUTE);
		}
	}
}
