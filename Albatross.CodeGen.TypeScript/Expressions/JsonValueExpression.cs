using Albatross.CodeGen.Syntax;
using Albatross.Text;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Albatross.CodeGen.TypeScript.Expressions {
	public record class JsonValueExpression : CodeNode, IExpression {
		public JsonValueExpression(params JsonPropertyExpression[] properties) {
			Properties = new ListOfNodes<JsonPropertyExpression>(properties) {
				LeftPadding = " ",
				RightPadding = " ",
			};
		}

		public JsonValueExpression(IEnumerable<JsonPropertyExpression> properties) : this(properties.ToArray()) { }

		public ListOfNodes<JsonPropertyExpression> Properties { get; }

		public override TextWriter Generate(TextWriter writer) {
			writer.Append("{").Code(Properties).Append("}");
			return writer;
		}

		public override IEnumerable<ICodeNode> Children => [Properties];
	}
}