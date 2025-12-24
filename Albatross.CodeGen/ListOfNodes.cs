using Albatross.Text;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Albatross.CodeGen {
	/// <summary>
	/// a list of syntax nodes, rendered with separator
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public record class ListOfNodes<T> : CodeNode, IEnumerable<T> where T : ICodeNode {
		/// <summary>
		/// Adds a node to the list
		/// </summary>
		/// <param name="node">The node to add</param>
		public void Add(T? node) {
			if (node != null && node is not NoOpExpression) {
				nodes.Add(node);
			}
		}

		public void Add(IEnumerable<T> nodes) {
			foreach (var node in nodes) {
				this.Add(node);
			}
		}

		/// <summary>
		/// Conditionally adds a node to the list
		/// </summary>
		/// <param name="condition">The condition to evaluate</param>
		/// <param name="func">Function to create the node if condition is true</param>
		public void Add(bool condition, Func<T> func) {
			if (condition) {
				this.Add(func());
			}
		}

		/// <summary>
		/// Conditionally adds multiple nodes to the list
		/// </summary>
		/// <param name="condition">The condition to evaluate</param>
		/// <param name="func">Function to create the nodes if condition is true</param>
		public void Add(bool condition, Func<IEnumerable<T>> func) {
			if (condition) {
				foreach (var item in func()) {
					this.Add(item);
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

		public bool Multiline { get; init; } = false;

		/// <summary>
		/// Generates the code representation of the list with separators and padding
		/// </summary>
		/// <param name="writer">The TextWriter to write the generated code to</param>
		/// <returns>The TextWriter for method chaining</returns>
		public override TextWriter Generate(TextWriter writer) {
			if (Multiline && this.nodes.Count > 0) {
				using var scope = writer.BeginScope(Prefix, PostFix);
				var seperator = $"{Separator}\n";
				scope.Writer.WriteItems(this.nodes,
					seperator, (w, item) => w.Code(item), this.LeftPadding, this.RightPadding);
			} else {
				writer.Append(this.Prefix)
					.WriteItems(this.nodes.Where(x => x is not NoOpExpression), Separator, (w, item) => w.Code(item), this.LeftPadding, this.RightPadding)
					.Append(this.PostFix);
			}
			return writer;
		}

		/// <summary>
		/// Gets an enumerator that iterates through the nodes
		/// </summary>
		/// <returns>An enumerator for the nodes</returns>
		public IEnumerator<T> GetEnumerator() => ((IEnumerable<T>)nodes).GetEnumerator();

		/// <summary>
		/// Gets the child nodes of this list
		/// </summary>
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