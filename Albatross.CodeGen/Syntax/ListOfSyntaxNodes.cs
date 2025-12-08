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
	public record class ListOfSyntaxNodes<T> : SyntaxNode, IEnumerable<T> where T : ISyntaxNode {
		public ListOfSyntaxNodes(params IEnumerable<T> nodes) {
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
				foreach(var item in func()) {
					nodes.Add(item);
				}
			}
		}

		private List<T> nodes = new();
		public string Separator { get; init; } = ", ";
		public string LeftPadding { get; init; } = string.Empty;
		public string RightPadding { get; init; } = string.Empty;
		public string Prefix { get; init; } = string.Empty;
		public string PostFix { get; init; } = string.Empty;

		public override TextWriter Generate(TextWriter writer) {
			writer.Append(this.Prefix)
				.WriteItems(this.nodes, Separator, (w, item) => w.Code(item), this.LeftPadding, this.RightPadding)
				.Append(this.PostFix);
			return writer;
		}

		public IEnumerator<T> GetEnumerator() => ((IEnumerable<T>)nodes).GetEnumerator();
		public override IEnumerable<ISyntaxNode> Children => nodes.Cast<ISyntaxNode>();
		IEnumerator IEnumerable.GetEnumerator() {
			return GetEnumerator();
		}
	}
}