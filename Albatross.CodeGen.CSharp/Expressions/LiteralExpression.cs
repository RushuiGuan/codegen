using Albatross.CodeGen.Syntax;
using System.Collections.Generic;

namespace Albatross.CodeGen.CSharp.Expressions {
	public abstract record class LiteralExpression : SyntaxNode, IExpression {
		public override IEnumerable<ISyntaxNode> Children => [];
	}
}