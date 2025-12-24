using Albatross.Collections;
using Albatross.Text;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Albatross.CodeGen.CSharp.Expressions {
	public record class IfExpression : CodeNode, ICodeBlock {
		public required IExpression Condition { get; init; }
		public CodeBlock IfBlock { get;  } = new CSharpCodeBlock();
		public CodeBlock ElseBlock { get; } = new CSharpCodeBlock();

		public override TextWriter Generate(TextWriter writer) {
			writer.Code(Defined.Keywords.If).OpenParenthesis().Code(Condition).CloseParenthesis().Code(IfBlock);
			if (ElseBlock.Any()) {
				writer.Code(Defined.Keywords.Else).Code(ElseBlock);
			}
			return writer;
		}

		public override IEnumerable<ICodeNode> Children => new List<ICodeNode> {
			Condition, IfBlock
		}.AddIfNotNull(ElseBlock);
	}
}