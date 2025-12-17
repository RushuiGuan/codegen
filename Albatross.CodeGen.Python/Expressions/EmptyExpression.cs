using Albatross.CodeGen.Syntax;
using System.Collections.Generic;
using System.IO;

namespace Albatross.CodeGen.Python.Expressions {
	public record class EmptyExpression : CodeNode, IExpression {
		public override IEnumerable<ICodeNode> Children => [];
		public override TextWriter Generate(TextWriter writer) {
			return writer;
		}
	}
}