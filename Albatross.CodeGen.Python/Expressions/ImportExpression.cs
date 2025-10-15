using Albatross.CodeGen.Syntax;
using Albatross.Text;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Albatross.CodeGen.Python.Expressions {
	public record class ImportExpression : SyntaxNode, ICodeElement {
		public ImportExpression(IEnumerable<IdentifierNameExpression> symbols) {
			this.Symbols = new ListOfSyntaxNodes<IdentifierNameExpression>(symbols.Distinct());
		}
		public ListOfSyntaxNodes<IdentifierNameExpression> Symbols { get; }
		public required ISourceExpression Source { get; init; }
		public override IEnumerable<ISyntaxNode> Children => [Symbols, Source];
		// import {format, parse} from 'date-fns';
		public override TextWriter Generate(TextWriter writer) {
			if(Symbols.Any()) {
				writer.Append("from ").Code(Source).Append(" import ").WriteItems(Symbols, ",", (w, x) => w.Code(x));
			}else{
				writer.Append("import ").Code(Source);
			}
			return writer;
		}
	}
}