using Microsoft.AspNetCore.Authorization;

namespace WebApp_Security.UnderTheHood.Authorization;

public class HrManagerAuthRequirement : IAuthorizationRequirement
{
	// this will be initialized in the authorization service handler
	// ... in program.cs
	public HrManagerAuthRequirement(int minimumActiveDays)
	{
		this.MinimumActiveDays = minimumActiveDays;
	}

	public int MinimumActiveDays { get; }
}

public class HrManagerAutRequirementHandler : AuthorizationHandler<HrManagerAuthRequirement>
{
	protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, HrManagerAuthRequirement requirement)
	{
		var employmentDateClaim = context.User.FindFirst(Constants.Claims.EMPLOYMENT_DATE);

		// if user has no employment claim ? just return
		if (null == employmentDateClaim)
			return Task.CompletedTask;

		// check if employee is over 10 days active
		var employmentDate = DateTime.Parse(employmentDateClaim.Value);
		var employmentActiveTime = DateTime.Today - employmentDate;

		// if employee does not fulfills the requirement ? return : succeed and return
		if (employmentActiveTime.Days <= requirement.MinimumActiveDays)
		{
			return Task.CompletedTask;
		}

		// this also takes the IAuthorizationRequirement interface
		context.Succeed(requirement);
		return Task.CompletedTask;
	}
}
