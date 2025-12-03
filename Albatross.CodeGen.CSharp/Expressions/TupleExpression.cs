using Albatross.CodeGen.Syntax;
using Albatross.Text;
using System.Collections.Generic;
using System.IO;

namespace Albatross.CodeGen.CSharp.Expressions {
	public record class TupleExpression : SyntaxNode, IExpression {
		public IExpression[] Items { get; init; }
		public override IEnumerable<ISyntaxNode> Children => Items;
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