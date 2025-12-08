using Albatross.CodeGen.Syntax;
using Albatross.Text;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Albatross.CodeGen.TypeScript.Expressions {
	public record class ImportExpression : SyntaxNode, ICodeElement {
		public ImportExpression(IEnumerable<IdentifierNameExpression> items) {
			this.Items = new ListOfSyntaxNodes<IdentifierNameExpression>(items.Distinct().OrderBy(x => x.Name)) {
				LeftPadding = "{ ",
				RightPadding = " }"
			};
		}

		public ListOfSyntaxNodes<IdentifierNameExpression> Items { get; }
		public required ISourceExpression Source { get; init; }

		public override IEnumerable<ISyntaxNode> Children => [Items, Source];

		// import {format, parse} from 'date-fns';
		public override TextWriter Generate(TextWriter writer) {
			if (Items.Any()) {
				writer.Append("import ").Code(Items).Append(" from ").Code(Source).Semicolon().WriteLine();
			}
			return writer;
		}
	}
}