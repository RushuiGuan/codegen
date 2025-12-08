using Albatross.CodeGen.CSharp.Expressions;
using Albatross.CodeGen.CSharp.Keywords;
using Albatross.CodeGen.Syntax;
using Albatross.Collections;
using Albatross.Text;
using System.Collections.Generic;
using System.IO;

namespace Albatross.CodeGen.CSharp.Declarations {
	public record class PropertyDeclaration : SyntaxNode, IDeclaration {
		public required ITypeExpression Type { get; init; }
		public required IdentifierNameExpression Name { get; init; }
		public IEnumerable<AttributeExpression> AttributeExpressions { get; init; } = [];
		public AccessModifierKeyword? AccessModifier { get; init; } = Defined.Keywords.Public;
		public AccessModifierKeyword? GetterAccessModifier { get; init; }
		public AccessModifierKeyword? SetterAccessModifier { get; init; }
		public IExpression? GetterBody { get; init; } = new NoOpExpression();
		public IExpression? SetterBody { get; init; } = new NoOpExpression();

		public override TextWriter Generate(TextWriter writer) {
			writer.CodeIfNotNull(this.AccessModifier);
			foreach (var attribute in AttributeExpressions) {
				writer.Code(attribute).WriteLine();
			}
			writer.Code(this.Type).Space().Code(this.Name);
			using var scope = writer.BeginScope();
			if (GetterBody != null) {
				scope.Writer.CodeIfNotNull(GetterAccessModifier)
					.Code(Defined.Keywords.Get)
					.Code(GetterBody is NoOpExpression ? new EndOfStatement() : GetterBody)
					.WriteLine();
			}
			if (SetterBody != null) {
				scope.Writer.CodeIfNotNull(SetterAccessModifier)
					.Code(Defined.Keywords.Set)
					.Code(SetterBody is NoOpExpression ? new EndOfStatement() : SetterBody)
					.AppendLine();
			}
			return writer;
		}

		public override IEnumerable<ISyntaxNode> Children {
			get {
				var list = new List<ISyntaxNode> {
						Type, Name
					}.AddIfNotNull(GetterBody)
					.AddIfNotNull(SetterBody);
				return list;
			}
		}
	}
}