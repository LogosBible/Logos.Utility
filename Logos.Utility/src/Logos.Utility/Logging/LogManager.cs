using System;

namespace Logos.Utility.Logging
{
	/// <summary>
	/// Enables logging and provides access to loggers.
	/// </summary>
	public static class LogManager
	{
		/// <summary>
		/// Initializes logging.
		/// </summary>
		/// <param name="createLoggerCore">Called to create a logger core. Null to disable logging.</param>
		/// <remarks>This method should be called exactly once.</remarks>
		public static void Initialize(Func<string, LoggerCore> createLoggerCore)
		{
			s_service.Initialize(createLoggerCore);
		}

		/// <summary>
		/// Gets a logger for the specified name.
		/// </summary>
		/// <param name="name">The name.</param>
		/// <returns>The logger.</returns>
		/// <remarks>This method returns the same object for the same name.</remarks>
		public static Logger GetLogger(string name)
		{
			return s_service.GetLogger(name);
		}

		static readonly LoggingService s_service = new LoggingService();
	}
}
