using Albatross.Text;
using System.Collections.Generic;
using System.IO;

namespace Albatross.CodeGen.CSharp.Expressions {
	public record class SwitchSection : CodeNode, IExpression {
		public required LiteralExpression Value { get; init; }
		public required IExpression Expression { get; init; }

		public override TextWriter Generate(TextWriter writer)
			=> writer.Code(Value).Append(" => ").Code(Expression);

		public override IEnumerable<ICodeNode> Children => [Value, Expression];
	}
	public record class SwitchExpression : CodeNode, IExpression {
		public required IIdentifierNameExpression Variable { get; init; }
		public ListOfNodes<SwitchSection> Sections { get; init; } = new();
		public override TextWriter Generate(TextWriter writer) {
			using var scope = writer.Code(Variable).Space().BeginScope("switch");
			scope.Writer.WriteItems(Sections, ",\n", (w, s) => w.Code(s));
			return writer;
		}
		public override IEnumerable<ICodeNode> Children => [Variable, .. Sections];
	}
}
