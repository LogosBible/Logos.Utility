using Logos.Utility.Basic;
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
		public static DisposibleObject TimedWarn(Logger logger, string message)
		{
			return logger.IsWarnEnabled ? TimedLog(logger.Warn, message, null) : DisposibleObject.Empty;
		}

		/// <summary>
		/// Logs the specified message before and after an operation, displaying the elapsed time.
		/// </summary>
		/// <param name="logger">The logger.</param>
		/// <param name="message">The message.</param>
		/// <param name="args">The message arguments.</param>
		/// <returns>A Scope that, when disposed, logs the elapsed time.</returns>
		public static DisposibleObject TimedWarn(Logger logger, string message, params object[] args)
		{
			return logger.IsWarnEnabled ? TimedLog(logger.Warn, message, args) : DisposibleObject.Empty;
		}

		/// <summary>
		/// Logs the specified message before and after an operation, displaying the elapsed time.
		/// </summary>
		/// <param name="logger">The logger.</param>
		/// <param name="message">The message.</param>
		/// <returns>A Scope that, when disposed, logs the elapsed time.</returns>
		public static DisposibleObject TimedInfo(Logger logger, string message)
		{
			return logger.IsInfoEnabled ? TimedLog(logger.Info, message, null) : DisposibleObject.Empty;
		}

		/// <summary>
		/// Logs the specified message before and after an operation, displaying the elapsed time.
		/// </summary>
		/// <param name="logger">The logger.</param>
		/// <param name="message">The message.</param>
		/// <param name="args">The message arguments.</param>
		/// <returns>A Scope that, when disposed, logs the elapsed time.</returns>
		public static DisposibleObject TimedInfo(Logger logger, string message, params object[] args)
		{
			return logger.IsInfoEnabled ? TimedLog(logger.Info, message, args) : DisposibleObject.Empty;
		}

		/// <summary>
		/// Logs the specified message before and after an operation, displaying the elapsed time.
		/// </summary>
		/// <param name="logger">The logger.</param>
		/// <param name="message">The message.</param>
		/// <returns>A Scope that, when disposed, logs the elapsed time.</returns>
		public static DisposibleObject TimedDebug(Logger logger, string message)
		{
			return logger.IsDebugEnabled ? TimedLog(logger.Debug, message, null) : DisposibleObject.Empty;
		}

		/// <summary>
		/// Logs the specified message before and after an operation, displaying the elapsed time.
		/// </summary>
		/// <param name="logger">The logger.</param>
		/// <param name="message">The message.</param>
		/// <param name="args">The message arguments.</param>
		/// <returns>A Scope that, when disposed, logs the elapsed time.</returns>
		public static DisposibleObject TimedDebug(Logger logger, string message, params object[] args)
		{
			return logger.IsDebugEnabled ? TimedLog(logger.Debug, message, args) : DisposibleObject.Empty;
		}

		private static DisposibleObject TimedLog(Action<string> doLog, string message, object[] args)
		{
			string formattedMessage = args == null || args.Length == 0 ? message : StringUtility.FormatInvariant(message, args);
			doLog("(Timed) " + formattedMessage);
			Stopwatch stopwatch = Stopwatch.StartNew();
			return DisposibleObject.Create(
				delegate
				{
					stopwatch.Stop();
					TimeSpan elapsed = stopwatch.Elapsed;
					StringUtility.FormatInvariant("({0}) {1}", TimeSpanUtility.FormatForLogging(elapsed.Ticks < 0 ? TimeSpan.Zero : elapsed), formattedMessage);
				});
		}
	}
}
