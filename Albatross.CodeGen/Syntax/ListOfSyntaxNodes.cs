using Albatross.Text;
using Microsoft.CodeAnalysis.CSharp.Syntax;
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
				.WriteItems(this.nodes.Where(x=>x is not NoOpExpression), Separator, (w, item) => w.Code(item), this.LeftPadding, this.RightPadding)
				.Append(this.PostFix);
			return writer;
		}

		public IEnumerator<T> GetEnumerator() => ((IEnumerable<T>)nodes).GetEnumerator();
		public override IEnumerable<ISyntaxNode> Children => nodes.Cast<ISyntaxNode>();
		IEnumerator IEnumerable.GetEnumerator() {
			return GetEnumerator();
		}

		public virtual bool Equals(ListOfSyntaxNodes<T>? other) {
			if(ReferenceEquals(this, other)){
				return true;
			}else if (other == null) {
				return false;
			}else {
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