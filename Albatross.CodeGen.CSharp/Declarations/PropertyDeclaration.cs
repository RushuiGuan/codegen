using Albatross.CodeGen.CSharp.Expressions;
using Albatross.CodeGen.CSharp.Keywords;
using Albatross.Collections;
using Albatross.Text;
using System.Collections.Generic;
using System.IO;

namespace Albatross.CodeGen.CSharp.Declarations {
	public record class PropertyDeclaration : CodeNode, IDeclaration {
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
				scope.Writer.CodeIfNotNull(GetterAccessModifier).Code(Defined.Keywords.Get);
				if (GetterBody is NoOpExpression) {
					scope.Writer.Code(new EndOfStatement());
				} else {
					using var subScope = scope.Writer.BeginScope();
					subScope.Writer.Code(GetterBody);
					if (GetterBody is not ICodeBlock) {
						subScope.Writer.Code(new EndOfStatement());
					}
				}
				scope.Writer.WriteLine();
			}
			if (SetterBody != null) {
				scope.Writer.CodeIfNotNull(SetterAccessModifier).Code(Defined.Keywords.Set);
				if (SetterBody is NoOpExpression) {
					scope.Writer.Code(new EndOfStatement());
				} else {
					using var subScope = scope.Writer.BeginScope();
					subScope.Writer.Code(SetterBody);
					if (SetterBody is not ICodeBlock) {
						subScope.Writer.Code(new EndOfStatement());
					}
				}
				scope.Writer.WriteLine();
			}
			return writer;
		}

		public override IEnumerable<ICodeNode> Children {
			get {
				var list = new List<ICodeNode> {
						Type, Name
					}.AddIfNotNull(GetterBody)
					.AddIfNotNull(SetterBody);
				return list;
			}
		}
	}
}
