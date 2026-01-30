using Albatross.Text;
using System.Collections.Generic;
using System.IO;

namespace Albatross.CodeGen.CSharp.Expressions {
	public record class DynamicTypeExpression : CodeNode, ITypeExpression {
		public override IEnumerable<ICodeNode> Children => [];
		public override TextWriter Generate(TextWriter writer) {
			return writer.Append("dynamic");
		}
	}
}