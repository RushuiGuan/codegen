using Albatross.CodeAnalysis;
using Albatross.CodeGen.Python;
using Albatross.CodeGen.Python.Declarations;
using Albatross.CodeGen.Python.Expressions;
using Albatross.CodeGen.WebClient.Models;
using Albatross.CodeGen.WebClient.Settings;
using Albatross.Text;
using Humanizer;
using Microsoft.CodeAnalysis;

namespace Albatross.CodeGen.WebClient.Python {
	public class ConvertDtoClassPropertyModelToFieldDeclaration : IConvertObject<DtoClassPropertyInfo, FieldDeclaration> {
		private readonly Compilation compilation;
		private readonly PythonWebClientSettings settings;
		private readonly IConvertObject<ITypeSymbol, ITypeExpression> typeConverter;

		public ConvertDtoClassPropertyModelToFieldDeclaration(Compilation compilation, PythonWebClientSettings settings, IConvertObject<ITypeSymbol, ITypeExpression> typeConverter) {
			this.compilation = compilation;
			this.settings = settings;
			this.typeConverter = typeConverter;
		}

		public FieldDeclaration Convert(DtoClassPropertyInfo from) {
			string name;
			if (settings.PropertyNameMapping.TryGetValue(from.FullName, out var mappedName)) {
				name = mappedName.Underscore();
			} else {
				name = from.Name.Underscore();
			}
			return new FieldDeclaration(name) {
				Type = typeConverter.Convert(from.PropertyType),
				Initializer = new InvocationExpression {
					CallableExpression = Defined.Identifiers.PydanticField,
					Arguments = new ListOfNodes<IExpression>(
						new ScopedVariableExpression {
							Identifier = new IdentifierNameExpression("alias"),
							Assignment = new StringLiteralExpression(from.Name.CamelCase())
						},
						GetDefaultExpression(from))
				}
			};
		}

		IExpression GetDefaultExpression(DtoClassPropertyInfo from) {
			if (from.PropertyType.IsCollection(compilation)) {
				return new ScopedVariableExpression {
					Identifier = new IdentifierNameExpression("default_factory"),
					Assignment = Defined.Identifiers.List
				};
			} else {
				IExpression assignment;
				if (from.PropertyType.IsNullable(compilation)) {
					assignment = new NoneLiteralExpression();
				} else if (from.PropertyType.SpecialType == SpecialType.System_Boolean) {
					assignment = new BooleanLiteralExpression(false);
				} else {
					return new NoOpExpression();
				}
				return new ScopedVariableExpression {
					Identifier = new IdentifierNameExpression("default"),
					Assignment = assignment,
				};
			}
		}

		object IConvertObject<DtoClassPropertyInfo>.
			Convert(DtoClassPropertyInfo from) => Convert(from);
	}
}