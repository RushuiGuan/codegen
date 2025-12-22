namespace Albatross.CodeGen {
	/// <summary>
	/// Generic interface for converting objects from one type to another with strongly typed result
	/// </summary>
	/// <typeparam name="From">The source type to convert from</typeparam>
	/// <typeparam name="To">The target type to convert to</typeparam>
	public interface IConvertObject<From, To> : IConvertObject<From> {
		/// <summary>
		/// Converts an object from the source type to the target type
		/// </summary>
		/// <param name="from">The source object to convert</param>
		/// <returns>The converted object of the target type</returns>
		new To Convert(From from);
	}

	/// <summary>
	/// Interface for converting objects from a specific type to an untyped result
	/// </summary>
	/// <typeparam name="From">The source type to convert from</typeparam>
	public interface IConvertObject<From> {
		/// <summary>
		/// Converts an object from the source type to an untyped result
		/// </summary>
		/// <param name="from">The source object to convert</param>
		/// <returns>The converted object as an untyped result</returns>
		object Convert(From from);
	}
}