using Albatross.CodeGen.CSharp.Declarations;
using Albatross.Text;
using System.Collections.Generic;
using System.IO;

namespace Albatross.CodeGen.CSharp.Expressions {
	public record class ForEachExpression : CodeNode, ICodeBlock{
		public required VariableDeclaration IterationVariable { get; init; }
		public required IExpression Collection { get; init; }
		public CodeBlock Body { get; } = new CSharpCodeBlock();

		public override TextWriter Generate(TextWriter writer)
			=> writer.Code(Defined.Keywords.ForEach)
				.OpenParenthesis()
				.Code(IterationVariable)
				.Space()
				.Code(Defined.Keywords.In)
				.Code(Collection)
				.CloseParenthesis()
				.Code(Body);
		public override IEnumerable<ICodeNode> Children => [IterationVariable, Collection, Body];
	}
}