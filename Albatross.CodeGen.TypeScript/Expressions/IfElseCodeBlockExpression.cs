using Albatross.Collections;
using Albatross.Text;
using System.Collections.Generic;
using System.IO;

namespace Albatross.CodeGen.TypeScript.Expressions {
	public record class IfElseCodeBlockExpression : CodeNode, IExpression {
		public IfElseCodeBlockExpression() { }

		public required IExpression Condition { get; init; }
		public IExpression CodeBlock { get; init; } = new NoOpExpression();
		public IExpression? ElseBlock { get; init; }

		public override IEnumerable<ICodeNode> Children => new List<ICodeNode> {
			Condition, CodeBlock
		}.AddIfNotNull(ElseBlock);

		public override TextWriter Generate(TextWriter writer) {
			using (var mainScope = writer.Append("if ").OpenParenthesis().Append(Condition).CloseParenthesis().BeginScope()) {
				mainScope.Writer.Code(CodeBlock);
			}
			if (ElseBlock != null) {
				using (var elseScope = writer.Append(" else").BeginScope()) {
					elseScope.Writer.Code(ElseBlock);
				}
			}
			return writer;
		}
	}
}