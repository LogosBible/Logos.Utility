using System;
using System.Collections.Generic;

namespace Logos.Utility.Logging
{
	/// <summary>
	/// Enables logging and provides access to loggers.
	/// </summary>
	internal sealed class LoggingService
	{
		/// <summary>
		/// Creates the logging service.
		/// </summary>
		public LoggingService()
			: this(null)
		{
		}

		/// <summary>
		/// Creates and initializes the logging service.
		/// </summary>
		public LoggingService(Func<string, LoggerCore> createLoggerCore)
		{
			m_lock = new object();
			m_loggers = new Dictionary<string, Logger>();
			m_createLoggerCore = createLoggerCore;
			m_isInitialized = createLoggerCore != null;
		}

		/// <summary>
		/// Initializes logging.
		/// </summary>
		/// <param name="createLoggerCore">Called to create a logger implementation. Null to disable logging.</param>
		/// <remarks>This method should be called exactly once. It can be called even after GetLogger
		/// has been called to create loggers; those loggers will be properly configured as well.</remarks>
		public void Initialize(Func<string, LoggerCore> createLoggerCore)
		{
			lock (m_lock)
			{
				if (m_isInitialized)
					throw new InvalidOperationException("Already initialized.");

				if (createLoggerCore != null)
				{
					m_createLoggerCore = createLoggerCore;
					foreach (KeyValuePair<string, Logger> namedLogger in m_loggers)
						namedLogger.Value.SetLoggerCore(createLoggerCore(namedLogger.Key));
				}

				m_isInitialized = true;
			}
		}

		/// <summary>
		/// Gets a logger for the specified name.
		/// </summary>
		/// <param name="name">The name.</param>
		/// <returns>The logger.</returns>
		/// <remarks>This method returns the same object for the same name.</remarks>
		public Logger GetLogger(string name)
		{
			lock (m_lock)
			{
				Logger logger;
				if (!m_loggers.TryGetValue(name, out logger))
				{
					logger = new Logger(name);
					m_loggers.Add(name, logger);

					if (m_createLoggerCore != null)
						logger.SetLoggerCore(m_createLoggerCore(name));
				}
				return logger;
			}
		}

		readonly object m_lock;
		readonly Dictionary<string, Logger> m_loggers;
		Func<string, LoggerCore> m_createLoggerCore;
		bool m_isInitialized;
	}
}