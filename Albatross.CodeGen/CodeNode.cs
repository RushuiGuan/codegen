using System.Collections.Generic;
using System.IO;

namespace Albatross.CodeGen {
	public abstract record class CodeNode : ICodeNode {
		public abstract TextWriter Generate(TextWriter writer);
		public virtual IEnumerable<ICodeNode> Children => System.Array.Empty<ICodeNode>();

		public IEnumerable<ICodeNode> GetDescendants() {
			foreach (var child in Children) {
				yield return child;
				foreach (var descendant in child.GetDescendants()) {
					yield return descendant;
				}
			}
		}
	}
}