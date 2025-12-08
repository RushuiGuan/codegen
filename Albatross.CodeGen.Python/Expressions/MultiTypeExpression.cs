using Albatross.CodeGen.Syntax;
using System.Collections.Generic;
using System.IO;

namespace Albatross.CodeGen.Python.Expressions {
	public record class MultiTypeExpression : ListOfSyntaxNodes<ITypeExpression>, ITypeExpression {
		public MultiTypeExpression(params IEnumerable<ITypeExpression> nodes) : base(nodes) {
			Separator = " | ";
		}
	}
}