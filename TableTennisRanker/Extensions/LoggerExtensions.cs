using System.Runtime.CompilerServices;
using Serilog.Context;

namespace TableTennisRanker.Extensions
{
	public static class LoggerExtensions
	{
		private const string MemberName = "MemberName";
		public static void ExtendedError(this Serilog.ILogger logger, string message, [CallerMemberName] string memberName = "")
		{
			using var prop = LogContext.PushProperty(MemberName, memberName);
			logger.Error("{Message}",message);
		}

		public static void ExtendedWarning(this Serilog.ILogger logger, string message, [CallerMemberName] string memberName = "")
		{
			using var prop = LogContext.PushProperty(MemberName, memberName);
			logger.Warning("{Message}",message);
		}

		public static void ExtendedInformation(this Serilog.ILogger logger, string message, [CallerMemberName] string memberName = "")
		{
			using var prop = LogContext.PushProperty(MemberName, memberName);
			logger.Information("{Message}",message);
		}

		public static void ExtendedDebug(this Serilog.ILogger logger, string message, [CallerMemberName] string memberName = "")
		{
			using var prop = LogContext.PushProperty(MemberName, memberName);
			logger.Debug("{Message}",message);
		}

		public static void ExtendedVerbose(this Serilog.ILogger logger, string message, [CallerMemberName] string memberName = "")
		{
			using var prop = LogContext.PushProperty(MemberName, memberName);
			logger.Verbose("{Message}",message);
		}
	}
}
