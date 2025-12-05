using Albatross.CodeGen.Syntax;
using Albatross.Collections;
using Albatross.Text;
using System.Collections.Generic;
using System.IO;

namespace Albatross.CodeGen.CSharp.Expressions {
	public record class IfExpression : SyntaxNode, IExpression {
		public required IExpression Condition { get; init; }
		public required IExpression IfBlock { get; init; }
		public IExpression? ElseBlock { get; init; }

		public override TextWriter Generate(TextWriter writer) {
			using (var mainScope = writer.Code(Defined.Keywords.If).OpenParenthesis().Code(Condition).CloseParenthesis().BeginScope()) {
				mainScope.Writer.Code(IfBlock);
			}
			if (ElseBlock != null) {
				using (var elseScope = writer.Code(Defined.Keywords.Else).BeginScope()) {
					elseScope.Writer.Code(ElseBlock);
				}
			}
			return writer;
		}

		public override IEnumerable<ISyntaxNode> Children => new List<ISyntaxNode> {
			Condition, IfBlock
		}.AddIfNotNull(ElseBlock);
	}
}