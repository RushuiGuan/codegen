using Albatross.CodeGen.Syntax;
using Albatross.Text;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Albatross.CodeGen.CSharp.Expressions {
	public class ImportCollection : ICodeElement {
		private IEnumerable<ImportExpression> items;

		public ImportCollection(IEnumerable<ImportExpression> imports, IEnumerable<ISyntaxNode> dependencies) {
			this.items = dependencies.OfType<QualifiedIdentifierNameExpression>().Select(x => new ImportExpression(x.Source))
				.Union(imports)
				.OrderBy(x => x.Namespace.Source)
				.ToArray();
		}

		public TextWriter Generate(TextWriter writer) {
			foreach (var import in items) {
				writer.Code(import).AppendLine();
			}
			return writer;
		}
	}
}