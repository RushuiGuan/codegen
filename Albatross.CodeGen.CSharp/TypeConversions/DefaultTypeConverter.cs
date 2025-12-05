using Albatross.CodeAnalysis.Symbols;
using Albatross.CodeGen.CSharp.Expressions;
using Albatross.CodeGen.Syntax;
using Microsoft.CodeAnalysis;
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