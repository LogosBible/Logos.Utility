
using System;
using System.Linq.Expressions;

namespace Logos.Utility
{
	/// <summary>
	/// Provides helper methods for expression tree operations.
	/// </summary>
	public static class ExpressionUtility
	{
		/// <summary>
		/// Gets the name of the property returned by the specified expression.
		/// </summary>
		/// <typeparam name="TProperty">The type of the property.</typeparam>
		/// <param name="expr">The member access expression.</param>
		/// <returns>The name of the property.</returns>
		public static string GetPropertyName<TProperty>(Expression<Func<TProperty>> expr)
		{
			var exprMember = expr.Body as MemberExpression;
			if (exprMember != null)
				return exprMember.Member.Name;

			throw new InvalidOperationException("ExpressionUtility.GetPropertyName failed on " + expr);
		}

		/// <summary>
		/// Gets the name of the property returned by the specified expression.
		/// </summary>
		/// <typeparam name="T">The type of the property owner.</typeparam>
		/// <typeparam name="TProperty">The type of the property.</typeparam>
		/// <param name="expr">The member access expression.</param>
		/// <returns>The name of the property.</returns>
		/// <remarks>Type-annotate the argument to the lambda to get type inference for this method. Example:
		/// <code>(FileInfo info) => info.FullName</code> rather than <code>info => info.FullName</code>.</remarks>
		public static string GetPropertyName<T, TProperty>(Expression<Func<T, TProperty>> expr)
		{
			var exprMember = expr.Body as MemberExpression;
			if (exprMember != null)
				return exprMember.Member.Name;

			throw new InvalidOperationException("ExpressionUtility.GetPropertyName failed on " + expr);
		}
	}
}
