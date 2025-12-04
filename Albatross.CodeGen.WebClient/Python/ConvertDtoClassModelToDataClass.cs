using Albatross.CodeGen.Python;
using Albatross.CodeGen.Python.Declarations;
using Albatross.CodeGen.Python.Expressions;
using Albatross.CodeGen.WebClient.Models;
using Albatross.CodeGen.WebClient.Settings;
using System.Collections.Generic;
using System.Linq;

namespace Albatross.CodeGen.WebClient.Python {
	public class ConvertDtoClassModelToDataClass : IConvertObject<DtoClassInfo, ClassDeclaration> {
		private readonly IConvertObject<DtoClassPropertyInfo, FieldDeclaration> propertyConverter;
		private readonly PythonWebClientSettings settings;

		public ConvertDtoClassModelToDataClass(CodeGenSettings settings, IConvertObject<DtoClassPropertyInfo, FieldDeclaration> propertyConverter) {
			this.propertyConverter = propertyConverter;
			this.settings = settings.PythonWebClientSettings;
		}

		public ClassDeclaration Convert(DtoClassInfo from) {
			if (!settings.DtoClassNameMapping.TryGetValue(from.FullName, out var className)) {
				className = from.Name;
			}
			var fields = new List<FieldDeclaration>{
				modelConfig
			};
			if (!string.IsNullOrEmpty(from.TypeDiscriminator)) {
				fields.Add(new FieldDeclaration("discriminator_") {
					Type = Defined.Types.String,
					Initializer = new InvocationSyntaxNodeBuilder()
						.WithCallableExpression(Defined.Identifiers.PydanticField)
						.AddArgument(new ScopedVariableExpression {
							Identifier = new IdentifierNameExpression("alias"),
							Assignment = new StringLiteralExpression("$type")
						}).AddArgument(new ScopedVariableExpression {
							Identifier = new IdentifierNameExpression("default"),
							Assignment = new StringLiteralExpression(from.TypeDiscriminator)
						}).Build()
				});
			}
			fields.AddRange(from.Properties.Select(propertyConverter.Convert));
			var declaration = new ClassDeclaration(className) {
				Fields = fields,
				BaseClassName = Defined.Identifiers.PydanticBaseModel,
			};
			return declaration;
		}

		FieldDeclaration modelConfig = new FieldDeclaration("model_config") {
			Initializer = new InvocationSyntaxNodeBuilder()
				.WithCallableExpression(new QualifiedIdentifierNameExpression("ConfigDict", Defined.Sources.Pydantic))
				.AddArgument(new ScopedVariableExpression {
					Identifier = new IdentifierNameExpression("populate_by_name"),
					Assignment = Defined.Literals.BooleanLiteral(true),
				}).AddArgument(new ScopedVariableExpression {
					Identifier = new IdentifierNameExpression("serialize_by_alias"),
					Assignment = Defined.Literals.BooleanLiteral(true),
				}).Build()
		};

		object IConvertObject<DtoClassInfo>.Convert(DtoClassInfo from) => Convert(from);
	}
}