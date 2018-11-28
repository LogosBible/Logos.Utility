using System;

namespace Logos.Utility.Logging
{
	/// <summary>
	/// The implementation of a logger.
	/// </summary>
	public abstract class LoggerCore
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="LoggerCore"/> class.
		/// </summary>
		protected LoggerCore()
		{
		}

		/// <summary>
		/// True if debug logging is enabled.
		/// </summary>
		protected abstract bool IsDebugEnabledCore { get; }

		/// <summary>
		/// True if info logging is enabled.
		/// </summary>
		protected abstract bool IsInfoEnabledCore { get; }

		/// <summary>
		/// True if warn logging is enabled.
		/// </summary>
		protected abstract bool IsWarnEnabledCore { get; }

		/// <summary>
		/// True if error logging is enabled.
		/// </summary>
		protected abstract bool IsErrorEnabledCore { get; }

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

		/// <summary>
		/// Raises the ConfigurationUpdated event; should be called when any of the logging enabled properties change.
		/// </summary>
		protected void RaiseConfigurationUpdated()
		{
			ConfigurationUpdated.Raise(this);
		}

		internal event EventHandler ConfigurationUpdated;

		internal bool IsDebugEnabled
		{
			get { return IsDebugEnabledCore; }
		}

		internal bool IsInfoEnabled
		{
			get { return IsInfoEnabledCore; }
		}

		internal bool IsWarnEnabled
		{
			get { return IsWarnEnabledCore; }
		}

		internal bool IsErrorEnabled
		{
			get { return IsErrorEnabledCore; }
		}

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

		internal static readonly LoggerCore Null = new NullLoggerCore();

		private class NullLoggerCore : LoggerCore
		{
			public NullLoggerCore()
			{
			}

			protected override bool IsDebugEnabledCore
			{
				get { return false; }
			}

			protected override bool IsInfoEnabledCore
			{
				get { return false; }
			}

			protected override bool IsWarnEnabledCore
			{
				get { return false; }
			}

			protected override bool IsErrorEnabledCore
			{
				get { return false; }
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
	}
}
