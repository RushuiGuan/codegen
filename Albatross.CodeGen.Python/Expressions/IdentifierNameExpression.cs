using Albatross.CodeGen.Syntax;
using Albatross.Text;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace Albatross.CodeGen.Python.Expressions {
	public record class IdentifierNameExpression : SyntaxNode, IIdentifierNameExpression {
		public static readonly Regex IdentifierName = new Regex(@"^[a-z_][a-z0-9_]*$", RegexOptions.Compiled | RegexOptions.IgnoreCase);

		public IdentifierNameExpression(string name) {
			if (IdentifierName.IsMatch(name)) {
				this.Name = name;
			} else {
				throw new ArgumentException($"Invalid identifier name {name}");
			}
		}

		public bool ForwardReference { get; init; }
		public string Name { get; }
		public ListGenericArguments GenericArguments { get; init; } = new();

		public override TextWriter Generate(TextWriter writer) {
			if (ForwardReference) { writer.Append("'"); }
			writer.Append(Name).Code(GenericArguments);
			if (ForwardReference) { writer.Append("'"); }
			return writer;
		}

		public override IEnumerable<ISyntaxNode> Children => [GenericArguments];
	}
}