using Albatross.Text;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Albatross.CodeGen.TypeScript.Expressions {
	public record class ImportCollection : CodeNode, IExpression {
		public IEnumerable<ImportExpression> Imports { get; }

		public ImportCollection(IEnumerable<ImportExpression> imports) {
			Imports = imports.GroupBy(x => x.Source)
			.Select(x => new ImportExpression(x.SelectMany(y => y.Items)) {
				Source = x.Key,
			}).OrderBy(x => x.Source.Source);
		}

		public ImportCollection(IEnumerable<ICodeNode> nodes) {
			Imports = nodes.OfType<QualifiedIdentifierNameExpression>()
				.GroupBy(x => x.Source)
				.Select(x => new ImportExpression(x.Select(y => y.Identifier)) {
					Source = x.Key,
				}).OrderBy(x => x.Source.Source);
		}
		public override TextWriter Generate(TextWriter writer) {
			writer.WriteItems(Imports, "", (writer, t) => writer.Code(t), null, null);
			return writer;
		}

		public override IEnumerable<ICodeNode> Children => Imports;
	}
}