using Albatross.CodeGen.Syntax;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Albatross.CodeGen.CSharp.Expressions {
	public record ListOfGenericArguments : IExpression {
		readonly ListOfSyntaxNodes<ITypeExpression> list;
		public ListOfGenericArguments(params IEnumerable<ITypeExpression> nodes) {
			if (!nodes.Any()) {
				throw new ArgumentException("ListOfGenericArguments requires at least one type argument");
			}
			list = new ListOfSyntaxNodes<ITypeExpression> {
				Nodes = nodes,
				LeftPadding = "<",
				RightPadding = ">"
			};
		}
		public TextWriter Generate(TextWriter writer) => writer.Code(list);
		public IEnumerable<ISyntaxNode> GetDescendants() => list;
	}
}