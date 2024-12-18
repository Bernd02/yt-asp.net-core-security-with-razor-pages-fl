using Microsoft.AspNetCore.Authorization;
using WebApp_Security.UnderTheHood;
using WebApp_Security.UnderTheHood.Authorization;

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

		// set cookie expiration time - video 12
		options.ExpireTimeSpan = TimeSpan.FromSeconds(10);
	});


// video 9 - compiler suggest to use AddAuthorizationBuilder instead of AddAuthorization
builder.Services.AddAuthorizationBuilder()
	// policy consists of a series of claims
	// policiy - admin only
	.AddPolicy(Constants.Policies.ADMIN_ONLY,
		config => config.RequireClaim(Constants.Claims.ADMIN_CLAIM))

	// policiy - manager only
	.AddPolicy(Constants.Policies.HR_MANAGER_ONLY,
		config => config
			.RequireClaim(Constants.Claims.ADMIN_CLAIM)
			.RequireClaim(Constants.Claims.HR_MANAGER_CLAIM)
			// the minActiveDays (10) may be configured in the appSettings
			.Requirements.Add(new HrManagerAuthRequirement(10)))

	// policiy - must belong to hr department
	.AddPolicy(Constants.Policies.POLICY_MUST_BELONG_TO_HR_DEPARTMENT,
		config => config.RequireClaim(Constants.Claims.DEPARTMENT_CLAIM, "HR"));

/*
	builder.Services.AddAuthorization(options =>
	{
		// policy consists of a series of claims
		// policiy - admin only
		options.AddPolicy(Constants.Policies.ADMIN_ONLY,
			config => config.RequireClaim(Constants.Claims.ADMIN_CLAIM));
	
		// policiy - manager only
		options.AddPolicy(Constants.Policies.HR_MANAGER_ONLY,
			config => config
				.RequireClaim(Constants.Claims.ADMIN_CLAIM)
				.RequireClaim(Constants.Claims.HR_MANAGER_CLAIM)
	
				// the minActiveDays (10) may be configured in the appSettings
				.Requirements.Add(new HrManagerAuthRequirement(10)));
	
		// policiy - must belong to hr department
		options.AddPolicy(Constants.Policies.POLICY_MUST_BELONG_TO_HR_DEPARTMENT,
			config => config.RequireClaim(Constants.Claims.DEPARTMENT_CLAIM, "HR"));
	});
*/


// register the handler for the HrManagerAuthRequirement
builder.Services.AddSingleton<IAuthorizationHandler, HrManagerAutRequirementHandler>();


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
