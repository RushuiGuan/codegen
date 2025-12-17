using Albatross.CodeGen.Syntax;
using System.Collections.Generic;

namespace Albatross.CodeGen.CSharp.Declarations {
	public record class ListOfParameterDeclarations : ListOfNodes<ParameterDeclaration>, IExpression {
		public ListOfParameterDeclarations(params IEnumerable<ParameterDeclaration> items) : base(items) {
			Prefix = "(";
			PostFix = ")";
		}
	}
}