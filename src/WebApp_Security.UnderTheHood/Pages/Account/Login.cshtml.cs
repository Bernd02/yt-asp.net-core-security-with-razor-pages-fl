using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;


namespace WebApp_Security.UnderTheHood.Pages.Account
{
	public class LoginModel : PageModel
	{
		public const string ROUTE = "/Account/Login";

		[BindProperty]
		public Credential? Credential { get; set; }

		public void OnGet()
		{

		}

		public async Task<IActionResult> OnPostAsync()
		{
			// simple validation
			if (false == this.ModelState.IsValid
				|| this.Credential.UserName != "admin"
				&& this.Credential.Password != "password")
			{
				return this.Page();
			}

			// create security context - video 6
			// authentication and authorization claims
			var claims = new List<Claim>()
			{
				new Claim(ClaimTypes.Name, this.Credential.UserName!),
				new Claim(ClaimTypes.Email, "admin@email.com"),
				new Claim(Constants.Claims.DEPARTMENT_CLAIM, "HR"),
				new Claim(Constants.Claims.ADMIN_CLAIM, "thisValueDoesNotMatter"),
				new Claim(Constants.Claims.HR_MANAGER_CLAIM, "thisValueDoesNotMatter"),

				// add HrManagerEmploymentDate - days active (-11) can come from a db or something
				new Claim(Constants.Claims.EMPLOYMENT_DATE, DateTime.Today.AddDays(-11).ToShortDateString()),
			};
			var identity = new ClaimsIdentity(claims, Constants.AuthTypes.AUTH_TYPE);
			var claimsPrincipal = new ClaimsPrincipal(identity);

			// create the auth cookie - encrypt its data - add it to the HttpContext.User
			// you can view this in the browser dev-tools at application > Cookies > CookieName
			await this.HttpContext.SignInAsync(Constants.AuthTypes.AUTH_TYPE, claimsPrincipal);

			return RedirectToPage(IndexModel.ROUTE);
		}
	}


	public class Credential
	{
		[Required]
		[Display(Name = "User Name")]
		public string UserName { get; set; } = string.Empty;

		[Required]
		[DataType(DataType.Password)]
		public string Password { get; set; } = string.Empty;
	}
}

