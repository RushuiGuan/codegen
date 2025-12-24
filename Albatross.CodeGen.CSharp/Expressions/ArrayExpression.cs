using Albatross.Text;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Albatross.CodeGen.CSharp.Expressions {
	public record class ArrayExpression : CodeNode, IExpression {
		public ArrayExpression() {
			Items = new ListOfNodes<IExpression> {
				Prefix = "{",
				PostFix = "}",
				LeftPadding = " ",
				RightPadding = " "
			};
		}
		public bool Multiline { get; init; }
		public required ITypeExpression Type { get; init; }
		public ListOfNodes<IExpression> Items { get; }
		public override TextWriter Generate(TextWriter writer)
			=> writer.Code(Defined.Keywords.New).Code(Type).Append("[]").Space().Code(Items);
		public override IEnumerable<ICodeNode> Children => [Type, Items];
	}
}
