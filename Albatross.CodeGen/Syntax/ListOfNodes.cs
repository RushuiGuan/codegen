using Albatross.Text;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Albatross.CodeGen.Syntax {
	/// <summary>
	/// a list of syntax nodes, rendered with separator
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public record class ListOfNodes<T> : CodeNode, IEnumerable<T> where T : ICodeNode {
		public ListOfNodes(params IEnumerable<T> nodes) {
			this.nodes.AddRange(nodes);
		}

		public void Add(T node) => nodes.Add(node);

		public void Add(bool condition, Func<T> func) {
			if (condition) {
				nodes.Add(func());
			}
		}

		public void Add(bool condition, Func<IEnumerable<T>> func) {
			if (condition) {
				foreach (var item in func()) {
					nodes.Add(item);
				}
			}
		}

		private List<T> nodes = new();
		/// <summary>
		/// The separator used to separate each node
		/// </summary>
		public string Separator { get; init; } = ", ";
		/// <summary>
		/// String to add before the first node.  Will not be added if there are no nodes.
		/// </summary>
		public string LeftPadding { get; init; } = string.Empty;
		/// <summary>
		/// String to add after the last node.  Will not be added if there are no nodes.
		/// </summary>
		public string RightPadding { get; init; } = string.Empty;
		/// <summary>
		/// String to add before the entire list
		/// </summary>
		public string Prefix { get; init; } = string.Empty;
		/// <summary>
		/// String to add after the entire list
		/// </summary>
		public string PostFix { get; init; } = string.Empty;
		
		/// <summary>
		/// String to add after each node
		/// </summary>
		public string NodePostfix { get; init; } = string.Empty;

		public override TextWriter Generate(TextWriter writer) {
			writer.Append(this.Prefix)
				.WriteItems(this.nodes.Where(x => x is not NoOpExpression), Separator, (w, item) => w.Code(item).Append(NodePostfix), this.LeftPadding, this.RightPadding)
				.Append(this.PostFix);
			return writer;
		}

		public IEnumerator<T> GetEnumerator() => ((IEnumerable<T>)nodes).GetEnumerator();
		public override IEnumerable<ICodeNode> Children => nodes.Cast<ICodeNode>();
		IEnumerator IEnumerable.GetEnumerator() {
			return GetEnumerator();
		}

		public virtual bool Equals(ListOfNodes<T>? other) {
			if (ReferenceEquals(this, other)) {
				return true;
			} else if (other == null) {
				return false;
			} else {
				return this.nodes.SequenceEqual(other.nodes);
			}
		}

		public override int GetHashCode() {
			var hash = new HashCode();
			hash.Add(Separator);
			hash.Add(LeftPadding);
			hash.Add(RightPadding);
			hash.Add(Prefix);
			hash.Add(PostFix);
			foreach (var item in nodes) {
				hash.Add(item);
			}
			return hash.ToHashCode();
		}
	}
}