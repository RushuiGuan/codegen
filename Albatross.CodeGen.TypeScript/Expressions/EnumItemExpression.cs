using Albatross.Collections;
using Albatross.Text;
using System;
using System.Collections.Generic;
using System.IO;

namespace Albatross.CodeGen.TypeScript.Expressions {
	public record class EnumItemExpression : CodeNode, IExpression {
		public EnumItemExpression(string name) {
			this.Identifier = new IdentifierNameExpression(name);
		}
		public IdentifierNameExpression Identifier { get; }
		public CodeNode? Expression { get; init; }

		public override IEnumerable<ICodeNode> Children => new List<ICodeNode> { Identifier, }.AddIfNotNull(Expression);


		public override TextWriter Generate(TextWriter writer) {
			if (Expression == null) {
				writer.Code(Identifier);
			} else {
				writer.Code(Identifier).Append(" = ").Code(Expression);
			}
			return writer;
		}
	}
}