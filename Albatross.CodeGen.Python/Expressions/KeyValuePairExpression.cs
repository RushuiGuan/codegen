using Albatross.CodeGen.Syntax;
using Albatross.Text;
using System.Collections.Generic;
using System.IO;

namespace Albatross.CodeGen.Python.Expressions {
	public record class KeyValuePairExpression : CodeNode, IExpression {
		public IExpression Key { get; }
		public IExpression Value { get; }
		public override IEnumerable<ICodeNode> Children => [Key, Value];

		public KeyValuePairExpression(IExpression key, IExpression value) {
			this.Key = key;
			this.Value = value;
		}

		public KeyValuePairExpression(string key, string value) : this(new StringLiteralExpression(key), new StringLiteralExpression(value)) { }

		public override TextWriter Generate(TextWriter writer) {
			return writer.Code(Key).Append(": ").Code(Value);
		}
	}
}