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
			Nodes = nodes;
		}
		public IEnumerable<T> Nodes { get; init; } = Array.Empty<T>();
		public string Separator { get; init; } = ", ";
		public string LeftPadding { get; init; } = string.Empty;
		public string RightPadding { get; init; } = string.Empty;
		public string Prefix { get; init; } = string.Empty;
		public string PostFix { get; init; } = string.Empty;

		public override TextWriter Generate(TextWriter writer) {
			writer.Append(this.Prefix)
				.WriteItems(this.Nodes, Separator, (w, item) => w.Code(item), this.LeftPadding, this.RightPadding)
				.Append(this.PostFix);
			return writer;
		}

		public IEnumerator<T> GetEnumerator() => ((IEnumerable<T>)Nodes).GetEnumerator();
		IEnumerator IEnumerable.GetEnumerator() => Nodes.GetEnumerator();
		public bool HasAny => Nodes.Any();
		public override IEnumerable<ISyntaxNode> Children => Nodes.Cast<ISyntaxNode>();
	}
}