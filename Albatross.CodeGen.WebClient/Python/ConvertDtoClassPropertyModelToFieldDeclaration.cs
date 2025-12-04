using Albatross.CodeGen.Python;
using Albatross.CodeGen.Syntax;
using Albatross.CodeGen.Python.Declarations;
using Albatross.CodeGen.Python.Expressions;
using Albatross.CodeGen.WebClient.Models;
using Humanizer;
using Microsoft.CodeAnalysis;
using Albatross.CodeGen.WebClient.Settings;
using Albatross.Text;
using Albatross.CodeAnalysis.Symbols;

namespace Albatross.CodeGen.WebClient.Python {
	public class ConvertDtoClassPropertyModelToFieldDeclaration : IConvertObject<DtoClassPropertyInfo, FieldDeclaration> {
		private readonly Compilation compilation;
		private readonly CodeGenSettings settings;
		private readonly IConvertObject<ITypeSymbol, ITypeExpression> typeConverter;

		public ConvertDtoClassPropertyModelToFieldDeclaration(Compilation compilation, CodeGenSettings settings, IConvertObject<ITypeSymbol, ITypeExpression> typeConverter) {
			this.compilation = compilation;
			this.settings = settings;
			this.typeConverter = typeConverter;
		}

		public FieldDeclaration Convert(DtoClassPropertyInfo from) {
			string name;
			if (settings.PythonWebClientSettings.PropertyNameMapping.TryGetValue(from.FullName, out var mappedName)) {
				name = mappedName.Underscore();
			} else {
				name = from.Name.Underscore();
			}
			return new FieldDeclaration(name) {
				Type = typeConverter.Convert(from.PropertyType),
				Initializer = new InvocationSyntaxNodeBuilder()
					.WithCallableExpression(Defined.Identifiers.PydanticField)
					.AddArgument(new ScopedVariableExpression {
						Identifier = new IdentifierNameExpression("alias"),
						Assignment = new StringLiteralExpression(from.Name.CamelCase())
					})
					.AddArgument(GetDefaultExpression(from)).Build()
			};
		}

		IExpression GetDefaultExpression(DtoClassPropertyInfo from) {
			var builder = new ScopedVariableSyntaxNodeBuilder();
			if (from.PropertyType.IsCollection(compilation)) {
				builder.WithName("default_factory");
				builder.WithExpression(Defined.Identifiers.List);
			} else {
				builder.WithName("default");
				if (from.PropertyType.IsNullable(compilation)) {
					builder.WithExpression(new NoneLiteralExpression());
				} else if (from.PropertyType.SpecialType == SpecialType.System_Boolean) {
					builder.WithExpression(new BooleanLiteralExpression(false));
				} else {
					return new NoOpExpression();
				}
			}
			return builder.Build();
		}

		object IConvertObject<DtoClassPropertyInfo>.Convert(DtoClassPropertyInfo from) => Convert(from);
	}
}