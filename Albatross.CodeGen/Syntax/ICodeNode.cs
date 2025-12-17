using System.Collections.Generic;

namespace Albatross.CodeGen.Syntax {
	/// <summary>
	/// present a node in the syntax tree.  A node should have one or more descendants.
	/// </summary>
	public interface ICodeNode : ICodeElement {
		IEnumerable<ICodeNode> GetDescendants();
	}
}