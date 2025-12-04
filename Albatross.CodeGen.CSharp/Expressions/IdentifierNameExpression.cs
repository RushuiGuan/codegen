using Albatross.CodeGen.Syntax;
using Albatross.Text;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace Albatross.CodeGen.CSharp.Expressions {
	public record class IdentifierNameExpression : SyntaxNode, IIdentifierNameExpression {
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
		public override IEnumerable<ISyntaxNode> Children => [];
		public string Name { get; }

		public override TextWriter Generate(TextWriter writer)
			=> writer.Append(Name);
	}
}