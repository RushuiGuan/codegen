using System;
using System.Collections.Generic;
using System.IO;

namespace Albatross.CodeGen.CSharp.Expressions {
	public record class NamespaceExpression : CodeNode, ISourceExpression {
		public IdentifierNameExpression Name { get; init; }
		public string Source => this.Name.Name;

		public NamespaceExpression(string name) {
			try {
				Name = new IdentifierNameExpression(name);
			} catch (ArgumentException) {
				throw new ArgumentException($"Invalid namespace identifier: '{name}'");
			}
		}
		public override TextWriter Generate(TextWriter writer) => writer.Code(Name);
	}
}