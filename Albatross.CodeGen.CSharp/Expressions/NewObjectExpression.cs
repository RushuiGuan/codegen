using Albatross.CodeGen.Syntax;
using Albatross.Text;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Albatross.CodeGen.CSharp.Expressions {
	public record class NewObjectExpression : SyntaxNode, IExpression {
		public required ITypeExpression Type { get; init; }
		public ListOfArguments Arguments { get; init; } = new();
		public ListOfSyntaxNodes<AssignmentExpression> Initializers { get; init; } = new();

		public override TextWriter Generate(TextWriter writer) {
			writer.Code(Defined.Keywords.New).Code(Type).Code(Arguments);
			if (Initializers.Any()) {
				using var scope = writer.BeginScope();
				foreach (var initializer in Initializers) {
					scope.Writer.Code(initializer).AppendLine(",");
				}
			}
			return writer;
		}

		public override IEnumerable<ISyntaxNode> Children => new List<ISyntaxNode> {
			Type,
			Arguments,
			Initializers,
		};
	}
}