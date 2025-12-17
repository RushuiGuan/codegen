using Albatross.CodeGen.Syntax;
using Albatross.Text;
using System.Collections.Generic;
using System.IO;

namespace Albatross.CodeGen.Python.Expressions {
	public record class ListExpression : CodeNode, IExpression {
		public IExpression[] Items { get; init; }
		public override IEnumerable<ICodeNode> Children => Items;

		public ListExpression(params IExpression[] items) {
			this.Items = items;
		}

		public override TextWriter Generate(TextWriter writer)
			=> writer.Append('[').WriteItems(Items, ", ", (w, item) => w.Code(item)).Append(']');
	}
}