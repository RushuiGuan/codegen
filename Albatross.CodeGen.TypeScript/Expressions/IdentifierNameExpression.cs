using Albatross.Text;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Albatross.CodeGen.TypeScript.Expressions {
	public record class IdentifierNameExpression : CodeNode, IIdentifierNameExpression {
		public IdentifierNameExpression(string name) {
			if (Defined.Patterns.IdentifierName.IsMatch(name)) {
				this.Name = name;
			} else {
				throw new ArgumentException($"Invalid identifier name {name}");
			}
		}

		public string Name { get; }
		public ListOfGenericArguments GenericArguments { get; init; } = new();

		public override TextWriter Generate(TextWriter writer) {
			writer.Append(Name).Code(GenericArguments);
			return writer;
		}

		public override IEnumerable<ICodeNode> Children => [GenericArguments];
	}
}