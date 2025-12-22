using Microsoft.CodeAnalysis;
using System.Diagnostics.CodeAnalysis;

namespace Albatross.CodeGen {
	/// <summary>
	/// Interface for converting type symbols to type expressions with precedence support for converter ordering
	/// </summary>
	public interface ITypeConverter {
		/// <summary>
		/// Gets the precedence of this converter; higher values are tried first
		/// </summary>
		public int Precedence { get; }
		
		/// <summary>
		/// Attempts to convert a type symbol to a type expression
		/// </summary>
		/// <param name="symbol">The type symbol to convert</param>
		/// <param name="factory">Factory for recursive type conversions</param>
		/// <param name="expression">The resulting type expression if conversion succeeds</param>
		/// <returns>True if conversion succeeded; otherwise, false</returns>
		bool TryConvert(ITypeSymbol symbol, IConvertObject<ITypeSymbol, ITypeExpression> factory, [NotNullWhen(true)] out ITypeExpression? expression);
	}
}