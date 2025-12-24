using Albatross.CodeGen.Python.Expressions;
using Albatross.Text;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Albatross.CodeGen.Python.Declarations {
	public record class MethodDeclaration : CodeNode, IDeclaration {
		public MethodDeclaration(string name) {
			Identifier = new IdentifierNameExpression(name);
		}

		public IdentifierNameExpression Identifier { get; }
		public IEnumerable<DecoratorExpression> Decorators { get; init; } = [];
		public ITypeExpression ReturnType { get; init; } = Defined.Types.None;
		public ListOfNodes<ParameterDeclaration> Parameters { get; init; } = new();
		public IEnumerable<IKeyword> Modifiers { get; init; } = [];
		public PythonCodeBlock Body { get; } = new PythonCodeBlock();
		public IExpression? DocString { get; init; }

		public override TextWriter Generate(TextWriter writer) {
			foreach (var decorator in Decorators) {
				writer.Code(decorator).WriteLine();
			}
			foreach (var modifier in Modifiers) {
				writer.Append(modifier.Name).Space();
			}
			writer.Append("def ").Code(Identifier)
				.OpenParenthesis().Code(Parameters).CloseParenthesis();
			writer.Append(" -> ").Code(ReturnType);

			var body = this.Body;
			if (DocString is not null) {
				body = new PythonCodeBlock {
					DocString,
					Body.Cast<IExpression>(),
				};
			}
			writer.Code(body);
			return writer;
		}

		public override IEnumerable<ICodeNode> Children
			=> new ICodeNode[] {
				this.Identifier,
				this.ReturnType, this.Body,
			}.Concat(Parameters).Concat(this.Decorators);
	}
}