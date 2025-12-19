using Albatross.Text;
using System.Collections.Generic;
using System.IO;

namespace Albatross.CodeGen.CSharp.Expressions {
	public record class LamdaExpression : CodeNode, IExpression {
		public required IExpression Parameter { get; init; }
		public required IExpression Body { get; init; }
		public override TextWriter Generate(TextWriter writer) {
			return writer.Code(Parameter).Append(" => ").Code(Body);
		}
		public override IEnumerable<ICodeNode> Children => [Parameter, Body];
	}
}
