using Albatross.CodeGen.Syntax;
using Albatross.Text;
using System.Collections.Generic;
using System.IO;

namespace Albatross.CodeGen.Python.Expressions {
	public record class ArrayLiteralExpression : LiteralExpression {
		public ListOfNodes<IExpression> Items { get; init; } = new ListOfNodes<IExpression>();
		public override TextWriter Generate(TextWriter writer) {
			writer.Append("[").Code(Items).Append("]");
			return writer;
		}
		public override IEnumerable<ICodeNode> Children => [Items];
	}
}