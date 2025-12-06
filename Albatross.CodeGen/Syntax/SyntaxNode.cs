using System.Collections.Generic;
using System.IO;

namespace Albatross.CodeGen.Syntax {
	public abstract record class SyntaxNode : ISyntaxNode {
		public abstract TextWriter Generate(TextWriter writer);
		public virtual IEnumerable<ISyntaxNode> Children => System.Array.Empty<ISyntaxNode>();

		public IEnumerable<ISyntaxNode> GetDescendants() {
			foreach (var child in Children) {
				yield return child;
				foreach (var descendant in child.GetDescendants()) {
					yield return descendant;
				}
			}
		}
	}
}