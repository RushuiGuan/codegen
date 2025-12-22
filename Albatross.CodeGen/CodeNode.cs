using System.Collections.Generic;
using System.IO;

namespace Albatross.CodeGen {
	/// <summary>
	/// Abstract base class for all code generation nodes that can produce code output
	/// </summary>
	public abstract record class CodeNode : ICodeNode {
		/// <summary>
		/// Generates the code representation of this node
		/// </summary>
		/// <param name="writer">The TextWriter to write the generated code to</param>
		/// <returns>The TextWriter for method chaining</returns>
		public abstract TextWriter Generate(TextWriter writer);
		
		/// <summary>
		/// Gets the immediate child nodes of this code node
		/// </summary>
		public virtual IEnumerable<ICodeNode> Children => System.Array.Empty<ICodeNode>();

		/// <summary>
		/// Recursively gets all descendant nodes in the syntax tree
		/// </summary>
		/// <returns>An enumerable of all descendant ICodeNode instances</returns>
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