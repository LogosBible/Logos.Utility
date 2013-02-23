namespace Logos.Utility.Logging
{
	/// <summary>
	/// The implementation of a logger.
	/// </summary>
	public abstract class LoggerImpl
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="LoggerImpl"/> class.
		/// </summary>
		/// <param name="logger">The logger.</param>
		protected LoggerImpl(Logger logger)
		{
			m_logger = logger;
		}

		/// <summary>
		/// Configures the logger.
		/// </summary>
		/// <param name="isDebugEnabled">True if debug logging is enabled.</param>
		/// <param name="isInfoEnabled">True if info logging is enabled.</param>
		/// <param name="isWarnEnabled">True if warn logging is enabled.</param>
		/// <param name="isErrorEnabled">True if error logging is enabled.</param>
		protected void ConfigureLogger(bool isDebugEnabled, bool isInfoEnabled, bool isWarnEnabled, bool isErrorEnabled)
		{
			m_logger.UpdateConfiguration(this, isDebugEnabled, isInfoEnabled, isWarnEnabled, isErrorEnabled);
		}

		/// <summary>
		/// Implements debug logging.
		/// </summary>
		/// <param name="message">The message.</param>
		/// <param name="args">The arguments.</param>
		/// <remarks>If the arguments are null or empty, the message should not be formatted.</remarks>
		protected abstract void DebugCore(string message, object[] args);

		/// <summary>
		/// Implements info logging.
		/// </summary>
		/// <param name="message">The message.</param>
		/// <param name="args">The arguments.</param>
		/// <remarks>If the arguments are null or empty, the message should not be formatted.</remarks>
		protected abstract void InfoCore(string message, object[] args);

		/// <summary>
		/// Implements warn logging.
		/// </summary>
		/// <param name="message">The message.</param>
		/// <param name="args">The arguments.</param>
		/// <remarks>If the arguments are null or empty, the message should not be formatted.</remarks>
		protected abstract void WarnCore(string message, object[] args);

		/// <summary>
		/// Implements error logging.
		/// </summary>
		/// <param name="message">The message.</param>
		/// <param name="args">The arguments.</param>
		/// <remarks>If the arguments are null or empty, the message should not be formatted.</remarks>
		protected abstract void ErrorCore(string message, object[] args);

		internal void Debug(string message, params object[] args)
		{
			DebugCore(message, args);
		}

		internal void Info(string message, params object[] args)
		{
			InfoCore(message, args);
		}

		internal void Warn(string message, params object[] args)
		{
			WarnCore(message, args);
		}

		internal void Error(string message, params object[] args)
		{
			ErrorCore(message, args);
		}

		internal static readonly LoggerImpl Null = new NullLoggerCore();

		private class NullLoggerCore : LoggerImpl
		{
			public NullLoggerCore()
				: base(null)
			{
			}

			protected override void DebugCore(string message, object[] args)
			{
			}

			protected override void InfoCore(string message, object[] args)
			{
			}

			protected override void WarnCore(string message, object[] args)
			{
			}

			protected override void ErrorCore(string message, object[] args)
			{
			}
		}

		readonly Logger m_logger;
	}
}
