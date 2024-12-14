using System.Security.Claims;
using WebApp_Security.UnderTheHood;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

builder.Services.AddAuthentication(Constants.AuthTypes.AUTH_TYPE)
	// add cookie auth handler - video 6 = 7
	.AddCookie(Constants.AuthTypes.AUTH_TYPE, options =>
	{
		options.Cookie.Name = Constants.AuthTypes.AUTH_TYPE;

		// path which is used when not logged in
		// default = /account/login
		options.LoginPath = "/Account/Login";

		// video 9
		options.AccessDeniedPath = "/Account/AccessDenied";
	});

// video 9
builder.Services.AddAuthorization(options =>
{
	// policy consists of a series of claims
	options.AddPolicy(Constants.Policies.ADMIN_ONLY,
		config => config.RequireClaim(Constants.Claims.ADMIN_CLAIM));

	options.AddPolicy(Constants.Policies.HR_MANAGER_ONLY,
		config => config
			.RequireClaim(Constants.Claims.ADMIN_CLAIM)
			.RequireClaim(Constants.Claims.HR_MANAGER_CLAIM));

	options.AddPolicy(Constants.Policies.POLICY_MUST_BELONG_TO_HR_DEPARTMENT,
		config => config.RequireClaim(Constants.Claims.DEPARTMENT_CLAIM, "HR"));
});


// --------------------------------------------------
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Error");
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
