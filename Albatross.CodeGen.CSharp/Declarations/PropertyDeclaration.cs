using Albatross.CodeGen.CSharp.Expressions;
using Albatross.CodeGen.CSharp.Modifiers;
using Albatross.CodeGen.Syntax;
using Albatross.Text;
using System.Collections.Generic;
using System.IO;

namespace Albatross.CodeGen.CSharp.Declarations {
	public class PropertyDeclaration : IDeclaration, ISyntaxNode{
		public required ITypeExpression Type { get; init; }
		public required IdentifierNameExpression Name { get; init; }
		public IEnumerable<AttributeExpression> AttributeExpressions { get; init; } = [];
		public AccessModifier? AccessModifier { get; init; } = AccessModifier.Public;
		public AccessModifier? GetterAccessModifier { get; init; }
		public AccessModifier? SetterAccessModifier { get; init; }
		public bool HasGetter { get; init; }
		public bool HasSetter { get; init; }
		public IExpression? GetterBody { get; init; }
		public IExpression? SetterBody { get; init; }
		
		public TextWriter Generate(TextWriter writer) {
			if (AccessModifier != null) {
				writer.Append(this.AccessModifier.Name).Space();
			}
			writer.Code(this.Type).Space().Code(this.Name);
			using var scope = writer.BeginScope();
			if (HasGetter || GetterBody != null) {
				if(GetterAccessModifier != null) {
					scope.Writer.Append(GetterAccessModifier.Name).Space();
				}
				writer.Append("get");
				if (GetterBody != null) {
					scope.Writer.Code(GetterBody);
				} else {
					writer.Semicolon();
				}
			}
			if(HasSetter || SetterBody != null) {
				if(SetterAccessModifier != null) {
					scope.Writer.Append(SetterAccessModifier.Name).Space();
				}
				writer.Append("set");
				if (SetterBody != null) {
					scope.Writer.Code(SetterBody);
				} else {
					writer.Semicolon();
				}
			}
			return writer;
		}

		public IEnumerable<ISyntaxNode> GetDescendants() {
			var list = new List<ISyntaxNode> {
				Type, Name, 
			};
			if(GetterBody != null) {
				list.Add(GetterBody);
			}
			if (SetterBody != null) {
				list.Add(SetterBody);
			}
			return list;
		}
	}
}