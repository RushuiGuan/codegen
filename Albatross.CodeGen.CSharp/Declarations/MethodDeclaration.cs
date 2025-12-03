using Albatross.CodeGen.CSharp.Expressions;
using Albatross.CodeGen.CSharp.Modifiers;
using Albatross.CodeGen.Syntax;
using Albatross.Text;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using NoOpExpression = Albatross.CodeGen.Syntax.NoOpExpression;

namespace Albatross.CodeGen.CSharp.Declarations {
	public class MethodDeclaration : IDeclaration, ISyntaxNode {
		public bool IsAbstract { get; init; }
		public bool IsVirtual { get; init; }
		public bool IsPartial { get; init; }

		public AccessModifier? AccessModifier { get; init; } = AccessModifier.Public;
		public required ITypeExpression ReturnType { get; init; }
		public ITypeExpression[] GenericArguments { get; init; } = [];
		public required IdentifierNameExpression Name { get; init; }
		public IEnumerable<AttributeExpression> AttributeExpressions { get; init; } = [];
		public IEnumerable<ParameterDeclaration> Parameters { get; init; } = [];
		public IExpression? Body { get; init; }
		public bool IsAsync { get; init; }

		public TextWriter Generate(TextWriter writer) {
			if (AccessModifier != null) {
				writer.Append(AccessModifier.Name).Space();
			}
			if (IsAbstract) { writer.Append("abstract "); }
			if (IsVirtual) { writer.Append("virtual "); }
			if (IsPartial) { writer.Append("partial "); }
			if (IsAsync) { writer.Append("async").Space(); }
			writer.Code(ReturnType).Space().Code(Name);
			if (GenericArguments.Any()) {
				writer.WriteItems(GenericArguments, ",", (w, item) => w.Code(item), "<", ">");
			}
			writer.WriteItems(Parameters, ",", (w, item) => w.Code(item), "(", ")");
			if (Body != null) {
				using var scope = writer.BeginScope();
				scope.Writer.Code(Body);
			} else {
				writer.Semicolon();
			}
			return writer;
		}

		public IEnumerable<ISyntaxNode> GetDescendants() {
			var list = new List<ISyntaxNode>(Parameters) {
				ReturnType, Name, Body ?? new NoOpExpression()
			};
			list.AddRange(GenericArguments);
			return list;
		}
	}
}