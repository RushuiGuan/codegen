using System.Collections.Generic;

namespace Albatross.CodeGen.CSharp.Declarations {
	public record class ListOfParameterDeclarations : ListOfNodes<ParameterDeclaration>, IExpression {
		public ListOfParameterDeclarations() {
			Prefix = "(";
			PostFix = ")";
		}
	}
}