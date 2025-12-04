using Albatross.CodeGen.CSharp.Expressions;
using Albatross.CodeGen.CSharp.Modifiers;
using Albatross.CodeGen.Syntax;
using Albatross.Text;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Albatross.CodeGen.CSharp.Declarations {
	public class MethodDeclaration : IDeclaration, ISyntaxNode {
		public bool IsAbstract { get; set; }
		public bool IsVirtual { get; set; }
		public bool IsPartial { get; set; }

		public required IdentifierNameExpression Name { get; init; }
		public required ITypeExpression ReturnType { get; set; }
		public AccessModifier? AccessModifier { get; set; } = AccessModifier.Public;

		public List<ITypeExpression> GenericArguments { get; init; } = new();
		public List<AttributeExpression> AttributeExpressions { get; init; } = new();
		public List<ParameterDeclaration> Parameters { get; init; } = new();
		public IExpression? Body { get; set; }
		public bool IsAsync { get; set; }

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