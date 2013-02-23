using System;

namespace Logos.Utility.Logging
{
	/// <summary>
	/// Logs messages.
	/// </summary>
	public sealed class Logger
	{
		/// <summary>
		/// Gets the name.
		/// </summary>
		/// <value>The name.</value>
		public string Name
		{
			get { return m_name; }
		}

		/// <summary>
		/// Gets a value indicating whether debug logging is enabled.
		/// </summary>
		/// <value>True if debug logging is enabled.</value>
		public bool IsDebugEnabled
		{
			get { return m_isDebugEnabled; }
		}

		/// <summary>
		/// Gets a value indicating whether info logging is enabled.
		/// </summary>
		/// <value>True if info logging is enabled.</value>
		public bool IsInfoEnabled
		{
			get { return m_isInfoEnabled; }
		}

		/// <summary>
		/// Gets a value indicating whether warn logging is enabled.
		/// </summary>
		/// <value>True if warn logging is enabled.</value>
		public bool IsWarnEnabled
		{
			get { return m_isWarnEnabled; }
		}

		/// <summary>
		/// Gets a value indicating whether error logging is enabled.
		/// </summary>
		/// <value>True if error logging is enabled.</value>
		public bool IsErrorEnabled
		{
			get { return m_isErrorEnabled; }
		}

		/// <summary>
		/// Writes the specified message using debug logging.
		/// </summary>
		/// <param name="message">The message.</param>
		public void Debug(string message)
		{
			if (m_isDebugEnabled)
				m_impl.Debug(message, null);
		}

		/// <summary>
		/// Writes the specified message using debug logging.
		/// </summary>
		/// <param name="message">The message.</param>
		/// <param name="args">The arguments.</param>
		/// <remarks>If the arguments are null or empty, the message is not formatted.</remarks>
		public void Debug(string message, params object[] args)
		{
			if (m_isDebugEnabled)
				m_impl.Debug(message, args);
		}

		/// <summary>
		/// Writes the specified message using info logging.
		/// </summary>
		/// <param name="message">The message.</param>
		public void Info(string message)
		{
			if (m_isInfoEnabled)
				m_impl.Info(message, null);
		}

		/// <summary>
		/// Writes the specified message using info logging.
		/// </summary>
		/// <param name="message">The message.</param>
		/// <param name="args">The arguments.</param>
		/// <remarks>If the arguments are null or empty, the message is not formatted.</remarks>
		public void Info(string message, params object[] args)
		{
			if (m_isInfoEnabled)
				m_impl.Info(message, args);
		}

		/// <summary>
		/// Writes the specified message using warn logging.
		/// </summary>
		/// <param name="message">The message.</param>
		public void Warn(string message)
		{
			if (m_isWarnEnabled)
				m_impl.Warn(message, null);
		}

		/// <summary>
		/// Writes the specified message using warn logging.
		/// </summary>
		/// <param name="message">The message.</param>
		/// <param name="args">The arguments.</param>
		/// <remarks>If the arguments are null or empty, the message is not formatted.</remarks>
		public void Warn(string message, params object[] args)
		{
			if (m_isWarnEnabled)
				m_impl.Warn(message, args);
		}

		/// <summary>
		/// Writes the specified message using error logging.
		/// </summary>
		/// <param name="message">The message.</param>
		public void Error(string message)
		{
			if (m_isErrorEnabled)
				m_impl.Error(message, null);
		}

		/// <summary>
		/// Writes the specified message using error logging.
		/// </summary>
		/// <param name="message">The message.</param>
		/// <param name="args">The arguments.</param>
		/// <remarks>If the arguments are null or empty, the message is not formatted.</remarks>
		public void Error(string message, params object[] args)
		{
			if (m_isErrorEnabled)
				m_impl.Error(message, args);
		}

		/// <summary>
		/// Raised when the logging configuration may have changed.
		/// </summary>
		public event EventHandler ConfigurationUpdated;

		internal Logger(string name)
		{
			m_name = name;
			m_impl = LoggerImpl.Null;
		}

		internal void UpdateConfiguration(LoggerImpl impl, bool isDebugEnabled, bool isInfoEnabled, bool isWarnEnabled, bool isErrorEnabled)
		{
			m_impl = impl ?? LoggerImpl.Null;
			m_isDebugEnabled = isDebugEnabled;
			m_isInfoEnabled = isInfoEnabled;
			m_isWarnEnabled = isWarnEnabled;
			m_isErrorEnabled = isErrorEnabled;

			ConfigurationUpdated.Raise(this);
		}

		readonly string m_name;
		LoggerImpl m_impl;
		bool m_isDebugEnabled;
		bool m_isInfoEnabled;
		bool m_isWarnEnabled;
		bool m_isErrorEnabled;
	}
}
