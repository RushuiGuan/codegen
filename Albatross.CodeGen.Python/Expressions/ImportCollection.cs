using Albatross.CodeGen.Syntax;
using Albatross.Text;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Albatross.CodeGen.Python.Expressions {
	public record class ImportCollection : CodeNode, IExpression {
		public IEnumerable<ImportExpression> Imports { get; }

		public ImportCollection(params IEnumerable<ImportExpression> imports) {
			this.Imports = imports.GroupBy(x => x.Source)
				.Select(x => new ImportExpression(x.SelectMany(y => y.Symbols).Distinct(EqualityComparer<IdentifierNameExpression>.Default)) {
					Source = x.Key,
				}).OrderBy(x => x.Source.Source).ToArray();
		}

		public ImportCollection(params IEnumerable<QualifiedIdentifierNameExpression> nodes) {
			this.Imports = nodes.GroupBy(x => x.Source)
				.Select(x => new ImportExpression(x) {
					Source = x.Key,
				})
				.OrderBy(x => x.Source.Source).ToArray();
		}

		public override TextWriter Generate(TextWriter writer) {
			writer.WriteItems(Imports, "", (w, t) => w.Code(t).AppendLine(), null, null);
			return writer;
		}

		public override IEnumerable<ICodeNode> Children => Imports;
	}
}