using Albatross.CodeGen.CSharp.Declarations;
using Albatross.CodeGen.Syntax;
using Albatross.Text;
using System;
using System.Collections.Generic;
using System.IO;

namespace Albatross.CodeGen.CSharp.Expressions {
	public record class ForeachExpression :SyntaxNode, IExpression {
		public required VariableDeclaration IterationVariable { get; init; }
		public required IExpression Collection { get; init; }
		public IExpression Body { get; init; } = new NoOpExpression();

		public override TextWriter Generate(TextWriter writer) {
			writer.Code(Defined.Keywords.ForEach).OpenParenthesis().Code(IterationVariable).Space().Code(Defined.Keywords.In).Code(Collection).CloseParenthesis();
			using var scope = writer.BeginScope();
			scope.Writer.Code(Body);
			return writer;
		}
		public override IEnumerable<ISyntaxNode> Children => [IterationVariable, Collection, Body];
	}
}
