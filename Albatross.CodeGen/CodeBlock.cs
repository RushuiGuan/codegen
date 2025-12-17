using System.Collections.Generic;

namespace Albatross.CodeGen {
	/// <summary>
	/// a node that's made of other nodes, each will be rendered on its own line
	/// the expression will skip any NewLineExpression and NoOpExpression nodes
	/// </summary>
	public record class CodeBlock : ListOfNodes<IExpression>, IExpression {
		public CodeBlock(params IEnumerable<IExpression> items) : base(items) {
			Separator = "\n";
		}
	}
}