using Albatross.Text;
using System.Collections.Generic;
using System.IO;

namespace Albatross.CodeGen.Python.Expressions {
	public record class TupleExpression : CodeNode, IExpression {
		public IExpression[] Items { get; init; }
		public override IEnumerable<ICodeNode> Children => Items;
		public bool LineBreak { get; set; }

		public TupleExpression(params IExpression[] items) {
			this.Items = items;
		}

		public override TextWriter Generate(TextWriter writer) {
			writer.Append('(');
			if (LineBreak) {
				writer.AppendLine();
			}
			writer.WriteItems(Items, ", ", (w, item) => w.Code(item));
			if (LineBreak) {
				writer.AppendLine();
			}
			writer.Append(')');
			return writer;
		}
	}
}