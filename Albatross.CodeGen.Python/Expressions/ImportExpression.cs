using Albatross.Text;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Albatross.CodeGen.Python.Expressions {
	public record class ImportExpression : CodeNode, IExpression {
		public ImportExpression(params IEnumerable<IdentifierNameExpression> symbols) {
			var unique = symbols.Distinct(EqualityComparer<IdentifierNameExpression>.Default).ToArray();
			this.Symbols = new ListOfNodes<IdentifierNameExpression>(symbols.Distinct(EqualityComparer<IdentifierNameExpression>.Default));
		}

		public ListOfNodes<IdentifierNameExpression> Symbols { get; }
		public required ISourceExpression Source { get; init; }

		public override TextWriter Generate(TextWriter writer) {
			if (Symbols.Any()) {
				writer.Append("from ").Code(Source).Append(" import ").WriteItems(Symbols, ", ", (w, x) => w.Code(x));
			} else {
				writer.Append("import ").Code(Source);
			}
			return writer;
		}
		public override IEnumerable<ICodeNode> Children => [Symbols, Source];
	}
}