using Albatross.CodeGen.CSharp.Expressions;
using Albatross.CodeGen.CSharp.Modifiers;
using Albatross.CodeGen.Syntax;
using Albatross.Text;
using System.Collections.Generic;
using System.IO;

namespace Albatross.CodeGen.CSharp.Declarations {
	public class InterfaceDeclaration : ISyntaxNode, IDeclaration {
		public InterfaceDeclaration(string name) {
			Name = new IdentifierNameExpression(name);
		}

		public AccessModifier? AccessModifier { get; init; } = AccessModifier.Public;
		public IdentifierNameExpression Name { get; init; }
		public ITypeExpression? BaseType { get; init; }
		public bool IsPartial { get; init; }

		public List<AttributeExpression> AttributeExpressions { get; init; } = new List<AttributeExpression>();
		public List<PropertyDeclaration> Properties { get; init; } = new List<PropertyDeclaration>();
		public List<FieldDeclaration> Fields { get; init; } = new List<FieldDeclaration>();
		public List<MethodDeclaration> Methods { get; init; } = new List<MethodDeclaration>();

		public TextWriter Generate(TextWriter writer) {
			foreach (var attribute in AttributeExpressions) {
				writer.Code(attribute).WriteLine();
			}
			if (AccessModifier != null) {
				writer.Append(AccessModifier.Name).Space();
			}
			if (IsPartial) { writer.Append("partial ");}
			writer.Append("interface ").Code(Name);
			if (BaseType != null) {
				writer.Append(" : ").Code(BaseType);
			}
			using (var scope = writer.BeginScope()) {
				foreach (var field in Fields) {
					scope.Writer.Code(field).WriteLine();
				}
				foreach (var property in Properties) {
					scope.Writer.Code(property).WriteLine();
				}
				foreach (var method in Methods) {
					scope.Writer.Code(method).WriteLine();
				}
			}
			return writer;
		}

		public IEnumerable<ISyntaxNode> GetDescendants() {
			var list = new List<ISyntaxNode>();
			list.AddRange(Fields);
			list.AddRange(Properties);
			list.AddRange(Methods);
			if (BaseType != null) {
				list.Add(BaseType);
			}
			return list;
		}
	}
}