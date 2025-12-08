using Albatross.CodeGen.Syntax;
using System.Collections.Generic;

namespace Albatross.CodeGen.TypeScript.Expressions {
	public record class MultiTypeExpression : ListOfSyntaxNodes<ITypeExpression>, ITypeExpression {
		public MultiTypeExpression(params IEnumerable<ITypeExpression> nodes) : base(nodes) {
			this.Separator = "|";
		}
	}
}