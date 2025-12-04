using Albatross.CodeGen.Syntax;
using Albatross.Text;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Albatross.CodeGen.TypeScript.Expressions {
	public record class ImportCollection : ListOfSyntaxNodes<ImportExpression> {
		public ImportCollection(IEnumerable<ImportExpression> imports) {
			Nodes = imports.GroupBy(x => x.Source)
			.Select(x => new ImportExpression(x.SelectMany(y => y.Items)) {
				Source = x.Key,
			}).OrderBy(x => x.Source.Source);
		}

		public ImportCollection(IEnumerable<ISyntaxNode> nodes) {
			nodes = nodes.OfType<QualifiedIdentifierNameExpression>()
				.GroupBy(x => x.Source)
				.Select(x => new ImportExpression(x.Select(y => y.Identifier)) {
					Source = x.Key,
				}).OrderBy(x => x.Source.Source);
		}
		public override TextWriter Generate(TextWriter writer) {
			var sorted = this.OrderBy(x => x.Source.Source).ToArray();
			writer.WriteItems(sorted, "", (writer, t) => writer.Code(t), null, null);
			return writer;
		}
	}
}