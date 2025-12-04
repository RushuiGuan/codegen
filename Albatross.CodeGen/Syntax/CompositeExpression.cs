using Albatross.Text;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Albatross.CodeGen.Syntax {
	/// <summary>
	/// a node that's made of other nodes, each will be rendered on its own line
	/// </summary>
	public record class CompositeExpression : SyntaxNode, IExpression {
		public CompositeExpression(params IEnumerable<ISyntaxNode> items) {
			Items = items;
		}
		public IEnumerable<ISyntaxNode> Items { get; init; }
		public override TextWriter Generate(TextWriter writer) {
			writer.WriteItems(Items.Where(x => !(x is NoOpExpression)), "\n", (w, x) => w.Code(x));
			return writer;
		}

		public override IEnumerable<ISyntaxNode> Children => Items;
	}
	public class CompositeExpressionBuilder : SyntaxNodeBuilder<ISyntaxNode> {
		public override ISyntaxNode Build() => new NoOpExpression();
	}
}