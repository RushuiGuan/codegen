using Albatross.CodeGen.Syntax;
using Albatross.Text;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Albatross.CodeGen.Python.Expressions {
	public record class DictionaryValueExpression : SyntaxNode, IExpression {
		public DictionaryValueExpression(params KeyValuePairExpression[] properties) {
			Properties = new ListOfSyntaxNodes<KeyValuePairExpression>(properties) {
				Padding = " "
			};
		}

		public DictionaryValueExpression(IEnumerable<KeyValuePairExpression> properties) : this(properties.ToArray()) { }

		public ListOfSyntaxNodes<KeyValuePairExpression> Properties { get; }

		public override TextWriter Generate(TextWriter writer) {
			writer.Append("{").Code(Properties).Append("}");
			return writer;
		}

		public override IEnumerable<ISyntaxNode> Children => [Properties];
	}
}