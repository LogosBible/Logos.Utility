using System;
using System.Diagnostics;

namespace Logos.Utility.Logging
{
	/// <summary>
	/// Helper methods for using a Logger.
	/// </summary>
	public static class LogUtility
	{
		/// <summary>
		/// Logs the specified message before and after an operation, displaying the elapsed time.
		/// </summary>
		/// <param name="logger">The logger.</param>
		/// <param name="message">The message.</param>
		/// <returns>A Scope that, when disposed, logs the elapsed time.</returns>
		public static Scope TimedWarn(this Logger logger, string message)
		{
			return logger.IsWarnEnabled ? TimedLog(logger.Warn, message, null) : Scope.Empty;
		}

		/// <summary>
		/// Logs the specified message before and after an operation, displaying the elapsed time.
		/// </summary>
		/// <param name="logger">The logger.</param>
		/// <param name="message">The message.</param>
		/// <param name="args">The message arguments.</param>
		/// <returns>A Scope that, when disposed, logs the elapsed time.</returns>
		public static Scope TimedWarn(this Logger logger, string message, params object[] args)
		{
			return logger.IsWarnEnabled ? TimedLog(logger.Warn, message, args) : Scope.Empty;
		}

		/// <summary>
		/// Logs the specified message before and after an operation, displaying the elapsed time.
		/// </summary>
		/// <param name="logger">The logger.</param>
		/// <param name="message">The message.</param>
		/// <returns>A Scope that, when disposed, logs the elapsed time.</returns>
		public static Scope TimedInfo(this Logger logger, string message)
		{
			return logger.IsInfoEnabled ? TimedLog(logger.Info, message, null) : Scope.Empty;
		}

		/// <summary>
		/// Logs the specified message before and after an operation, displaying the elapsed time.
		/// </summary>
		/// <param name="logger">The logger.</param>
		/// <param name="message">The message.</param>
		/// <param name="args">The message arguments.</param>
		/// <returns>A Scope that, when disposed, logs the elapsed time.</returns>
		public static Scope TimedInfo(this Logger logger, string message, params object[] args)
		{
			return logger.IsInfoEnabled ? TimedLog(logger.Info, message, args) : Scope.Empty;
		}

		/// <summary>
		/// Logs the specified message before and after an operation, displaying the elapsed time.
		/// </summary>
		/// <param name="logger">The logger.</param>
		/// <param name="message">The message.</param>
		/// <returns>A Scope that, when disposed, logs the elapsed time.</returns>
		public static Scope TimedDebug(this Logger logger, string message)
		{
			return logger.IsDebugEnabled ? TimedLog(logger.Debug, message, null) : Scope.Empty;
		}

		/// <summary>
		/// Logs the specified message before and after an operation, displaying the elapsed time.
		/// </summary>
		/// <param name="logger">The logger.</param>
		/// <param name="message">The message.</param>
		/// <param name="args">The message arguments.</param>
		/// <returns>A Scope that, when disposed, logs the elapsed time.</returns>
		public static Scope TimedDebug(this Logger logger, string message, params object[] args)
		{
			return logger.IsDebugEnabled ? TimedLog(logger.Debug, message, args) : Scope.Empty;
		}

		private static Scope TimedLog(Action<string> doLog, string message, object[] args)
		{
			string formattedMessage = args == null || args.Length == 0 ? message : message.FormatInvariant(args);
			doLog("(Timed) " + formattedMessage);
			Stopwatch stopwatch = Stopwatch.StartNew();
			return Scope.Create(
				delegate
				{
					stopwatch.Stop();
					TimeSpan elapsed = stopwatch.Elapsed;
					doLog("({0}) {1}".FormatInvariant(TimeSpanUtility.FormatForLogging(elapsed.Ticks < 0 ? TimeSpan.Zero : elapsed), formattedMessage));
				});
		}
	}
}
