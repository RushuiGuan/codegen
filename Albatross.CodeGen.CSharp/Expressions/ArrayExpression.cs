using Albatross.Text;
using System.Collections.Generic;
using System.IO;

namespace Albatross.CodeGen.CSharp.Expressions {
	public record class ArrayExpression : CodeNode, IExpression {
		public ArrayExpression(bool multiline = false) {
			Items = new ListOfNodes<IExpression> {
				Multiline = multiline,
				Prefix = "{",
				PostFix = "}",
				LeftPadding = multiline ? "" : " ",
				RightPadding = multiline ? "" : " ",
				Separator = multiline ? "," : ", "
			};
		}
		public required ITypeExpression Type { get; init; }
		public ListOfNodes<IExpression> Items { get; }
		public override TextWriter Generate(TextWriter writer)
			=> writer.Code(Defined.Keywords.New).Code(Type).Append("[]").Space().Code(Items);
		public override IEnumerable<ICodeNode> Children => [Type, Items];
	}
}
