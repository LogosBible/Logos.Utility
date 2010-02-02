
namespace Logos.Utility
{
	/// <summary>
	/// Indicates whether an object takes ownership of an item.
	/// </summary>
	public enum Ownership
	{
		/// <summary>
		/// The object does not own this item.
		/// </summary>
		None,

		/// <summary>
		/// The object owns this item, and is responsible for releasing it.
		/// </summary>
		Owns
	}
}
