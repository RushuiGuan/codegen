using Albatross.CodeGen.Syntax;
using Albatross.CodeGen.Python.Expressions;
using Albatross.CodeGen.Python.Modifiers;
using Albatross.Text;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Albatross.CodeGen.Python.Declarations {
	public record class MethodDeclaration : SyntaxNode, IDeclaration, ICodeElement {
		public MethodDeclaration(string name) {
			Identifier = new IdentifierNameExpression(name);
		}

		public IdentifierNameExpression Identifier { get; }
		public IEnumerable<DecoratorExpression> Decorators { get; init; } = [];
		public ITypeExpression ReturnType { get; init; } = Defined.Types.None();
		public ListOfSyntaxNodes<ParameterDeclaration> Parameters { get; init; } = new();
		public IEnumerable<IModifier> Modifiers { get; init; } = [];
		public IExpression Body { get; init; } = new EmptyExpression();

		public override TextWriter Generate(TextWriter writer) {
			foreach (var decorator in Decorators) {
				writer.Code(decorator).WriteLine();
			}
			foreach (var modifier in Modifiers) {
				writer.Append(modifier).Space();
			}
			writer.Append("def ").Code(Identifier)
				.OpenParenthesis().Code(Parameters).CloseParenthesis();
			if (!object.Equals(this.ReturnType, Defined.Types.None())) {
				writer.Append(" -> ").Code(ReturnType);
			}
			using (var scope = writer.BeginScope()) {
				scope.Writer.Code(Body);
			}
			writer.WriteLine();
			return writer;
		}

		public override IEnumerable<ISyntaxNode> Children =>
			new ISyntaxNode[] {
				this.Identifier, this.ReturnType, this.Body,
			}.Concat(Parameters).Concat(this.Decorators);
	}
}