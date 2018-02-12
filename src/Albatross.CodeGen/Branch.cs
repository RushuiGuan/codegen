using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Albatross.CodeGen {
	public class Branch : INode, IEnumerable<INode> {
		public Branch(params INode[] nodes) {
			Nodes = nodes;
		}

		public IEnumerable<INode> Nodes { get; private set; }
		public bool IsLeaf => false;

		public string Name => string.Empty;

		public IEnumerator<INode> GetEnumerator() {
			return Nodes.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator() {
			return Nodes.GetEnumerator();
		}
	}
}
