using System;
using System.Collections.Generic;

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
		/// <param name="configureLogger">Called to initialize a logger. Null to disable logging.</param>
		/// <remarks>This method should be called exactly once.</remarks>
		public static void Initialize(Action<Logger> configureLogger)
		{
			lock (s_lock)
			{
				if (s_isInitialized)
					throw new InvalidOperationException();

				if (configureLogger != null)
				{
					s_configureLogger = configureLogger;
					foreach (Logger logger in s_loggers.Values)
						configureLogger(logger);
				}

				s_isInitialized = true;
			}
		}

		/// <summary>
		/// Gets a logger for the specified name.
		/// </summary>
		/// <param name="name">The name.</param>
		/// <returns>The logger.</returns>
		/// <remarks>This method returns the same object for the same name.</remarks>
		public static Logger GetLogger(string name)
		{
			lock (s_lock)
			{
				Logger logger;
				if (!s_loggers.TryGetValue(name, out logger))
				{
					logger = new Logger(name);
					s_loggers.Add(name, logger);

					if (s_configureLogger != null)
						s_configureLogger(logger);
				}
				return logger;
			}
		}

		internal static bool IsInitialized
		{
			get { return s_isInitialized; }
		}

		static readonly object s_lock = new object();
		static readonly Dictionary<string, Logger> s_loggers = new Dictionary<string, Logger>();
		static Action<Logger> s_configureLogger;
		static bool s_isInitialized;
	}
}
