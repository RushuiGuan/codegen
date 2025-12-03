using Albatross.CodeGen.CSharp.Expressions;
using Albatross.CodeGen.CSharp.Modifiers;
using Albatross.CodeGen.Syntax;
using Albatross.Text;
using System.Collections.Generic;
using System.IO;

namespace Albatross.CodeGen.CSharp.Declarations {
	public class ClassDeclaration : ISyntaxNode, IDeclaration {
		public ClassDeclaration(string name) {
			Name = new IdentifierNameExpression(name);
		}

		public AccessModifier? AccessModifier { get; init; } = AccessModifier.Public;
		public IdentifierNameExpression Name { get; init; }
		public ITypeExpression? BaseType { get; init; }
		public bool IsStatic { get; init; }
		public bool IsSealed { get; init; }
		public bool IsAbstract { get; init; }
		public bool IsPartial { get; init; }
		public bool IsRecord { get; init; }

		public IEnumerable<AttributeExpression> AttributeExpressions { get; init; } = [];
		public IEnumerable<ConstructorDeclaration> Constructors { get; init; } = [];
		public IEnumerable<PropertyDeclaration> Properties { get; init; } = [];
		public IEnumerable<FieldDeclaration> Fields { get; init; } = [];
		public IEnumerable<MethodDeclaration> Methods { get; init; } = [];

		public TextWriter Generate(TextWriter writer) {
			foreach (var attribute in AttributeExpressions) {
				writer.Code(attribute).WriteLine();
			}
			if (AccessModifier != null) {
				writer.Append(AccessModifier.Name).Space();
			}
			if(IsStatic) { writer.Append("static "); }
			if(IsSealed) { writer.Append("sealed "); }
			if(IsRecord) { writer.Append("record "); }
			if (IsPartial) { writer.Append("partial ");}
			if(IsAbstract) { writer.Append("abstract "); }
			writer.Append("class ").Code(Name);
			if (BaseType != null) {
				writer.Append(" : ").Code(BaseType);
			}
			using (var scope = writer.BeginScope()) {
				foreach(var constructor in Constructors) {
					scope.Writer.Code(constructor).WriteLine();
				}
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
			list.AddRange(Constructors);
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