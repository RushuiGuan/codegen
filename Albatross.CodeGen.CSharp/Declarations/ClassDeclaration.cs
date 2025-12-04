using Albatross.CodeGen.CSharp.Expressions;
using Albatross.CodeGen.CSharp.Modifiers;
using Albatross.CodeGen.Syntax;
using Albatross.Text;
using System;
using System.Collections.Generic;
using System.IO;

namespace Albatross.CodeGen.CSharp.Declarations {
	public class ClassDeclaration : ISyntaxNode, IDeclaration {
		public ClassDeclaration(string name) {
			Name = new IdentifierNameExpression(name);
		}

		public AccessModifier? AccessModifier { get; set; } = AccessModifier.Public;
		public IdentifierNameExpression Name { get; set; }
		public List<ITypeExpression> BaseTypes { get; set; } = new();
		public bool IsStatic { get; set; }
		public bool IsSealed { get; set; }
		public bool IsAbstract { get; set; }
		public bool IsPartial { get; set; }
		public bool IsRecord { get; set; }

		public List<AttributeExpression> AttributeExpressions { get; init; } = new();
		public List<ConstructorDeclaration> Constructors { get; init; } = new();
		public List<PropertyDeclaration> Properties { get; init; } = new();
		public List<FieldDeclaration> Fields { get; init; } = new();
		public List<MethodDeclaration> Methods { get; init; } = new();

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
			writer.WriteItems(BaseTypes, ", ", (w, args) => w.Code(args), " : ", null);
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
			list.AddRange(BaseTypes);
			return list;
		}
	}
}