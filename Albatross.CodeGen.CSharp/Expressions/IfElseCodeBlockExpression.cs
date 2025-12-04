using Albatross.CodeGen.Syntax;
using Albatross.Collections;
using Albatross.Text;
using System.Collections.Generic;
using System.IO;

namespace Albatross.CodeGen.CSharp.Expressions {
	public record class IfElseCodeBlockExpression : ISyntaxNode, IExpression {
		public required IExpression Condition { get; init; }
		public required IExpression CodeBlock { get; init; }
		public IExpression? ElseBlock { get; init; }

		public  TextWriter Generate(TextWriter writer) {
			using (var mainScope = writer.Append("if ").OpenParenthesis().Code(Condition).CloseParenthesis().BeginScope()) {
				mainScope.Writer.Code(CodeBlock);
			}
			if (ElseBlock != null) {
				using (var elseScope = writer.Append(" else ").BeginScope()) {
					elseScope.Writer.Code(ElseBlock);
				}
			}
			return writer;
		}

		public IEnumerable<ISyntaxNode> GetDescendants() => new List<ISyntaxNode> {
			Condition, CodeBlock
		}.AddIfNotNull(ElseBlock);
	}
}