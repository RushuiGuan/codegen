using System.Collections.Generic;
using System.IO;

namespace Albatross.CodeGen.TypeScript.Expressions {
	public record class JsonValueExpression : CodeNode, IExpression {
		public JsonValueExpression(params IEnumerable<JsonPropertyExpression> properties) {
			Properties = new ListOfNodes<JsonPropertyExpression> {
				Prefix = "{",
				PostFix = "}",
				LeftPadding = " ",
				RightPadding = " ",
			};
			this.Properties.Add(properties);
		}

		public ListOfNodes<JsonPropertyExpression> Properties { get; }
		public override TextWriter Generate(TextWriter writer) => writer.Code(Properties);
		public override IEnumerable<ICodeNode> Children => [Properties];
	}
}