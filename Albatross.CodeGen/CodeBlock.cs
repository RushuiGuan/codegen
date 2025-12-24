using System.Collections.Generic;

namespace Albatross.CodeGen {
	public interface ICodeBlock : IExpression { }
	/// <summary>
	/// a node that's made of other nodes, each will be rendered on its own line
	/// the expression will skip any NewLineExpression and NoOpExpression nodes
	/// </summary>
	public record class CodeBlock : ListOfNodes<IExpression>, ICodeBlock {
		public CodeBlock() {
			Separator = "";
			Multiline = true;
		}
	}
}