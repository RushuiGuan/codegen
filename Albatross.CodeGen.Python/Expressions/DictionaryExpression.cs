using Albatross.Text;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Albatross.CodeGen.Python.Expressions {
	public record class DictionaryExpression : CodeNode, IExpression {
		public DictionaryExpression(params IEnumerable<KeyValuePairExpression> properties) {
			Properties.Add(properties);
		}
		public ListOfNodes<KeyValuePairExpression> Properties { get; } = new ListOfNodes<KeyValuePairExpression>();
		public bool LineBreak { get; set; }

		public override TextWriter Generate(TextWriter writer) {
			if (LineBreak) {
				using var scope = writer.BeginPythonLineBreak("{", "}");
				scope.Writer.Code(Properties);
			} else {
				writer.Append("{").Code(Properties).Append("}");
			}
			return writer;
		}

		public override IEnumerable<ICodeNode> Children => [Properties];
	}
}