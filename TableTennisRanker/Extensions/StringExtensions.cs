namespace TableTennisRanker.Extensions
{
	public static class StringExtensions
	{
		public static string WithoutDomain(this string userName)
		{
			return userName.Replace("ODIN\\", "");
		}

		public static string ExpandField(this string template, string fieldName, string value)
		{
			return template.Replace($"%{fieldName}%", value);
		}
	}
}
