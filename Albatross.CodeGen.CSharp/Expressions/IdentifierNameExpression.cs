using Albatross.Text;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace Albatross.CodeGen.CSharp.Expressions {
	public record class IdentifierNameExpression : CodeNode, IIdentifierNameExpression {
		public static readonly Regex IdentifierName = new Regex(@"^
			(?:[a-z_][a-z0-9_]*)
			(?:\.[a-z_][a-z0-9_]*)*
			$", RegexOptions.Compiled | RegexOptions.IgnoreCase | RegexOptions.IgnorePatternWhitespace);

		public IdentifierNameExpression(string name) {
			if (IdentifierName.IsMatch(name)) {
				this.Name = name;
			} else {
				throw new ArgumentException($"Invalid identifier name {name}");
			}
		}
		public string Name { get; }
		public ListOfGenericArguments GenericArguments { get; init; } = new();
		public override TextWriter Generate(TextWriter writer) => writer.Append(Name).Code(GenericArguments);
		public override IEnumerable<ICodeNode> Children => [GenericArguments];
	}
}