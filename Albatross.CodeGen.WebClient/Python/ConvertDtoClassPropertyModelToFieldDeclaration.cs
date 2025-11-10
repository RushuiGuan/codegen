using Albatross.CodeGen.Python;
using Albatross.CodeGen.Syntax;
using Albatross.CodeGen.Python.Declarations;
using Albatross.CodeGen.Python.Expressions;
using Albatross.CodeGen.WebClient.Models;
using Humanizer;
using Microsoft.CodeAnalysis;

namespace Albatross.CodeGen.WebClient.Python {
	public class ConvertDtoClassPropertyModelToFieldDeclaration : IConvertObject<DtoClassPropertyInfo, FieldDeclaration> {
		private readonly IConvertObject<ITypeSymbol, ITypeExpression> typeConverter;

		public ConvertDtoClassPropertyModelToFieldDeclaration(IConvertObject<ITypeSymbol, ITypeExpression> typeConverter) {
			this.typeConverter = typeConverter;
		}

		public FieldDeclaration Convert(DtoClassPropertyInfo from) {
			return new FieldDeclaration(from.Name.Underscore()) {
				Type = typeConverter.Convert(from.PropertyType),
				Initializer = new InvocationExpression {
					CallableExpression = Defined.Identifiers.PydanticField,
					ArgumentList = new ListOfSyntaxNodes<IExpression>(
						new ScopedVariableExpression {
							Identifier = new IdentifierNameExpression("alias"),
							Assignment = new StringLiteralExpression(from.Name.Camelize())
						}
					)
				},
			};
		}

		object IConvertObject<DtoClassPropertyInfo>.Convert(DtoClassPropertyInfo from) => Convert(from);
	}
}