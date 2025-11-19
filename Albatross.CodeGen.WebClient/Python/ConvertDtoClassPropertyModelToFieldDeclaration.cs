using Albatross.CodeGen.Python;
using Albatross.CodeGen.Syntax;
using Albatross.CodeGen.Python.Declarations;
using Albatross.CodeGen.Python.Expressions;
using Albatross.CodeGen.WebClient.Models;
using Humanizer;
using Microsoft.CodeAnalysis;
using Albatross.CodeGen.WebClient.Settings;
using Albatross.Text;

namespace Albatross.CodeGen.WebClient.Python {
	public class ConvertDtoClassPropertyModelToFieldDeclaration : IConvertObject<DtoClassPropertyInfo, FieldDeclaration> {
		private readonly CodeGenSettings settings;
		private readonly IConvertObject<ITypeSymbol, ITypeExpression> typeConverter;

		public ConvertDtoClassPropertyModelToFieldDeclaration(CodeGenSettings settings, IConvertObject<ITypeSymbol, ITypeExpression> typeConverter) {
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
				Initializer = new InvocationExpression {
					CallableExpression = Defined.Identifiers.PydanticField,
					ArgumentList = new ListOfSyntaxNodes<IExpression>(
							new ScopedVariableExpression {
								Identifier = new IdentifierNameExpression("alias"),
								Assignment = new StringLiteralExpression(from.Name.CamelCase())
							}
						)
				},
			};
		}

		object IConvertObject<DtoClassPropertyInfo>.Convert(DtoClassPropertyInfo from) => Convert(from);
	}
}