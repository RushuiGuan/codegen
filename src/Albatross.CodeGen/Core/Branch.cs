using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Albatross.CodeGen.Core {
	/// <summary>
	/// This class represent a branch object for the composite code generation.  The object itself is an <see cref="Albatross.CodeGen.Core.INode"/> and it can contain other nodes.
	/// </summary>
	public class Branch : INode, IEnumerable<INode> {
		public Branch(params INode[] nodes) {
			Nodes = nodes;
		}

		/// <summary>
		/// A collection of children <see cref="Albatross.CodeGen.INode">nodes</see>.
		/// </summary>
		public IEnumerable<INode> Nodes { get; private set; }

		#region INode
		public string Name => string.Empty;
		public object Source { get; set; }
		public object Option { get; set; }
		public ICodeGenerator CodeGenerator { get; set; }
		#endregion

		#region IEnumerable
		public IEnumerator<INode> GetEnumerator() { return Nodes.GetEnumerator(); }
		IEnumerator IEnumerable.GetEnumerator() { return Nodes.GetEnumerator(); }
		#endregion
	}
}
