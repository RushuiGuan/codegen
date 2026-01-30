using Albatross.CodeAnalysis;
using Albatross.CodeGen.CSharp.Expressions;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Linq;

namespace Albatross.CodeGen.CSharp.TypeConversions {
	public class DefaultTypeConverter : IConvertObject<ITypeSymbol, ITypeExpression> {
		public bool UseQualifiedNames { get; set; } = false;
		
		object IConvertObject<ITypeSymbol>.Convert(ITypeSymbol from) {
			return Convert(from);
		}

		static IIdentifierNameExpression GetIdentifier(INamedTypeSymbol symbol, bool useQualifiedNames) {
			var @namespace = symbol.ContainingNamespace.GetFullNamespace();
			if (string.IsNullOrEmpty(@namespace)) {
				return new IdentifierNameExpression(symbol.Name);
			} else {
				if (useQualifiedNames) {
					return new QualifiedIdentifierNameExpression(symbol.Name, new NamespaceExpression(@namespace));
				} else {
					return new IdentifierNameExpression($"{@namespace}.{symbol.Name}");
				}
			}
		}

		public ITypeExpression Convert(ITypeSymbol symbol) {
			switch (symbol.SpecialType) {
				case SpecialType.System_String:
					return symbol.IsNullableReferenceType() ? Defined.Types.NullableString : Defined.Types.String;
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
					return symbol.IsNullableReferenceType() ? Defined.Types.NullableObject : Defined.Types.Object;
			}
			if (symbol.TypeKind == TypeKind.Dynamic) {
				return new DynamicTypeExpression();
			}else if (symbol is INamedTypeSymbol namedTypeSymbol) {
				if (namedTypeSymbol.IsGenericTypeDefinition()) {
					throw new InvalidOperationException("Cannot convert generic type definition");
				} else if (namedTypeSymbol.IsGenericType) {
					return new TypeExpression(GetIdentifier(namedTypeSymbol, this.UseQualifiedNames)) {
						GenericArguments = namedTypeSymbol.TypeArguments.Select(Convert).ToArray()
					};
				} else {
					return new TypeExpression(GetIdentifier(namedTypeSymbol, this.UseQualifiedNames)) {
						Nullable = symbol.IsNullableReferenceType()
					};
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