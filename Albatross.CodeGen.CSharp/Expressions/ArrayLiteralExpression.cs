using Albatross.CodeGen.Syntax;
using Albatross.Text;
using System.Collections.Generic;
using System.IO;

namespace Albatross.CodeGen.CSharp.Expressions {
	public record class ArrayLiteralExpression : LiteralExpression {
		public ListOfSyntaxNodes<IExpression> Items { get; init; } = new ListOfSyntaxNodes<IExpression>();
		public override TextWriter Generate(TextWriter writer) {
			writer.Append("[").Code(Items).Append("]");
			return writer;
		}
		public override IEnumerable<ISyntaxNode> Children => [Items];
	}
}