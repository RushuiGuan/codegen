using Albatross.CodeGen.Syntax;
using Albatross.Text;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Albatross.CodeGen.TypeScript.Expressions {
	public record class MultiPartIdentifierNameExpression : SyntaxNode, IIdentifierNameExpression {
		public MultiPartIdentifierNameExpression(params IEnumerable<IIdentifierNameExpression> expressions) {
			this.Parts = expressions;
		}
		public MultiPartIdentifierNameExpression(params IEnumerable<string> names) {
			this.Parts = names.Select(x => new IdentifierNameExpression(x));
		}
		IEnumerable<IIdentifierNameExpression> Parts { get; }
		public ListOfGenericArguments GenericArguments { get; init; } = new();

		public override IEnumerable<ISyntaxNode> Children => Parts;
		public override TextWriter Generate(TextWriter writer) {
			writer.WriteItems(Parts, ".", (w, t) => w.Code(t));
			writer.Code(GenericArguments);
			return writer;
		}
	}
}