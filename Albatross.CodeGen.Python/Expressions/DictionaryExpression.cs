using Albatross.CodeGen.Syntax;
using Albatross.Text;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Albatross.CodeGen.Python.Expressions {
	public record class DictionaryExpression : CodeNode, IExpression {
		public DictionaryExpression(params KeyValuePairExpression[] properties) {
			Properties = new ListOfNodes<KeyValuePairExpression>(properties);
		}

		public bool LineBreak { get; set; }

		public DictionaryExpression(IEnumerable<KeyValuePairExpression> properties) : this(properties.ToArray()) { }

		public ListOfNodes<KeyValuePairExpression> Properties { get; }

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