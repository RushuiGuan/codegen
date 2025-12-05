using Albatross.CodeAnalysis.Symbols;
using Albatross.CodeGen.CSharp.Expressions;
using Albatross.CodeGen.Syntax;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Linq;

namespace Albatross.CodeGen.CSharp.TypeConversions {
	public class DefaultTypeConverter : IConvertObject<ITypeSymbol, ITypeExpression> {
		private readonly Compilation compilation;

		public DefaultTypeConverter(Compilation compilation) {
			this.compilation = compilation;
		}

		object IConvertObject<ITypeSymbol>.Convert(ITypeSymbol from) {
			return Convert(from);
		}

		public ITypeExpression Convert(ITypeSymbol symbol) {
			switch (symbol.SpecialType) {
				case SpecialType.System_String:
					return Defined.Types.String;
				case SpecialType.System_Boolean:
					return Defined.Types.Bool;
				case SpecialType.System_Int32:
					return Defined.Types.Int;
				case SpecialType.System_Int64:
					return Defined.Types.Long;
				case SpecialType.System_Double:
					return Defined.Types.Double;
				case SpecialType.System_Single:
					return Defined.Types.Float;
				case SpecialType.System_Decimal:
					return Defined.Types.Decimal;
				case SpecialType.System_Void:
					return Defined.Types.Void;
				case SpecialType.System_Char:
					return Defined.Types.Char;
				case SpecialType.System_Object:
					return Defined.Types.Object;
			}
			if (symbol is INamedTypeSymbol namedTypeSymbol) {
				if (namedTypeSymbol.IsGenericTypeDefinition()) {
					throw new InvalidOperationException("Cannot convert generic type definition");
				} else if (namedTypeSymbol.IsGenericType) {
					return new TypeExpression(namedTypeSymbol.Name) {
						GenericArguments = namedTypeSymbol.TypeArguments.Select(Convert).ToArray()
					};
				} else {
					return new TypeExpression(namedTypeSymbol.Name);
				}
			} else if (symbol is IArrayTypeSymbol arrayTypeSymbol) {
				return new ArrayTypeExpression {
					Type = Convert(arrayTypeSymbol.ElementType)
				};
			}
			throw new NotSupportedException($"Type symbol of type {symbol.GetType().FullName} is not supported");
		}
	}
}