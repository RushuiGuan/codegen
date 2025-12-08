using Albatross.Text;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Albatross.CodeGen.Syntax {
	/// <summary>
	/// a node that's made of other nodes, each will be rendered on its own line
	/// the expression will skip any NewLineExpression and NoOpExpression nodes
	/// </summary>
	public record class CodeBlock : SyntaxNode, IExpression {
		public string LineTerminator { get; init; } = string.Empty;
		public IEnumerable<IExpression> Items { get; init; }
		
		public CodeBlock(params IEnumerable<IExpression> items) {
			Items = items;
		}

		public override TextWriter Generate(TextWriter writer) {
			writer.WriteItems(Items.Where(x => !(x is NoOpExpression)), "\n", (w, x) => {
				if (x is not NewLineExpression) {
					w.Code(x).Append(LineTerminator);
				}
			});
			return writer;
		}

		public override IEnumerable<ISyntaxNode> Children => Items;
	}
}