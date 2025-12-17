using Albatross.CodeGen.CSharp.Expressions;
using Albatross.CodeGen.CSharp.Keywords;
using Albatross.CodeGen.Syntax;
using Albatross.Collections;
using Albatross.Text;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Albatross.CodeGen.CSharp.Declarations {
	public record class ConstructorDeclaration : CodeNode, IDeclaration {
		public AccessModifierKeyword? AccessModifier { get; init; } = Defined.Keywords.Public;
		public required IdentifierNameExpression Name { get; init; }
		public ListOfParameterDeclarations Parameters { get; init; } = new();
		public InvocationExpression? BaseConstructorInvocation { get; init; }
		public IExpression Body { get; init; } = new NoOpExpression();

		public override TextWriter Generate(TextWriter writer) {
			if (AccessModifier != null) {
				writer.Append(AccessModifier.Name).Space();
			}
			writer.Code(Name).Code(Parameters);
			if (BaseConstructorInvocation != null) {
				writer.Append(" : ").Code(BaseConstructorInvocation);
			}
			using var scope = writer.BeginScope();
			scope.Writer.Code(Body);
			return writer;
		}

		public override IEnumerable<ICodeNode> Children {
			get => new List<ICodeNode> {
				Parameters,
				Name,
				Body
			}.AddIfNotNull(BaseConstructorInvocation);
		}
	}
}