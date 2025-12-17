using System.Collections.Generic;

namespace Albatross.CodeGen.TypeScript.Expressions {
	public record class ListOfEnumItems : ListOfNodes<EnumItemExpression> {
		public ListOfEnumItems(IEnumerable<EnumItemExpression> items) : base(items) {
			this.Separator = ",\n";
		}
	}
}