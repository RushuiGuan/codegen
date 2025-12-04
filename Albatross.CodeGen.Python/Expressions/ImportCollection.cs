using Albatross.CodeGen.Syntax;
using Albatross.Text;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Albatross.CodeGen.Python.Expressions {
	public record class ImportCollection : ListOfSyntaxNodes<ImportExpression> {
		public ImportCollection(IEnumerable<ImportExpression> imports) {
			this.Nodes = imports.GroupBy(x => x.Source).Select(x => new ImportExpression(x.SelectMany(y => y.Symbols)) {
				Source = x.Key,
			});
		}

		public ImportCollection(IEnumerable<ISyntaxNode> nodes) {
			this.Nodes = nodes.OfType<QualifiedIdentifierNameExpression>()
				.GroupBy(x => x.Source)
				.Select(x => new ImportExpression(x.Select(y => y.Identifier)) {
					Source = x.Key,
				}).OrderBy(x => x.Source);
		}
		public override TextWriter Generate(TextWriter writer) {
			var sorted = this.OrderBy(x => x.Source.ToString()).ToArray();
			writer.WriteItems(sorted, "", (writer, t) => writer.Code(t).AppendLine(), null, null);
			return writer;
		}
	}
}