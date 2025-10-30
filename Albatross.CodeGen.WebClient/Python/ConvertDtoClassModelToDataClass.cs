using Albatross.CodeGen.Python;
using Albatross.CodeGen.Python.Declarations;
using Albatross.CodeGen.WebClient.Models;
using System.Linq;

namespace Albatross.CodeGen.WebClient.Python {
	public class ConvertDtoClassModelToDataClass : IConvertObject<DtoClassInfo, ClassDeclaration> {
		private readonly IConvertObject<DtoClassPropertyInfo, FieldDeclaration> propertyConverter;

		public ConvertDtoClassModelToDataClass(IConvertObject<DtoClassPropertyInfo, FieldDeclaration> propertyConverter) {
			this.propertyConverter = propertyConverter;
		}

		public ClassDeclaration Convert(DtoClassInfo from) {
			var declaration = new ClassDeclaration(from.Name) {
				Fields = from.Properties.Select(x => propertyConverter.Convert(x)).ToList(),
				BaseClassName = Defined.Identifiers.PydanticBaseModel,
			};
			return declaration;
		}

		object IConvertObject<DtoClassInfo>.Convert(DtoClassInfo from) => Convert(from);
	}
}