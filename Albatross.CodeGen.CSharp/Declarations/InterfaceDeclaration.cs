using Albatross.CodeGen.CSharp.Expressions;
using Albatross.CodeGen.CSharp.Keywords;
using Albatross.Collections;
using Albatross.Text;
using System.Collections.Generic;
using System.IO;

namespace Albatross.CodeGen.CSharp.Declarations {
	public record class InterfaceDeclaration : CodeNode, IDeclaration {
		public InterfaceDeclaration(string name) {
			Name = new IdentifierNameExpression(name);
		}

		public AccessModifierKeyword? AccessModifier { get; init; } = Defined.Keywords.Public;
		public IdentifierNameExpression Name { get; init; }
		public ITypeExpression? BaseType { get; init; }
		public bool IsPartial { get; init; }
		public ListOfGenericArguments ListOfGenericArguments { get; init; } = new();

		public IEnumerable<AttributeExpression> Attributes { get; init; } = [];
		public IEnumerable<PropertyDeclaration> Properties { get; init; } = [];
		public IEnumerable<FieldDeclaration> Fields { get; init; } = [];
		public IEnumerable<MethodDeclaration> Methods { get; init; } = [];

		public override TextWriter Generate(TextWriter writer) {
			foreach (var attribute in Attributes) {
				writer.Code(attribute).WriteLine();
			}
			if (AccessModifier != null) {
				writer.Append(AccessModifier.Name).Space();
			}
			if (IsPartial) { writer.Append("partial "); }
			writer.Append("interface ").Code(Name).Code(ListOfGenericArguments);
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

		public override IEnumerable<ICodeNode> Children {
			get {
				var list = new List<ICodeNode>(Attributes);
				list.AddRange(Fields);
				list.AddRange(Properties);
				list.AddRange(Methods);
				list.AddIfNotNull(BaseType);
				list.Add(Name);
				list.Add(ListOfGenericArguments);
				return list;
			}
		}
	}
}