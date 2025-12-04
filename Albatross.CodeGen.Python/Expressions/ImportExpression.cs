using Albatross.CodeGen.Syntax;
using Albatross.Text;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Albatross.CodeGen.Python.Expressions {
	public record class ImportExpression : IExpression {
		public ImportExpression(IEnumerable<IdentifierNameExpression> symbols) {
			this.Symbols = new ListOfSyntaxNodes<IdentifierNameExpression> {
				Nodes = symbols.Distinct(),
			};
		}
		public ListOfSyntaxNodes<IdentifierNameExpression> Symbols { get; }
		public required ISourceExpression Source { get; init; }
		public TextWriter Generate(TextWriter writer) {
			if(Symbols.Any()) {
				writer.Append("from ").Code(Source).Append(" import ").WriteItems(Symbols, ", ", (w, x) => w.Code(x));
			}else{
				writer.Append("import ").Code(Source);
			}
			return writer;
		}

		public IEnumerable<ISyntaxNode> GetDescendants() => [Symbols, Source];
	}
}