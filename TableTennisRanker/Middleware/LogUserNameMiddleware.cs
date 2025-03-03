using Serilog.Context;
using TableTennisRanker.Extensions;

namespace TableTennisRanker.Middleware
{
	public class LogUserNameMiddleware(RequestDelegate next)
	{
		public Task Invoke(HttpContext context)
		{
			LogContext.PushProperty("UserName", context.User.Identity!.Name!.WithoutDomain());

			return next(context);
		}
	}
}
