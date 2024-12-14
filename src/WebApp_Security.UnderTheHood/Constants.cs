namespace WebApp_Security.UnderTheHood;

internal static class Constants
{
	internal static class AuthTypes
	{
		public const string AUTH_TYPE = "MyCookieAuthType";
	}

	internal static class Policies
	{
		public const string POLICY_MUST_BELONG_TO_HR_DEPARTMENT = "MustBelongToHrDepartment";
		public const string ADMIN_ONLY = "AdminOnly";
		public const string HR_MANAGER_ONLY = "HrManagerOnly";
	}

	internal static class Claims
	{
		public const string DEPARTMENT_CLAIM = "DepartmentClaim";
		public const string ADMIN_CLAIM = "AdminClaim";
		public const string HR_MANAGER_CLAIM = "HrManagerClaim";
	}
}
