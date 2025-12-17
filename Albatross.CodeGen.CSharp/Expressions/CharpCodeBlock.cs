using System.Collections.Generic;

namespace Albatross.CodeGen.CSharp.Expressions {
	public record class CSharpCodeBlock : CodeBlock {
		public CSharpCodeBlock(params IEnumerable<IExpression> items) : base(items) {
			NodePostfix = ";";
		}
	}
}