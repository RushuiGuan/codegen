using Albatross.CodeGen.Python.Declarations;
using Albatross.CodeGen.Python.Expressions;
using Albatross.CodeGen.WebClient.Models;
using System.Linq;

namespace Albatross.CodeGen.WebClient.Python {
	public class ConvertEnumModelToPythonEnum : IConvertObject<EnumInfo, EnumDeclaration> {
		public EnumDeclaration Convert(EnumInfo from) {
			return new EnumDeclaration(from.Name) {
				Fields = from
					.Members
					.Select(x => new EnumMemberDeclaration(x.Name.ToUpperInvariant(), from.UseTextAsValue ? new StringLiteralExpression(x.Name) : new NumberLiteralExpression(x.NumericValue)))
			};
		}
		object IConvertObject<EnumInfo>.Convert(EnumInfo from) => this.Convert(from);
	}
}