
using System;
using System.Collections.Generic;

namespace Logos.Utility.Collection
{
	/// <summary>
	/// Provides methods for manipulating dictionaries.
	/// </summary>
	/// <remarks>See <a href="http://code.logos.com/blog/2008/02/getoraddvalue.html">GetOrAddValue</a>.</remarks>
	public static class DictionaryUtility
	{
		/// <summary>
		/// Gets a value from the dictionary, adding and returning a new instance if it is missing.
		/// </summary>
		/// <param name="dict">The dictionary.</param>
		/// <param name="key">The key.</param>
		/// <returns>The new or existing value.</returns>
		public static TValue GetOrAddValue<TKey, TValue>(IDictionary<TKey, TValue> dict, TKey key) where TValue : new()
		{
			TValue value;
			if (dict.TryGetValue(key, out value))
				return value;
			value = new TValue();
			dict.Add(key, value);
			return value;
		}

		/// <summary>
		/// Gets a value from the dictionary, adding and returning a new instance if it is missing.
		/// </summary>
		/// <typeparam name="TKey">The type of the key.</typeparam>
		/// <typeparam name="TValue">The type of the value.</typeparam>
		/// <param name="dict">The dictionary.</param>
		/// <param name="key">The key.</param>
		/// <param name="creator">Used to create a new value if necessary</param>
		/// <returns>The new or existing value.</returns>
		public static TValue GetOrAddValue<TKey, TValue>(IDictionary<TKey, TValue> dict, TKey key, Func<TValue> creator)
		{
			TValue value;
			if (dict.TryGetValue(key, out value))
				return value;
			value = creator();
			dict.Add(key, value);
			return value;
		}

		/// <summary>
		/// Gets a value from the dictionary, returning a default value if it is missing.
		/// </summary>
		/// <typeparam name="TKey">The type of the key.</typeparam>
		/// <typeparam name="TValue">The type of the value.</typeparam>
		/// <param name="dict">The dictionary.</param>
		/// <param name="key">The key.</param>
		/// <returns>The value, or a default value.</returns>
		public static TValue GetValueOrDefault<TKey, TValue>(IDictionary<TKey, TValue> dict, TKey key)
		{
			// specification for IDictionary<> requires that the returned value be the default if it fails
			TValue value = default(TValue);
			dict.TryGetValue(key, out value);
			return value;
		}

		/// <summary>
		/// Gets a value from the dictionary, returning the specified default value if it is missing.
		/// </summary>
		/// <typeparam name="TKey">The type of the key.</typeparam>
		/// <typeparam name="TValue">The type of the value.</typeparam>
		/// <param name="dict">The dictionary.</param>
		/// <param name="key">The key.</param>
		/// <param name="defaultValue">The default value.</param>
		/// <returns>The value, or a default value.</returns>
		public static TValue GetValueOrDefault<TKey, TValue>(IDictionary<TKey, TValue> dict, TKey key, TValue defaultValue)
		{
			TValue value;
			return dict.TryGetValue(key, out value) ? value : defaultValue;
		}

		/// <summary>
		/// Gets a value from the dictionary, returning the generated default value if it is missing.
		/// </summary>
		/// <typeparam name="TKey">The type of the key.</typeparam>
		/// <typeparam name="TValue">The type of the value.</typeparam>
		/// <param name="dict">The dictionary.</param>
		/// <param name="key">The key.</param>
		/// <param name="defaultCreator">The default value generator.</param>
		/// <returns>The value, or a default value.</returns>
		public static TValue GetValueOrDefault<TKey, TValue>(IDictionary<TKey, TValue> dict, TKey key, Func<TValue> defaultCreator)
		{
			TValue value;
			return dict.TryGetValue(key, out value) ? value : defaultCreator();
		}
	}
}
