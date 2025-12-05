using Albatross.CodeGen.Syntax;
using System.Collections.Generic;
using System.IO;

namespace Albatross.CodeGen.CSharp.Expressions {
	public record class AssignmentExpression : IExpression{
		public required IExpression Left { get; init; }
		public required IExpression Expression { get; init; }

		public TextWriter Generate(TextWriter writer)
			=> writer.Code(Left).Code(Defined.Operators.Assignment).Code(Expression);

		public IEnumerable<ISyntaxNode> GetDescendants()
			=> [Left, Expression];
	}
}